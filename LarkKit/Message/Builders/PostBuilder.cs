using LarkKit.Message.Content;

namespace LarkKit.Message.Builders;

/// <summary>
/// 富文本消息构建器
/// 支持多语言、链接、@用户等功能
/// </summary>
public class PostBuilder
{
    private string? _title;
    private readonly Dictionary<string, List<PostContentItem>> _content;

    /// <summary>
    /// 初始化 PostBuilder 的新实例
    /// </summary>
    public PostBuilder()
    {
        _content = new Dictionary<string, List<PostContentItem>>();
    }

    /// <summary>
    /// 设置标题
    /// </summary>
    /// <param name="title">标题</param>
    /// <returns>PostBuilder 实例</returns>
    public PostBuilder Title(string title)
    {
        _title = title;
        return this;
    }

    /// <summary>
    /// 添加中文内容
    /// </summary>
    /// <param name="items">内容项</param>
    /// <returns>PostBuilder 实例</returns>
    public PostBuilder AddChinese(params PostContentItem[] items)
    {
        return AddContent("zh_cn", items);
    }

    /// <summary>
    /// 添加英文内容
    /// </summary>
    /// <param name="items">内容项</param>
    /// <returns>PostBuilder 实例</returns>
    public PostBuilder AddEnglish(params PostContentItem[] items)
    {
        return AddContent("en_us", items);
    }

    /// <summary>
    /// 添加日语内容
    /// </summary>
    /// <param name="items">内容项</param>
    /// <returns>PostBuilder 实例</returns>
    public PostBuilder AddJapanese(params PostContentItem[] items)
    {
        return AddContent("ja_jp", items);
    }

    /// <summary>
    /// 添加内容
    /// </summary>
    /// <param name="language">语言代码</param>
    /// <param name="items">内容项</param>
    /// <returns>PostBuilder 实例</returns>
    public PostBuilder AddContent(string language, params PostContentItem[] items)
    {
        if (!_content.ContainsKey(language))
        {
            _content[language] = new List<PostContentItem>();
        }
        _content[language].AddRange(items);
        return this;
    }

    /// <summary>
    /// 构建富文本消息内容
    /// </summary>
    /// <returns>PostContent 实例</returns>
    public Content.PostContent Build()
    {
        return new Content.PostContent(_title, _content);
    }
}

/// <summary>
/// 富文本内容项构建器
/// </summary>
public class PostContentItemBuilder
{
    /// <summary>
    /// 创建文本内容项
    /// </summary>
    /// <param name="text">文本内容</param>
    /// <returns>PostContentItem 实例</returns>
    public static PostContentItem Text(string text)
    {
        return PostContentItem.CreateText(text);
    }

    /// <summary>
    /// 创建链接内容项
    /// </summary>
    /// <param name="text">链接文本</param>
    /// <param name="href">链接地址</param>
    /// <returns>PostContentItem 实例</returns>
    public static PostContentItem Link(string text, string href)
    {
        return PostContentItem.Link(text, href);
    }

    /// <summary>
    /// 创建 @ 用户内容项
    /// </summary>
    /// <param name="userId">用户 ID</param>
    /// <returns>PostContentItem 实例</returns>
    public static PostContentItem AtUser(string userId)
    {
        return PostContentItem.AtUser(userId);
    }

    /// <summary>
    /// 创建 @ 所有人内容项
    /// </summary>
    /// <returns>PostContentItem 实例</returns>
    public static PostContentItem AtAll()
    {
        return new PostContentItem { Tag = "at", UserId = "all" };
    }
}
