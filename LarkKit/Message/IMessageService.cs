using LarkKit.Message.Content;
using LarkKit.Models;

namespace LarkKit.Message;

/// <summary>
/// 消息服务接口
/// </summary>
public interface IMessageService
{
    /// <summary>
    /// 发送文本消息
    /// </summary>
    /// <param name="receiveId">接收者 ID</param>
    /// <param name="text">文本内容</param>
    /// <param name="receiveIdType">接收者 ID 类型</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>消息 ID</returns>
    Task<string> SendTextAsync(string receiveId, string text, ReceiveIdType receiveIdType = ReceiveIdType.OpenId, CancellationToken cancellationToken = default);

    /// <summary>
    /// 发送图片消息
    /// </summary>
    /// <param name="receiveId">接收者 ID</param>
    /// <param name="imageKey">图片 Key</param>
    /// <param name="receiveIdType">接收者 ID 类型</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>消息 ID</returns>
    Task<string> SendImageAsync(string receiveId, string imageKey, ReceiveIdType receiveIdType = ReceiveIdType.OpenId, CancellationToken cancellationToken = default);

    /// <summary>
    /// 发送富文本消息
    /// </summary>
    /// <param name="receiveId">接收者 ID</param>
    /// <param name="content">富文本内容</param>
    /// <param name="receiveIdType">接收者 ID 类型</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>消息 ID</returns>
    Task<string> SendPostAsync(string receiveId, PostContent content, ReceiveIdType receiveIdType = ReceiveIdType.OpenId, CancellationToken cancellationToken = default);

    /// <summary>
    /// 发送卡片消息
    /// </summary>
    /// <param name="receiveId">接收者 ID</param>
    /// <param name="card">卡片内容（JSON 字符串或对象）</param>
    /// <param name="receiveIdType">接收者 ID 类型</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>消息 ID</returns>
    Task<string> SendCardAsync(string receiveId, string card, ReceiveIdType receiveIdType = ReceiveIdType.OpenId, CancellationToken cancellationToken = default);

    /// <summary>
    /// 撤回消息
    /// </summary>
    /// <param name="messageId">消息 ID</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>任务</returns>
    Task WithdrawAsync(string messageId, CancellationToken cancellationToken = default);

    /// <summary>
    /// 批量发送文本消息
    /// </summary>
    /// <param name="receiveIds">接收者 ID 列表</param>
    /// <param name="text">文本内容</param>
    /// <param name="receiveIdType">接收者 ID 类型</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>消息 ID 列表</returns>
    Task<List<string>> BatchSendTextAsync(List<string> receiveIds, string text, ReceiveIdType receiveIdType = ReceiveIdType.OpenId, CancellationToken cancellationToken = default);
}
