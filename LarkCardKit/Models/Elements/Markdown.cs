using System.Text.Json.Serialization;

namespace LarkCardKit.Models.Elements;

public class Markdown : Element
{
    public override string Tag => "markdown";

    [JsonPropertyName("element_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public new string? ElementId { get; set; }

    [JsonPropertyName("content")]
    public string Content { get; set; } = string.Empty;

    [JsonPropertyName("text_size")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? TextSize { get; set; }

    [JsonPropertyName("text_color")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? TextColor { get; set; }

    [JsonPropertyName("text_align")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? TextAlign { get; set; }

    [JsonPropertyName("icon")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public MarkdownIcon? Icon { get; set; }

    [JsonPropertyName("margin")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public new string? Margin { get; set; }
}

public class MarkdownIcon
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
