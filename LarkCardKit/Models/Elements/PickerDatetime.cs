using System.Text.Json.Serialization;
using LarkCardKit.Models.Behaviors;

namespace LarkCardKit.Models.Elements;

public class PickerDatetime : Element
{
    public override string Tag => "picker_datetime";

    [JsonPropertyName("element_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public new string? ElementId { get; set; }

    [JsonPropertyName("name")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Name { get; set; }
    
    [JsonPropertyName("required")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? Required { get; set; }
    
    [JsonPropertyName("placeholder")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public PlainText? Placeholder { get; set; }
    
    /// <summary>
    /// 文本标签
    /// </summary>
    [JsonPropertyName("label")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public PlainText? Label { get; set; }
    
    [JsonPropertyName("initial_datetime")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? InitialDatetime { get; set; }
    
    [JsonPropertyName("width")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Width { get; set; }
    
    [JsonPropertyName("disabled")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? Disabled { get; set; }

    /// <summary>
    /// 禁用提示
    /// </summary>
    [JsonPropertyName("disabled_tips")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public PlainText? DisabledTips { get; set; }

    [JsonPropertyName("value")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public object? Value { get; set; }
    
    [JsonPropertyName("confirm")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ConfirmConfig? Confirm { get; set; }
    
    [JsonPropertyName("behaviors")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<object>? Behaviors { get; set; }

    /// <summary>
    /// 外边距
    /// </summary>
    [JsonPropertyName("margin")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public new string? Margin { get; set; }
}
