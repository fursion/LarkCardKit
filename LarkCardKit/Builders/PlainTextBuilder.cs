using LarkCardKit.Models.Elements;

namespace LarkCardKit.Builders;

public class PlainTextBuilder
{
    private readonly PlainText _plainText = new();
    
    public PlainTextBuilder Content(string content)
    {
        _plainText.Content = content;
        return this;
    }
    
    public PlainTextBuilder TextSize(string textSize)
    {
        _plainText.TextSize = textSize;
        return this;
    }
    
    public PlainTextBuilder TextColor(string textColor)
    {
        _plainText.TextColor = textColor;
        return this;
    }
    
    public PlainTextBuilder TextAlign(string textAlign)
    {
        _plainText.TextAlign = textAlign;
        return this;
    }
    
    public PlainTextBuilder Notation(bool notation = true)
    {
        _plainText.Notation = notation;
        return this;
    }
    
    public PlainTextBuilder Width(string width)
    {
        _plainText.Width = width;
        return this;
    }
    
    public PlainTextBuilder ElementId(string id)
    {
        _plainText.ElementId = id;
        return this;
    }
    
    public PlainTextBuilder Margin(string margin)
    {
        _plainText.Margin = margin;
        return this;
    }
    
    public PlainText Build() => _plainText;
}
