using System.Text.Json.Serialization;

namespace LarkCardKit.Models.Elements;

public class Checker : Element
{
    public override string Tag => "checker";

    /// <summary>
    /// 元素唯一标识
    /// </summary>
    [JsonPropertyName("element_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public new string? ElementId { get; set; }

    [JsonPropertyName("name")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Name { get; set; }
    
    [JsonPropertyName("checked")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? Checked { get; set; }
    
    [JsonPropertyName("text")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Element? Text { get; set; }
    
    [JsonPropertyName("overall_checkable")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? OverallCheckable { get; set; }
    
    [JsonPropertyName("button_area")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public object? ButtonArea { get; set; }
    
    [JsonPropertyName("checked_style")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public CheckedStyle? CheckedStyle { get; set; }
    
    [JsonPropertyName("margin")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public new string? Margin { get; set; }
    
    [JsonPropertyName("padding")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Padding { get; set; }
    
    [JsonPropertyName("confirm")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ConfirmConfig? Confirm { get; set; }
    
    [JsonPropertyName("behaviors")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<object>? Behaviors { get; set; }
    
    [JsonPropertyName("hover_tips")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public PlainText? HoverTips { get; set; }
    
    [JsonPropertyName("disabled")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? Disabled { get; set; }
    
    [JsonPropertyName("disabled_tips")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public PlainText? DisabledTips { get; set; }
}

public class CheckedStyle
{
    [JsonPropertyName("show_strikethrough")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? ShowStrikethrough { get; set; }
    
    [JsonPropertyName("opacity")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public double? Opacity { get; set; }
}
