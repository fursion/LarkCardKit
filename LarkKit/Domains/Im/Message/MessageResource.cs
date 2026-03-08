using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using LarkKit.Core.Auth;
using LarkKit.Core.Config;
using LarkKit.Models;
using LarkKit.Models.Requests;
using LarkKit.Models.Responses;
using LarkKit.Util;
using Microsoft.Extensions.Logging;

namespace LarkKit.Domains.Im;

/// <summary>
/// 消息资源实现
/// </summary>
public class MessageResource : IMessageResource
{
    private readonly HttpClient _httpClient;
    private readonly ITokenProvider _tokenProvider;
    private readonly LarkOptions _options;
    private readonly ILogger<MessageResource>? _logger;

    /// <summary>
    /// 初始化 MessageResource 的新实例
    /// </summary>
    public MessageResource(HttpClient httpClient, ITokenProvider tokenProvider, LarkOptions options, ILogger<MessageResource>? logger = null)
    {
        _httpClient = httpClient;
        _tokenProvider = tokenProvider;
        _options = options;
        _logger = logger;
    }

    /// <summary>
    /// 创建消息（发送消息）
    /// </summary>
    public async Task<MessageCreateResponse> CreateAsync(MessageCreateRequest request, CancellationToken cancellationToken = default)
    {
        var token = await _tokenProvider.GetTokenAsync(cancellationToken);

        var url = $"/open-apis/im/v1/messages?receive_id_type={request.Params.ReceiveIdType}";

        // 构建请求体
        var body = new
        {
            receive_id = request.Data.ReceiveId,
            msg_type = request.Data.MsgType,
            content = request.Data.Content  // content 已经是字符串，直接传递
        };

        var jsonBody = JsonSerializer.Serialize(body);

        using var httpRequest = new HttpRequestMessage(HttpMethod.Post, url);
        httpRequest.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token.Token);
        httpRequest.Content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

        var response = await _httpClient.SendAsync(httpRequest, cancellationToken);
        var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            _logger?.LogError(
                "飞书 API 请求失败:\n" +
                "状态码: {StatusCode}\n" +
                "请求 URL: {Url}\n" +
                "请求体: {RequestBody}\n" +
                "响应体: {ResponseBody}",
                response.StatusCode,
                url,
                jsonBody,
                responseContent);
            
            throw new Core.Exception.LarkException(
                Models.LarkErrorCode.MessageSendFailed,
                $"HTTP 错误：{response.StatusCode}\n响应体: {responseContent}\n请求体: {jsonBody}",
                responseContent);
        }

        var larkResponse = LarkJsonSerializer.Deserialize<LarkResponse<MessageCreateResponse>>(responseContent);

        if (larkResponse?.Code != 0)
        {
            _logger?.LogError(
                "飞书 API 返回错误:\n" +
                "错误码: {Code}\n" +
                "错误消息: {Message}\n" +
                "请求体: {RequestBody}\n" +
                "响应体: {ResponseBody}",
                larkResponse?.Code,
                larkResponse?.Message,
                jsonBody,
                responseContent);
            
            throw new Core.Exception.LarkException(
                Models.LarkErrorCode.MessageSendFailed,
                $"{larkResponse?.Message ?? "发送消息失败"}\n请求体: {jsonBody}\n响应体: {responseContent}",
                larkResponse?.Error);
        }

        return larkResponse.Data ?? new MessageCreateResponse();
    }

    /// <summary>
    /// 获取消息
    /// </summary>
    public async Task<MessageGetResponse> GetAsync(string messageId, CancellationToken cancellationToken = default)
    {
        var token = await _tokenProvider.GetTokenAsync(cancellationToken);

        var url = $"/open-apis/im/v1/messages/{messageId}";

        using var httpRequest = new HttpRequestMessage(HttpMethod.Get, url);
        httpRequest.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token.Token);

        var response = await _httpClient.SendAsync(httpRequest, cancellationToken);
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);
        var larkResponse = LarkJsonSerializer.Deserialize<LarkResponse<MessageGetResponse>>(responseContent);

        if (larkResponse?.Code != 0)
        {
            throw new Core.Exception.LarkException(
                Models.LarkErrorCode.MessageNotFound,
                larkResponse?.Message ?? "获取消息失败",
                larkResponse?.Error);
        }

        return larkResponse.Data ?? new MessageGetResponse();
    }

    /// <summary>
    /// 删除消息（撤回消息）
    /// </summary>
    public async Task DeleteAsync(string messageId, CancellationToken cancellationToken = default)
    {
        var token = await _tokenProvider.GetTokenAsync(cancellationToken);

        var url = $"/open-apis/im/v1/messages/{messageId}";

        using var httpRequest = new HttpRequestMessage(HttpMethod.Delete, url);
        httpRequest.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token.Token);

        var response = await _httpClient.SendAsync(httpRequest, cancellationToken);
        response.EnsureSuccessStatusCode();
    }
}
