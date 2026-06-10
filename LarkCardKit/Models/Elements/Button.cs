using System.Text.Json.Serialization;
using LarkCardKit.Models.Behaviors;

namespace LarkCardKit.Models.Elements;

public class Button : Element
{
    public override string Tag => "button";

    [JsonPropertyName("type")]
    public string? Type { get; set; }
    
    [JsonPropertyName("size")]
    public string? Size { get; set; }
    
    [JsonPropertyName("width")]
    public string? Width { get; set; }
    
    [JsonPropertyName("text")]
    public PlainText? Text { get; set; }
    
    [JsonPropertyName("icon")]
    public object? Icon { get; set; }
    
    [JsonPropertyName("hover_tips")]
    public PlainText? HoverTips { get; set; }
    
    [JsonPropertyName("disabled")]
    public bool? Disabled { get; set; }
    
    [JsonPropertyName("disabled_tips")]
    public PlainText? DisabledTips { get; set; }
    
    [JsonPropertyName("confirm")]
    public ConfirmConfig? Confirm { get; set; }
    
    [JsonPropertyName("behaviors")]
    public List<object>? Behaviors { get; set; }
    
    [JsonPropertyName("name")]
    public string? Name { get; set; }
    
    [JsonPropertyName("form_action_type")]
    public string? FormActionType { get; set; }

    /// <summary>
    /// 点击行为（已废弃，请使用 behaviors 属性）
    /// </summary>
    [Obsolete("请使用 behaviors 属性代替")]
    [JsonPropertyName("onclick")]
    public object? OnClick { get; set; }
}

public class ConfirmConfig
{
    [JsonPropertyName("title")]
    public PlainText? Title { get; set; }
    
    [JsonPropertyName("text")]
    public PlainText? Text { get; set; }
}
