# LarkKit 快速开始指南

## 安装

将 LarkKit 项目添加到您的解决方案中，或通过 NuGet 安装（待发布）。

## 基本使用

### 1. 直接使用客户端

```csharp
using LarkKit;
using LarkKit.Core.Config;

// 创建配置
var options = new LarkOptions
{
    AppId = "your_app_id",
    AppSecret = "your_app_secret"
};

// 创建客户端
var client = new LarkClient(options);

// 初始化（预加载 Token）
await client.InitializeAsync();

// 发送文本消息
var messageId = await client.Message.SendTextAsync(
    receiveId: "user_open_id",
    text: "Hello, LarkKit!"
);

Console.WriteLine($"消息发送成功，ID: {messageId}");

// 释放资源
client.Dispose();
```

### 2. 使用依赖注入（推荐）

在 `Program.cs` 或 `Startup.cs` 中：

```csharp
using Microsoft.Extensions.DependencyInjection;
using LarkKit;

var services = new ServiceCollection();

// 从配置读取
services.AddLarkClient(configuration.GetSection("Lark"));

// 或使用委托配置
services.AddLarkClient(options =>
{
    options.AppId = "your_app_id";
    options.AppSecret = "your_app_secret";
    options.BaseUrl = "https://open.feishu.cn";
});

var serviceProvider = services.BuildServiceProvider();

// 通过 DI 获取客户端
var larkClient = serviceProvider.GetRequiredService<ILarkClient>();
await larkClient.InitializeAsync();

// 发送消息
await larkClient.Message.SendTextAsync("user_open_id", "Hello from DI!");
```

### 3. 发送不同类型的消息

```csharp
// 发送文本消息
await client.Message.SendTextAsync("user_id", "这是一条文本消息");

// 发送图片消息（需要先上传图片获取 image_key）
await client.Message.SendImageAsync("user_id", "image_key_xxx");

// 发送富文本消息
var postContent = new PostContent
{
    Title = "富文本消息",
    Content = new Dictionary<string, List<PostContentItem>>
    {
        ["zh_cn"] = new List<PostContentItem>
        {
            PostContentItem.Text("你好，"),
            PostContentItem.Link("点击这里", "https://example.com"),
            PostContentItem.AtUser("user_open_id")
        }
    }
};
await client.Message.SendPostAsync("user_id", postContent);

// 发送卡片消息
var cardJson = @"
{
  ""config"": {
    ""wide_screen_mode"": true
  },
  ""header"": {
    ""title"": ""卡片标题"",
    ""template"": ""blue""
  },
  ""elements"": [...]
}
";
await client.Message.SendCardAsync("user_id", cardJson);

// 撤回消息
await client.Message.WithdrawAsync(messageId);
```

## 配置选项

```json
{
  "Lark": {
    "AppId": "your_app_id",
    "AppSecret": "your_app_secret",
    "Timeout": 30,
    "MaxRetries": 3,
    "RetryDelayMs": 1000,
    "BaseUrl": "https://open.feishu.cn",
    "TokenRefreshAheadMinutes": 5
  }
}
```

## 错误处理

```csharp
try
{
    await client.Message.SendTextAsync("user_id", "Hello");
}
catch (LarkException ex)
{
    Console.WriteLine($"错误码：{ex.ErrorCode}");
    Console.WriteLine($"错误信息：{ex.ErrorMessage}");
    Console.WriteLine($"排查建议：{ex.Suggestion}");
}
catch (Exception ex)
{
    Console.WriteLine($"其他错误：{ex.Message}");
}
```

## 最佳实践

1. **安全存储 AppSecret**：不要硬编码在代码中，使用环境变量或密钥管理服务
2. **复用客户端**：`LarkClient` 是线程安全的，建议作为单例使用
3. **错误重试**：SDK 已内置重试机制，可通过配置调整
4. **日志记录**：传入 `ILogger` 以获取详细的请求/响应日志

## 待实现功能

- 批量消息发送
- 消息内容构建器（TextBuilder, PostBuilder）
- WebSocket 长连接和事件处理
- 通讯录、云文档、会议等模块
