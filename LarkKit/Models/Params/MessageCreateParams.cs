using System.Text.Json.Serialization;

namespace LarkKit.Models.Params;

/// <summary>
/// 创建消息参数
/// </summary>
public class MessageCreateParams
{
    /// <summary>
    /// 接收者 ID 类型：open_id, user_id, union_id, email, chat_id
    /// </summary>
    [JsonPropertyName("receive_id_type")]
    public string ReceiveIdType { get; set; } = "open_id";
}
