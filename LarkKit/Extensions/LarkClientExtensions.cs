using System.Text.Json;
using LarkKit.Domains.Im;
using LarkKit.Models.Data;
using LarkKit.Models.Params;
using LarkKit.Models.Requests;

namespace LarkKit.Extensions;

/// <summary>
/// LarkClient 扩展方法
/// 提供快捷调用和向后兼容
/// </summary>
public static class LarkClientExtensions
{
    /// <summary>
    /// 发送文本消息（快捷方法）
    /// </summary>
    public static async Task<string> SendMessageAsync(
        this ILarkClient client,
        string receiveId,
        string text,
        string receiveIdType = "open_id",
        CancellationToken cancellationToken = default)
    {
        var request = new MessageCreateRequest
        {
            Data = new MessageCreateData
            {
                ReceiveId = receiveId,
                MsgType = "text",
                Content = JsonSerializer.Serialize(new { text })
            },
            Params = new MessageCreateParams
            {
                ReceiveIdType = receiveIdType
            }
        };

        var response = await client.Im.Message.CreateAsync(request, cancellationToken);
        return response.MessageId;
    }

    /// <summary>
    /// 发送卡片消息（快捷方法）
    /// </summary>
    public static async Task<string> SendCardAsync(
        this ILarkClient client,
        string receiveId,
        string cardJson,
        string receiveIdType = "open_id",
        CancellationToken cancellationToken = default)
    {
        var request = new MessageCreateRequest
        {
            Data = new MessageCreateData
            {
                ReceiveId = receiveId,
                MsgType = "interactive",
                Content = cardJson
            },
            Params = new MessageCreateParams
            {
                ReceiveIdType = receiveIdType
            }
        };

        var response = await client.Im.Message.CreateAsync(request, cancellationToken);
        return response.MessageId;
    }

    /// <summary>
    /// 撤回消息（快捷方法）
    /// </summary>
    public static async Task WithdrawMessageAsync(
        this ILarkClient client,
        string messageId,
        CancellationToken cancellationToken = default)
    {
        await client.Im.Message.DeleteAsync(messageId, cancellationToken);
    }
}
