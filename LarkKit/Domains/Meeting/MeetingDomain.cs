using LarkKit.Core.Auth;
using LarkKit.Core.Config;

namespace LarkKit.Domains.Meeting;

/// <summary>
/// 会议领域实现（预留）
/// </summary>
public class MeetingDomain : IMeetingDomain
{
    private readonly Lazy<IRoomResource> _room;
    private readonly Lazy<IMeetingResource> _meeting;

    /// <summary>
    /// 会议室资源
    /// </summary>
    public IRoomResource Room => _room.Value;

    /// <summary>
    /// 会议资源
    /// </summary>
    public IMeetingResource Meeting => _meeting.Value;

    /// <summary>
    /// 初始化 MeetingDomain 的新实例
    /// </summary>
    public MeetingDomain(HttpClient httpClient, ITokenProvider tokenProvider, LarkOptions options)
    {
        _room = new Lazy<IRoomResource>(() => new RoomResource());
        _meeting = new Lazy<IMeetingResource>(() => new MeetingResource());
    }
}

/// <summary>
/// 会议室资源实现（预留）
/// </summary>
internal class RoomResource : IRoomResource
{
    public Task<object> ListAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException("Meeting.Room.ListAsync 尚未实现");
    }
}

/// <summary>
/// 会议资源实现（预留）
/// </summary>
internal class MeetingResource : IMeetingResource
{
    public Task<object> CreateAsync(object request, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException("Meeting.Meeting.CreateAsync 尚未实现");
    }
}
