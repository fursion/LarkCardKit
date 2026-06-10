using System.Text.Json.Serialization;
using LarkCardKit.Models.Behaviors;

namespace LarkCardKit.Models.Elements;

/// <summary>
/// 时间选择器组件
/// </summary>
/// <remarks>
/// 时间选择器组件是用于提供时间选项的交互组件。
/// 支持嵌套在分栏、表单容器、折叠面板、循环容器、交互容器中使用。
/// </remarks>
public class PickerTime : Element
{
    /// <inheritdoc/>
    public override string Tag => "picker_time";

    /// <summary>
    /// 时间选择器唯一标识，表单容器中必填
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }
    
    /// <summary>
    /// 是否必填
    /// </summary>
    [JsonPropertyName("required")]
    public bool? Required { get; set; }
    
    /// <summary>
    /// 占位文本
    /// </summary>
    [JsonPropertyName("placeholder")]
    public PlainText? Placeholder { get; set; }
    
    /// <summary>
    /// 文本标签
    /// </summary>
    [JsonPropertyName("label")]
    public PlainText? Label { get; set; }
    
    /// <summary>
    /// 初始时间，格式 HH:mm
    /// </summary>
    [JsonPropertyName("initial_time")]
    public string? InitialTime { get; set; }
    
    /// <summary>
    /// 时间选择器宽度
    /// </summary>
    /// <remarks>
    /// 支持以下枚举值：
    /// - default: 默认宽度
    /// - fill: 卡片最大支持宽度
    /// - [100,∞)px: 自定义宽度
    /// </remarks>
    [JsonPropertyName("width")]
    public string? Width { get; set; }
    
    /// <summary>
    /// 是否禁用
    /// </summary>
    [JsonPropertyName("disabled")]
    public bool? Disabled { get; set; }

    /// <summary>
    /// 禁用提示
    /// </summary>
    [JsonPropertyName("disabled_tips")]
    public PlainText? DisabledTips { get; set; }

    /// <summary>
    /// 二次确认配置
    /// </summary>
    [JsonPropertyName("confirm")]
    public ConfirmConfig? Confirm { get; set; }

    /// <summary>
    /// 交互行为列表
    /// </summary>
    [JsonPropertyName("behaviors")]
    public List<object>? Behaviors { get; set; }

    }
