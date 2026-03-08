using LarkCardKit.Enums;
using LarkCardKit.Models.Behaviors;
using LarkCardKit.Models.Elements;
using LarkCardKit.Templates;

namespace LarkCardKit.Builders;

/// <summary>
/// 输入框构建器，用于构建文本输入组件
/// </summary>
/// <remarks>
/// 输入框支持多种输入类型：
/// <list type="bullet">
///   <item><description>Text - 单行文本</description></item>
///   <item><description>MultilineText - 多行文本</description></item>
///   <item><description>Password - 密码输入</description></item>
/// </list>
/// </remarks>
/// <example>
/// 以下示例演示如何使用 InputBuilder：
/// <code>
/// var input = new InputBuilder()
///     .Name("username")
///     .Placeholder("请输入用户名")
///     .Required()
///     .Build();
/// </code>
/// </example>
public class InputBuilder
{
    private readonly Input _input = new();
    
    /// <summary>
    /// 设置输入框的唯一标识名称（表单容器中必填）
    /// </summary>
    /// <param name="name">输入框名称</param>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    public InputBuilder Name(string name)
    {
        _input.Name = name;
        return this;
    }
    
    /// <summary>
    /// 设置是否必填
    /// </summary>
    /// <param name="required">是否必填，默认为 true</param>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    public InputBuilder Required(bool required = true)
    {
        _input.Required = required;
        return this;
    }
    
    /// <summary>
    /// 设置占位提示文本
    /// </summary>
    /// <param name="placeholder">占位文本内容</param>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    public InputBuilder Placeholder(string placeholder)
    {
        _input.Placeholder = new PlainText { Content = placeholder };
        return this;
    }
    
    /// <summary>
    /// 设置默认值
    /// </summary>
    /// <param name="value">默认值内容</param>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    public InputBuilder DefaultValue(string value)
    {
        _input.DefaultValue = value;
        return this;
    }
    
    /// <summary>
    /// 设置输入类型
    /// </summary>
    /// <param name="type">
    /// 输入类型
    /// <para>Text - 单行文本</para>
    /// <para>MultilineText - 多行文本</para>
    /// <para>Password - 密码输入</para>
    /// </param>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    public InputBuilder Type(InputType type)
    {
        _input.InputType = type switch
        {
            InputType.Text => "text",
            InputType.MultilineText => "multiline_text",
            InputType.Password => "password",
            _ => "text"
        };
        return this;
    }
    
    /// <summary>
    /// 设置文本标签
    /// </summary>
    /// <param name="label">标签文本内容</param>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    public InputBuilder Label(string label)
    {
        _input.Label = new PlainText { Content = label };
        return this;
    }
    
    /// <summary>
    /// 设置标签位置
    /// </summary>
    /// <param name="position">标签位置：top（上方）或 left（左侧）</param>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    public InputBuilder LabelPosition(string position)
    {
        _input.LabelPosition = position;
        return this;
    }
    
    /// <summary>
    /// 设置最大输入长度
    /// </summary>
    /// <param name="length">最大长度，取值范围 1-1000</param>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    public InputBuilder MaxLength(int length)
    {
        _input.MaxLength = length;
        return this;
    }
    
    /// <summary>
    /// 设置多行文本的默认行数
    /// </summary>
    /// <param name="rows">行数</param>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    public InputBuilder Rows(int rows)
    {
        _input.Rows = rows;
        return this;
    }
    
    /// <summary>
    /// 设置是否自适应高度
    /// </summary>
    /// <param name="resize">是否自适应，默认为 true</param>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    public InputBuilder AutoResize(bool resize = true)
    {
        _input.AutoResize = resize;
        return this;
    }
    
    /// <summary>
    /// 设置多行文本的最大行数
    /// </summary>
    /// <param name="rows">最大行数</param>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    public InputBuilder MaxRows(int rows)
    {
        _input.MaxRows = rows;
        return this;
    }
    
    /// <summary>
    /// 设置输入框宽度
    /// </summary>
    /// <param name="width">宽度值，如 "default", "fill", "200px"</param>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    public InputBuilder Width(string width)
    {
        _input.Width = width;
        return this;
    }
    
    /// <summary>
    /// 设置元素唯一标识
    /// </summary>
    /// <param name="id">元素 ID</param>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    public InputBuilder ElementId(string id)
    {
        _input.ElementId = id;
        return this;
    }
    
    /// <summary>
    /// 设置外边距
    /// </summary>
    /// <param name="margin">外边距值，如 "4px", "8px 12px"</param>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    public InputBuilder Margin(string margin)
    {
        _input.Margin = margin;
        return this;
    }
    
    /// <summary>
    /// 设置是否禁用
    /// </summary>
    /// <param name="disabled">是否禁用，默认为 true</param>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    public InputBuilder Disabled(bool disabled = true)
    {
        _input.Disabled = disabled;
        return this;
    }

    /// <summary>
    /// 设置是否禁用（使用模板值）
    /// </summary>
    /// <param name="value">模板值对象</param>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    /// <remarks>
    /// 使用此方法时，禁用状态将在调用 <see cref="CardBuilder.SetParameter"/> 时动态填充
    /// </remarks>
    public InputBuilder Disabled(TemplateValue value)
    {
        var placeholder = value.DefaultValue != null
            ? $"${{{value.Key}:{value.DefaultValue}}}"
            : $"${{{value.Key}}}";
        _input.Disabled = placeholder;
        return this;
    }
    
    /// <summary>
    /// 设置禁用提示文本
    /// </summary>
    /// <param name="tips">禁用提示内容</param>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    public InputBuilder DisabledTips(string tips)
    {
        _input.DisabledTips = new PlainText { Content = tips };
        return this;
    }
    
    /// <summary>
    /// 设置输入变更时的回调数据
    /// </summary>
    /// <param name="callbackData">回调数据对象</param>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    public InputBuilder OnChange(object callbackData)
    {
        _input.Behaviors ??= new List<object>();
        _input.Behaviors.Add(new CallbackBehavior { Value = callbackData });
        return this;
    }
    
    /// <summary>
    /// 构建输入框对象
    /// </summary>
    /// <returns>完整的 <see cref="Input"/> 对象</returns>
    public Input Build() => _input;
}
