using System.Text.Json.Serialization;
using LarkCardKit.Models.Elements;

namespace LarkCardKit.Models.Elements;

/// <summary>
/// 普通文本组件（飞书卡片 2.0 格式）
/// 用于展示普通文本内容，支持设置布局属性
/// text 属性只能是 PlainText (tag: "plain_text")
/// </summary>
public class TextDiv : Element
{
    /// <inheritdoc/>
    public override string Tag => "div";
    
    /// <summary>
    /// 文本内容（只能是 PlainText）
    /// </summary>
    [JsonPropertyName("text")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public PlainText? Text { get; set; }
    
    /// <summary>
    /// 布局方向
    /// </summary>
    [JsonPropertyName("direction")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Direction { get; set; }
    
    /// <summary>
    /// 内边距
    /// </summary>
    [JsonPropertyName("padding")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Padding { get; set; }
    
    /// <summary>
    /// 垂直间距
    /// </summary>
    [JsonPropertyName("vertical_spacing")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? VerticalSpacing { get; set; }
    
    /// <summary>
    /// 水平间距
    /// </summary>
    [JsonPropertyName("horizontal_spacing")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? HorizontalSpacing { get; set; }
    
    /// <summary>
    /// 水平对齐方式
    /// </summary>
    [JsonPropertyName("horizontal_align")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? HorizontalAlign { get; set; }
    
    /// <summary>
    /// 垂直对齐方式
    /// </summary>
    [JsonPropertyName("vertical_align")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? VerticalAlign { get; set; }
    
    /// <summary>
    /// 背景样式
    /// </summary>
    [JsonPropertyName("background_style")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? BackgroundStyle { get; set; }
    
    /// <summary>
    /// 元素 ID
    /// </summary>
    [JsonPropertyName("element_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public new string? ElementId { get; set; }
    
    /// <summary>
    /// 外边距
    /// </summary>
    [JsonPropertyName("margin")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public new string? Margin { get; set; }
    
    /// <summary>
    /// 宽度
    /// </summary>
    [JsonPropertyName("width")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Width { get; set; }
}
