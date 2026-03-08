using System.Text.Json.Serialization;
using LarkKit.Models.Data;
using LarkKit.Models.Params;

namespace LarkKit.Models.Requests;

/// <summary>
/// 创建消息请求
/// </summary>
public class MessageCreateRequest
{
    /// <summary>
    /// 消息数据
    /// </summary>
    [JsonPropertyName("data")]
    public MessageCreateData Data { get; set; } = new();

    /// <summary>
    /// 请求参数
    /// </summary>
    [JsonPropertyName("params")]
    public MessageCreateParams Params { get; set; } = new();
}
