using LarkKit;
using LarkKit.Core.Config;
using LarkKit.Domains.Contact;
using LarkKit.Domains.Drive;
using LarkKit.Domains.Im;
using LarkKit.Domains.Meeting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// 依赖注入扩展方法
/// </summary>
public static class LarkServiceCollectionExtensions
{
    /// <summary>
    /// 添加飞书客户端服务
    /// </summary>
    public static IServiceCollection AddLarkClient(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var larkOptions = new LarkOptions();
        configuration.GetSection("Lark").Bind(larkOptions);
        return services.AddLarkClient(larkOptions);
    }

    /// <summary>
    /// 添加飞书客户端服务
    /// </summary>
    public static IServiceCollection AddLarkClient(
        this IServiceCollection services,
        Action<LarkOptions> configureOptions)
    {
        var larkOptions = new LarkOptions();
        configureOptions(larkOptions);
        return services.AddLarkClient(larkOptions);
    }

    /// <summary>
    /// 添加飞书客户端服务
    /// </summary>
    public static IServiceCollection AddLarkClient(
        this IServiceCollection services,
        LarkOptions options)
    {
        services.AddSingleton(options);
        services.AddSingleton<ILarkClient>(sp =>
        {
            var logger = sp.GetService<ILogger<LarkClient>>();
            var client = new LarkClient(options, logger);
            client.InitializeAsync().Wait();
            return client;
        });
        return services;
    }

    /// <summary>
    /// 只添加 IM 领域服务
    /// </summary>
    public static IServiceCollection AddLarkImDomain(
        this IServiceCollection services,
        Action<LarkOptions> configureOptions)
    {
        var larkOptions = new LarkOptions();
        configureOptions(larkOptions);

        services.AddSingleton(larkOptions);
        services.AddSingleton<ILarkClient>(sp =>
        {
            var logger = sp.GetService<ILogger<LarkClient>>();
            var client = new LarkClient(larkOptions, logger);
            client.InitializeAsync().Wait();
            return client;
        });

        services.AddSingleton<IImDomain>(sp => sp.GetRequiredService<ILarkClient>().Im);
        return services;
    }
}
