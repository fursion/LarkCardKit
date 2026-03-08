namespace LarkKit.Domains.Drive;

/// <summary>
/// 云文档领域接口
/// </summary>
public interface IDriveDomain
{
    /// <summary>
    /// 文件资源
    /// </summary>
    IFileResource File { get; }

    /// <summary>
    /// 文件夹资源
    /// </summary>
    IFolderResource Folder { get; }
}

/// <summary>
/// 文件资源接口
/// </summary>
public interface IFileResource
{
    /// <summary>
    /// 上传文件
    /// </summary>
    Task<object> UploadAsync(object request, CancellationToken cancellationToken = default);
}

/// <summary>
/// 文件夹资源接口
/// </summary>
public interface IFolderResource
{
    /// <summary>
    /// 创建文件夹
    /// </summary>
    Task<object> CreateAsync(object request, CancellationToken cancellationToken = default);
}
