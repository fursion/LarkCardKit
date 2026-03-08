using LarkCardKit.Models.Behaviors;
using LarkCardKit.Models.Elements;

namespace LarkCardKit.Builders;

public class CheckerBuilder
{
    private readonly Checker _checker = new();
    
    public CheckerBuilder Name(string name)
    {
        _checker.Name = name;
        return this;
    }
    
    public CheckerBuilder Checked(bool @checked = true)
    {
        _checker.Checked = @checked;
        return this;
    }
    
    public CheckerBuilder Text(string text)
    {
        _checker.Text = new PlainText { Content = text };
        return this;
    }
    
    public CheckerBuilder MarkdownText(string content)
    {
        _checker.Text = new Markdown { Content = content };
        return this;
    }
    
    public CheckerBuilder OverallCheckable(bool overallCheckable = true)
    {
        _checker.OverallCheckable = overallCheckable;
        return this;
    }
    
    public CheckerBuilder CheckedStyle(Action<CheckedStyleBuilder> configure)
    {
        var builder = new CheckedStyleBuilder();
        configure(builder);
        _checker.CheckedStyle = builder.Build();
        return this;
    }
    
    public CheckerBuilder Margin(string margin)
    {
        _checker.Margin = margin;
        return this;
    }
    
    public CheckerBuilder Padding(string padding)
    {
        _checker.Padding = padding;
        return this;
    }
    
    public CheckerBuilder Confirm(string title, string text)
    {
        _checker.Confirm = new ConfirmConfig
        {
            Title = new PlainText { Content = title },
            Text = new PlainText { Content = text }
        };
        return this;
    }
    
    public CheckerBuilder HoverTips(string hoverTips)
    {
        _checker.HoverTips = new PlainText { Content = hoverTips };
        return this;
    }
    
    public CheckerBuilder Disabled(bool disabled = true)
    {
        _checker.Disabled = disabled;
        return this;
    }
    
    public CheckerBuilder DisabledTips(string disabledTips)
    {
        _checker.DisabledTips = new PlainText { Content = disabledTips };
        return this;
    }
    
    public CheckerBuilder OnClick(object callbackData)
    {
        _checker.Behaviors ??= new List<object>();
        _checker.Behaviors.Add(new CallbackBehavior { Value = callbackData });
        return this;
    }
    
    public Checker Build() => _checker;
}

public class CheckedStyleBuilder
{
    private readonly CheckedStyle _checkedStyle = new();
    
    public CheckedStyleBuilder ShowStrikethrough(bool show = true)
    {
        _checkedStyle.ShowStrikethrough = show;
        return this;
    }
    
    public CheckedStyleBuilder Opacity(double opacity)
    {
        _checkedStyle.Opacity = opacity;
        return this;
    }
    
    public CheckedStyle Build() => _checkedStyle;
}
