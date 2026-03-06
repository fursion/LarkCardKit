using System.Text.Json.Serialization;

namespace LarkCardKit.Models.Elements;

/// <summary>
/// 纯文本元素
/// </summary>
public class PlainText : Element
{
    /// <inheritdoc/>
    public override string Tag => "plain_text";
    
    /// <summary>
    /// 文本内容，最多支持 100 个字符
    /// </summary>
    [JsonPropertyName("content")]
    public string Content { get; set; } = string.Empty;
    
    /// <summary>
    /// 是否使用 notation 样式（灰色小字）
    /// </summary>
    [JsonPropertyName("notation")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? Notation { get; set; }
    
    /// <summary>
    /// 文本宽度
    /// </summary>
    [JsonPropertyName("width")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Width { get; set; }
}
