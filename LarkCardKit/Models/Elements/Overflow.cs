using System.Text.Json.Serialization;
using LarkCardKit.Models.Behaviors;

namespace LarkCardKit.Models.Elements;

public class Overflow : Element
{
    public override string Tag => "overflow";


    [JsonPropertyName("options")]
    public List<OverflowOption>? Options { get; set; }

    [JsonPropertyName("width")]
    public string? Width { get; set; }

    [JsonPropertyName("disabled")]
    public bool? Disabled { get; set; }

    /// <summary>
    /// 禁用提示
    /// </summary>
    [JsonPropertyName("disabled_tips")]
    public PlainText? DisabledTips { get; set; }

    [JsonPropertyName("behaviors")]
    public List<object>? Behaviors { get; set; }

}

public class OverflowOption
{
    [JsonPropertyName("value")]
    public string Value { get; set; } = string.Empty;
    
    [JsonPropertyName("text")]
    public PlainText? Text { get; set; }
    
    [JsonPropertyName("icon")]
    public object? Icon { get; set; }
}
