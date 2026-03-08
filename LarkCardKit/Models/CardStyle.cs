using System.Text.Json.Serialization;

namespace LarkCardKit.Models;

public class CardStyle
{
    [JsonPropertyName("text_size")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public object? TextSize { get; set; }

    [JsonPropertyName("color")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public object? Color { get; set; }
}
