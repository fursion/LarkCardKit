using System.Text.Json.Serialization;

namespace LarkKit.Message.Content;

/// <summary>
/// 分享群名片消息内容
/// </summary>
public class ShareChatContent
{
    /// <summary>
    /// 要分享的群聊 ID
    /// </summary>
    [JsonPropertyName("share_chat_id")]
    public string ShareChatId { get; set; } = string.Empty;

    /// <summary>
    /// 初始化 ShareChatContent 的新实例
    /// </summary>
    public ShareChatContent() { }

    /// <summary>
    /// 初始化 ShareChatContent 的新实例
    /// </summary>
    /// <param name="shareChatId">要分享的群聊 ID</param>
    public ShareChatContent(string shareChatId)
    {
        ShareChatId = shareChatId;
    }
}
