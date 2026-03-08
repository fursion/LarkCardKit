using LarkKit.Core.Auth;
using LarkKit.Core.Config;

namespace LarkKit.Domains.Drive;

/// <summary>
/// 云文档领域实现（预留）
/// </summary>
public class DriveDomain : IDriveDomain
{
    private readonly Lazy<IFileResource> _file;
    private readonly Lazy<IFolderResource> _folder;

    /// <summary>
    /// 文件资源
    /// </summary>
    public IFileResource File => _file.Value;

    /// <summary>
    /// 文件夹资源
    /// </summary>
    public IFolderResource Folder => _folder.Value;

    /// <summary>
    /// 初始化 DriveDomain 的新实例
    /// </summary>
    public DriveDomain(HttpClient httpClient, ITokenProvider tokenProvider, LarkOptions options)
    {
        _file = new Lazy<IFileResource>(() => new FileResource());
        _folder = new Lazy<IFolderResource>(() => new FolderResource());
    }
}

/// <summary>
/// 文件资源实现（预留）
/// </summary>
internal class FileResource : IFileResource
{
    public Task<object> UploadAsync(object request, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException("Drive.File.UploadAsync 尚未实现");
    }
}

/// <summary>
/// 文件夹资源实现（预留）
/// </summary>
internal class FolderResource : IFolderResource
{
    public Task<object> CreateAsync(object request, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException("Drive.Folder.CreateAsync 尚未实现");
    }
}
