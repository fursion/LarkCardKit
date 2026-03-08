namespace LarkKit.Core.Config;

/// <summary>
/// 飞书 SDK 配置选项
/// </summary>
public class LarkOptions
{
    /// <summary>
    /// 应用 App ID
    /// </summary>
    public string AppId { get; set; } = string.Empty;

    /// <summary>
    /// 应用 App Secret
    /// </summary>
    /// <remarks>
    /// 安全提示：不应硬编码此值，建议使用环境变量或密钥管理服务
    /// </remarks>
    public string AppSecret { get; set; } = string.Empty;

    /// <summary>
    /// HTTP 请求超时时间（秒），默认 30 秒
    /// </summary>
    public int Timeout { get; set; } = 30;

    /// <summary>
    /// 重试次数，默认 3 次
    /// </summary>
    public int MaxRetries { get; set; } = 3;

    /// <summary>
    /// 重试间隔（毫秒），默认 1000 毫秒
    /// </summary>
    public int RetryDelayMs { get; set; } = 1000;

    /// <summary>
    /// 飞书 API 基础地址
    /// </summary>
    public string BaseUrl { get; set; } = "https://open.feishu.cn";

    /// <summary>
    /// Token 刷新提前时间（分钟），默认 5 分钟
    /// </summary>
    public int TokenRefreshAheadMinutes { get; set; } = 5;
}
