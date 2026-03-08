using LarkKit.Core.Auth;
using LarkKit.Core.Config;
using Microsoft.Extensions.Logging;

namespace LarkKit.Domains.Im;

/// <summary>
/// IM 领域实现
/// </summary>
public class ImDomain : IImDomain
{
    private readonly HttpClient _httpClient;
    private readonly ITokenProvider _tokenProvider;
    private readonly LarkOptions _options;
    private readonly ILoggerFactory? _loggerFactory;

    private readonly Lazy<IMessageResource> _message;

    /// <summary>
    /// 消息资源
    /// </summary>
    public IMessageResource Message => _message.Value;

    /// <summary>
    /// 初始化 ImDomain 的新实例
    /// </summary>
    public ImDomain(HttpClient httpClient, ITokenProvider tokenProvider, LarkOptions options, ILoggerFactory? loggerFactory = null)
    {
        _httpClient = httpClient;
        _tokenProvider = tokenProvider;
        _options = options;
        _loggerFactory = loggerFactory;

        _message = new Lazy<IMessageResource>(() => 
            new MessageResource(httpClient, tokenProvider, options, loggerFactory?.CreateLogger<MessageResource>()));
    }
}
