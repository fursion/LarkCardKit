namespace LarkKit.Core.Auth;

/// <summary>
/// 租户访问令牌模型
/// </summary>
public class TenantAccessToken
{
    /// <summary>
    /// 访问令牌
    /// </summary>
    public string Token { get; set; } = string.Empty;

    /// <summary>
    /// 过期时间（UTC）
    /// </summary>
    public DateTime ExpiresAt { get; set; }

    /// <summary>
    /// 令牌是否已过期
    /// </summary>
    public bool IsExpired => DateTime.UtcNow >= ExpiresAt;

    /// <summary>
    /// 令牌是否即将过期
    /// </summary>
    /// <param name="aheadMinutes">提前多少分钟</param>
    /// <returns>是否即将过期</returns>
    public bool IsExpiringSoon(int aheadMinutes = 5)
    {
        return DateTime.UtcNow >= ExpiresAt.AddMinutes(-aheadMinutes);
    }
}
