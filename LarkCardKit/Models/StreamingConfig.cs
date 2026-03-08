using System.Text.Json.Serialization;

namespace LarkCardKit.Models;

public class StreamingConfig
{
    [JsonPropertyName("print_frequency_ms")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public object? PrintFrequencyMs { get; set; }

    [JsonPropertyName("print_step")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public object? PrintStep { get; set; }

    [JsonPropertyName("print_strategy")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? PrintStrategy { get; set; }
}
