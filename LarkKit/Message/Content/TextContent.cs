using System.Text.Json.Serialization;

namespace LarkKit.Message.Content;

/// <summary>
/// 文本消息内容
/// </summary>
public class TextContent
{
    /// <summary>
    /// 文本内容（支持 @ 语法：<at user_id="xxx">User Name</at>）
    /// </summary>
    [JsonPropertyName("text")]
    public string Text { get; set; } = string.Empty;

    /// <summary>
    /// 初始化 TextContent 的新实例
    /// </summary>
    public TextContent() { }

    /// <summary>
    /// 初始化 TextContent 的新实例
    /// </summary>
    /// <param name="text">文本内容</param>
    public TextContent(string text)
    {
        Text = text;
    }
}
