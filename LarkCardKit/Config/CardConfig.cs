using System.Text.Json.Serialization;

namespace LarkCardKit.Config;

/// <summary>
/// 卡片配置类
/// </summary>
public class CardConfig
{
    /// <summary>
    /// 是否为共享卡片，默认为 true
    /// </summary>
    [JsonPropertyName("update_multi")]
    public bool UpdateMulti { get; set; } = true;
    
    /// <summary>
    /// 是否启用流式更新模式，默认为 false
    /// </summary>
    [JsonPropertyName("streaming_mode")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? StreamingMode { get; set; }
    
    /// <summary>
    /// 流式更新的摘要信息
    /// </summary>
    [JsonPropertyName("summary")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public StreamingSummary? Summary { get; set; }
}

/// <summary>
/// 流式更新摘要配置
/// </summary>
public class StreamingSummary
{
    /// <summary>
    /// 自定义摘要内容
    /// </summary>
    [JsonPropertyName("content")]
    public string Content { get; set; } = "生成中";
    
    /// <summary>
    /// 摘要的多语言配置
    /// </summary>
    [JsonPropertyName("i18n_content")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Dictionary<string, string>? I18nContent { get; set; }
}
