using LarkCardKit.Enums;
using LarkCardKit.Models.Behaviors;
using LarkCardKit.Models.Elements;
using LarkCardKit.Templates;

namespace LarkCardKit.Builders;

/// <summary>
/// 按钮构建器，用于构建交互按钮组件
/// </summary>
/// <remarks>
/// 按钮支持多种交互行为：
/// <list type="bullet">
///   <item><description>使用 <see cref="OnClick"/> 设置回调数据交互</description></item>
///   <item><description>使用 <see cref="OnClickUrl"/> 设置跳转链接</description></item>
///   <item><description>使用 <see cref="Submit"/> 或 <see cref="Reset"/> 设置表单动作</description></item>
/// </list>
/// </remarks>
/// <example>
/// 以下示例演示如何使用 ButtonBuilder：
/// <code>
/// var button = new ButtonBuilder()
///     .Text("点击我")
///     .Type(ButtonType.Primary)
///     .OnClick(new { action = "click" })
///     .Build();
/// </code>
/// </example>
public class ButtonBuilder
{
    private readonly Button _button = new();

    /// <summary>
    /// 设置按钮显示文本
    /// </summary>
    /// <param name="text">按钮文本内容</param>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    public ButtonBuilder Text(string text)
    {
        _button.Text = new PlainText { Content = text };
        return this;
    }

    /// <summary>
    /// 设置按钮类型
    /// </summary>
    /// <param name="type">
    /// 按钮类型
    /// <para>Primary - 主要按钮（蓝色）</para>
    /// <para>Default - 默认按钮（灰色）</para>
    /// <para>Danger - 危险按钮（红色）</para>
    /// </param>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    public ButtonBuilder Type(ButtonType type)
    {
        _button.Type = type.ToString().ToLower();
        return this;
    }

    /// <summary>
    /// 设置按钮类型（使用模板值）
    /// </summary>
    /// <param name="value">模板值对象</param>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    /// <remarks>
    /// 使用此方法时，按钮类型将在调用 <see cref="CardBuilder.SetParameter"/> 时动态填充
    /// </remarks>
    public ButtonBuilder Type(TemplateValue value)
    {
        var placeholder = value.DefaultValue != null
            ? $"${{{value.Key}:{value.DefaultValue}}}"
            : $"${{{value.Key}}}";
        _button.Type = placeholder;
        return this;
    }

    /// <summary>
    /// 设置按钮尺寸
    /// </summary>
    /// <param name="size">
    /// 按钮尺寸
    /// <para>Tiny - 超小</para>
    /// <para>Small - 小</para>
    /// <para>Medium - 中等</para>
    /// <para>Large - 大</para>
    /// </param>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    public ButtonBuilder Size(ButtonSize size)
    {
        _button.Size = size.ToString().ToLower();
        return this;
    }

    /// <summary>
    /// 设置按钮宽度
    /// </summary>
    /// <param name="width">宽度值，如 "auto", "fill", "200px"</param>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    public ButtonBuilder Width(string width)
    {
        _button.Width = width;
        return this;
    }

    /// <summary>
    /// 设置元素唯一标识
    /// </summary>
    /// <param name="id">元素 ID</param>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    public ButtonBuilder ElementId(string id)
    {
        _button.ElementId = id;
        return this;
    }

    /// <summary>
    /// 设置外边距
    /// </summary>
    /// <param name="margin">外边距值，如 "4px", "8px 12px"</param>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    public ButtonBuilder Margin(string margin)
    {
        _button.Margin = margin;
        return this;
    }

    /// <summary>
    /// 设置是否禁用
    /// </summary>
    /// <param name="disabled">是否禁用，默认为 true</param>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    public ButtonBuilder Disabled(bool disabled = true)
    {
        _button.Disabled = disabled;
        return this;
    }

    /// <summary>
    /// 设置禁用提示文本
    /// </summary>
    /// <param name="tips">禁用提示内容</param>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    public ButtonBuilder DisabledTips(string tips)
    {
        _button.DisabledTips = new PlainText { Content = tips };
        return this;
    }

    /// <summary>
    /// 设置悬停提示文本
    /// </summary>
    /// <param name="tips">悬停提示内容</param>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    public ButtonBuilder HoverTips(string tips)
    {
        _button.HoverTips = new PlainText { Content = tips };
        return this;
    }

    /// <summary>
    /// 设置二次确认弹窗
    /// </summary>
    /// <param name="title">确认弹窗标题</param>
    /// <param name="text">确认弹窗内容</param>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    public ButtonBuilder Confirm(string title, string text)
    {
        _button.Confirm = new ConfirmConfig
        {
            Title = new PlainText { Content = title },
            Text = new PlainText { Content = text }
        };
        return this;
    }

    /// <summary>
    /// 设置点击回调数据
    /// </summary>
    /// <param name="callbackData">回调数据对象，点击时会将此数据回传</param>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    /// <example>
    /// <code>
    /// button.OnClick(new { action = "submit", id = "123" });
    /// </code>
    /// </example>
    public ButtonBuilder OnClick(object callbackData)
    {
        _button.Behaviors ??= new List<object>();
        _button.Behaviors.Add(new CallbackBehavior { Value = callbackData });
        return this;
    }

    /// <summary>
    /// 设置点击跳转 URL
    /// </summary>
    /// <param name="url">默认跳转地址</param>
    /// <param name="pcUrl">PC 端跳转地址（可选）</param>
    /// <param name="iosUrl">iOS 端跳转地址（可选）</param>
    /// <param name="androidUrl">Android 端跳转地址（可选）</param>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    public ButtonBuilder OnClickUrl(string url, string? pcUrl = null, string? iosUrl = null, string? androidUrl = null)
    {
        _button.Behaviors ??= new List<object>();
        var behavior = new OpenUrlBehavior
        {
            DefaultUrl = url,
            PcUrl = pcUrl,
            IosUrl = iosUrl,
            AndroidUrl = androidUrl
        };
        _button.Behaviors.Add(behavior);
        return this;
    }

    /// <summary>
    /// 设置点击跳转 URL（飞书卡片 2.0 格式）
    /// </summary>
    /// <param name="url">跳转地址</param>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    public ButtonBuilder OnClickUrlV2(string url)
    {
        _button.OnClick = new UrlClickAction { Url = url };
        return this;
    }

    /// <summary>
    /// 设置表单动作类型（用于表单容器内的按钮）
    /// </summary>
    /// <param name="actionType">动作类型：submit（提交）或 reset（重置）</param>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    public ButtonBuilder FormAction(string actionType)
    {
        _button.FormActionType = actionType;
        return this;
    }

    /// <summary>
    /// 设置为提交按钮（用于表单容器）
    /// </summary>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    public ButtonBuilder Submit()
    {
        _button.FormActionType = "submit";
        return this;
    }

    /// <summary>
    /// 设置为重置按钮（用于表单容器）
    /// </summary>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    public ButtonBuilder Reset()
    {
        _button.FormActionType = "reset";
        return this;
    }

    /// <summary>
    /// 构建按钮对象
    /// </summary>
    /// <returns>完整的 <see cref="Button"/> 对象</returns>
    public Button Build() => _button;
}
