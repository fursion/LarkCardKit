using System.Text.Json.Serialization;

namespace LarkCardKit.Models.Elements;

public class Markdown : Element
{
    public override string Tag => "markdown";


    [JsonPropertyName("content")]
    public string Content { get; set; } = string.Empty;

    [JsonPropertyName("text_size")]
    public string? TextSize { get; set; }

    [JsonPropertyName("text_color")]
    public string? TextColor { get; set; }

    [JsonPropertyName("text_align")]
    public string? TextAlign { get; set; }

    [JsonPropertyName("icon")]
    public MarkdownIcon? Icon { get; set; }

}

public class MarkdownIcon
{
    [JsonPropertyName("tag")]
    public string Tag { get; set; } = "standard_icon";
    
    [JsonPropertyName("token")]
    public string? Token { get; set; }
    
    [JsonPropertyName("color")]
    public string? Color { get; set; }
    
    [JsonPropertyName("img_key")]
    public string? ImgKey { get; set; }
}
