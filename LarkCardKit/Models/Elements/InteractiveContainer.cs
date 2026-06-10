using System.Text.Json.Serialization;
using LarkCardKit.Models.Elements;

namespace LarkCardKit.Models.Elements;

public class InteractiveContainer : Element
{
    public override string Tag => "interactive_container";



    [JsonPropertyName("width")]
    public string? Width { get; set; }

    [JsonPropertyName("height")]
    public string? Height { get; set; }

    [JsonPropertyName("elements")]
    public List<Element> Elements { get; set; } = new();

    [JsonPropertyName("direction")]
    public string? Direction { get; set; }

    [JsonPropertyName("padding")]
    public string? Padding { get; set; }

    [JsonPropertyName("vertical_spacing")]
    public string? VerticalSpacing { get; set; }

    [JsonPropertyName("horizontal_spacing")]
    public string? HorizontalSpacing { get; set; }

    [JsonPropertyName("horizontal_align")]
    public string? HorizontalAlign { get; set; }

    [JsonPropertyName("vertical_align")]
    public string? VerticalAlign { get; set; }

    [JsonPropertyName("background_style")]
    public string? BackgroundStyle { get; set; }

    [JsonPropertyName("has_border")]
    public bool? HasBorder { get; set; }

    [JsonPropertyName("border_color")]
    public string? BorderColor { get; set; }

    [JsonPropertyName("corner_radius")]
    public string? CornerRadius { get; set; }

    [JsonPropertyName("behaviors")]
    public List<object>? Behaviors { get; set; }

    [JsonPropertyName("hover_tips")]
    public PlainText? HoverTips { get; set; }

    [JsonPropertyName("disabled")]
    public bool? Disabled { get; set; }

    [JsonPropertyName("disabled_tips")]
    public PlainText? DisabledTips { get; set; }

    [JsonPropertyName("confirm")]
    public ConfirmConfig? Confirm { get; set; }
}
