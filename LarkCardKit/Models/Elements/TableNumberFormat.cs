using System.Text.Json.Serialization;

namespace LarkCardKit.Models.Elements;

/// <summary>
/// 表格数字格式配置
/// </summary>
/// <remarks>
/// 用于配置数字类型列的显示格式，包括货币符号、小数位数和千分位分隔符。
/// </remarks>
public class TableNumberFormat
{
    /// <summary>
    /// 数字前展示的货币单位
    /// </summary>
    /// <remarks>
    /// 支持 1 个字符的货币单位文本，如 "¥"、"$"。
    /// </remarks>
    [JsonPropertyName("symbol")]
    public string? Symbol { get; set; }
    
    /// <summary>
    /// 数字的小数点位数
    /// </summary>
    /// <remarks>
    /// 支持 [0,10] 的整数。默认不限制小数点位数。
    /// </remarks>
    [JsonPropertyName("precision")]
    public int? Precision { get; set; }
    
    /// <summary>
    /// 是否生效按千分位逗号分割的数字样式
    /// </summary>
    /// <remarks>
    /// 默认值 false。
    /// </remarks>
    [JsonPropertyName("separator")]
    public bool? Separator { get; set; }
}
