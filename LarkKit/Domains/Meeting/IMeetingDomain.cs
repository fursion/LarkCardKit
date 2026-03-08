namespace LarkKit.Domains.Meeting;

/// <summary>
/// 会议领域接口
/// </summary>
public interface IMeetingDomain
{
    /// <summary>
    /// 会议室资源
    /// </summary>
    IRoomResource Room { get; }

    /// <summary>
    /// 会议资源
    /// </summary>
    IMeetingResource Meeting { get; }
}

/// <summary>
/// 会议室资源接口
/// </summary>
public interface IRoomResource
{
    /// <summary>
    /// 获取会议室列表
    /// </summary>
    Task<object> ListAsync(CancellationToken cancellationToken = default);
}

/// <summary>
/// 会议资源接口
/// </summary>
public interface IMeetingResource
{
    /// <summary>
    /// 创建会议
    /// </summary>
    Task<object> CreateAsync(object request, CancellationToken cancellationToken = default);
}
