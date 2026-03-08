using LarkKit.Core.Auth;
using LarkKit.Core.Config;
using LarkKit.Domains.Contact;
using LarkKit.Domains.Drive;
using LarkKit.Domains.Im;
using LarkKit.Domains.Meeting;
using Microsoft.Extensions.Logging;

namespace LarkKit;

/// <summary>
/// 飞书客户端接口
/// </summary>
public interface ILarkClient : IDisposable
{
    /// <summary>
    /// IM 领域（消息、群聊等）
    /// </summary>
    IImDomain Im { get; }

    /// <summary>
    /// 通讯录领域（用户、部门等）
    /// </summary>
    IContactDomain Contact { get; }

    /// <summary>
    /// 云文档领域（文件、文件夹等）
    /// </summary>
    IDriveDomain Drive { get; }

    /// <summary>
    /// 会议领域（会议室、会议等）
    /// </summary>
    IMeetingDomain Meeting { get; }

    /// <summary>
    /// 初始化客户端
    /// </summary>
    Task InitializeAsync(CancellationToken cancellationToken = default);
}
