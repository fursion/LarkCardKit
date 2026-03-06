using LarkCardKit.Enums;
using LarkCardKit.Models.Behaviors;
using LarkCardKit.Models.Elements;

namespace LarkCardKit.Builders;

/// <summary>
/// 按钮构建器
/// </summary>
public class ButtonBuilder
{
    private readonly Button _button = new();
    
    public ButtonBuilder Text(string text)
    {
        _button.Text = new PlainText { Content = text };
        return this;
    }
    
    public ButtonBuilder Type(ButtonType type)
    {
        _button.Type = type.ToString().ToLower();
        return this;
    }
    
    public ButtonBuilder Size(ButtonSize size)
    {
        _button.Size = size.ToString().ToLower();
        return this;
    }
    
    public ButtonBuilder Width(string width)
    {
        _button.Width = width;
        return this;
    }
    
    public ButtonBuilder ElementId(string id)
    {
        _button.ElementId = id;
        return this;
    }
    
    public ButtonBuilder Margin(string margin)
    {
        _button.Margin = margin;
        return this;
    }
    
    public ButtonBuilder Disabled(bool disabled = true)
    {
        _button.Disabled = disabled;
        return this;
    }
    
    public ButtonBuilder DisabledTips(string tips)
    {
        _button.DisabledTips = new PlainText { Content = tips };
        return this;
    }
    
    public ButtonBuilder HoverTips(string tips)
    {
        _button.HoverTips = new PlainText { Content = tips };
        return this;
    }
    
    public ButtonBuilder Confirm(string title, string text)
    {
        _button.Confirm = new ConfirmConfig
        {
            Title = new PlainText { Content = title },
            Text = new PlainText { Content = text }
        };
        return this;
    }
    
    public ButtonBuilder OnClick(object callbackData)
    {
        _button.Behaviors ??= new List<object>();
        _button.Behaviors.Add(new CallbackBehavior { Value = callbackData });
        return this;
    }
    
    public ButtonBuilder OnClickUrl(string url, string? pcUrl = null, string? iosUrl = null, string? androidUrl = null)
    {
        _button.Behaviors ??= new List<object>();
        var behavior = new OpenUrlBehavior
        {
            DefaultUrl = url,
            PcUrl = pcUrl,
            IosUrl = iosUrl,
            AndroidUrl = androidUrl
        };
        _button.Behaviors.Add(behavior);
        return this;
    }
    
    public Button Build() => _button;
}
