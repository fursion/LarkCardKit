using System.Text.Json.Serialization;

namespace LarkCardKit.Models.Elements;

/// <summary>
/// 表格表头样式配置
/// </summary>
/// <remarks>
/// 用于配置表格表头的样式、风格等。
/// </remarks>
public class TableHeaderStyle
{
    /// <summary>
    /// 表头文本对齐方式
    /// </summary>
    /// <remarks>
    /// 可选值：left（左对齐）、center（居中对齐）、right（右对齐）
    /// </remarks>
    [JsonPropertyName("text_align")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? TextAlign { get; set; }
    
    /// <summary>
    /// 表头文本大小
    /// </summary>
    /// <remarks>
    /// 可选值：normal（正文 14px）、heading（标题 16px）
    /// </remarks>
    [JsonPropertyName("text_size")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? TextSize { get; set; }
    
    /// <summary>
    /// 表头背景色
    /// </summary>
    /// <remarks>
    /// 可选值：grey（灰色）、none（无背景色）
    /// </remarks>
    [JsonPropertyName("background_style")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? BackgroundStyle { get; set; }
    
    /// <summary>
    /// 表头文本颜色
    /// </summary>
    /// <remarks>
    /// 可选值：default（浅色主题黑色，深色主题白色）、grey（灰色）
    /// </remarks>
    [JsonPropertyName("text_color")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? TextColor { get; set; }
    
    /// <summary>
    /// 表头文本是否加粗
    /// </summary>
    [JsonPropertyName("bold")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? Bold { get; set; }
    
    /// <summary>
    /// 表头文本的行数
    /// </summary>
    /// <remarks>
    /// 支持大于等于 1 的整数。
    /// </remarks>
    [JsonPropertyName("lines")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? Lines { get; set; }
}
