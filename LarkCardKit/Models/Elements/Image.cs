using System.Text.Json.Serialization;
using LarkCardKit.Enums;

namespace LarkCardKit.Models.Elements;

public class Image : Element
{
    public override string Tag => "img";


    [JsonPropertyName("img_key")]
    public string ImgKey { get; set; } = string.Empty;

    [JsonPropertyName("title")]
    public PlainText? Title { get; set; }

    [JsonPropertyName("scale_type")]
    public string? ScaleType { get; set; }

    [JsonPropertyName("size")]
    public string? Size { get; set; }

    [JsonPropertyName("corner_radius")]
    public string? CornerRadius { get; set; }

    [JsonPropertyName("transparent")]
    public bool? Transparent { get; set; }

    [JsonPropertyName("preview")]
    public bool? Preview { get; set; }

    [JsonPropertyName("alt")]
    public PlainText? Alt { get; set; }


    [JsonIgnore]
    public string? Width { get; set; }
}
