using LarkCardKit.Models.Elements;

namespace LarkCardKit.Builders;

/// <summary>
/// 表格构建器
/// </summary>
/// <remarks>
/// 用于构建表格组件。表格支持多种数据类型的列，包括文本、数字、选项标签、人员列表、日期等。
/// <para>注意事项：</para>
/// <list type="bullet">
///   <item><description>单张卡片最多支持放置五个表格组件</description></item>
///   <item><description>表格组件不可被内嵌在其它组件内，只可放在卡片根节点下</description></item>
///   <item><description>最多支持添加 50 列，超出 50 列的内容不展示</description></item>
/// </list>
/// </remarks>
/// <example>
/// 以下示例演示如何使用 TableBuilder：
/// <code>
/// var card = CardBuilder.Create()
///     .Header(h => h.Title("客户数据表"))
///     .Body(b => b
///         .Table(table => table
///             .PageSize(5)
///             .Column(col => col.Name("name").DisplayName("客户名称").DataType("text"))
///             .Column(col => col.Name("amount").DisplayName("金额").DataType("number"))
///             .Row(row => row.TextCell("name", "飞书科技").NumberCell("amount", 168))))
///     .Build();
/// </code>
/// </example>
public class TableBuilder
{
    private readonly Table _table = new();
    
    /// <summary>
    /// 设置元素唯一标识
    /// </summary>
    /// <param name="id">元素 ID</param>
    public TableBuilder ElementId(string id)
    {
        _table.ElementId = id;
        return this;
    }
    
    /// <summary>
    /// 设置外边距
    /// </summary>
    /// <param name="margin">外边距值</param>
    public TableBuilder Margin(string margin)
    {
        _table.Margin = margin;
        return this;
    }
    
    /// <summary>
    /// 设置每页最大展示的数据行数
    /// </summary>
    /// <param name="pageSize">行数，1-10</param>
    public TableBuilder PageSize(int pageSize)
    {
        _table.PageSize = pageSize;
        return this;
    }
    
    /// <summary>
    /// 设置表格行高
    /// </summary>
    /// <param name="rowHeight">
    /// 行高：
    /// - low：低（默认）
    /// - middle：中
    /// - high：高
    /// - auto：自适应内容
    /// - [32,124]px：自定义行高
    /// </param>
    public TableBuilder RowHeight(string rowHeight)
    {
        _table.RowHeight = rowHeight;
        return this;
    }
    
    /// <summary>
    /// 设置最大行高
    /// </summary>
    /// <param name="maxHeight">最大行高，如 50px</param>
    public TableBuilder RowMaxHeight(string maxHeight)
    {
        _table.RowMaxHeight = maxHeight;
        return this;
    }
    
    /// <summary>
    /// 设置是否冻结首列
    /// </summary>
    /// <param name="freeze">是否冻结，默认 true</param>
    public TableBuilder FreezeFirstColumn(bool freeze = true)
    {
        _table.FreezeFirstColumn = freeze;
        return this;
    }
    
    /// <summary>
    /// 设置表头样式
    /// </summary>
    /// <param name="configure">表头样式配置</param>
    public TableBuilder HeaderStyle(Action<TableHeaderStyleBuilder> configure)
    {
        var builder = new TableHeaderStyleBuilder();
        configure(builder);
        _table.HeaderStyle = builder.Build();
        return this;
    }
    
    /// <summary>
    /// 添加列定义
    /// </summary>
    /// <param name="configure">列配置</param>
    public TableBuilder Column(Action<TableColumnBuilder> configure)
    {
        var builder = new TableColumnBuilder();
        configure(builder);
        _table.Columns ??= new List<TableColumn>();
        _table.Columns.Add(builder.Build());
        return this;
    }
    
    /// <summary>
    /// 添加行数据
    /// </summary>
    /// <param name="configure">行数据配置</param>
    public TableBuilder Row(Action<TableRowBuilder> configure)
    {
        var builder = new TableRowBuilder();
        configure(builder);
        _table.Rows ??= new List<Dictionary<string, object?>>();
        _table.Rows.Add(builder.Build());
        return this;
    }
    
    /// <summary>
    /// 批量设置行数据
    /// </summary>
    /// <param name="rows">行数据列表</param>
    public TableBuilder Rows(IEnumerable<Dictionary<string, object?>> rows)
    {
        _table.Rows = rows.ToList();
        return this;
    }
    
    /// <summary>
    /// 构建表格对象
    /// </summary>
    public Table Build() => _table;
}
