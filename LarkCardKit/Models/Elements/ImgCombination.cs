using System.Text.Json.Serialization;

namespace LarkCardKit.Models.Elements;

public class ImgCombination : Element
{
    public override string Tag => "img_combination";

    [JsonPropertyName("element_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public new string? ElementId { get; set; }

    [JsonPropertyName("combination_mode")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? CombinationMode { get; set; }

    [JsonPropertyName("combination_transparent")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? CombinationTransparent { get; set; }

    [JsonPropertyName("corner_radius")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? CornerRadius { get; set; }

    [JsonPropertyName("img_list")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<ImgCombinationItem>? ImgList { get; set; }

    [JsonPropertyName("margin")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public new string? Margin { get; set; }
}

public class ImgCombinationItem
{
    [JsonPropertyName("img_key")]
    public string ImgKey { get; set; } = string.Empty;
    
    [JsonPropertyName("transparent")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? Transparent { get; set; }
}
