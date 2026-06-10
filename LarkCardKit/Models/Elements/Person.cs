using System.Text.Json.Serialization;

namespace LarkCardKit.Models.Elements;

public class Person : Element
{
    public override string Tag => "person";


    [JsonPropertyName("user_id")]
    public string? UserId { get; set; }

    [JsonPropertyName("open_id")]
    public string? OpenId { get; set; }

    [JsonPropertyName("union_id")]
    public string? UnionId { get; set; }

    [JsonPropertyName("style")]
    public string? Style { get; set; }

    [JsonPropertyName("show_name")]
    public bool? ShowName { get; set; }

    [JsonPropertyName("show_avatar")]
    public bool? ShowAvatar { get; set; }

}
