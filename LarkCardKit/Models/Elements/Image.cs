using System.Text.Json.Serialization;
using LarkCardKit.Enums;

namespace LarkCardKit.Models.Elements;

/// <summary>
/// 图片组件
/// </summary>
public class Image : Element
{
    /// <inheritdoc/>
    public override string Tag => "img";
    
    /// <summary>
    /// 图片的 img_key
    /// </summary>
    [JsonPropertyName("img_key")]
    public string ImgKey { get; set; } = string.Empty;
    
    /// <summary>
    /// 图片尺寸
    /// </summary>
    [JsonPropertyName("size")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Size { get; set; }
    
    /// <summary>
    /// 图片宽度
    /// </summary>
    [JsonPropertyName("width")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Width { get; set; }
    
    /// <summary>
    /// 替代文本
    /// </summary>
    [JsonPropertyName("alt")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public PlainText? Alt { get; set; }
}
