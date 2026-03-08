using LarkCardKit.Enums;
using LarkCardKit.Models.Behaviors;
using LarkCardKit.Models.Elements;

namespace LarkCardKit.Builders;

/// <summary>
/// Div 容器构建器，用于构建可嵌套的容器组件
/// </summary>
/// <remarks>
/// Div 容器可以包含多个子元素，支持垂直和水平布局
/// </remarks>
/// <example>
/// 以下示例演示如何使用 DivBuilder：
/// <code>
/// var div = new DivBuilder()
///     .Vertical()
///     .VerticalSpacing("8px")
///     .PlainText("文本内容")
///     .Button(btn => btn.Text("按钮"))
///     .Build();
/// </code>
/// </example>
public class DivBuilder
{
    private readonly Div _div = new();
    
    /// <summary>
    /// 设置布局方向
    /// </summary>
    /// <param name="direction">方向值："vertical" 或 "horizontal"</param>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    public DivBuilder Direction(string direction)
    {
        _div.Direction = direction;
        return this;
    }
    
    /// <summary>
    /// 设置为垂直布局
    /// </summary>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    public DivBuilder Vertical()
    {
        _div.Direction = "vertical";
        return this;
    }
    
    /// <summary>
    /// 设置为水平布局
    /// </summary>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    public DivBuilder Horizontal()
    {
        _div.Direction = "horizontal";
        return this;
    }
    
    /// <summary>
    /// 设置内边距
    /// </summary>
    /// <param name="padding">内边距值，如 "4px", "8px 12px"</param>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    public DivBuilder Padding(string padding)
    {
        _div.Padding = padding;
        return this;
    }
    
    /// <summary>
    /// 设置垂直间距
    /// </summary>
    /// <param name="spacing">间距值，如 "small", "medium", "8px"</param>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    public DivBuilder VerticalSpacing(string spacing)
    {
        _div.VerticalSpacing = spacing;
        return this;
    }
    
    /// <summary>
    /// 设置水平间距
    /// </summary>
    /// <param name="spacing">间距值，如 "small", "medium", "8px"</param>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    public DivBuilder HorizontalSpacing(string spacing)
    {
        _div.HorizontalSpacing = spacing;
        return this;
    }
    
    /// <summary>
    /// 设置水平对齐方式
    /// </summary>
    /// <param name="align">对齐方式：Left, Center, Right</param>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    public DivBuilder HorizontalAlign(AlignType align)
    {
        _div.HorizontalAlign = align.ToString().ToLower();
        return this;
    }
    
    /// <summary>
    /// 设置垂直对齐方式
    /// </summary>
    /// <param name="align">对齐方式：Top, Center, Bottom</param>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    public DivBuilder VerticalAlign(AlignType align)
    {
        _div.VerticalAlign = align.ToString().ToLower();
        return this;
    }
    
    /// <summary>
    /// 设置外边距
    /// </summary>
    /// <param name="margin">外边距值，如 "4px", "8px 12px"</param>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    public DivBuilder Margin(string margin)
    {
        _div.Margin = margin;
        return this;
    }
    
    /// <summary>
    /// 设置元素唯一标识
    /// </summary>
    /// <param name="id">元素 ID</param>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    public DivBuilder ElementId(string id)
    {
        _div.ElementId = id;
        return this;
    }
    
    /// <summary>
    /// 添加子元素
    /// </summary>
    /// <param name="element">要添加的元素</param>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    public DivBuilder Add(Element element)
    {
        _div.Elements ??= new List<Element>();
        _div.Elements.Add(element);
        return this;
    }
    
    /// <summary>
    /// 添加纯文本元素
    /// </summary>
    /// <param name="content">文本内容</param>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    public DivBuilder PlainText(string content)
    {
        _div.Text = new Models.Elements.PlainText { Content = content };
        return this;
    }
    
    /// <summary>
    /// 添加 Markdown 元素
    /// </summary>
    /// <param name="content">Markdown 内容</param>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    public DivBuilder Markdown(string content)
    {
        _div.Text = new Models.Elements.Markdown { Content = content };
        return this;
    }
    
    /// <summary>
    /// 添加按钮元素
    /// </summary>
    /// <param name="configure">按钮构建器配置</param>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    public DivBuilder Button(Action<ButtonBuilder> configure)
    {
        var builder = new ButtonBuilder();
        configure(builder);
        _div.Elements ??= new List<Element>();
        _div.Elements.Add(builder.Build());
        return this;
    }
    
    /// <summary>
    /// 添加图片元素
    /// </summary>
    /// <param name="imgKey">图片的 img_key</param>
    /// <param name="configure">图片构建器配置（可选）</param>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    public DivBuilder Image(string imgKey, Action<ImageBuilder>? configure = null)
    {
        var builder = new ImageBuilder(imgKey);
        configure?.Invoke(builder);
        _div.Elements ??= new List<Element>();
        _div.Elements.Add(builder.Build());
        return this;
    }

    /// <summary>
    /// 设置文本内容（飞书卡片 2.0 格式，使用 text 字段）
    /// </summary>
    /// <param name="content">文本内容</param>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    public DivBuilder Text(string content)
    {
        _div.Text = new PlainText { Content = content };
        return this;
    }

    /// <summary>
    /// 设置 Markdown 文本（飞书卡片 2.0 格式，使用 text 字段）
    /// </summary>
    /// <param name="content">Markdown 内容</param>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    public DivBuilder MarkdownText(string content)
    {
        _div.Text = new Markdown { Content = content };
        return this;
    }

    /// <summary>
    /// 构建 Div 容器对象
    /// </summary>
    /// <returns>完整的 <see cref="Div"/> 对象</returns>
    public Div Build() => _div;
}
