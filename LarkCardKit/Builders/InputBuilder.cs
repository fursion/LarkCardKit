using LarkCardKit.Enums;
using LarkCardKit.Models.Behaviors;
using LarkCardKit.Models.Elements;

namespace LarkCardKit.Builders;

/// <summary>
/// 输入框构建器
/// </summary>
public class InputBuilder
{
    private readonly Input _input = new();
    
    public InputBuilder Name(string name)
    {
        _input.Name = name;
        return this;
    }
    
    public InputBuilder Required(bool required = true)
    {
        _input.Required = required;
        return this;
    }
    
    public InputBuilder Placeholder(string placeholder)
    {
        _input.Placeholder = new PlainText { Content = placeholder };
        return this;
    }
    
    public InputBuilder DefaultValue(string value)
    {
        _input.DefaultValue = value;
        return this;
    }
    
    public InputBuilder Type(InputType type)
    {
        _input.InputType = type switch
        {
            InputType.Text => "text",
            InputType.MultilineText => "multiline_text",
            InputType.Password => "password",
            _ => "text"
        };
        return this;
    }
    
    public InputBuilder Label(string label)
    {
        _input.Label = new PlainText { Content = label };
        return this;
    }
    
    public InputBuilder LabelPosition(string position)
    {
        _input.LabelPosition = position;
        return this;
    }
    
    public InputBuilder MaxLength(int length)
    {
        _input.MaxLength = length;
        return this;
    }
    
    public InputBuilder Rows(int rows)
    {
        _input.Rows = rows;
        return this;
    }
    
    public InputBuilder AutoResize(bool resize = true)
    {
        _input.AutoResize = resize;
        return this;
    }
    
    public InputBuilder MaxRows(int rows)
    {
        _input.MaxRows = rows;
        return this;
    }
    
    public InputBuilder Width(string width)
    {
        _input.Width = width;
        return this;
    }
    
    public InputBuilder ElementId(string id)
    {
        _input.ElementId = id;
        return this;
    }
    
    public InputBuilder Margin(string margin)
    {
        _input.Margin = margin;
        return this;
    }
    
    public InputBuilder Disabled(bool disabled = true)
    {
        _input.Disabled = disabled;
        return this;
    }
    
    public InputBuilder DisabledTips(string tips)
    {
        _input.DisabledTips = new PlainText { Content = tips };
        return this;
    }
    
    public InputBuilder OnChange(object callbackData)
    {
        _input.Behaviors ??= new List<object>();
        _input.Behaviors.Add(new CallbackBehavior { Value = callbackData });
        return this;
    }
    
    public Input Build() => _input;
}
