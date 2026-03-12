using System.Text.Json.Serialization;

namespace LarkCardKit.Models.Elements;

/// <summary>
/// Markdown 文本对象（Tag: "lark_md"）
/// 用于 CardHeader.title 等文本属性，不用于 TextDiv.text
/// </summary>
public class MarkdownText : Element
{
    /// <inheritdoc/>
    public override string Tag => "lark_md";
    
    /// <summary>
    /// Markdown 格式的内容
    /// </summary>
    [JsonPropertyName("content")]
    public string Content { get; set; } = string.Empty;
    
    /// <summary>
    /// 文本大小
    /// </summary>
    [JsonPropertyName("text_size")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? TextSize { get; set; }
    
    /// <summary>
    /// 文本颜色
    /// </summary>
    [JsonPropertyName("text_color")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? TextColor { get; set; }
    
    /// <summary>
    /// 文本对齐方式
    /// </summary>
    [JsonPropertyName("text_align")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? TextAlign { get; set; }
    
    /// <summary>
    /// 是否启用标记
    /// </summary>
    [JsonPropertyName("notation")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? Notation { get; set; }
    
    /// <summary>
    /// 宽度
    /// </summary>
    [JsonPropertyName("width")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Width { get; set; }
    
    /// <summary>
    /// 最大显示行数
    /// </summary>
    [JsonPropertyName("lines")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? Lines { get; set; }
}
