using System.Text.Json.Serialization;

namespace LarkCardKit.Models.Elements;

/// <summary>
/// 标题后缀标签
/// </summary>
/// <remarks>
/// 用于在卡片标题后添加标签，最多设置 3 个标签，超出不展示。
/// </remarks>
public class TextTag
{
    /// <summary>
    /// 标签类型，固定值 "text_tag"
    /// </summary>
    [JsonPropertyName("tag")]
    public string Tag => "text_tag";
    
    /// <summary>
    /// 元素唯一标识
    /// </summary>
    /// <remarks>
    /// 用于在调用组件相关接口中指定元素。
    /// 仅允许使用字母、数字和下划线，必须以字母开头，不得超过 20 字符。
    /// </remarks>
    [JsonPropertyName("element_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? ElementId { get; set; }
    
    /// <summary>
    /// 标签文本内容
    /// </summary>
    [JsonPropertyName("text")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public PlainText? Text { get; set; }
    
    /// <summary>
    /// 标签颜色
    /// </summary>
    /// <remarks>
    /// 可选值：neutral, blue, turquoise, lime, orange, violet, indigo, wathet, green, yellow, red, purple, carmine
    /// </remarks>
    [JsonPropertyName("color")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Color { get; set; }
}
