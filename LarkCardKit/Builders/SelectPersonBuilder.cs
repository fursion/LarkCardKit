using LarkCardKit.Models.Behaviors;
using LarkCardKit.Models.Elements;

namespace LarkCardKit.Builders;

public class SelectPersonBuilder
{
    private readonly SelectPerson _selectPerson = new();
    
    public SelectPersonBuilder Name(string name)
    {
        _selectPerson.Name = name;
        return this;
    }
    
    public SelectPersonBuilder Required(bool required = true)
    {
        _selectPerson.Required = required;
        return this;
    }
    
    public SelectPersonBuilder Placeholder(string placeholder)
    {
        _selectPerson.Placeholder = new PlainText { Content = placeholder };
        return this;
    }
    
    public SelectPersonBuilder InitialOption(string openId)
    {
        _selectPerson.InitialOption = openId;
        return this;
    }
    
    public SelectPersonBuilder Width(string width)
    {
        _selectPerson.Width = width;
        return this;
    }
    
    public SelectPersonBuilder Disabled(bool disabled = true)
    {
        _selectPerson.Disabled = disabled;
        return this;
    }
    
    public SelectPersonBuilder ElementId(string id)
    {
        _selectPerson.ElementId = id;
        return this;
    }
    
    public SelectPersonBuilder Margin(string margin)
    {
        _selectPerson.Margin = margin;
        return this;
    }
    
    public SelectPersonBuilder Confirm(string title, string text)
    {
        _selectPerson.Confirm = new ConfirmConfig
        {
            Title = new PlainText { Content = title },
            Text = new PlainText { Content = text }
        };
        return this;
    }
    
    public SelectPersonBuilder OnChange(object callbackData)
    {
        _selectPerson.Behaviors ??= new List<object>();
        _selectPerson.Behaviors.Add(new CallbackBehavior { Value = callbackData });
        return this;
    }
    
    public SelectPerson Build() => _selectPerson;
}
