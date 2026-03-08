using LarkCardKit.Models.Elements;

namespace LarkCardKit.Builders;

public class MarkdownBuilder
{
    private readonly Markdown _markdown = new();
    
    public MarkdownBuilder Content(string content)
    {
        _markdown.Content = content;
        return this;
    }
    
    public MarkdownBuilder TextSize(string textSize)
    {
        _markdown.TextSize = textSize;
        return this;
    }
    
    public MarkdownBuilder TextColor(string textColor)
    {
        _markdown.TextColor = textColor;
        return this;
    }
    
    public MarkdownBuilder TextAlign(string textAlign)
    {
        _markdown.TextAlign = textAlign;
        return this;
    }
    
    public MarkdownBuilder Icon(string token, string? color = null)
    {
        _markdown.Icon = new MarkdownIcon
        {
            Tag = "standard_icon",
            Token = token,
            Color = color
        };
        return this;
    }
    
    public MarkdownBuilder CustomIcon(string imgKey)
    {
        _markdown.Icon = new MarkdownIcon
        {
            Tag = "custom_icon",
            ImgKey = imgKey
        };
        return this;
    }
    
    public MarkdownBuilder ElementId(string id)
    {
        _markdown.ElementId = id;
        return this;
    }
    
    public MarkdownBuilder Margin(string margin)
    {
        _markdown.Margin = margin;
        return this;
    }
    
    public Markdown Build() => _markdown;
}
