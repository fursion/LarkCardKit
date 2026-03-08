using LarkKit.Core.Auth;
using LarkKit.Core.Config;
using LarkKit.Core.Exception;
using LarkKit.Models;
using LarkKit.Util;
using Microsoft.Extensions.Logging;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using SystemException = System.Exception;

namespace LarkKit.Core.Http;

/// <summary>
/// 飞书 HTTP 客户端实现
/// </summary>
public class LarkHttpClient : ILarkHttpClient
{
    private readonly HttpClient _httpClient;
    private readonly ITokenProvider _tokenProvider;
    private readonly LarkOptions _options;
    private readonly ILogger<LarkHttpClient>? _logger;

    /// <summary>
    /// 初始化 LarkHttpClient 的新实例
    /// </summary>
    /// <param name="httpClient">HTTP 客户端</param>
    /// <param name="tokenProvider">Token 提供者</param>
    /// <param name="options">配置选项</param>
    /// <param name="logger">日志记录器</param>
    public LarkHttpClient(
        HttpClient httpClient,
        ITokenProvider tokenProvider,
        LarkOptions options,
        ILogger<LarkHttpClient>? logger = null)
    {
        _httpClient = httpClient;
        _tokenProvider = tokenProvider;
        _options = options;
        _logger = logger;

        _httpClient.BaseAddress = new Uri(options.BaseUrl);
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        _httpClient.Timeout = TimeSpan.FromSeconds(options.Timeout);
    }

    /// <summary>
    /// 发送 GET 请求
    /// </summary>
    public async Task<T?> GetAsync<T>(string url, CancellationToken cancellationToken = default)
    {
        return await SendRequestAsync<T>(HttpMethod.Get, url, null, cancellationToken);
    }

    /// <summary>
    /// 发送 POST 请求
    /// </summary>
    public async Task<TResponse?> PostAsync<TRequest, TResponse>(string url, TRequest data, CancellationToken cancellationToken = default)
    {
        return await SendRequestAsync<TResponse>(HttpMethod.Post, url, data, cancellationToken);
    }

    /// <summary>
    /// 发送 POST 请求（无响应数据）
    /// </summary>
    public async Task PostAsync<T>(string url, T data, CancellationToken cancellationToken = default)
    {
        await SendRequestAsync<object>(HttpMethod.Post, url, data, cancellationToken);
    }

    /// <summary>
    /// 发送 PUT 请求
    /// </summary>
    public async Task<TResponse?> PutAsync<TRequest, TResponse>(string url, TRequest data, CancellationToken cancellationToken = default)
    {
        return await SendRequestAsync<TResponse>(HttpMethod.Put, url, data, cancellationToken);
    }

    /// <summary>
    /// 发送 DELETE 请求
    /// </summary>
    public async Task<T?> DeleteAsync<T>(string url, CancellationToken cancellationToken = default)
    {
        return await SendRequestAsync<T>(HttpMethod.Delete, url, null, cancellationToken);
    }

    /// <summary>
    /// 发送 HTTP 请求（通用方法）
    /// </summary>
    private async Task<T?> SendRequestAsync<T>(HttpMethod method, string url, object? data, CancellationToken cancellationToken)
    {
        int retryCount = 0;
        SystemException? lastException = null;

        while (retryCount <= _options.MaxRetries)
        {
            try
            {
                // 获取 Token
                var token = await _tokenProvider.GetTokenAsync(cancellationToken);

                // 创建请求
                var request = new HttpRequestMessage(method, url);
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token.Token);

                // 添加请求体（如果有）
                if (data != null)
                {
                    var json = LarkJsonSerializer.Serialize(data);
                    request.Content = new StringContent(json, Encoding.UTF8, "application/json");
                }

                // 记录请求日志
                _logger?.LogDebug("发送请求：{Method} {Url}", method, url);

                // 发送请求
                var response = await _httpClient.SendAsync(request, cancellationToken);
                var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);

                // 记录响应日志
                _logger?.LogDebug("响应内容：{Content}", MaskSensitiveData(responseContent));

                // 处理响应
                if (!response.IsSuccessStatusCode)
                {
                    throw new HttpRequestException($"HTTP {(int)response.StatusCode} {response.ReasonPhrase}")
                    {
                        Data = { ["StatusCode"] = (int)response.StatusCode }
                    };
                }

                // 反序列化响应
                var larkResponse = LarkJsonSerializer.Deserialize<LarkResponse<T>>(responseContent) ?? throw new LarkException(
                        LarkErrorCode.InternalServerError,
                        "响应解析失败",
                        "响应内容为空或格式不正确");
                if (larkResponse.Code != 0)
                {
                    throw new LarkException(
                        (LarkErrorCode)larkResponse.Code,
                        larkResponse.Message ?? "API 调用失败",
                        larkResponse.Error,
                        (int?)response.StatusCode);
                }

                return larkResponse.Data;
            }
            catch (HttpRequestException ex) when (retryCount < _options.MaxRetries)
            {
                lastException = ex;
                retryCount++;
                var delay = _options.RetryDelayMs * (int)Math.Pow(2, retryCount - 1);
                _logger?.LogWarning(ex, "请求失败，{Delay}ms 后重试（第 {Retry} 次）", delay, retryCount);
                await Task.Delay(delay, cancellationToken);
            }
        }

        var httpStatusCode = lastException is HttpRequestException hre ? (int?)hre.StatusCode : null;
        throw new LarkException(
            LarkErrorCode.InternalServerError,
            $"请求失败，已重试 {_options.MaxRetries} 次",
            "请检查网络连接或服务状态",
            httpStatusCode);
    }

    /// <summary>
    /// 脱敏敏感数据（用于日志）
    /// </summary>
    private string MaskSensitiveData(string content)
    {
        // 简单脱敏：替换 token
        return content.Replace(_options.AppSecret, "***");
    }
}
