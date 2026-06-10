using System.Text.Json.Serialization;

namespace LarkCardKit.Models.Elements;

public class Checker : Element
{
    public override string Tag => "checker";

    [JsonPropertyName("name")]
    public string? Name { get; set; }
    
    [JsonPropertyName("checked")]
    public bool? Checked { get; set; }
    
    [JsonPropertyName("text")]
    public Element? Text { get; set; }
    
    [JsonPropertyName("overall_checkable")]
    public bool? OverallCheckable { get; set; }
    
    [JsonPropertyName("button_area")]
    public object? ButtonArea { get; set; }
    
    [JsonPropertyName("checked_style")]
    public CheckedStyle? CheckedStyle { get; set; }
    
    
    [JsonPropertyName("padding")]
    public string? Padding { get; set; }
    
    [JsonPropertyName("confirm")]
    public ConfirmConfig? Confirm { get; set; }
    
    [JsonPropertyName("behaviors")]
    public List<object>? Behaviors { get; set; }
    
    [JsonPropertyName("hover_tips")]
    public PlainText? HoverTips { get; set; }
    
    [JsonPropertyName("disabled")]
    public bool? Disabled { get; set; }
    
    [JsonPropertyName("disabled_tips")]
    public PlainText? DisabledTips { get; set; }
}

public class CheckedStyle
{
    [JsonPropertyName("show_strikethrough")]
    public bool? ShowStrikethrough { get; set; }
    
    [JsonPropertyName("opacity")]
    public double? Opacity { get; set; }
}
