using LarkKit.Core.Auth;
using LarkKit.Core.Config;

namespace LarkKit.Domains.Contact;

/// <summary>
/// 通讯录领域实现
/// </summary>
public class ContactDomain : IContactDomain
{
    private readonly HttpClient _httpClient;
    private readonly ITokenProvider _tokenProvider;
    private readonly LarkOptions _options;

    private readonly Lazy<IUserResource> _user;
    private readonly Lazy<IDepartmentResource> _department;

    /// <summary>
    /// 用户资源
    /// </summary>
    public IUserResource User => _user.Value;

    /// <summary>
    /// 部门资源
    /// </summary>
    public IDepartmentResource Department => _department.Value;

    /// <summary>
    /// 初始化 ContactDomain 的新实例
    /// </summary>
    public ContactDomain(HttpClient httpClient, ITokenProvider tokenProvider, LarkOptions options)
    {
        _httpClient = httpClient;
        _tokenProvider = tokenProvider;
        _options = options;

        _user = new Lazy<IUserResource>(() => new UserResource(httpClient, tokenProvider, options));
        _department = new Lazy<IDepartmentResource>(() => new DepartmentResource(httpClient, tokenProvider, options));
    }
}

/// <summary>
/// 用户资源实现（预留）
/// </summary>
internal class UserResource : IUserResource
{
    public UserResource(HttpClient httpClient, ITokenProvider tokenProvider, LarkOptions options) { }

    public Task<object> GetAsync(string userId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException("Contact.User.GetAsync 尚未实现");
    }
}

/// <summary>
/// 部门资源实现（预留）
/// </summary>
internal class DepartmentResource : IDepartmentResource
{
    public DepartmentResource(HttpClient httpClient, ITokenProvider tokenProvider, LarkOptions options) { }

    public Task<object> ListAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException("Contact.Department.ListAsync 尚未实现");
    }
}
