using LarkCardKit.Models.Behaviors;
using LarkCardKit.Models.Elements;

namespace LarkCardKit.Builders;

/// <summary>
/// 表单容器构建器
/// </summary>
public class FormBuilder
{
    private readonly Form _form = new();
    
    public FormBuilder Name(string name)
    {
        _form.Name = name;
        return this;
    }
    
    public FormBuilder Direction(string direction)
    {
        _form.Direction = direction;
        return this;
    }
    
    public FormBuilder Vertical()
    {
        _form.Direction = "vertical";
        return this;
    }
    
    public FormBuilder Horizontal()
    {
        _form.Direction = "horizontal";
        return this;
    }
    
    public FormBuilder Padding(string padding)
    {
        _form.Padding = padding;
        return this;
    }
    
    public FormBuilder VerticalSpacing(string spacing)
    {
        _form.VerticalSpacing = spacing;
        return this;
    }
    
    public FormBuilder HorizontalSpacing(string spacing)
    {
        _form.HorizontalSpacing = spacing;
        return this;
    }
    
    public FormBuilder HorizontalAlign(string align)
    {
        _form.HorizontalAlign = align;
        return this;
    }
    
    public FormBuilder VerticalAlign(string align)
    {
        _form.VerticalAlign = align;
        return this;
    }
    
    public FormBuilder ElementId(string id)
    {
        _form.ElementId = id;
        return this;
    }
    
    public FormBuilder Margin(string margin)
    {
        _form.Margin = margin;
        return this;
    }
    
    public FormBuilder Add(Element element)
    {
        _form.Elements.Add(element);
        return this;
    }
    
    public FormBuilder Input(Action<InputBuilder> configure)
    {
        var builder = new InputBuilder();
        configure(builder);
        _form.Elements.Add(builder.Build());
        return this;
    }
    
    public FormBuilder Button(Action<ButtonBuilder> configure)
    {
        var builder = new ButtonBuilder();
        configure(builder);
        _form.Elements.Add(builder.Build());
        return this;
    }
    
    public FormBuilder ColumnSet(Action<ColumnSetBuilder> configure)
    {
        var builder = new ColumnSetBuilder();
        configure(builder);
        _form.Elements.Add(builder.Build());
        return this;
    }
    
    public FormBuilder Select(Action<SelectBuilder> configure)
    {
        var builder = new SelectBuilder();
        configure(builder);
        _form.Elements.Add(builder.Build());
        return this;
    }
    
    public FormBuilder DatePicker(Action<DatePickerBuilder> configure)
    {
        var builder = new DatePickerBuilder();
        configure(builder);
        _form.Elements.Add(builder.Build());
        return this;
    }
    
    public FormBuilder Checkbox(Action<CheckboxBuilder> configure)
    {
        var builder = new CheckboxBuilder();
        configure(builder);
        _form.Elements.Add(builder.Build());
        return this;
    }
    
    public FormBuilder PlainText(string content)
    {
        _form.Elements.Add(new PlainText { Content = content });
        return this;
    }
    
    public FormBuilder Div(Action<DivBuilder> configure)
    {
        var builder = new DivBuilder();
        configure(builder);
        _form.Elements.Add(builder.Build());
        return this;
    }
    
    public Form Build() => _form;
}

/// <summary>
/// 分栏容器构建器
/// </summary>
public class ColumnSetBuilder
{
    private readonly ColumnSet _columnSet = new();
    
    public ColumnSetBuilder Margin(string margin)
    {
        _columnSet.Margin = margin;
        return this;
    }
    
    public ColumnSetBuilder BackgroundStyle(string style)
    {
        _columnSet.BackgroundStyle = style;
        return this;
    }
    
    public ColumnSetBuilder HorizontalSpacing(string spacing)
    {
        _columnSet.HorizontalSpacing = spacing;
        return this;
    }
    
    public ColumnSetBuilder AddColumn(Action<ColumnBuilder> configure)
    {
        var builder = new ColumnBuilder();
        configure(builder);
        _columnSet.Columns.Add(builder.Build());
        return this;
    }
    
    public ColumnSet Build() => _columnSet;
}

/// <summary>
/// 分栏列构建器
/// </summary>
public class ColumnBuilder
{
    private readonly Column _column = new();
    
    public ColumnBuilder Width(string width)
    {
        _column.Width = width;
        return this;
    }
    
    public ColumnBuilder VerticalAlign(string align)
    {
        _column.VerticalAlign = align;
        return this;
    }
    
    public ColumnBuilder ElementId(string id)
    {
        _column.ElementId = id;
        return this;
    }
    
    public ColumnBuilder Margin(string margin)
    {
        _column.Margin = margin;
        return this;
    }
    
    public ColumnBuilder Add(Element element)
    {
        _column.Elements.Add(element);
        return this;
    }
    
    public ColumnBuilder Button(Action<ButtonBuilder> configure)
    {
        var builder = new ButtonBuilder();
        configure(builder);
        _column.Elements.Add(builder.Build());
        return this;
    }
    
    public ColumnBuilder PlainText(string content)
    {
        _column.Elements.Add(new PlainText { Content = content });
        return this;
    }
    
    public ColumnBuilder Markdown(string content)
    {
        _column.Elements.Add(new Markdown { Content = content });
        return this;
    }
    
    public ColumnBuilder Image(string imgKey, Action<ImageBuilder>? configure = null)
    {
        var builder = new ImageBuilder(imgKey);
        configure?.Invoke(builder);
        _column.Elements.Add(builder.Build());
        return this;
    }
    
    public Column Build() => _column;
}
