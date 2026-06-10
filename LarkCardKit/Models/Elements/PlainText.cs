using System.Text.Json.Serialization;

namespace LarkCardKit.Models.Elements;

public class PlainText : Element
{
    public override string Tag => "plain_text";
    
    [JsonPropertyName("content")]
    public string Content { get; set; } = string.Empty;
    
    [JsonPropertyName("text_size")]
    public string? TextSize { get; set; }
    
    [JsonPropertyName("text_color")]
    public string? TextColor { get; set; }
    
    [JsonPropertyName("text_align")]
    public string? TextAlign { get; set; }
    
    [JsonPropertyName("notation")]
    public bool? Notation { get; set; }
    
    [JsonPropertyName("width")]
    public string? Width { get; set; }
}
