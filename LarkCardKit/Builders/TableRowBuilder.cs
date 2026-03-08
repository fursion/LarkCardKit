namespace LarkCardKit.Builders;

/// <summary>
/// 表格行数据构建器
/// </summary>
/// <remarks>
/// 用于构建表格的行数据。支持多种数据类型的单元格。
/// </remarks>
public class TableRowBuilder
{
    private readonly Dictionary<string, object?> _row = new();
    
    /// <summary>
    /// 添加单元格数据
    /// </summary>
    /// <param name="columnName">列键名</param>
    /// <param name="value">单元格值</param>
    public TableRowBuilder Cell(string columnName, object? value)
    {
        _row[columnName] = value;
        return this;
    }
    
    /// <summary>
    /// 添加文本单元格
    /// </summary>
    /// <param name="columnName">列键名</param>
    /// <param name="text">文本内容</param>
    public TableRowBuilder TextCell(string columnName, string text)
    {
        _row[columnName] = text;
        return this;
    }
    
    /// <summary>
    /// 添加数字单元格
    /// </summary>
    /// <param name="columnName">列键名</param>
    /// <param name="number">数字值</param>
    public TableRowBuilder NumberCell(string columnName, double number)
    {
        _row[columnName] = number;
        return this;
    }
    
    /// <summary>
    /// 添加日期单元格
    /// </summary>
    /// <param name="columnName">列键名</param>
    /// <param name="timestamp">Unix 毫秒级时间戳</param>
    public TableRowBuilder DateCell(string columnName, long timestamp)
    {
        _row[columnName] = timestamp;
        return this;
    }
    
    /// <summary>
    /// 添加日期单元格（从 DateTime 转换）
    /// </summary>
    /// <param name="columnName">列键名</param>
    /// <param name="dateTime">日期时间</param>
    public TableRowBuilder DateCell(string columnName, DateTime dateTime)
    {
        var timestamp = ((DateTimeOffset)dateTime.ToUniversalTime()).ToUnixTimeMilliseconds();
        _row[columnName] = timestamp;
        return this;
    }
    
    /// <summary>
    /// 添加单个选项标签单元格
    /// </summary>
    /// <param name="columnName">列键名</param>
    /// <param name="text">选项文本</param>
    /// <param name="color">选项颜色</param>
    public TableRowBuilder OptionCell(string columnName, string text, string color = "blue")
    {
        _row[columnName] = new List<Dictionary<string, string>>
        {
            new() { { "text", text }, { "color", color } }
        };
        return this;
    }
    
    /// <summary>
    /// 添加多个选项标签单元格
    /// </summary>
    /// <param name="columnName">列键名</param>
    /// <param name="options">选项列表（文本，颜色）</param>
    public TableRowBuilder OptionsCell(string columnName, params (string text, string color)[] options)
    {
        var optionsList = options.Select(o => new Dictionary<string, string>
        {
            { "text", o.text },
            { "color", o.color }
        }).ToList();
        _row[columnName] = optionsList;
        return this;
    }
    
    /// <summary>
    /// 添加单个人员单元格
    /// </summary>
    /// <param name="columnName">列键名</param>
    /// <param name="userId">用户 ID（open_id、user_id 或 union_id）</param>
    public TableRowBuilder PersonCell(string columnName, string userId)
    {
        _row[columnName] = userId;
        return this;
    }
    
    /// <summary>
    /// 添加多个人员单元格
    /// </summary>
    /// <param name="columnName">列键名</param>
    /// <param name="userIds">用户 ID 列表</param>
    public TableRowBuilder PersonsCell(string columnName, params string[] userIds)
    {
        _row[columnName] = userIds.ToList();
        return this;
    }
    
    /// <summary>
    /// 添加 Markdown 单元格
    /// </summary>
    /// <param name="columnName">列键名</param>
    /// <param name="markdown">Markdown 内容</param>
    public TableRowBuilder MarkdownCell(string columnName, string markdown)
    {
        _row[columnName] = markdown;
        return this;
    }
    
    /// <summary>
    /// 添加链接单元格（lark_md 类型）
    /// </summary>
    /// <param name="columnName">列键名</param>
    /// <param name="text">链接文本</param>
    /// <param name="url">链接地址</param>
    public TableRowBuilder LinkCell(string columnName, string text, string url)
    {
        _row[columnName] = $"[{text}]({url})";
        return this;
    }
    
    /// <summary>
    /// 构建行数据字典
    /// </summary>
    public Dictionary<string, object?> Build() => _row;
}
