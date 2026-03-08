using System.Text.Json.Serialization;
using LarkCardKit.Enums;

namespace LarkCardKit.Models.Elements;

public class Image : Element
{
    public override string Tag => "img";
    
    [JsonPropertyName("img_key")]
    public string ImgKey { get; set; } = string.Empty;
    
    [JsonPropertyName("title")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public PlainText? Title { get; set; }
    
    [JsonPropertyName("scale_type")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? ScaleType { get; set; }
    
    [JsonPropertyName("size")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Size { get; set; }
    
    [JsonPropertyName("corner_radius")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? CornerRadius { get; set; }
    
    [JsonPropertyName("transparent")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? Transparent { get; set; }
    
    [JsonPropertyName("preview")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? Preview { get; set; }
    
    [JsonPropertyName("alt")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public PlainText? Alt { get; set; }
    
    [JsonIgnore]
    public string? Width { get; set; }
}
