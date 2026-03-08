using LarkKit.Core.Auth;
using LarkKit.Core.Config;
using LarkKit.Domains.Contact;
using LarkKit.Domains.Drive;
using LarkKit.Domains.Im;
using LarkKit.Domains.Meeting;
using Microsoft.Extensions.Logging;

namespace LarkKit;

/// <summary>
/// 飞书客户端实现
/// 采用延迟加载，按需创建领域实例
/// </summary>
public class LarkClient : ILarkClient
{
    private readonly LarkOptions _options;
    private readonly HttpClient _httpClient;
    private readonly ITokenProvider _tokenProvider;
    private readonly ILogger<LarkClient>? _logger;
    private readonly ILoggerFactory? _loggerFactory;
    private bool _disposed;

    // 延迟加载领域实例
    private readonly Lazy<IImDomain> _im;
    private readonly Lazy<IContactDomain> _contact;
    private readonly Lazy<IDriveDomain> _drive;
    private readonly Lazy<IMeetingDomain> _meeting;

    /// <summary>
    /// IM 领域（消息、群聊等）
    /// </summary>
    public IImDomain Im => _im.Value;

    /// <summary>
    /// 通讯录领域（用户、部门等）
    /// </summary>
    public IContactDomain Contact => _contact.Value;

    /// <summary>
    /// 云文档领域（文件、文件夹等）
    /// </summary>
    public IDriveDomain Drive => _drive.Value;

    /// <summary>
    /// 会议领域（会议室、会议等）
    /// </summary>
    public IMeetingDomain Meeting => _meeting.Value;

    /// <summary>
    /// 初始化 LarkClient 的新实例
    /// </summary>
    public LarkClient(LarkOptions options, ILogger<LarkClient>? logger = null, ILoggerFactory? loggerFactory = null)
    {
        _options = options;
        _logger = logger;
        _loggerFactory = loggerFactory;

        // 初始化核心资源
        _httpClient = new HttpClient
        {
            BaseAddress = new Uri(options.BaseUrl),
            Timeout = TimeSpan.FromSeconds(options.Timeout)
        };

        var tokenProviderLogger = loggerFactory?.CreateLogger<TenantTokenProvider>();
        _tokenProvider = new TenantTokenProvider(options, _httpClient, tokenProviderLogger);

        // 延迟加载领域
        _im = new Lazy<IImDomain>(() => new ImDomain(_httpClient, _tokenProvider, _options, _loggerFactory));
        _contact = new Lazy<IContactDomain>(() => new ContactDomain(_httpClient, _tokenProvider, _options));
        _drive = new Lazy<IDriveDomain>(() => new DriveDomain(_httpClient, _tokenProvider, _options));
        _meeting = new Lazy<IMeetingDomain>(() => new MeetingDomain(_httpClient, _tokenProvider, _options));
    }

    /// <summary>
    /// 初始化客户端
    /// </summary>
    public async Task InitializeAsync(CancellationToken cancellationToken = default)
    {
        _logger?.LogInformation("初始化 LarkClient");
        await _tokenProvider.GetTokenAsync(cancellationToken);
        _logger?.LogInformation("LarkClient 初始化完成");
    }

    /// <summary>
    /// 释放资源
    /// </summary>
    public void Dispose()
    {
        if (!_disposed)
        {
            _httpClient.Dispose();
            _disposed = true;
            _logger?.LogInformation("LarkClient 已释放");
        }
        GC.SuppressFinalize(this);
    }
}
