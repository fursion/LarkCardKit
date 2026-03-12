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
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Name { get; set; }
    
    /// <summary>
    /// 是否必填
    /// </summary>
    [JsonPropertyName("required")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? Required { get; set; }
    
    /// <summary>
    /// 占位文本
    /// </summary>
    [JsonPropertyName("placeholder")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public PlainText? Placeholder { get; set; }
    
    /// <summary>
    /// 文本标签
    /// </summary>
    [JsonPropertyName("label")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public PlainText? Label { get; set; }
    
    /// <summary>
    /// 初始日期，格式 yyyy-MM-dd
    /// </summary>
    [JsonPropertyName("initial_date")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? InitialDate { get; set; }
    
    /// <summary>
    /// 日期选择器宽度
    /// </summary>
    [JsonPropertyName("width")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Width { get; set; }
    
    /// <summary>
    /// 是否禁用
    /// </summary>
    [JsonPropertyName("disabled")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? Disabled { get; set; }
    
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
