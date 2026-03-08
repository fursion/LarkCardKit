using LarkCardKit.Models.Behaviors;
using LarkCardKit.Models.Elements;

namespace LarkCardKit.Builders;

/// <summary>
/// 表单容器构建器，用于构建表单组件
/// </summary>
/// <remarks>
/// 表单容器可以包含输入框、选择器等表单元素，支持表单提交和重置
/// </remarks>
/// <example>
/// 以下示例演示如何使用 FormBuilder：
/// <code>
/// var form = new FormBuilder()
///     .Name("userForm")
///     .Vertical()
///     .Input(i => i.Name("username").Required())
///     .Input(i => i.Name("email"))
///     .Button(btn => btn.Text("提交").Submit())
///     .Build();
/// </code>
/// </example>
public class FormBuilder
{
    private readonly Form _form = new();
    
    /// <summary>
    /// 设置表单名称
    /// </summary>
    /// <param name="name">表单名称</param>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    public FormBuilder Name(string name)
    {
        _form.Name = name;
        return this;
    }
    
    /// <summary>
    /// 设置布局方向
    /// </summary>
    /// <param name="direction">方向值："vertical" 或 "horizontal"</param>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    public FormBuilder Direction(string direction)
    {
        _form.Direction = direction;
        return this;
    }
    
    /// <summary>
    /// 设置为垂直布局
    /// </summary>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    public FormBuilder Vertical()
    {
        _form.Direction = "vertical";
        return this;
    }
    
    /// <summary>
    /// 设置为水平布局
    /// </summary>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    public FormBuilder Horizontal()
    {
        _form.Direction = "horizontal";
        return this;
    }
    
    /// <summary>
    /// 设置内边距
    /// </summary>
    /// <param name="padding">内边距值，如 "4px", "8px 12px"</param>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    public FormBuilder Padding(string padding)
    {
        _form.Padding = padding;
        return this;
    }
    
    /// <summary>
    /// 设置垂直间距
    /// </summary>
    /// <param name="spacing">间距值，如 "small", "medium", "8px"</param>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    public FormBuilder VerticalSpacing(string spacing)
    {
        _form.VerticalSpacing = spacing;
        return this;
    }
    
    /// <summary>
    /// 设置水平间距
    /// </summary>
    /// <param name="spacing">间距值，如 "small", "medium", "8px"</param>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    public FormBuilder HorizontalSpacing(string spacing)
    {
        _form.HorizontalSpacing = spacing;
        return this;
    }
    
    /// <summary>
    /// 设置水平对齐方式
    /// </summary>
    /// <param name="align">对齐方式</param>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    public FormBuilder HorizontalAlign(string align)
    {
        _form.HorizontalAlign = align;
        return this;
    }
    
    /// <summary>
    /// 设置垂直对齐方式
    /// </summary>
    /// <param name="align">对齐方式</param>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    public FormBuilder VerticalAlign(string align)
    {
        _form.VerticalAlign = align;
        return this;
    }
    
    /// <summary>
    /// 设置元素唯一标识
    /// </summary>
    /// <param name="id">元素 ID</param>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    public FormBuilder ElementId(string id)
    {
        _form.ElementId = id;
        return this;
    }
    
    /// <summary>
    /// 设置外边距
    /// </summary>
    /// <param name="margin">外边距值，如 "4px", "8px 12px"</param>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    public FormBuilder Margin(string margin)
    {
        _form.Margin = margin;
        return this;
    }
    
    /// <summary>
    /// 添加子元素
    /// </summary>
    /// <param name="element">要添加的元素</param>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    public FormBuilder Add(Element element)
    {
        _form.Elements.Add(element);
        return this;
    }
    
    /// <summary>
    /// 添加输入框元素
    /// </summary>
    /// <param name="configure">输入框构建器配置</param>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    public FormBuilder Input(Action<InputBuilder> configure)
    {
        var builder = new InputBuilder();
        configure(builder);
        _form.Elements.Add(builder.Build());
        return this;
    }
    
