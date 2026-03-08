namespace LarkKit.Models;

/// <summary>
/// 接收者 ID 类型枚举
/// </summary>
public enum ReceiveIdType
{
    /// <summary>
    /// 用户的 open_id
    /// </summary>
    OpenId,

    /// <summary>
    /// 用户的 user_id
    /// </summary>
    UserId,

    /// <summary>
    /// 用户的 union_id
    /// </summary>
    UnionId,

    /// <summary>
    /// 群聊的 chat_id
    /// </summary>
    ChatId
}
