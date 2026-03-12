using LarkCardKit.Enums;
using LarkCardKit.Models.Elements;

namespace LarkCardKit.Builders;

/// <summary>
/// 普通文本元素构建器
/// 用于构建飞书卡片 2.0 格式的普通文本组件（PlainTextElement）
/// </summary>
/// <remarks>
/// <para>PlainTextElement 是飞书卡片 2.0 中的普通文本展示组件（tag: "div"）。</para>
/// <para>主要用途：</para>
/// <list type="bullet">
/// <item>展示纯文本内容（text 属性只能是 PlainText）</item>
/// <item>设置文本样式（字号、颜色、对齐方式）</item>
/// <item>控制布局方向和间距</item>
/// </list>
/// <para>注意：PlainTextElement.Text 属性只能是 PlainText 类型（tag: "plain_text"），不能是 Markdown。</para>
/// </remarks>
/// <example>
/// 使用示例：
/// <code>
/// // 简单用法 - 纯文本
/// var element = new PlainTextElementBuilder()
///     .Text("这是普通文本内容")
///     .Build();
/// 
/// // 设置样式
/// var element = new PlainTextElementBuilder()
///     .Text("居中文本")
///     .HorizontalAlign(AlignType.Center)
///     .Padding("8px")
///     .Build();
/// 
/// // 垂直布局
/// var element = new PlainTextElementBuilder()
///     .Vertical()
///     .VerticalSpacing("8px")
///     .Text("第一行")
///     .Text("第二行")
///     .Build();
/// </code>
/// </example>
public class PlainTextElementBuilder
{
    private readonly PlainTextElement _element = new();

    /// <summary>
    /// 设置纯文本内容（text 字段只能是 PlainText）
    /// </summary>
    /// <param name="content">文本内容</param>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    public PlainTextElementBuilder Text(string content)
    {
        _element.Text = new PlainText { Content = content };
        return this;
    }

    /// <summary>
    /// 设置布局方向
    /// </summary>
    /// <param name="direction">方向值："vertical"（垂直）或 "horizontal"（水平）</param>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    public PlainTextElementBuilder Direction(string direction)
    {
        _element.Direction = direction;
        return this;
    }

    /// <summary>
    /// 设置为垂直布局（默认）
    /// </summary>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    public PlainTextElementBuilder Vertical()
    {
        _element.Direction = "vertical";
        return this;
    }

    /// <summary>
    /// 设置为水平布局
    /// </summary>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    public PlainTextElementBuilder Horizontal()
    {
        _element.Direction = "horizontal";
        return this;
    }

    /// <summary>
    /// 设置内边距
    /// </summary>
    /// <param name="padding">内边距值，如 "4px", "8px 12px"</param>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    public PlainTextElementBuilder Padding(string padding)
    {
        _element.Padding = padding;
        return this;
    }

    /// <summary>
    /// 设置垂直间距
    /// </summary>
    /// <param name="spacing">间距值，如 "small", "medium", "8px"</param>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    public PlainTextElementBuilder VerticalSpacing(string spacing)
    {
        _element.VerticalSpacing = spacing;
        return this;
    }

    /// <summary>
    /// 设置水平间距
    /// </summary>
    /// <param name="spacing">间距值，如 "small", "medium", "8px"</param>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    public PlainTextElementBuilder HorizontalSpacing(string spacing)
    {
        _element.HorizontalSpacing = spacing;
        return this;
    }

    /// <summary>
    /// 设置水平对齐方式
    /// </summary>
    /// <param name="align">对齐方式：Left（左）, Center（中）, Right（右）</param>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    public PlainTextElementBuilder HorizontalAlign(AlignType align)
    {
        _element.HorizontalAlign = align.ToString().ToLower();
        return this;
    }

    /// <summary>
    /// 设置垂直对齐方式
    /// </summary>
    /// <param name="align">对齐方式：Top（上）, Center（中）, Bottom（下）</param>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    public PlainTextElementBuilder VerticalAlign(AlignType align)
    {
        _element.VerticalAlign = align.ToString().ToLower();
        return this;
    }

    /// <summary>
    /// 设置外边距
    /// </summary>
    /// <param name="margin">外边距值，如 "4px", "8px 12px"</param>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    public PlainTextElementBuilder Margin(string margin)
    {
        _element.Margin = margin;
        return this;
    }

    /// <summary>
    /// 设置元素唯一标识
    /// </summary>
    /// <param name="id">元素 ID</param>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    public PlainTextElementBuilder ElementId(string id)
    {
        _element.ElementId = id;
        return this;
    }

    /// <summary>
    /// 设置宽度
    /// </summary>
    /// <param name="width">宽度值，如 "100px", "200px"</param>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    public PlainTextElementBuilder Width(string width)
    {
        _element.Width = width;
        return this;
    }

    /// <summary>
    /// 设置背景样式
    /// </summary>
    /// <param name="style">背景样式，如 "bg-grey", "bg-blue-light"</param>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    public PlainTextElementBuilder BackgroundStyle(string style)
    {
        _element.BackgroundStyle = style;
        return this;
    }

    /// <summary>
    /// 构建 PlainTextElement 对象
    /// </summary>
    /// <returns>完整的 <see cref="PlainTextElement"/> 对象</returns>
    public PlainTextElement Build() => _element;
}
