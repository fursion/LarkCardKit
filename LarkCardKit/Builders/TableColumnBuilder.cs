using LarkCardKit.Models.Elements;

namespace LarkCardKit.Builders;

/// <summary>
/// 表格列构建器
/// </summary>
public class TableColumnBuilder
{
    private readonly TableColumn _column = new();
    
    /// <summary>
    /// 设置列的 key（键名）
    /// </summary>
    /// <param name="name">列键名，用于在行数据中指定数据填充的单元格</param>
    public TableColumnBuilder Name(string name)
    {
        _column.Name = name;
        return this;
    }
    
    /// <summary>
    /// 设置列的展示名称
    /// </summary>
    /// <param name="displayName">在表头展示的列名称</param>
    public TableColumnBuilder DisplayName(string displayName)
    {
        _column.DisplayName = displayName;
        return this;
    }
    
    /// <summary>
    /// 设置列宽度
    /// </summary>
    /// <param name="width">
    /// 列宽度：
    /// - auto：自适应内容宽度
    /// - 自定义宽度：如 120px，取值范围 [80px, 600px]
    /// - 自定义百分比：如 25%，取值范围 [1%, 100%]
    /// </param>
    public TableColumnBuilder Width(string width)
    {
        _column.Width = width;
        return this;
    }
    
    /// <summary>
    /// 设置列数据类型
    /// </summary>
    /// <param name="dataType">
    /// 数据类型：
    /// - text：普通文本（默认）
    /// - lark_md：支持 Markdown 格式的文本
    /// - options：选项标签
    /// - number：数字
    /// - persons：人员列表
    /// - date：日期时间
    /// - markdown：完整 Markdown 语法
    /// </param>
    public TableColumnBuilder DataType(string dataType)
    {
        _column.DataType = dataType;
        return this;
    }
    
    /// <summary>
    /// 设置垂直对齐方式
    /// </summary>
    /// <param name="align">对齐方式：top、center、bottom</param>
    public TableColumnBuilder VerticalAlign(string align)
    {
        _column.VerticalAlign = align;
        return this;
    }
    
    /// <summary>
    /// 设置水平对齐方式
    /// </summary>
    /// <param name="align">对齐方式：left、center、right</param>
    public TableColumnBuilder HorizontalAlign(string align)
    {
        _column.HorizontalAlign = align;
        return this;
    }
    
    /// <summary>
    /// 设置数字格式
    /// </summary>
    /// <param name="configure">数字格式配置</param>
    public TableColumnBuilder NumberFormat(Action<TableNumberFormatBuilder> configure)
    {
        var builder = new TableNumberFormatBuilder();
        configure(builder);
        _column.Format = builder.Build();
        return this;
    }
    
    /// <summary>
    /// 设置日期格式
    /// </summary>
    /// <param name="format">
    /// 日期格式，如：
    /// - YYYY/MM/DD
    /// - YYYY-MM-DD
    /// - YYYY/MM/DD HH:mm
    /// </param>
    public TableColumnBuilder DateFormat(string format)
    {
        _column.DateFormat = format;
        return this;
    }
    
    /// <summary>
    /// 构建列对象
    /// </summary>
    public TableColumn Build() => _column;
}

/// <summary>
/// 表格数字格式构建器
/// </summary>
public class TableNumberFormatBuilder
{
    private readonly TableNumberFormat _format = new();
    
    /// <summary>
    /// 设置货币符号
    /// </summary>
    /// <param name="symbol">货币符号，如 "¥"、"$"</param>
    public TableNumberFormatBuilder Symbol(string symbol)
    {
        _format.Symbol = symbol;
        return this;
    }
    
    /// <summary>
    /// 设置小数位数
    /// </summary>
    /// <param name="precision">小数位数，0-10</param>
    public TableNumberFormatBuilder Precision(int precision)
    {
        _format.Precision = precision;
        return this;
    }
    
    /// <summary>
    /// 设置是否使用千分位分隔符
    /// </summary>
    /// <param name="separator">是否使用千分位，默认 true</param>
    public TableNumberFormatBuilder Separator(bool separator = true)
    {
        _format.Separator = separator;
        return this;
    }
    
    /// <summary>
    /// 构建数字格式对象
    /// </summary>
    public TableNumberFormat Build() => _format;
}
