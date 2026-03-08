using LarkCardKit.Models.Behaviors;
using LarkCardKit.Models.Elements;

namespace LarkCardKit.Builders;

/// <summary>
/// 时间选择器构建器，用于构建时间选择组件
/// </summary>
/// <remarks>
/// 时间选择器组件是用于提供时间选项的交互组件。
/// 支持嵌套在分栏、表单容器、折叠面板、循环容器、交互容器中使用。
/// </remarks>
/// <example>
/// 以下示例演示如何使用 PickerTimeBuilder：
/// <code>
/// var pickerTime = new PickerTimeBuilder()
///     .Name("meetingTime")
///     .Placeholder("请选择时间")
///     .InitialTime("09:00")
///     .Build();
/// </code>
/// </example>
public class PickerTimeBuilder
{
    private readonly PickerTime _pickerTime = new();
    
    /// <summary>
    /// 设置时间选择器的唯一标识名称（表单容器中必填）
    /// </summary>
    /// <param name="name">时间选择器名称</param>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    public PickerTimeBuilder Name(string name)
    {
        _pickerTime.Name = name;
        return this;
    }
    
    /// <summary>
    /// 设置是否必填
    /// </summary>
    /// <param name="required">是否必填，默认为 true</param>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    public PickerTimeBuilder Required(bool required = true)
    {
        _pickerTime.Required = required;
        return this;
    }
    
    /// <summary>
    /// 设置占位提示文本
    /// </summary>
    /// <param name="placeholder">占位文本内容</param>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    public PickerTimeBuilder Placeholder(string placeholder)
    {
        _pickerTime.Placeholder = new PlainText { Content = placeholder };
        return this;
    }
    
    /// <summary>
    /// 设置初始时间
    /// </summary>
    /// <param name="time">初始时间，格式为 HH:mm（如 "09:00", "14:30"）</param>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    public PickerTimeBuilder InitialTime(string time)
    {
        _pickerTime.InitialTime = time;
        return this;
    }
    
    /// <summary>
    /// 设置时间选择器宽度
    /// </summary>
    /// <param name="width">
    /// 宽度值
    /// <para>预设值："default"（默认宽度）, "fill"（卡片最大支持宽度）</para>
    /// <para>自定义值：[100,∞)px，如 "200px"</para>
    /// </param>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    public PickerTimeBuilder Width(string width)
    {
        _pickerTime.Width = width;
        return this;
    }
    
    /// <summary>
    /// 设置是否禁用
    /// </summary>
    /// <param name="disabled">是否禁用，默认为 true</param>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    public PickerTimeBuilder Disabled(bool disabled = true)
    {
        _pickerTime.Disabled = disabled;
        return this;
    }
    
    /// <summary>
    /// 设置元素唯一标识
    /// </summary>
    /// <param name="id">元素 ID</param>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    public PickerTimeBuilder ElementId(string id)
    {
        _pickerTime.ElementId = id;
        return this;
    }
    
    /// <summary>
    /// 设置外边距
    /// </summary>
    /// <param name="margin">
    /// 外边距值，支持范围 [-99,99]px
    /// <para>示例："4px", "8px 12px", "4px 8px 4px 8px"</para>
    /// </param>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    public PickerTimeBuilder Margin(string margin)
    {
        _pickerTime.Margin = margin;
        return this;
    }
    
    /// <summary>
    /// 设置二次确认弹窗
    /// </summary>
    /// <param name="title">确认弹窗标题</param>
    /// <param name="text">确认弹窗内容</param>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    public PickerTimeBuilder Confirm(string title, string text)
    {
        _pickerTime.Confirm = new ConfirmConfig
        {
            Title = new PlainText { Content = title },
            Text = new PlainText { Content = text }
        };
        return this;
    }
    
    /// <summary>
    /// 设置时间变更时的回调数据
    /// </summary>
    /// <param name="callbackData">回调数据对象</param>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    public PickerTimeBuilder OnChange(object callbackData)
    {
        _pickerTime.Behaviors ??= new List<object>();
        _pickerTime.Behaviors.Add(new CallbackBehavior { Value = callbackData });
        return this;
    }
    
    /// <summary>
    /// 构建时间选择器对象
    /// </summary>
    /// <returns>完整的 <see cref="PickerTime"/> 对象</returns>
    public PickerTime Build() => _pickerTime;
}
