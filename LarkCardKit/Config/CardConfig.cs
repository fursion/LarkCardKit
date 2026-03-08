using System.Text.Json.Serialization;
using LarkCardKit.Models;

namespace LarkCardKit.Config;

public class CardConfig
{
    [JsonPropertyName("update_multi")]
    public bool UpdateMulti { get; set; } = true;

    [JsonPropertyName("streaming_mode")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? StreamingMode { get; set; }

    [JsonPropertyName("streaming_config")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public StreamingConfig? StreamingConfig { get; set; }

    [JsonPropertyName("summary")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public StreamingSummary? Summary { get; set; }

    [JsonPropertyName("locales")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string[]? Locales { get; set; }

    [JsonPropertyName("enable_forward")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? EnableForward { get; set; }

    [JsonPropertyName("width_mode")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? WidthMode { get; set; }

    [JsonPropertyName("use_custom_translation")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? UseCustomTranslation { get; set; }

    [JsonPropertyName("enable_forward_interaction")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? EnableForwardInteraction { get; set; }

    [JsonPropertyName("style")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public CardStyle? Style { get; set; }
}

public class StreamingSummary
{
    [JsonPropertyName("content")]
    public string Content { get; set; } = "生成中";

    [JsonPropertyName("i18n_content")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Dictionary<string, string>? I18nContent { get; set; }
}
