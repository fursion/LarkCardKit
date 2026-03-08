using System.Text.Json.Serialization;

namespace LarkCardKit.Models;

public class CardLink
{
    [JsonPropertyName("url")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Url { get; set; }
    
    [JsonPropertyName("pc_url")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? PcUrl { get; set; }
    
    [JsonPropertyName("ios_url")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? IosUrl { get; set; }
    
    [JsonPropertyName("android_url")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? AndroidUrl { get; set; }
}
