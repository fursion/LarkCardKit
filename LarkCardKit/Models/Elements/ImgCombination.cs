using System.Text.Json.Serialization;

namespace LarkCardKit.Models.Elements;

public class ImgCombination : Element
{
    public override string Tag => "img_combination";


    [JsonPropertyName("combination_mode")]
    public string? CombinationMode { get; set; }

    [JsonPropertyName("combination_transparent")]
    public bool? CombinationTransparent { get; set; }

    [JsonPropertyName("corner_radius")]
    public string? CornerRadius { get; set; }

    [JsonPropertyName("img_list")]
    public List<ImgCombinationItem>? ImgList { get; set; }

}

public class ImgCombinationItem
{
    [JsonPropertyName("img_key")]
    public string ImgKey { get; set; } = string.Empty;
    
    [JsonPropertyName("transparent")]
    public bool? Transparent { get; set; }
}
