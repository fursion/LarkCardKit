using System.Text.Json.Serialization;
using LarkCardKit.Models.Elements;

namespace LarkCardKit.Models.Elements;

/// <summary>
/// 表单容器组件
/// </summary>
public class Form : Element
{
    /// <inheritdoc/>
    public override string Tag => "form";
    
    /// <summary>
    /// 表单容器唯一标识，必填
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
    
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
    /// 子元素列表
    /// </summary>
    [JsonPropertyName("elements")]
    public List<Element> Elements { get; set; } = new();
}
