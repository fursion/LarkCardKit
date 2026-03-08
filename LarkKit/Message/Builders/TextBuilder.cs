using System.Text;

namespace LarkKit.Message.Builders;

/// <summary>
/// 文本消息构建器
/// 支持构建带 @ 用户的文本消息
/// </summary>
public class TextBuilder
{
    private readonly StringBuilder _textBuilder;
    private readonly List<string> _atUserIds;

    /// <summary>
    /// 初始化 TextBuilder 的新实例
    /// </summary>
    public TextBuilder()
    {
        _textBuilder = new StringBuilder();
        _atUserIds = new List<string>();
    }

    /// <summary>
    /// 添加文本
    /// </summary>
    /// <param name="text">文本内容</param>
    /// <returns>TextBuilder 实例</returns>
    public TextBuilder Text(string text)
    {
        _textBuilder.Append(text);
        return this;
    }

    /// <summary>
    /// @ 用户
    /// </summary>
    /// <param name="userId">用户 ID</param>
    /// <param name="userName">用户名称（可选）</param>
    /// <returns>TextBuilder 实例</returns>
    public TextBuilder AtUser(string userId, string? userName = null)
    {
        _atUserIds.Add(userId);
        var name = userName ?? userId;
        _textBuilder.Append($"<at user_id=\"{userId}\">{name}</at>");
        return this;
    }

    /// <summary>
    /// @ 所有人
    /// </summary>
    /// <returns>TextBuilder 实例</returns>
    public TextBuilder AtAll()
    {
        _textBuilder.Append("<at user_id=\"all\">所有人</at>");
        return this;
    }

    /// <summary>
    /// 添加换行
    /// </summary>
    /// <returns>TextBuilder 实例</returns>
    public TextBuilder NewLine()
    {
        _textBuilder.AppendLine();
        return this;
    }

    /// <summary>
    /// 构建文本消息内容
    /// </summary>
    /// <returns>TextContent 实例</returns>
    public Content.TextContent Build()
    {
        return new Content.TextContent(_textBuilder.ToString());
    }

    /// <summary>
    /// 获取构建的文本内容
    /// </summary>
    /// <returns>文本字符串</returns>
    public override string ToString()
    {
        return _textBuilder.ToString();
    }
}
