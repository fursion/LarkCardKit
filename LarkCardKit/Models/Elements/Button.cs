using System.Text.Json.Serialization;
using LarkCardKit.Enums;
using LarkCardKit.Models.Behaviors;

namespace LarkCardKit.Models.Elements;

/// <summary>
/// 按钮组件
/// </summary>
public class Button : Element
{
    /// <inheritdoc/>
    public override string Tag => "button";
    
    /// <summary>
    /// 按钮类型
    /// </summary>
    [JsonPropertyName("type")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Type { get; set; }
    
    /// <summary>
    /// 按钮尺寸
    /// </summary>
    [JsonPropertyName("size")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Size { get; set; }
    
    /// <summary>
    /// 按钮宽度
    /// </summary>
    [JsonPropertyName("width")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Width { get; set; }
    
    /// <summary>
    /// 按钮文本
    /// </summary>
    [JsonPropertyName("text")]
    public PlainText? Text { get; set; }
    
    /// <summary>
    /// 前缀图标
    /// </summary>
    [JsonPropertyName("icon")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public object? Icon { get; set; }
    
    /// <summary>
    /// 悬浮提示（PC 端）
    /// </summary>
    [JsonPropertyName("hover_tips")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public PlainText? HoverTips { get; set; }
    
    /// <summary>
    /// 是否禁用
    /// </summary>
    [JsonPropertyName("disabled")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? Disabled { get; set; }
    
    /// <summary>
    /// 禁用提示
    /// </summary>
    [JsonPropertyName("disabled_tips")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public PlainText? DisabledTips { get; set; }
    
    /// <summary>
    /// 二次确认配置
    /// </summary>
    [JsonPropertyName("confirm")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ConfirmConfig? Confirm { get; set; }
    
    /// <summary>
    /// 交互行为列表
    /// </summary>
    [JsonPropertyName("behaviors")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<object>? Behaviors { get; set; }
}

/// <summary>
/// 二次确认配置
/// </summary>
public class ConfirmConfig
{
    /// <summary>
    /// 确认弹窗标题
    /// </summary>
    [JsonPropertyName("title")]
    public PlainText? Title { get; set; }
    
    /// <summary>
    /// 确认弹窗内容
    /// </summary>
    [JsonPropertyName("text")]
    public PlainText? Text { get; set; }
}
