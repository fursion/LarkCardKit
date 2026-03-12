using System.Text.Json.Serialization;
using LarkCardKit.Models.Elements;

namespace LarkCardKit.Models.Elements;

/// <summary>
/// 普通文本元素组件（飞书卡片 2.0 格式）
/// 用于展示普通文本内容，支持设置字号、颜色、对齐方式等样式
/// </summary>
/// <remarks>
/// <para>Div 组件是飞书卡片 2.0 中的普通文本展示组件，属于展示类组件。</para>
/// <para>主要功能：</para>
/// <list type="bullet">
/// <item>展示纯文本（PlainText）或富文本（Markdown）内容</item>
/// <item>支持设置字号、颜色、对齐方式等样式</item>
/// <item>支持垂直或水平布局方向</item>
/// <item>可设置间距、内边距、背景样式等</item>
/// </list>
/// <para><strong>注意：</strong>此组件名为 Div，但实际上是"普通文本组件"，而非 HTML 中的容器概念。</para>
/// </remarks>
/// <example>
/// 使用示例：
/// <code>
/// // 创建简单的纯文本元素
/// var div = new PlainTextElement
/// {
///     Text = new PlainText { Content = "这是普通文本内容" }
/// };
/// 
/// // 使用构建器创建
/// var div = new PlainTextElementBuilder()
///     .Text("文本内容")
///     .Direction("vertical")
///     .Padding("8px")
///     .Build();
/// </code>
/// </example>
public class PlainTextElement : Element
{
    /// <inheritdoc/>
    public override string Tag => "div";

    /// <summary>
    /// 文本内容（飞书卡片 2.0 格式，推荐使用）
    /// </summary>
    /// <remarks>
    /// 支持 PlainText（纯文本）或 Markdown（富文本）两种类型
    /// </remarks>
    [JsonPropertyName("text")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Element? Text { get; set; }

    /// <summary>
    /// 子元素列表（旧格式，用于兼容）
    /// </summary>
    /// <remarks>
    /// 当使用 direction 属性进行布局时，可使用 elements 字段
    /// </remarks>
    [JsonPropertyName("elements")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<Element>? Elements { get; set; }

    /// <summary>
    /// 布局方向
    /// </summary>
    /// <value>
    /// <c>"vertical"</c> - 垂直布局（默认值）<br/>
    /// <c>"horizontal"</c> - 水平布局
    /// </value>
    [JsonPropertyName("direction")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Direction { get; set; }

    /// <summary>
    /// 内边距
    /// </summary>
    /// <example>"4px", "8px 12px", "8px 12px 8px 12px"</example>
    [JsonPropertyName("padding")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Padding { get; set; }

    /// <summary>
    /// 垂直间距
    /// </summary>
    /// <example>"small", "medium", "8px"</example>
    [JsonPropertyName("vertical_spacing")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? VerticalSpacing { get; set; }

    /// <summary>
    /// 水平间距
    /// </summary>
    /// <example>"small", "medium", "8px"</example>
    [JsonPropertyName("horizontal_spacing")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? HorizontalSpacing { get; set; }

    /// <summary>
    /// 水平对齐方式
    /// </summary>
    /// <value>
    /// <c>"left"</c> - 左对齐<br/>
    /// <c>"center"</c> - 居中对齐<br/>
    /// <c>"right"</c> - 右对齐
    /// </value>
    [JsonPropertyName("horizontal_align")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? HorizontalAlign { get; set; }

    /// <summary>
    /// 垂直对齐方式
    /// </summary>
    /// <value>
    /// <c>"top"</c> - 顶部对齐<br/>
    /// <c>"center"</c> - 居中对齐<br/>
    /// <c>"bottom"</c> - 底部对齐
    /// </value>
    [JsonPropertyName("vertical_align")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? VerticalAlign { get; set; }

    /// <summary>
    /// 文本宽度
    /// </summary>
    /// <example>"100px", "200px"</example>
    [JsonPropertyName("width")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Width { get; set; }

    /// <summary>
    /// 背景样式
    /// </summary>
    /// <example>"bg-grey", "bg-blue-light"</example>
    [JsonPropertyName("background_style")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? BackgroundStyle { get; set; }
}
