using LarkCardKit.Models.Behaviors;
using LarkCardKit.Models.Elements;

namespace LarkCardKit.Builders;

public class OverflowBuilder
{
    private readonly Overflow _overflow = new();
    
    public OverflowBuilder AddOption(string value, string text)
    {
        _overflow.Options ??= new List<OverflowOption>();
        _overflow.Options.Add(new OverflowOption
        {
            Value = value,
            Text = new PlainText { Content = text }
        });
        return this;
    }
    
    public OverflowBuilder Width(string width)
    {
        _overflow.Width = width;
        return this;
    }
    
    public OverflowBuilder Disabled(bool disabled = true)
    {
        _overflow.Disabled = disabled;
        return this;
    }
    
    public OverflowBuilder ElementId(string id)
    {
        _overflow.ElementId = id;
        return this;
    }
    
    public OverflowBuilder Margin(string margin)
    {
        _overflow.Margin = margin;
        return this;
    }
    
    public OverflowBuilder OnClick(object callbackData)
    {
        _overflow.Behaviors ??= new List<object>();
        _overflow.Behaviors.Add(new CallbackBehavior { Value = callbackData });
        return this;
    }
    
    public Overflow Build() => _overflow;
}
