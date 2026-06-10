using System.Text.Json.Serialization;

namespace LarkCardKit.Models.Elements;

/// <summary>
/// 图表组件
/// </summary>
public class Chart : Element
{
    /// <inheritdoc/>
    public override string Tag => "chart";

    /// <summary>
    /// 元素唯一标识
    /// </summary>
    [JsonPropertyName("element_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public new string? ElementId { get; set; }

    /// <summary>
    /// 图表类型
    /// </summary>
    [JsonPropertyName("chart_type")]
    public string ChartType { get; set; } = string.Empty;

    /// <summary>
    /// 图表数据
    /// </summary>
    [JsonPropertyName("data")]
    public object Data { get; set; } = new();

    /// <summary>
    /// 图表宽度
    /// </summary>
    [JsonPropertyName("width")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Width { get; set; }

    /// <summary>
    /// 外边距
    /// </summary>
    [JsonPropertyName("margin")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public new string? Margin { get; set; }
}
