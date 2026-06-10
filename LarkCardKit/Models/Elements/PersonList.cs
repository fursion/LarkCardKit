using System.Text.Json.Serialization;

namespace LarkCardKit.Models.Elements;

public class PersonList : Element
{
    public override string Tag => "person_list";

    [JsonPropertyName("element_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public new string? ElementId { get; set; }

    [JsonPropertyName("user_ids")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<string>? UserIds { get; set; }

    [JsonPropertyName("open_ids")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<string>? OpenIds { get; set; }

    [JsonPropertyName("union_ids")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<string>? UnionIds { get; set; }

    [JsonPropertyName("style")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Style { get; set; }

    [JsonPropertyName("margin")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public new string? Margin { get; set; }
}
