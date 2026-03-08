using System.Text.Json.Serialization;
using LarkCardKit.Models.Elements;

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
    /// 头部副标题
    /// </summary>
    [JsonPropertyName("subtitle")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public object? Subtitle { get; set; }
    
    /// <summary>
    /// 标题后缀标签列表
    /// </summary>
    /// <remarks>
    /// 最多设置 3 个标签，超出不展示。
    /// </remarks>
    [JsonPropertyName("text_tag_list")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<TextTag>? TextTagList { get; set; }
    
    /// <summary>
    /// 标题主题样式颜色
    /// </summary>
    /// <remarks>
    /// 可选值：blue, wathet, turquoise, green, yellow, orange, red, carmine, violet, purple, indigo, grey, default
    /// </remarks>
    [JsonPropertyName("template")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Template { get; set; }
    
    /// <summary>
    /// 头部图标
    /// </summary>
    [JsonPropertyName("icon")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public HeaderIcon? Icon { get; set; }
    
    /// <summary>
    /// 头部内边距
    /// </summary>
    [JsonPropertyName("padding")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Padding { get; set; }
}
