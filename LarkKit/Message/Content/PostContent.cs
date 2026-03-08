using System.Text.Json.Serialization;

namespace LarkKit.Message.Content;

/// <summary>
/// 富文本消息内容
/// </summary>
public class PostContent
{
    /// <summary>
    /// 消息标题
    /// </summary>
    [JsonPropertyName("title")]
    public string? Title { get; set; }

    /// <summary>
    /// 多语言内容
    /// </summary>
    [JsonPropertyName("content")]
    public Dictionary<string, List<PostContentItem>> Content { get; set; } = new();

    /// <summary>
    /// 初始化 PostContent 的新实例
    /// </summary>
    public PostContent() { }

    /// <summary>
    /// 初始化 PostContent 的新实例
    /// </summary>
    /// <param name="title">标题</param>
    /// <param name="content">多语言内容</param>
    public PostContent(string? title, Dictionary<string, List<PostContentItem>> content)
    {
        Title = title;
        Content = content;
    }
}

/// <summary>
/// 富文本消息内容项
/// </summary>
public class PostContentItem
{
    /// <summary>
    /// 文本标签
    /// </summary>
    [JsonPropertyName("tag")]
    public string Tag { get; set; } = "text";

    /// <summary>
    /// 文本内容
    /// </summary>
    [JsonPropertyName("text")]
    public string? Text { get; set; }

    /// <summary>
    /// 链接地址（当 tag 为 a 时）
    /// </summary>
    [JsonPropertyName("href")]
    public string? Href { get; set; }

    /// <summary>
    /// 用户 ID（当 tag 为 at 时）
    /// </summary>
    [JsonPropertyName("user_id")]
    public string? UserId { get; set; }

    /// <summary>
    /// 初始化 PostContentItem 的新实例
    /// </summary>
    public PostContentItem() { }

    /// <summary>
    /// 创建文本内容项
    /// </summary>
    /// <param name="text">文本内容</param>
    /// <returns>PostContentItem 实例</returns>
    public static PostContentItem CreateText(string text) => new() { Tag = "text", Text = text };

    /// <summary>
    /// 创建链接内容项
    /// </summary>
    /// <param name="text">链接文本</param>
    /// <param name="href">链接地址</param>
    /// <returns>PostContentItem 实例</returns>
    public static PostContentItem Link(string text, string href) => new() { Tag = "a", Text = text, Href = href };

    /// <summary>
    /// 创建 @ 用户内容项
    /// </summary>
    /// <param name="userId">用户 ID</param>
    /// <returns>PostContentItem 实例</returns>
    public static PostContentItem AtUser(string userId) => new() { Tag = "at", UserId = userId };

    /// <summary>
    /// 创建换行内容项
    /// </summary>
    /// <returns>PostContentItem 实例</returns>
    public static PostContentItem NewLine() => new() { Tag = "text", Text = "\n" };
}
