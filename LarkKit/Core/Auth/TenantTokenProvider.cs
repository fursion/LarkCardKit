using LarkKit.Core.Config;
using LarkKit.Core.Exception;
using LarkKit.Models;
using LarkKit.Util;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;
using System.Text.Json.Serialization;

namespace LarkKit.Core.Auth;

/// <summary>
/// 租户级 Token 提供者
/// </summary>
/// <remarks>
/// 初始化 TenantTokenProvider 的新实例
/// </remarks>
/// <param name="options">配置选项</param>
/// <param name="httpClient">HTTP 客户端</param>
/// <param name="logger">日志记录器</param>
public class TenantTokenProvider(
    LarkOptions options,
    HttpClient httpClient,
    ILogger<TenantTokenProvider>? logger = null) : ITokenProvider
{
    private readonly LarkOptions _options = options;
    private readonly HttpClient _httpClient = httpClient;
    private readonly ILogger<TenantTokenProvider>? _logger = logger;
    private TenantAccessToken? _cachedToken;
    private readonly Lock _lock = new();


    /// <summary>
    /// 获取访问令牌（带缓存）
    /// </summary>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>访问令牌</returns>
    public async Task<TenantAccessToken> GetTokenAsync(CancellationToken cancellationToken = default)
    {
        lock (_lock)
        {
            if (_cachedToken != null && !_cachedToken.IsExpiringSoon(_options.TokenRefreshAheadMinutes))
            {
                _logger?.LogDebug("使用缓存的 Token");
                return _cachedToken;
            }
        }

        return await RefreshTokenAsync(cancellationToken);
    }

    /// <summary>
    /// 刷新访问令牌
    /// </summary>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>访问令牌</returns>
    public async Task<TenantAccessToken> RefreshTokenAsync(CancellationToken cancellationToken = default)
    {
        lock (_lock)
        {
            if (_cachedToken != null && !_cachedToken.IsExpired)
            {
                _logger?.LogDebug("Token 未过期，无需刷新");
                return _cachedToken;
            }
        }

        _logger?.LogInformation("开始刷新租户 Token");

        var requestUrl = "/open-apis/auth/v3/tenant_access_token/internal";
        var requestBody = new
        {
            app_id = _options.AppId,
            app_secret = _options.AppSecret
        };

        try
        {
            var response = await _httpClient.PostAsJsonAsync(requestUrl, requestBody, cancellationToken);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadFromJsonAsync<LarkDirectResponse<TokenResponse>>(
                cancellationToken: cancellationToken);

            if (responseContent == null)
            {
                throw new LarkException(
                    LarkErrorCode.InternalServerError,
                    "获取 Token 响应为空",
                    "请检查飞书服务状态");
            }

            if (responseContent.Code != 0)
            {
                throw new LarkException(
                    (LarkErrorCode)responseContent.Code,
                    responseContent.Message ?? "获取 Token 失败",
                    "请检查 App ID 和 App Secret 是否正确");
            }

            var token = new TenantAccessToken
            {
                Token = responseContent.TenantAccessToken ?? throw new LarkException(LarkErrorCode.InternalServerError, "Token 为空"),
                ExpiresAt = DateTime.UtcNow.AddSeconds(responseContent.Expire)
            };

            lock (_lock)
            {
                _cachedToken = token;
            }

            _logger?.LogInformation("Token 刷新成功，过期时间：{ExpiresAt}", token.ExpiresAt);
            return token;
        }
        catch (HttpRequestException ex)
        {
            _logger?.LogError(ex, "获取 Token 时发生 HTTP 错误");
            throw new LarkException(
                LarkErrorCode.InternalServerError,
                "获取 Token 时发生网络错误",
                "请检查网络连接",
                (int?)ex.StatusCode);
        }
    }

    /// <summary>
    /// Token 响应模型
    /// </summary>
    private class TokenResponse
    {
        [JsonPropertyName("tenant_access_token")]
        public string TenantAccessToken { get; set; } = string.Empty;

        [JsonPropertyName("expire")]
        public int Expire { get; set; }
    }
}
