using System.Text.Json.Serialization;

namespace LarkCardKit.Models.Elements;

/// <summary>
/// 分栏容器组件
/// </summary>
public class ColumnSet : Element
{
    /// <inheritdoc/>
    public override string Tag => "column_set";
    
    /// <summary>
    /// 分栏列列表
    /// </summary>
    [JsonPropertyName("columns")]
    public List<Column> Columns { get; set; } = new();
    
    /// <summary>
    /// 外边距
    /// </summary>
    [JsonPropertyName("margin")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public new string? Margin { get; set; }
    
    /// <summary>
    /// 背景样式
    /// </summary>
    [JsonPropertyName("background_style")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? BackgroundStyle { get; set; }
    
    /// <summary>
    /// 水平间距
    /// </summary>
    [JsonPropertyName("horizontal_spacing")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? HorizontalSpacing { get; set; }

    /// <summary>
    /// 弹性模式
    /// </summary>
    /// <remarks>
    /// 可选值：none（不弹性）、flow（流动布局）
    /// </remarks>
    [JsonPropertyName("flex_mode")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? FlexMode { get; set; }
}

/// <summary>
/// 分栏列
/// </summary>
public class Column : Element
{
    /// <inheritdoc/>
    public override string Tag => "column";
    
    /// <summary>
    /// 列宽度，支持 auto 或自定义值
    /// </summary>
    [JsonPropertyName("width")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Width { get; set; }
    
    /// <summary>
    /// 垂直对齐方式
    /// </summary>
    [JsonPropertyName("vertical_align")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? VerticalAlign { get; set; }
    
    /// <summary>
    /// 列内元素列表
    /// </summary>
    [JsonPropertyName("elements")]
    public List<Element> Elements { get; set; } = new();
}
