using System.Text.Json.Serialization;

namespace LarkCardKit.Models.Elements;

public class Person : Element
{
    public override string Tag => "person";
    
    [JsonPropertyName("user_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? UserId { get; set; }
    
    [JsonPropertyName("open_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? OpenId { get; set; }
    
    [JsonPropertyName("union_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? UnionId { get; set; }
    
    [JsonPropertyName("style")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Style { get; set; }
    
    [JsonPropertyName("show_name")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? ShowName { get; set; }
    
    [JsonPropertyName("show_avatar")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? ShowAvatar { get; set; }
}
