using System.Text.Json;
using System.Text.Json.Serialization;

namespace LarkKit.Message.Content;

/// <summary>
/// 卡片消息内容（交互式）
/// </summary>
public class InteractiveContent
{
    /// <summary>
    /// 卡片配置（JSON 格式）
    /// </summary>
    [JsonPropertyName("config")]
    public CardConfig? Config { get; set; }

    /// <summary>
    /// 卡片元素
    /// </summary>
    [JsonPropertyName("elements")]
    public List<JsonElement>? Elements { get; set; }

    /// <summary>
    /// 卡片头部
    /// </summary>
    [JsonPropertyName("header")]
    public CardHeader? Header { get; set; }

    /// <summary>
    /// 卡片类型
    /// </summary>
    [JsonPropertyName("card_type")]
    public string? CardType { get; set; }

    /// <summary>
    /// 初始化 InteractiveContent 的新实例
    /// </summary>
    public InteractiveContent() { }

    /// <summary>
    /// 从 JSON 字符串创建 InteractiveContent
    /// </summary>
    /// <param name="json">卡片 JSON</param>
    /// <returns>InteractiveContent 实例</returns>
    public static InteractiveContent FromJson(string json)
    {
        return Util.LarkJsonSerializer.Deserialize<InteractiveContent>(json) ?? new InteractiveContent();
    }
}

/// <summary>
/// 卡片配置
/// </summary>
public class CardConfig
{
    /// <summary>
    /// 宽屏模式
    /// </summary>
    [JsonPropertyName("wide_screen_mode")]
    public bool? WideScreenMode { get; set; }

    /// <summary>
    /// 启用转发
    /// </summary>
    [JsonPropertyName("enable_forward")]
    public bool? EnableForward { get; set; }
}

/// <summary>
/// 卡片头部
/// </summary>
public class CardHeader
{
    /// <summary>
    /// 标题
    /// </summary>
    [JsonPropertyName("title")]
    public string? Title { get; set; }

    /// <summary>
    /// 模板颜色
    /// </summary>
    [JsonPropertyName("template")]
    public string? Template { get; set; }
}
