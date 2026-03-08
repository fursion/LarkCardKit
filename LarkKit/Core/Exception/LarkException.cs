using LarkKit.Models;

namespace LarkKit.Core.Exception;

/// <summary>
/// 飞书 SDK 自定义异常
/// </summary>
public class LarkException : System.Exception
{
    /// <summary>
    /// 错误码
    /// </summary>
    public LarkErrorCode ErrorCode { get; }

    /// <summary>
    /// 错误描述
    /// </summary>
    public string ErrorMessage { get; }

    /// <summary>
    /// 排查建议
    /// </summary>
    public string? Suggestion { get; }

    /// <summary>
    /// HTTP 状态码
    /// </summary>
    public int? HttpStatusCode { get; }

    /// <summary>
    /// 初始化 LarkException 的新实例
    /// </summary>
    /// <param name="errorCode">错误码</param>
    /// <param name="errorMessage">错误描述</param>
    /// <param name="suggestion">排查建议</param>
    /// <param name="httpStatusCode">HTTP 状态码</param>
    public LarkException(
        LarkErrorCode errorCode,
        string errorMessage,
        string? suggestion = null,
        int? httpStatusCode = null)
        : base(errorMessage)
    {
        ErrorCode = errorCode;
        ErrorMessage = errorMessage;
        Suggestion = suggestion;
        HttpStatusCode = httpStatusCode;
    }

    /// <summary>
    /// 初始化 LarkException 的新实例
    /// </summary>
    /// <param name="errorCode">错误码</param>
    /// <param name="errorMessage">错误描述</param>
    /// <param name="innerException">内层异常</param>
    public LarkException(LarkErrorCode errorCode, string errorMessage, System.Exception innerException)
        : base(errorMessage, innerException)
    {
        ErrorCode = errorCode;
        ErrorMessage = errorMessage;
    }

    /// <summary>
    /// 获取异常信息
    /// </summary>
    public override string ToString()
    {
        var message = $"LarkException: [{ErrorCode}] {ErrorMessage}";
        if (!string.IsNullOrEmpty(Suggestion))
        {
            message += $"\nSuggestion: {Suggestion}";
        }
        if (HttpStatusCode.HasValue)
        {
            message += $"\nHTTP Status: {HttpStatusCode.Value}";
        }
        return message;
    }
}
