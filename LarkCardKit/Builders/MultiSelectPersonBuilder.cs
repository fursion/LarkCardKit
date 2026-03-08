using LarkCardKit.Models.Behaviors;
using LarkCardKit.Models.Elements;

namespace LarkCardKit.Builders;

public class MultiSelectPersonBuilder
{
    private readonly MultiSelectPerson _multiSelectPerson = new();
    
    public MultiSelectPersonBuilder Name(string name)
    {
        _multiSelectPerson.Name = name;
        return this;
    }
    
    public MultiSelectPersonBuilder Required(bool required = true)
    {
        _multiSelectPerson.Required = required;
        return this;
    }
    
    public MultiSelectPersonBuilder Placeholder(string placeholder)
    {
        _multiSelectPerson.Placeholder = new PlainText { Content = placeholder };
        return this;
    }
    
    public MultiSelectPersonBuilder SelectedValues(params string[] openIds)
    {
        _multiSelectPerson.SelectedValues = openIds.ToList();
        return this;
    }
    
    public MultiSelectPersonBuilder AddSelectedValue(string openId)
    {
        _multiSelectPerson.SelectedValues ??= new List<string>();
        _multiSelectPerson.SelectedValues.Add(openId);
        return this;
    }
    
    public MultiSelectPersonBuilder Options(params string[] openIds)
    {
        _multiSelectPerson.Options = openIds.Select(id => new PersonOption { Value = id }).ToList();
        return this;
    }
    
    public MultiSelectPersonBuilder AddOption(string openId)
    {
        _multiSelectPerson.Options ??= new List<PersonOption>();
        _multiSelectPerson.Options.Add(new PersonOption { Value = openId });
        return this;
    }
    
    public MultiSelectPersonBuilder Width(string width)
    {
        _multiSelectPerson.Width = width;
        return this;
    }
    
    public MultiSelectPersonBuilder Disabled(bool disabled = true)
    {
        _multiSelectPerson.Disabled = disabled;
        return this;
    }
    
    public MultiSelectPersonBuilder ElementId(string id)
    {
        _multiSelectPerson.ElementId = id;
        return this;
    }
    
    public MultiSelectPersonBuilder Margin(string margin)
    {
        _multiSelectPerson.Margin = margin;
        return this;
    }
    
    public MultiSelectPersonBuilder Confirm(string title, string text)
    {
        _multiSelectPerson.Confirm = new ConfirmConfig
        {
            Title = new PlainText { Content = title },
            Text = new PlainText { Content = text }
        };
        return this;
    }
    
    public MultiSelectPersonBuilder OnChange(object callbackData)
    {
        _multiSelectPerson.Behaviors ??= new List<object>();
        _multiSelectPerson.Behaviors.Add(new CallbackBehavior { Value = callbackData });
        return this;
    }
    
    public MultiSelectPerson Build() => _multiSelectPerson;
}
