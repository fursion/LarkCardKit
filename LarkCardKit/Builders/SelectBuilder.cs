using LarkCardKit.Models.Behaviors;
using LarkCardKit.Models.Elements;

namespace LarkCardKit.Builders;

/// <summary>
/// 选择器构建器
/// </summary>
public class SelectBuilder
{
    private readonly Select _select = new();
    
    public SelectBuilder Name(string name)
    {
        _select.Name = name;
        return this;
    }
    
    public SelectBuilder Required(bool required = true)
    {
        _select.Required = required;
        return this;
    }
    
    public SelectBuilder MultiSelect(bool multi = true)
    {
        _select.MultiSelect = multi;
        return this;
    }
    
    public SelectBuilder Placeholder(string placeholder)
    {
        _select.Placeholder = new PlainText { Content = placeholder };
        return this;
    }
    
    public SelectBuilder Label(string label)
    {
        _select.Label = new PlainText { Content = label };
        return this;
    }
    
    public SelectBuilder LabelPosition(string position)
    {
        // Select 组件不支持 label_position，这里忽略
        return this;
    }
    
    public SelectBuilder InitialOption(string option)
    {
        _select.InitialOption = option;
        return this;
    }
    
    public SelectBuilder Width(string width)
    {
        _select.Width = width;
        return this;
    }
    
    public SelectBuilder Disabled(bool disabled = true)
    {
        _select.Disabled = disabled;
        return this;
    }
    
    public SelectBuilder ElementId(string id)
    {
        _select.ElementId = id;
        return this;
    }
    
    public SelectBuilder Margin(string margin)
    {
        _select.Margin = margin;
        return this;
    }
    
    public SelectBuilder AddOption(string value, string text)
    {
        _select.Options ??= new List<SelectOption>();
        _select.Options.Add(new SelectOption
        {
            Value = value,
            Text = new PlainText { Content = text }
        });
        return this;
    }
    
    public SelectBuilder OnChange(object callbackData)
    {
        _select.Behaviors ??= new List<object>();
        _select.Behaviors.Add(new CallbackBehavior { Value = callbackData });
        return this;
    }
    
    public Select Build() => _select;
}

/// <summary>
/// 日期选择器构建器
/// </summary>
public class DatePickerBuilder
{
    private readonly DatePicker _datePicker = new();
    
    public DatePickerBuilder Name(string name)
    {
        _datePicker.Name = name;
        return this;
    }
    
    public DatePickerBuilder Required(bool required = true)
    {
        _datePicker.Required = required;
        return this;
    }
    
    public DatePickerBuilder Placeholder(string placeholder)
    {
        _datePicker.Placeholder = new PlainText { Content = placeholder };
        return this;
    }
    
    public DatePickerBuilder InitialDate(string date)
    {
        _datePicker.InitialDate = date;
        return this;
    }
    
    public DatePickerBuilder Width(string width)
    {
        _datePicker.Width = width;
        return this;
    }
    
    public DatePickerBuilder Disabled(bool disabled = true)
    {
        _datePicker.Disabled = disabled;
        return this;
    }
    
    public DatePickerBuilder ElementId(string id)
    {
        _datePicker.ElementId = id;
        return this;
    }
    
    public DatePickerBuilder Margin(string margin)
    {
        _datePicker.Margin = margin;
        return this;
    }
    
    public DatePickerBuilder Confirm(string title, string text)
    {
        _datePicker.Confirm = new ConfirmConfig
        {
            Title = new PlainText { Content = title },
            Text = new PlainText { Content = text }
        };
        return this;
    }
    
    public DatePickerBuilder OnChange(object callbackData)
    {
        _datePicker.Behaviors ??= new List<object>();
        _datePicker.Behaviors.Add(new CallbackBehavior { Value = callbackData });
        return this;
    }
    
    public DatePicker Build() => _datePicker;
}

/// <summary>
/// 勾选器构建器
/// </summary>
public class CheckboxBuilder
{
    private readonly Checkbox _checkbox = new();
    
    public CheckboxBuilder Name(string name)
    {
        _checkbox.Name = name;
        return this;
    }
    
    public CheckboxBuilder Required(bool required = true)
    {
        _checkbox.Required = required;
        return this;
    }
    
    public CheckboxBuilder ElementId(string id)
    {
        _checkbox.ElementId = id;
        return this;
    }
    
    public CheckboxBuilder Margin(string margin)
    {
        _checkbox.Margin = margin;
        return this;
    }
    
    public CheckboxBuilder AddOption(string value, string text)
    {
        _checkbox.Options ??= new List<CheckboxOption>();
        _checkbox.Options.Add(new CheckboxOption
        {
            Value = value,
            Text = new PlainText { Content = text }
        });
        return this;
    }
    
    public CheckboxBuilder InitialSelected(params string[] values)
    {
        _checkbox.InitialSelectedOptions = values.ToList();
        return this;
    }
    
    public CheckboxBuilder OnChange(object callbackData)
    {
        _checkbox.Behaviors ??= new List<object>();
        _checkbox.Behaviors.Add(new CallbackBehavior { Value = callbackData });
        return this;
    }
    
    public Checkbox Build() => _checkbox;
}