    /// <summary>
    /// 添加按钮元素
    /// </summary>
    /// <param name="configure">按钮构建器配置</param>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    public FormBuilder Button(Action<ButtonBuilder> configure)
    {
        var builder = new ButtonBuilder();
        configure(builder);
        _form.Elements.Add(builder.Build());
        return this;
    }
    
    /// <summary>
    /// 添加分栏容器元素
    /// </summary>
    /// <param name="configure">分栏构建器配置</param>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    public FormBuilder ColumnSet(Action<ColumnSetBuilder> configure)
    {
        var builder = new ColumnSetBuilder();
        configure(builder);
        _form.Elements.Add(builder.Build());
        return this;
    }
    
    /// <summary>
    /// 添加选择器元素
    /// </summary>
    /// <param name="configure">选择器构建器配置</param>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    public FormBuilder Select(Action<SelectBuilder> configure)
    {
        var builder = new SelectBuilder();
        configure(builder);
        _form.Elements.Add(builder.Build());
        return this;
    }
    
    /// <summary>
    /// 添加日期选择器元素
    /// </summary>
    /// <param name="configure">日期选择器构建器配置</param>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    public FormBuilder DatePicker(Action<DatePickerBuilder> configure)
    {
        var builder = new DatePickerBuilder();
        configure(builder);
        _form.Elements.Add(builder.Build());
        return this;
    }
    
    /// <summary>
    /// 添加勾选器元素
    /// </summary>
    /// <param name="configure">勾选器构建器配置</param>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    public FormBuilder Checkbox(Action<CheckboxBuilder> configure)
    {
        var builder = new CheckboxBuilder();
        configure(builder);
        _form.Elements.Add(builder.Build());
        return this;
    }
    
    /// <summary>
    /// 添加时间选择器元素
    /// </summary>
    /// <param name="configure">时间选择器构建器配置</param>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    public FormBuilder PickerTime(Action<PickerTimeBuilder> configure)
    {
        var builder = new PickerTimeBuilder();
        configure(builder);
        _form.Elements.Add(builder.Build());
        return this;
    }
    
    /// <summary>
    /// 添加分割线元素
    /// </summary>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    public FormBuilder Hr()
    {
        _form.Elements.Add(new Hr());
        return this;
    }
    
    /// <summary>
    /// 添加纯文本元素
    /// </summary>
    /// <param name="content">文本内容</param>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    public FormBuilder PlainText(string content)
    {
        _form.Elements.Add(new Div
        {
            Text = new Models.Elements.PlainText { Content = content }
        });
        return this;
    }
    
    /// <summary>
    /// 添加 Markdown 元素
    /// </summary>
    /// <param name="content">Markdown 内容</param>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    public FormBuilder Markdown(string content)
    {
        _form.Elements.Add(new Div
        {
            Text = new Models.Elements.Markdown { Content = content }
        });
        return this;
    }
    
    /// <summary>
    /// 添加 Div 容器元素
    /// </summary>
    /// <param name="configure">Div 构建器配置</param>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    public FormBuilder Div(Action<DivBuilder> configure)
    {
        var builder = new DivBuilder();
        configure(builder);
        _form.Elements.Add(builder.Build());
        return this;
    }
    
    /// <summary>
    /// 构建表单对象
    /// </summary>
    /// <returns>完整的 <see cref="Form"/> 对象</returns>
    public Form Build() => _form;
}

/// <summary>
/// 分栏容器构建器，用于构建多列布局
/// </summary>
/// <example>
/// 以下示例演示如何使用 ColumnSetBuilder：
/// <code>
/// var columnSet = new ColumnSetBuilder()
///     .Margin("8px 0")
///     .AddColumn(col => col.Width("auto").PlainText("左侧"))
///     .AddColumn(col => col.Width("fill").PlainText("右侧"))
///     .Build();
/// </code>
/// </example>
public class ColumnSetBuilder
{
    private readonly ColumnSet _columnSet = new();
    
    /// <summary>
    /// 设置外边距
    /// </summary>
    /// <param name="margin">外边距值，如 "4px", "8px 12px"</param>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    public ColumnSetBuilder Margin(string margin)
    {
        _columnSet.Margin = margin;
        return this;
    }
    
