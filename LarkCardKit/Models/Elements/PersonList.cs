using System.Text.Json.Serialization;

namespace LarkCardKit.Models.Elements;

public class PersonList : Element
{
    public override string Tag => "person_list";
    
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
}
