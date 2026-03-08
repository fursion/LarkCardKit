using LarkCardKit.Models.Elements;

namespace LarkCardKit.Builders;

/// <summary>
/// 后缀标签构建器
/// </summary>
/// <remarks>
/// 用于构建卡片标题的后缀标签。最多可添加 3 个标签，超出不展示。
/// </remarks>
/// <example>
/// 以下示例演示如何使用 TextTagBuilder：
/// <code>
/// var textTag = new TextTagBuilder()
///     .Text("进行中")
///     .Color("blue")
///     .Build();
/// </code>
/// </example>
public class TextTagBuilder
{
    private readonly TextTag _textTag = new();
    
    /// <summary>
    /// 设置标签文本内容
    /// </summary>
    /// <param name="content">标签文本</param>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    public TextTagBuilder Text(string content)
    {
        _textTag.Text = new PlainText { Content = content };
        return this;
    }
    
    /// <summary>
    /// 设置标签颜色
    /// </summary>
    /// <param name="color">
    /// 颜色枚举值
    /// <para>可选值：neutral, blue, turquoise, lime, orange, violet, indigo, wathet, green, yellow, red, purple, carmine</para>
    /// </param>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    public TextTagBuilder Color(string color)
    {
        _textTag.Color = color;
        return this;
    }
    
    /// <summary>
    /// 设置元素唯一标识
    /// </summary>
    /// <param name="id">元素 ID</param>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    public TextTagBuilder ElementId(string id)
    {
        _textTag.ElementId = id;
        return this;
    }
    
    /// <summary>
    /// 构建后缀标签对象
    /// </summary>
    /// <returns>完整的 <see cref="TextTag"/> 对象</returns>
    public TextTag Build() => _textTag;
}
