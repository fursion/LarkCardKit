using System.Text.Json.Serialization;
using LarkCardKit.Models.Behaviors;

namespace LarkCardKit.Models.Elements;

/// <summary>
/// 日期选择器组件
/// </summary>
public class DatePicker : Element
{
    /// <inheritdoc/>
    public override string Tag => "date_picker";

    /// <summary>
    /// 日期选择器唯一标识，表单容器中必填
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
    /// 初始日期，格式 yyyy-MM-dd
    /// </summary>
    [JsonPropertyName("initial_date")]
    public string? InitialDate { get; set; }
    
    /// <summary>
    /// 日期选择器宽度
    /// </summary>
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
