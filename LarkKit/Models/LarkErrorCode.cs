namespace LarkKit.Models;

/// <summary>
/// 飞书 API 错误码枚举
/// </summary>
public enum LarkErrorCode
{
    /// <summary>
    /// 成功
    /// </summary>
    Ok = 0,

    /// <summary>
    /// 错误请求
    /// </summary>
    BadRequest = 400,

    /// <summary>
    /// 凭证无效
    /// </summary>
    Unauthorized = 401,

    /// <summary>
    /// 权限不足
    /// </summary>
    Forbidden = 403,

    /// <summary>
    /// 资源不存在
    /// </summary>
    NotFound = 404,

    /// <summary>
    /// 内部服务器错误
    /// </summary>
    InternalServerError = 500,

    /// <summary>
    /// 服务不可用
    /// </summary>
    ServiceUnavailable = 503,

    /// <summary>
    /// Token 无效或过期
    /// </summary>
    InvalidToken = 99991663,

    /// <summary>
    /// 参数错误
    /// </summary>
    InvalidParams = 99991664,

    /// <summary>
    /// 请求限流
    /// </summary>
    RateLimited = 99991665,

    /// <summary>
    /// 消息发送失败
    /// </summary>
    MessageSendFailed = 99991666,

    /// <summary>
    /// 用户不存在
    /// </summary>
    UserNotFound = 99991667,

    /// <summary>
    /// 部门不存在
    /// </summary>
    DepartmentNotFound = 99991668,

    /// <summary>
    /// 群聊不存在
    /// </summary>
    ChatNotFound = 99991669,

    /// <summary>
    /// 消息不存在
    /// </summary>
    MessageNotFound = 99991670,

    /// <summary>
    /// 图片不存在
    /// </summary>
    ImageNotFound = 99991671,

    /// <summary>
    /// 签名验证失败
    /// </summary>
    SignatureVerificationFailed = 99991672
}
