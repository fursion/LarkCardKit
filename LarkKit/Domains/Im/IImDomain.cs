namespace LarkKit.Domains.Im;

/// <summary>
/// IM 领域接口
/// </summary>
public interface IImDomain
{
    /// <summary>
    /// 消息资源
    /// </summary>
    IMessageResource Message { get; }
}
