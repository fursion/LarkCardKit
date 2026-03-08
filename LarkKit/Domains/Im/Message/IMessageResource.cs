using LarkKit.Models.Requests;
using LarkKit.Models.Responses;

namespace LarkKit.Domains.Im;

/// <summary>
/// 消息资源接口
/// </summary>
public interface IMessageResource
{
    /// <summary>
    /// 创建消息（发送消息）
    /// </summary>
    Task<MessageCreateResponse> CreateAsync(MessageCreateRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// 获取消息
    /// </summary>
    Task<MessageGetResponse> GetAsync(string messageId, CancellationToken cancellationToken = default);

    /// <summary>
    /// 删除消息（撤回消息）
    /// </summary>
    Task DeleteAsync(string messageId, CancellationToken cancellationToken = default);
}
