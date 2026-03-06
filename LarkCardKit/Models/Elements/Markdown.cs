using System.Text.Json.Serialization;

namespace LarkCardKit.Models.Elements;

/// <summary>
/// Markdown 文本元素
/// </summary>
public class Markdown : Element
{
    /// <inheritdoc/>
    public override string Tag => "markdown";
    
    /// <summary>
    /// Markdown 内容，支持标准 Markdown 语法和部分 HTML 标签
    /// </summary>
    [JsonPropertyName("content")]
    public string Content { get; set; } = string.Empty;
}
