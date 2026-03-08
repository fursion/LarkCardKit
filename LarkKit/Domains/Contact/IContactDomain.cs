namespace LarkKit.Domains.Contact;

/// <summary>
/// 通讯录领域接口
/// </summary>
public interface IContactDomain
{
    /// <summary>
    /// 用户资源
    /// </summary>
    IUserResource User { get; }

    /// <summary>
    /// 部门资源
    /// </summary>
    IDepartmentResource Department { get; }
}

/// <summary>
/// 用户资源接口
/// </summary>
public interface IUserResource
{
    /// <summary>
    /// 获取用户信息
    /// </summary>
    Task<object> GetAsync(string userId, CancellationToken cancellationToken = default);
}

/// <summary>
/// 部门资源接口
/// </summary>
public interface IDepartmentResource
{
    /// <summary>
    /// 获取部门列表
    /// </summary>
    Task<object> ListAsync(CancellationToken cancellationToken = default);
}
