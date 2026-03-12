using System.Text.Json.Serialization;
using LarkCardKit.Models.Elements;

namespace LarkCardKit.Models;

/// <summary>
/// 卡片主体配置
/// </summary>
public class CardBody
{
    /// <summary>
    /// 卡片元素列表
    /// </summary>
    [JsonPropertyName("elements")]
    public List<Element> Elements { get; set; } = new();
    
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
    /// 内边距
    /// </summary>
    [JsonPropertyName("padding")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Padding { get; set; }
    
    /// <summary>
    /// 排列方向：vertical/horizontal
    /// </summary>
    [JsonPropertyName("direction")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Direction { get; set; }
    
    /// <summary>
    /// 水平对齐方式：left/center/right
    /// </summary>
    [JsonPropertyName("horizontal_align")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? HorizontalAlign { get; set; }
    
    /// <summary>
    /// 垂直对齐方式：top/center/bottom
    /// </summary>
    [JsonPropertyName("vertical_align")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? VerticalAlign { get; set; }
}
