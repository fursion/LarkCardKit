namespace LarkKit.Models;

/// <summary>
/// 消息类型枚举
/// </summary>
public enum MessageType
{
    /// <summary>
    /// 文本消息
    /// </summary>
    Text,

    /// <summary>
    /// 图片消息
    /// </summary>
    Image,

    /// <summary>
    /// 富文本消息
    /// </summary>
    Post,

    /// <summary>
    /// 卡片消息（交互式）
    /// </summary>
    Interactive,

    /// <summary>
    /// 分享群名片
    /// </summary>
    ShareChat
}
