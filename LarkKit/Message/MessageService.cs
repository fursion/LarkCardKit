using LarkKit.Core.Http;
using LarkKit.Core.Exception;
using LarkKit.Message.Content;
using LarkKit.Models;
using LarkKit.Util;

namespace LarkKit.Message;

/// <summary>
/// 消息服务实现
/// </summary>
/// <remarks>
/// 初始化 MessageService 的新实例
/// </remarks>
/// <param name="httpClient">HTTP 客户端</param>
public class MessageService(ILarkHttpClient httpClient) : IMessageService
{
    private readonly ILarkHttpClient _httpClient = httpClient;


    /// <summary>
    /// 发送文本消息
    /// </summary>
    public async Task<string> SendTextAsync(string receiveId, string text, ReceiveIdType receiveIdType = ReceiveIdType.OpenId, CancellationToken cancellationToken = default)
    {
        var url = "/open-apis/im/v1/messages";
        var content = new TextContent(text);
        
        var request = new
        {
            receive_id = receiveId,
            msg_type = "text",
            content = LarkJsonSerializer.Serialize(content)
        };

        var response = await _httpClient.PostAsync<dynamic, MessageSendResponse>(url, request, cancellationToken);
        return response?.MessageId ?? throw new LarkException(LarkErrorCode.MessageSendFailed, "发送消息失败，未返回 message_id");
    }

    /// <summary>
    /// 发送图片消息
    /// </summary>
    public async Task<string> SendImageAsync(string receiveId, string imageKey, ReceiveIdType receiveIdType = ReceiveIdType.OpenId, CancellationToken cancellationToken = default)
    {
        var url = "/open-apis/im/v1/messages";
        var content = new ImageContent(imageKey);
        
        var request = new
        {
            receive_id = receiveId,
            msg_type = "image",
            content = LarkJsonSerializer.Serialize(content)
        };

        var response = await _httpClient.PostAsync<dynamic, MessageSendResponse>(url, request, cancellationToken);
        return response?.MessageId ?? throw new LarkException(LarkErrorCode.MessageSendFailed, "发送消息失败，未返回 message_id");
    }

    /// <summary>
    /// 发送富文本消息
    /// </summary>
    public async Task<string> SendPostAsync(string receiveId, PostContent content, ReceiveIdType receiveIdType = ReceiveIdType.OpenId, CancellationToken cancellationToken = default)
    {
        var url = "/open-apis/im/v1/messages";
        
        var request = new
        {
            receive_id = receiveId,
            msg_type = "post",
            content = LarkJsonSerializer.Serialize(content)
        };

        var response = await _httpClient.PostAsync<dynamic, MessageSendResponse>(url, request, cancellationToken);
        return response?.MessageId ?? throw new LarkException(LarkErrorCode.MessageSendFailed, "发送消息失败，未返回 message_id");
    }

    /// <summary>
    /// 发送卡片消息
    /// </summary>
    public async Task<string> SendCardAsync(string receiveId, string card, ReceiveIdType receiveIdType = ReceiveIdType.OpenId, CancellationToken cancellationToken = default)
    {
        var url = "/open-apis/im/v1/messages";
        
        var request = new
        {
            receive_id = receiveId,
            msg_type = "interactive",
            content = card
        };

        var response = await _httpClient.PostAsync<dynamic, MessageSendResponse>(url, request, cancellationToken);
        return response?.MessageId ?? throw new LarkException(LarkErrorCode.MessageSendFailed, "发送消息失败，未返回 message_id");
    }

    /// <summary>
    /// 撤回消息
    /// </summary>
    public async Task WithdrawAsync(string messageId, CancellationToken cancellationToken = default)
    {
        var url = $"/open-apis/im/v1/messages/{messageId}";
        await _httpClient.DeleteAsync<object>(url, cancellationToken);
    }

    /// <summary>
    /// 批量发送文本消息
    /// </summary>
    public async Task<List<string>> BatchSendTextAsync(List<string> receiveIds, string text, ReceiveIdType receiveIdType = ReceiveIdType.OpenId, CancellationToken cancellationToken = default)
    {
        var messageIds = new List<string>();
        
        // 批量发送：并发发送多个消息
        var tasks = receiveIds.Select(id => SendTextAsync(id, text, receiveIdType, cancellationToken));
        var results = await Task.WhenAll(tasks);
        
        messageIds.AddRange(results);
        return messageIds;
    }

    /// <summary>
    /// 消息发送响应
    /// </summary>
    private class MessageSendResponse
    {
        public string MessageId { get; set; } = string.Empty;
    }
}
