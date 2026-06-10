using System.Text.Json.Serialization;

namespace LarkCardKit.Models.Elements;

/// <summary>
/// 表格组件
/// </summary>
/// <remarks>
/// 表格组件支持在表格中添加普通文本、富文本、选项标签、数字、人员列表、日期类型的内容。
/// <para>注意事项：</para>
/// <list type="bullet">
///   <item><description>单张卡片最多支持放置五个表格组件</description></item>
///   <item><description>表格组件不可被内嵌在其它组件内，只可放在卡片根节点下</description></item>
///   <item><description>最多支持添加 50 列，超出 50 列的内容不展示</description></item>
/// </list>
/// </remarks>
public class Table : Element
{
    /// <inheritdoc/>
    public override string Tag => "table";
    
    /// <summary>
    /// 每页最大展示的数据行数
    /// </summary>
    /// <remarks>
    /// 支持 [1,10] 整数，默认值 5。
    /// </remarks>
    [JsonPropertyName("page_size")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? PageSize { get; set; }
    
    /// <summary>
    /// 表格的行高
    /// </summary>
    /// <remarks>
    /// 可选值：
    /// - low：低（默认值）
    /// - middle：中
    /// - high：高
    /// - auto：行高自适应内容
    /// - [32,124]px：自定义行高，如 40px
    /// </remarks>
    [JsonPropertyName("row_height")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? RowHeight { get; set; }
    
    /// <summary>
    /// 最大行高
    /// </summary>
    /// <remarks>
    /// 当 row_height 为 auto 时生效。取值范围 [32,999]px。
    /// </remarks>
    [JsonPropertyName("row_max_height")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? RowMaxHeight { get; set; }
    
    /// <summary>
    /// 是否冻结首列
    /// </summary>
    /// <remarks>
    /// true：冻结首列，左右滚动表格时不滚动首列
    /// false：不冻结首列（默认值）
    /// </remarks>
    [JsonPropertyName("freeze_first_column")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? FreezeFirstColumn { get; set; }
    
    /// <summary>
    /// 表头样式风格
    /// </summary>
    [JsonPropertyName("header_style")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public TableHeaderStyle? HeaderStyle { get; set; }
    
    /// <summary>
    /// 列对象数组
    /// </summary>
    /// <remarks>
    /// 最多支持添加 50 列，超出 50 列的内容不展示。
    /// </remarks>
    [JsonPropertyName("columns")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<TableColumn>? Columns { get; set; }
    
    /// <summary>
    /// 行对象数组
    /// </summary>
    /// <remarks>
    /// 与列定义对应的数据。用 "name":VALUE 的形式，定义每一行的数据内容。
    /// name 即你自定义的列标记。
    /// </remarks>
    [JsonPropertyName("rows")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<Dictionary<string, object?>>? Rows { get; set; }

    /// <summary>
    /// 外边距
    /// </summary>
    [JsonPropertyName("margin")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public new string? Margin { get; set; }
}
