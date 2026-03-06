using System.Text.Json.Serialization;

namespace LarkCardKit.Models;

/// <summary>
/// 卡片头部配置
/// </summary>
public class CardHeader
{
    /// <summary>
    /// 头部标题
    /// </summary>
    [JsonPropertyName("title")]
    public object? Title { get; set; }
    
    /// <summary>
    /// 头部图标
    /// </summary>
    [JsonPropertyName("icon")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public object? Icon { get; set; }
    
    /// <summary>
    /// 头部内边距
    /// </summary>
    [JsonPropertyName("padding")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Padding { get; set; }
}