    /// <summary>
    /// 设置背景样式
    /// </summary>
    /// <param name="style">背景样式</param>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    public ColumnSetBuilder BackgroundStyle(string style)
    {
        _columnSet.BackgroundStyle = style;
        return this;
    }
    
    /// <summary>
    /// 设置水平间距
    /// </summary>
    /// <param name="spacing">间距值</param>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    public ColumnSetBuilder HorizontalSpacing(string spacing)
    {
        _columnSet.HorizontalSpacing = spacing;
        return this;
    }
    
    /// <summary>
    /// 添加列
    /// </summary>
    /// <param name="configure">列构建器配置</param>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    public ColumnSetBuilder AddColumn(Action<ColumnBuilder> configure)
    {
        var builder = new ColumnBuilder();
        configure(builder);
        _columnSet.Columns.Add(builder.Build());
        return this;
    }
    
    /// <summary>
    /// 构建分栏容器对象
    /// </summary>
    /// <returns>完整的 <see cref="ColumnSet"/> 对象</returns>
    public ColumnSet Build() => _columnSet;
}

/// <summary>
/// 分栏列构建器，用于构建分栏容器中的列
/// </summary>
public class ColumnBuilder
{
    private readonly Column _column = new();
    
    /// <summary>
    /// 设置列宽度
    /// </summary>
    /// <param name="width">
    /// 宽度值
    /// <para>"auto" - 自适应内容宽度</para>
    /// <para>"fill" - 填充剩余空间</para>
    /// <para>"100px" - 固定宽度</para>
    /// </param>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    public ColumnBuilder Width(string width)
    {
        _column.Width = width;
        return this;
    }
    
    /// <summary>
    /// 设置垂直对齐方式
    /// </summary>
    /// <param name="align">对齐方式</param>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    public ColumnBuilder VerticalAlign(string align)
    {
        _column.VerticalAlign = align;
        return this;
    }
    
    /// <summary>
    /// 设置元素唯一标识
    /// </summary>
    /// <param name="id">元素 ID</param>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    public ColumnBuilder ElementId(string id)
    {
        _column.ElementId = id;
        return this;
    }
    
    /// <summary>
    /// 设置外边距
    /// </summary>
    /// <param name="margin">外边距值</param>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    public ColumnBuilder Margin(string margin)
    {
        _column.Margin = margin;
        return this;
    }
    
    /// <summary>
    /// 添加子元素
    /// </summary>
    /// <param name="element">要添加的元素</param>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    public ColumnBuilder Add(Element element)
    {
        _column.Elements.Add(element);
        return this;
    }
    
    /// <summary>
    /// 添加按钮元素
    /// </summary>
    /// <param name="configure">按钮构建器配置</param>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    public ColumnBuilder Button(Action<ButtonBuilder> configure)
    {
        var builder = new ButtonBuilder();
        configure(builder);
        _column.Elements.Add(builder.Build());
        return this;
    }
    
    /// <summary>
    /// 添加纯文本元素
    /// </summary>
    /// <param name="content">文本内容</param>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    public ColumnBuilder PlainText(string content)
    {
        _column.Elements.Add(new Div
        {
            Text = new Models.Elements.PlainText { Content = content }
        });
        return this;
    }
    
    /// <summary>
    /// 添加 Markdown 元素
    /// </summary>
    /// <param name="content">Markdown 内容</param>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    public ColumnBuilder Markdown(string content)
    {
        _column.Elements.Add(new Div
        {
            Text = new Models.Elements.Markdown { Content = content }
        });
        return this;
    }
    
    /// <summary>
    /// 添加图片元素
    /// </summary>
    /// <param name="imgKey">图片的 img_key</param>
    /// <param name="configure">图片构建器配置（可选）</param>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    public ColumnBuilder Image(string imgKey, Action<ImageBuilder>? configure = null)
    {
        var builder = new ImageBuilder(imgKey);
        configure?.Invoke(builder);
        _column.Elements.Add(builder.Build());
        return this;
    }
    
    /// <summary>
    /// 构建列对象
    /// </summary>
    /// <returns>完整的 <see cref="Column"/> 对象</returns>
    public Column Build() => _column;
}
