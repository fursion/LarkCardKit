using System.Text.Json.Serialization;

namespace LarkKit.Models.Responses;

/// <summary>
/// 创建消息响应
/// </summary>
public class MessageCreateResponse
{
    /// <summary>
    /// 消息 ID
    /// </summary>
    [JsonPropertyName("message_id")]
    public string MessageId { get; set; } = string.Empty;

    /// <summary>
    /// 创建时间
    /// </summary>
    [JsonPropertyName("create_time")]
    public string? CreateTime { get; set; }
}

/// <summary>
/// 获取消息响应
/// </summary>
public class MessageGetResponse
{
    /// <summary>
    /// 消息 ID
    /// </summary>
    [JsonPropertyName("message_id")]
    public string MessageId { get; set; } = string.Empty;

    /// <summary>
    /// 消息类型
    /// </summary>
    [JsonPropertyName("msg_type")]
    public string? MsgType { get; set; }

    /// <summary>
    /// 消息内容
    /// </summary>
    [JsonPropertyName("content")]
    public string? Content { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    [JsonPropertyName("create_time")]
    public string? CreateTime { get; set; }
}
