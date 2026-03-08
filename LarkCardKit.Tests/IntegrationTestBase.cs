using LarkCardKit.Builders;
using LarkCardKit.Config;
using LarkCardKit.Enums;
using LarkCardKit.Models;
using LarkCardKit.Templates;
using LarkKit;
using LarkKit.Core.Config;
using LarkKit.Models.Params;
using LarkKit.Models.Requests;
using LarkKit.Models.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Xunit;

namespace LarkCardKit.Tests;

public abstract class IntegrationTestBase : IAsyncLifetime, IDisposable
{
    protected IConfiguration Configuration { get; private set; } = null!;
    protected LarkClient? Client { get; private set; }
    protected ILogger<LarkClient> Logger { get; private set; } = null!;
    protected ILoggerFactory LoggerFactoryInstance { get; private set; } = null!;
    
    protected string? TestEmail { get; private set; }
    protected string? TestOpenId { get; private set; }
    protected bool IsConfigured { get; private set; }
    
    public async Task InitializeAsync()
    {
        Configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true)
            .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development"}.json", optional: true)
            .AddEnvironmentVariables()
            .Build();
        
        var appId = Configuration["Lark:AppId"] ?? Configuration["LARK_APP_ID"];
        var appSecret = Configuration["Lark:AppSecret"] ?? Configuration["LARK_APP_SECRET"];
        
        TestEmail = Configuration["TestSettings:TestEmail"] ?? Configuration["TEST_EMAIL"];
        TestOpenId = Configuration["TestSettings:TestOpenId"] ?? Configuration["TEST_OPEN_ID"];
        
        IsConfigured = !string.IsNullOrEmpty(appId) 
            && appId != "your_app_id"
            && !string.IsNullOrEmpty(appSecret) 
            && appSecret != "your_app_secret"
            && (!string.IsNullOrEmpty(TestEmail) || !string.IsNullOrEmpty(TestOpenId));
        
        LoggerFactoryInstance = Microsoft.Extensions.Logging.LoggerFactory.Create(builder =>
        {
            builder.SetMinimumLevel(LogLevel.Warning);
        });
        
        Logger = LoggerFactoryInstance.CreateLogger<LarkClient>();
        
        if (IsConfigured)
        {
            var options = new LarkOptions
            {
                AppId = appId!,
                AppSecret = appSecret!,
                BaseUrl = Configuration["Lark:BaseUrl"] ?? "https://open.feishu.cn",
                Timeout = int.Parse(Configuration["Lark:Timeout"] ?? "30")
            };
            
            Client = new LarkClient(options, Logger, LoggerFactoryInstance);
            await Client.InitializeAsync();
        }
    }
    
    public async Task DisposeAsync()
    {
        if (Client != null)
        {
            Client.Dispose();
        }
        LoggerFactoryInstance?.Dispose();
        await Task.CompletedTask;
    }
    
    public void Dispose()
    {
        Client?.Dispose();
        LoggerFactoryInstance?.Dispose();
    }
    
    protected void SkipIfNotConfigured()
    {
        if (!IsConfigured)
        {
            throw new InvalidOperationException("集成测试跳过：未配置飞书应用凭据。请设置 appsettings.json 或环境变量 LARK_APP_ID/LARK_APP_SECRET/TEST_EMAIL");
        }
    }
    
    protected string GetReceiveId()
    {
        if (!string.IsNullOrEmpty(TestEmail))
            return TestEmail!;
        if (!string.IsNullOrEmpty(TestOpenId))
            return TestOpenId!;
        throw new InvalidOperationException("未配置测试接收者，请设置 TestEmail 或 TestOpenId");
    }
    
    protected string GetReceiveIdType()
    {
        if (!string.IsNullOrEmpty(TestEmail))
            return "email";
        if (!string.IsNullOrEmpty(TestOpenId))
            return "open_id";
        throw new InvalidOperationException("未配置测试接收者");
    }
    
    protected async Task<string> SendCardAsync(string cardJson)
    {
        if (Client == null)
            throw new InvalidOperationException("客户端未初始化");
            
        var request = new MessageCreateRequest
        {
            Data = new MessageCreateData
            {
                ReceiveId = GetReceiveId(),
                MsgType = "interactive",
                Content = cardJson
            },
            Params = new MessageCreateParams { ReceiveIdType = GetReceiveIdType() }
        };
        
        var response = await Client.Im.Message.CreateAsync(request);
        return response.MessageId;
    }
    
    protected async Task<string> SendCardAsync(Card card)
    {
        return await SendCardAsync(card.ToJson());
    }
    
    protected async Task<string> SendCardAsync(string testName, Card card)
    {
        var messageId = await SendCardAsync(card.ToJson());
        return messageId;
    }
    
    protected async Task DelayAsync(int milliseconds = 500)
    {
        await Task.Delay(milliseconds);
    }
}
