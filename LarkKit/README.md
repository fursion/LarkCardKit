# LarkKit 开发进度

## 已完成的任务

### ✅ Task 1: 项目基础结构
- 使用 `dotnet new classlib` 创建 LarkKit 项目
- 创建模块化目录结构（Core, Message, Event, Models, Util）
- 添加必要的 NuGet 包（System.Text.Json, Microsoft.Extensions.Logging.Abstractions, Microsoft.Extensions.DependencyInjection.Abstractions）

### ✅ Task 2: 核心层 - 基础模型和工具
- [LarkErrorCode.cs](file://c:\Users\60435\Desktop\LarkCardKit\src\LarkKit\Models\LarkErrorCode.cs) - 错误码枚举
- [LarkResponse.cs](file://c:\Users\60435\Desktop\LarkCardKit\src\LarkKit\Models\LarkResponse.cs) - 通用响应模型
- [LarkException.cs](file://c:\Users\60435\Desktop\LarkCardKit\src\LarkKit\Core\Exception\LarkException.cs) - 自定义异常
- [LarkJsonSerializer.cs](file://c:\Users\60435\Desktop\LarkCardKit\src\LarkKit\Util\LarkJsonSerializer.cs) - JSON 序列化工具
- [MessageType.cs](file://c:\Users\60435\Desktop\LarkCardKit\src\LarkKit\Models\MessageType.cs) - 消息类型枚举
- [ReceiveIdType.cs](file://c:\Users\60435\Desktop\LarkCardKit\src\LarkKit\Models\ReceiveIdType.cs) - 接收者类型枚举
- 消息内容模型：
  - [TextContent.cs](file://c:\Users\60435\Desktop\LarkCardKit\src\LarkKit\Message\Content\TextContent.cs) - 文本消息内容
  - [ImageContent.cs](file://c:\Users\60435\Desktop\LarkCardKit\src\LarkKit\Message\Content\ImageContent.cs) - 图片消息内容
  - [PostContent.cs](file://c:\Users\60435\Desktop\LarkCardKit\src\LarkKit\Message\Content\PostContent.cs) - 富文本消息内容
  - [InteractiveContent.cs](file://c:\Users\60435\Desktop\LarkCardKit\src\LarkKit\Message\Content\InteractiveContent.cs) - 卡片消息内容
  - [ShareChatContent.cs](file://c:\Users\60435\Desktop\LarkCardKit\src\LarkKit\Message\Content\ShareChatContent.cs) - 分享群名片内容

### ✅ Task 3: 核心层 - 认证与 Token 管理
- [LarkOptions.cs](file://c:\Users\60435\Desktop\LarkCardKit\src\LarkKit\Core\Config\LarkOptions.cs) - 配置选项
- [TenantAccessToken.cs](file://c:\Users\60435\Desktop\LarkCardKit\src\LarkKit\Core\Auth\TenantAccessToken.cs) - Token 模型
- [ITokenProvider.cs](file://c:\Users\60435\Desktop\LarkCardKit\src\LarkKit\Core\Auth\ITokenProvider.cs) - Token 提供者接口
- [TenantTokenProvider.cs](file://c:\Users\60435\Desktop\LarkCardKit\src\LarkKit\Core\Auth\TenantTokenProvider.cs) - 租户 Token 实现（带缓存和自动刷新）

### ✅ Task 4: 核心层 - 请求签名验证
- [ISigner.cs](file://c:\Users\60435\Desktop\LarkCardKit\src\LarkKit\Core\Sign\ISigner.cs) - 签名验证接口
- [Sha256Signer.cs](file://c:\Users\60435\Desktop\LarkCardKit\src\LarkKit\Core\Sign\Sha256Signer.cs) - HMAC-SHA256 签名实现

### ✅ Task 5: 核心层 - HTTP 客户端
- [ILarkHttpClient.cs](file://c:\Users\60435\Desktop\LarkCardKit\src\LarkKit\Core\Http\ILarkHttpClient.cs) - HTTP 客户端接口
- [LarkHttpClient.cs](file://c:\Users\60435\Desktop\LarkCardKit\src\LarkKit\Core\Http\LarkHttpClient.cs) - HTTP 客户端实现（带自动鉴权、重试、日志）

## 待完成的任务

### ⏳ Task 6: 消息模块 - 消息发送 API
- 需要创建：IMessageService, MessageService, 各种发送方法

### ⏳ Task 7-16: 其他模块
- 批量消息、内容构建器、事件模块、统一客户端、依赖注入、示例项目、测试项目等

## 下一步计划

1. 完成消息模块（Task 6-8）
2. 完成事件模块（Task 9-11）
3. 完成统一客户端入口（Task 12）
4. 完成依赖注入支持（Task 13）
5. 创建示例项目（Task 14）
