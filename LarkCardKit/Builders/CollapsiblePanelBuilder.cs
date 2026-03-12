using LarkCardKit.Models.Elements;

namespace LarkCardKit.Builders;

/// <summary>
/// 折叠面板构建器
/// </summary>
public class CollapsiblePanelBuilder
{
    private readonly CollapsiblePanel _panel = new();

    /// <summary>
    /// 设置是否展开
    /// </summary>
    public CollapsiblePanelBuilder Expanded(bool expanded)
    {
        _panel.Expanded = expanded;
        return this;
    }

    /// <summary>
    /// 设置头部标题
    /// </summary>
    public CollapsiblePanelBuilder Title(string content)
    {
        if (_panel.Header == null)
        {
            _panel.Header = new CollapsiblePanelHeader();
        }
        _panel.Header.Title = new PlainText { Content = content };
        return this;
    }

    /// <summary>
    /// 设置头部图标
    /// </summary>
    public CollapsiblePanelBuilder HeaderIcon(HeaderIcon icon)
    {
        if (_panel.Header == null)
        {
            _panel.Header = new CollapsiblePanelHeader();
        }
        _panel.Header.Icon = icon;
        return this;
    }

    /// <summary>
    /// 设置标准图标
    /// </summary>
    public CollapsiblePanelBuilder StandardIcon(string token, string? color = null)
    {
        var icon = new HeaderIcon
        {
            Tag = "standard_icon",
            Token = token,
            Color = color
        };
        return HeaderIcon(icon);
    }

    /// <summary>
    /// 设置自定义图标
    /// </summary>
    public CollapsiblePanelBuilder CustomIcon(string imgKey)
    {
        var icon = new HeaderIcon
        {
            Tag = "custom_icon",
            ImgKey = imgKey
        };
        return HeaderIcon(icon);
    }

    /// <summary>
    /// 设置展开时的图标
    /// </summary>
    public CollapsiblePanelBuilder ExpandedIcon(HeaderIcon icon)
    {
        if (_panel.Header == null)
        {
            _panel.Header = new CollapsiblePanelHeader();
        }
        _panel.Header.ExpandedIcon = icon;
        return this;
    }

    /// <summary>
    /// 添加元素
    /// </summary>
    public CollapsiblePanelBuilder AddElement(Element element)
    {
        _panel.Elements.Add(element);
        return this;
    }

    /// <summary>
    /// 设置元素 ID
    /// </summary>
    public CollapsiblePanelBuilder ElementId(string id)
    {
        _panel.ElementId = id;
        return this;
    }

    /// <summary>
    /// 设置外边距
    /// </summary>
    public CollapsiblePanelBuilder Margin(string margin)
    {
        _panel.Margin = margin;
        return this;
    }

    /// <summary>
    /// 构建 CollapsiblePanel 对象
    /// </summary>
    public CollapsiblePanel Build()
    {
        return _panel;
    }
}
