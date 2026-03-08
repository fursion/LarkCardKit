using System.Text.Json.Serialization;

namespace LarkCardKit.Models;

public class HeaderIcon
{
    [JsonPropertyName("tag")]
    public string Tag { get; set; } = "standard_icon";
    
    [JsonPropertyName("token")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Token { get; set; }
    
    [JsonPropertyName("color")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Color { get; set; }
    
    [JsonPropertyName("img_key")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? ImgKey { get; set; }
}
