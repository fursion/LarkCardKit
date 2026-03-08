using LarkCardKit.Models.Elements;

namespace LarkCardKit.Builders;

/// <summary>
/// 表格表头样式构建器
/// </summary>
public class TableHeaderStyleBuilder
{
    private readonly TableHeaderStyle _style = new();
    
    /// <summary>
    /// 设置文本对齐方式
    /// </summary>
    /// <param name="align">对齐方式：left、center、right</param>
    public TableHeaderStyleBuilder TextAlign(string align)
    {
        _style.TextAlign = align;
        return this;
    }
    
    /// <summary>
    /// 设置文本大小
    /// </summary>
    /// <param name="size">字号：normal（正文）、heading（标题）</param>
    public TableHeaderStyleBuilder TextSize(string size)
    {
        _style.TextSize = size;
        return this;
    }
    
    /// <summary>
    /// 设置背景色
    /// </summary>
    /// <param name="style">背景色：grey、none</param>
    public TableHeaderStyleBuilder BackgroundStyle(string style)
    {
        _style.BackgroundStyle = style;
        return this;
    }
    
    /// <summary>
    /// 设置文本颜色
    /// </summary>
    /// <param name="color">颜色：default、grey</param>
    public TableHeaderStyleBuilder TextColor(string color)
    {
        _style.TextColor = color;
        return this;
    }
    
    /// <summary>
    /// 设置是否加粗
    /// </summary>
    /// <param name="bold">是否加粗，默认 true</param>
    public TableHeaderStyleBuilder Bold(bool bold = true)
    {
        _style.Bold = bold;
        return this;
    }
    
    /// <summary>
    /// 设置文本行数
    /// </summary>
    /// <param name="lines">行数，大于等于 1</param>
    public TableHeaderStyleBuilder Lines(int lines)
    {
        _style.Lines = lines;
        return this;
    }
    
    /// <summary>
    /// 构建表头样式对象
    /// </summary>
    public TableHeaderStyle Build() => _style;
}
