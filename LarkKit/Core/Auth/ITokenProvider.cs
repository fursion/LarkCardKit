namespace LarkKit.Core.Auth;

/// <summary>
/// Token 提供者接口
/// </summary>
public interface ITokenProvider
{
    /// <summary>
    /// 获取访问令牌
    /// </summary>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>访问令牌</returns>
    Task<TenantAccessToken> GetTokenAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// 刷新访问令牌
    /// </summary>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>访问令牌</returns>
    Task<TenantAccessToken> RefreshTokenAsync(CancellationToken cancellationToken = default);
}
