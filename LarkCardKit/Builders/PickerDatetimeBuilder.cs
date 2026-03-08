using LarkCardKit.Models.Behaviors;
using LarkCardKit.Models.Elements;

namespace LarkCardKit.Builders;

public class PickerDatetimeBuilder
{
    private readonly PickerDatetime _pickerDatetime = new();
    
    public PickerDatetimeBuilder Name(string name)
    {
        _pickerDatetime.Name = name;
        return this;
    }
    
    public PickerDatetimeBuilder Required(bool required = true)
    {
        _pickerDatetime.Required = required;
        return this;
    }
    
    public PickerDatetimeBuilder Placeholder(string placeholder)
    {
        _pickerDatetime.Placeholder = new PlainText { Content = placeholder };
        return this;
    }
    
    public PickerDatetimeBuilder InitialDatetime(string datetime)
    {
        _pickerDatetime.InitialDatetime = datetime;
        return this;
    }
    
    public PickerDatetimeBuilder InitialDatetime(DateTime datetime)
    {
        _pickerDatetime.InitialDatetime = datetime.ToString("yyyy-MM-dd HH:mm");
        return this;
    }
    
    public PickerDatetimeBuilder Width(string width)
    {
        _pickerDatetime.Width = width;
        return this;
    }
    
    public PickerDatetimeBuilder Disabled(bool disabled = true)
    {
        _pickerDatetime.Disabled = disabled;
        return this;
    }
    
    public PickerDatetimeBuilder ElementId(string id)
    {
        _pickerDatetime.ElementId = id;
        return this;
    }
    
    public PickerDatetimeBuilder Margin(string margin)
    {
        _pickerDatetime.Margin = margin;
        return this;
    }
    
    public PickerDatetimeBuilder Value(object value)
    {
        _pickerDatetime.Value = value;
        return this;
    }
    
    public PickerDatetimeBuilder Confirm(string title, string text)
    {
        _pickerDatetime.Confirm = new ConfirmConfig
        {
            Title = new PlainText { Content = title },
            Text = new PlainText { Content = text }
        };
        return this;
    }
    
    public PickerDatetimeBuilder OnChange(object callbackData)
    {
        _pickerDatetime.Behaviors ??= new List<object>();
        _pickerDatetime.Behaviors.Add(new CallbackBehavior { Value = callbackData });
        return this;
    }
    
    public PickerDatetime Build() => _pickerDatetime;
}
