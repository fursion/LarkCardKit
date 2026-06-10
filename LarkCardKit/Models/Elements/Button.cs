using System.Text.Json.Serialization;
using LarkCardKit.Models.Behaviors;

namespace LarkCardKit.Models.Elements;

public class Button : Element
{
    public override string Tag => "button";

    /// <summary>
    /// 元素唯一标识
    /// </summary>
    [JsonPropertyName("element_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public new string? ElementId { get; set; }

    [JsonPropertyName("type")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Type { get; set; }
    
    [JsonPropertyName("size")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Size { get; set; }
    
    [JsonPropertyName("width")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Width { get; set; }
    
    [JsonPropertyName("text")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public PlainText? Text { get; set; }
    
    [JsonPropertyName("icon")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public object? Icon { get; set; }
    
    [JsonPropertyName("hover_tips")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public PlainText? HoverTips { get; set; }
    
    [JsonPropertyName("disabled")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? Disabled { get; set; }
    
    [JsonPropertyName("disabled_tips")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public PlainText? DisabledTips { get; set; }
    
    [JsonPropertyName("confirm")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ConfirmConfig? Confirm { get; set; }
    
    [JsonPropertyName("behaviors")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<object>? Behaviors { get; set; }
    
    [JsonPropertyName("name")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Name { get; set; }
    
    [JsonPropertyName("form_action_type")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? FormActionType { get; set; }

    /// <summary>
    /// 外边距
    /// </summary>
    [JsonPropertyName("margin")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public new string? Margin { get; set; }

    /// <summary>
    /// 点击行为（已废弃，请使用 behaviors 属性）
    /// </summary>
    [Obsolete("请使用 behaviors 属性代替")]
    [JsonPropertyName("onclick")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public object? OnClick { get; set; }
}

public class ConfirmConfig
{
    [JsonPropertyName("title")]
    public PlainText? Title { get; set; }
    
    [JsonPropertyName("text")]
    public PlainText? Text { get; set; }
}
