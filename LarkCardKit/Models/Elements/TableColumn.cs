using System.Text.Json.Serialization;

namespace LarkCardKit.Models.Elements;

/// <summary>
/// 表格列定义
/// </summary>
/// <remarks>
/// 用于定义表格的列，包括列的键名、展示名称、数据类型、宽度、对齐方式等。
/// 最多支持添加 50 列，超出 50 列的内容不展示。
/// </remarks>
public class TableColumn
{
    /// <summary>
    /// 列的 key（键名）
    /// </summary>
    /// <remarks>
    /// 必填。用于在行数据对象数组中，指定数据填充的单元格。
    /// </remarks>
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
    
    /// <summary>
    /// 列的展示名称
    /// </summary>
    /// <remarks>
    /// 在表头展示的列名称。不填或为空则不展示列名称。
    /// </remarks>
    [JsonPropertyName("display_name")]
    public string? DisplayName { get; set; }
    
    /// <summary>
    /// 列宽度
    /// </summary>
    /// <remarks>
    /// 可选值：
    /// - auto：自适应内容宽度
    /// - 自定义宽度：如 120px，取值范围 [80px, 600px]
    /// - 自定义百分比：如 25%，取值范围 [1%, 100%]
    /// </remarks>
    [JsonPropertyName("width")]
    public string? Width { get; set; }
    
    /// <summary>
    /// 列数据类型
    /// </summary>
    /// <remarks>
    /// 可选值：
    /// - text：不带格式的普通文本（默认值）
    /// - lark_md：支持部分 Markdown 格式的文本
    /// - options：选项标签
    /// - number：数字
    /// - persons：人员列表
    /// - date：日期时间
    /// - markdown：支持完整 Markdown 语法的文本内容
    /// </remarks>
    [JsonPropertyName("data_type")]
    public string? DataType { get; set; }
    
    /// <summary>
    /// 列内数据垂直对齐方式
    /// </summary>
    /// <remarks>
    /// 可选值：top（顶部对齐）、center（中间对齐）、bottom（底部对齐）
    /// </remarks>
    [JsonPropertyName("vertical_align")]
    public string? VerticalAlign { get; set; }
    
    /// <summary>
    /// 列内数据水平对齐方式
    /// </summary>
    /// <remarks>
    /// 可选值：left（左对齐）、center（居中对齐）、right（右对齐）
    /// 默认数字类型的数据右对齐，其它文本左对齐。
    /// </remarks>
    [JsonPropertyName("horizontal_align")]
    public string? HorizontalAlign { get; set; }
    
    /// <summary>
    /// 数字格式配置
    /// </summary>
    /// <remarks>
    /// 仅当 data_type 为 number 时生效。
    /// </remarks>
    [JsonPropertyName("format")]
    public TableNumberFormat? Format { get; set; }
    
    /// <summary>
    /// 日期格式
    /// </summary>
    /// <remarks>
    /// 仅当 data_type 为 date 时生效。
    /// 推荐格式：YYYY/MM/DD、YYYY-MM-DD、YYYY/MM/DD HH:mm 等。
    /// </remarks>
    [JsonPropertyName("date_format")]
    public string? DateFormat { get; set; }
}
