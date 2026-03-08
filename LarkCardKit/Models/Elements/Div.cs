using System.Text.Json.Serialization;
using LarkCardKit.Enums;

namespace LarkCardKit.Models.Elements;

/// <summary>
/// 普通容器组件（飞书卡片 2.0 格式）
/// </summary>
public class Div : Element
{
    /// <inheritdoc/>
    public override string Tag => "div";

    /// <summary>
    /// 文本内容（飞书卡片 2.0 格式）
    /// </summary>
    [JsonPropertyName("text")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Element? Text { get; set; }

    /// <summary>
    /// 子元素列表（旧版本兼容）
    /// </summary>
    [JsonPropertyName("elements")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<Element>? Elements { get; set; }

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
    /// 文本宽度
    /// </summary>
    [JsonPropertyName("width")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Width { get; set; }
}
