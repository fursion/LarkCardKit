using System.Text.Json.Serialization;

namespace LarkKit.Models.Data;

/// <summary>
/// 创建消息数据
/// </summary>
public class MessageCreateData
{
    /// <summary>
    /// 接收者 ID
    /// </summary>
    [JsonPropertyName("receive_id")]
    public string ReceiveId { get; set; } = string.Empty;

    /// <summary>
    /// 消息类型：text, post, image, interactive, file, audio, media, sticker, share_chat, share_user
    /// </summary>
    [JsonPropertyName("msg_type")]
    public string MsgType { get; set; } = "text";

    /// <summary>
    /// 消息内容（JSON 字符串）
    /// </summary>
    [JsonPropertyName("content")]
    public string Content { get; set; } = string.Empty;

    /// <summary>
    /// 租户 Key（商店应用必填）
    /// </summary>
    [JsonPropertyName("tenant_key")]
    public string? TenantKey { get; set; }
}
