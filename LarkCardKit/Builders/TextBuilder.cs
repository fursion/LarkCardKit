using LarkCardKit.Models.Elements;

namespace LarkCardKit.Builders;

/// <summary>
/// 文本构建器
/// 支持构建 PlainText (tag: "plain_text") 和 MarkdownText (tag: "lark_md")
/// </summary>
public class TextBuilder
{
    private Element _text;
    
    public TextBuilder()
    {
        _text = new PlainText();
    }
    
    /// <summary>
    /// 设置文本标签
    /// </summary>
    /// <param name="tag">"plain_text" 或 "lark_md"</param>
    public TextBuilder Tag(string tag)
    {
        if (tag == "lark_md")
        {
            _text = new MarkdownText();
        }
        else
        {
            _text = new PlainText();
        }
        return this;
    }
    
    /// <summary>
    /// 设置为 Markdown 标签（快捷方法）
    /// </summary>
    public TextBuilder AsMarkdown()
    {
        _text = new MarkdownText();
        return this;
    }
    
    /// <summary>
    /// 设置文本内容
    /// </summary>
    public TextBuilder Content(string content)
    {
        if (_text is PlainText plainText)
        {
            plainText.Content = content;
        }
        else if (_text is MarkdownText markdownText)
        {
            markdownText.Content = content;
        }
        return this;
    }
    
    /// <summary>
    /// 设置文本大小
    /// </summary>
    public TextBuilder TextSize(string textSize)
    {
        if (_text is PlainText plainText)
        {
            plainText.TextSize = textSize;
        }
        else if (_text is MarkdownText markdownText)
        {
            markdownText.TextSize = textSize;
        }
        return this;
    }
    
    /// <summary>
    /// 设置文本颜色
    /// </summary>
    public TextBuilder TextColor(string textColor)
    {
        if (_text is PlainText plainText)
        {
            plainText.TextColor = textColor;
        }
        else if (_text is MarkdownText markdownText)
        {
            markdownText.TextColor = textColor;
        }
        return this;
    }
    
    /// <summary>
    /// 设置文本对齐方式
    /// </summary>
    public TextBuilder TextAlign(string textAlign)
    {
        if (_text is PlainText plainText)
        {
            plainText.TextAlign = textAlign;
        }
        else if (_text is MarkdownText markdownText)
        {
            markdownText.TextAlign = textAlign;
        }
        return this;
    }
    
    /// <summary>
    /// 设置标记
    /// </summary>
    public TextBuilder Notation(bool notation = true)
    {
        if (_text is PlainText plainText)
        {
            plainText.Notation = notation;
        }
        else if (_text is MarkdownText markdownText)
        {
            markdownText.Notation = notation;
        }
        return this;
    }
    
    /// <summary>
    /// 设置宽度
    /// </summary>
    public TextBuilder Width(string width)
    {
        if (_text is PlainText plainText)
        {
            plainText.Width = width;
        }
        else if (_text is MarkdownText markdownText)
        {
            markdownText.Width = width;
        }
        return this;
    }
    
    /// <summary>
    /// 设置最大显示行数（仅 MarkdownText 支持）
    /// </summary>
    public TextBuilder Lines(int lines)
    {
        if (_text is MarkdownText markdownText)
        {
            markdownText.Lines = lines;
        }
        return this;
    }
    
    /// <summary>
    /// 设置元素 ID
    /// </summary>
    public TextBuilder ElementId(string id)
    {
        if (_text is PlainText plainText)
        {
            plainText.ElementId = id;
        }
        else if (_text is MarkdownText markdownText)
        {
            markdownText.ElementId = id;
        }
        return this;
    }
    
    /// <summary>
    /// 设置外边距（仅 PlainText 支持）
    /// </summary>
    public TextBuilder Margin(string margin)
    {
        if (_text is PlainText plainText)
        {
            plainText.Margin = margin;
        }
        return this;
    }
    
    /// <summary>
    /// 构建文本对象
    /// </summary>
    public Element Build() => _text;
}
