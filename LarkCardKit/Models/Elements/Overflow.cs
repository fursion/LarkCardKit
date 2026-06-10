using System.Text.Json.Serialization;
using LarkCardKit.Models.Behaviors;

namespace LarkCardKit.Models.Elements;

public class Overflow : Element
{
    public override string Tag => "overflow";

    [JsonPropertyName("element_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public new string? ElementId { get; set; }

    [JsonPropertyName("options")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<OverflowOption>? Options { get; set; }

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

    [JsonPropertyName("behaviors")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<object>? Behaviors { get; set; }

    [JsonPropertyName("margin")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public new string? Margin { get; set; }
}

public class OverflowOption
{
    [JsonPropertyName("value")]
    public string Value { get; set; } = string.Empty;
    
    [JsonPropertyName("text")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public PlainText? Text { get; set; }
    
    [JsonPropertyName("icon")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public object? Icon { get; set; }
}
