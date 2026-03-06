using LarkCardKit.Enums;
using LarkCardKit.Models.Behaviors;
using LarkCardKit.Models.Elements;

namespace LarkCardKit.Builders;

/// <summary>
/// Div 容器构建器
/// </summary>
public class DivBuilder
{
    private readonly Div _div = new();
    
    public DivBuilder Direction(string direction)
    {
        _div.Direction = direction;
        return this;
    }
    
    public DivBuilder Vertical()
    {
        _div.Direction = "vertical";
        return this;
    }
    
    public DivBuilder Horizontal()
    {
        _div.Direction = "horizontal";
        return this;
    }
    
    public DivBuilder Padding(string padding)
    {
        _div.Padding = padding;
        return this;
    }
    
    public DivBuilder VerticalSpacing(string spacing)
    {
        _div.VerticalSpacing = spacing;
        return this;
    }
    
    public DivBuilder HorizontalSpacing(string spacing)
    {
        _div.HorizontalSpacing = spacing;
        return this;
    }
    
    public DivBuilder HorizontalAlign(AlignType align)
    {
        _div.HorizontalAlign = align.ToString().ToLower();
        return this;
    }
    
    public DivBuilder VerticalAlign(AlignType align)
    {
        _div.VerticalAlign = align.ToString().ToLower();
        return this;
    }
    
    public DivBuilder Margin(string margin)
    {
        _div.Margin = margin;
        return this;
    }
    
    public DivBuilder ElementId(string id)
    {
        _div.ElementId = id;
        return this;
    }
    
    public DivBuilder Add(Element element)
    {
        _div.Elements.Add(element);
        return this;
    }
    
    public DivBuilder PlainText(string content)
    {
        _div.Elements.Add(new PlainText { Content = content });
        return this;
    }
    
    public DivBuilder Markdown(string content)
    {
        _div.Elements.Add(new Markdown { Content = content });
        return this;
    }
    
    public DivBuilder Button(Action<ButtonBuilder> configure)
    {
        var builder = new ButtonBuilder();
        configure(builder);
        _div.Elements.Add(builder.Build());
        return this;
    }
    
    public DivBuilder Image(string imgKey, Action<ImageBuilder>? configure = null)
    {
        var builder = new ImageBuilder(imgKey);
        configure?.Invoke(builder);
        _div.Elements.Add(builder.Build());
        return this;
    }
    
    public Div Build() => _div;
}
