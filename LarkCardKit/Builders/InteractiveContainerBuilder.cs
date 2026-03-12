using LarkCardKit.Enums;
using LarkCardKit.Models.Behaviors;
using LarkCardKit.Models.Elements;

namespace LarkCardKit.Builders;

public class InteractiveContainerBuilder
{
    private readonly InteractiveContainer _container = new();

    public InteractiveContainerBuilder Width(string width)
    {
        _container.Width = width;
        return this;
    }

    public InteractiveContainerBuilder Height(string height)
    {
        _container.Height = height;
        return this;
    }

    public InteractiveContainerBuilder Direction(string direction)
    {
        _container.Direction = direction;
        return this;
    }

    public InteractiveContainerBuilder Vertical()
    {
        _container.Direction = "vertical";
        return this;
    }

    public InteractiveContainerBuilder Horizontal()
    {
        _container.Direction = "horizontal";
        return this;
    }

    public InteractiveContainerBuilder Padding(string padding)
    {
        _container.Padding = padding;
        return this;
    }

    public InteractiveContainerBuilder VerticalSpacing(string spacing)
    {
        _container.VerticalSpacing = spacing;
        return this;
    }

    public InteractiveContainerBuilder HorizontalSpacing(string spacing)
    {
        _container.HorizontalSpacing = spacing;
        return this;
    }

    public InteractiveContainerBuilder HorizontalAlign(AlignType align)
    {
        _container.HorizontalAlign = align.ToString().ToLower();
        return this;
    }

    public InteractiveContainerBuilder VerticalAlign(AlignType align)
    {
        _container.VerticalAlign = align.ToString().ToLower();
        return this;
    }

    public InteractiveContainerBuilder BackgroundStyle(string style)
    {
        _container.BackgroundStyle = style;
        return this;
    }

    public InteractiveContainerBuilder HasBorder(bool hasBorder = true)
    {
        _container.HasBorder = hasBorder;
        return this;
    }

    public InteractiveContainerBuilder BorderColor(string color)
    {
        _container.BorderColor = color;
        return this;
    }

    public InteractiveContainerBuilder CornerRadius(string radius)
    {
        _container.CornerRadius = radius;
        return this;
    }

    public InteractiveContainerBuilder Margin(string margin)
    {
        _container.Margin = margin;
        return this;
    }

    public InteractiveContainerBuilder ElementId(string id)
    {
        _container.ElementId = id;
        return this;
    }

    public InteractiveContainerBuilder HoverTips(string tips)
    {
        _container.HoverTips = new PlainText { Content = tips };
        return this;
    }

    public InteractiveContainerBuilder Disabled(bool disabled = true)
    {
        _container.Disabled = disabled;
        return this;
    }

    public InteractiveContainerBuilder DisabledTips(string tips)
    {
        _container.DisabledTips = new PlainText { Content = tips };
        return this;
    }

    public InteractiveContainerBuilder Confirm(string title, string text)
    {
        _container.Confirm = new ConfirmConfig
        {
            Title = new PlainText { Content = title },
            Text = new PlainText { Content = text }
        };
        return this;
    }

    public InteractiveContainerBuilder Add(Element element)
    {
        _container.Elements.Add(element);
        return this;
    }

    public InteractiveContainerBuilder PlainText(string content)
    {
        _container.Elements.Add(new TextDiv { Text = new PlainText { Content = content } });
        return this;
    }

    public InteractiveContainerBuilder Markdown(string content)
    {
        _container.Elements.Add(new Markdown { Content = content });
        return this;
    }

    public InteractiveContainerBuilder Button(Action<ButtonBuilder> configure)
    {
        var builder = new ButtonBuilder();
        configure(builder);
        _container.Elements.Add(builder.Build());
        return this;
    }

    public InteractiveContainerBuilder Image(string imgKey, Action<ImageBuilder>? configure = null)
    {
        var builder = new ImageBuilder(imgKey);
        configure?.Invoke(builder);
        _container.Elements.Add(builder.Build());
        return this;
    }

    public InteractiveContainerBuilder TextDiv(Action<TextDivBuilder> configure)
    {
        var builder = new TextDivBuilder();
        configure(builder);
        _container.Elements.Add(builder.Build());
        return this;
    }

    public InteractiveContainerBuilder ColumnSet(Action<ColumnSetBuilder> configure)
    {
        var builder = new ColumnSetBuilder();
        configure(builder);
        _container.Elements.Add(builder.Build());
        return this;
    }

    public InteractiveContainerBuilder OnClick(string url)
    {
        _container.Behaviors ??= new List<object>();
        _container.Behaviors.Add(new OpenUrlBehavior { DefaultUrl = url });
        return this;
    }

    public InteractiveContainerBuilder OnClick(OpenUrlBehavior behavior)
    {
        _container.Behaviors ??= new List<object>();
        _container.Behaviors.Add(behavior);
        return this;
    }

    public InteractiveContainerBuilder OnCallback(object? value = null)
    {
        _container.Behaviors ??= new List<object>();
        _container.Behaviors.Add(new CallbackBehavior { Value = value });
        return this;
    }

    public InteractiveContainer Build() => _container;
}
