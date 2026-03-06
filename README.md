# LarkCardKit - 飞书卡片构建器 SDK

基于飞书卡片 JSON 2.0 规范的 C# 函数式 UI 搭建 SDK

## 功能特性

- ✅ 完全符合飞书卡片 JSON 2.0 规范
- ✅ 函数式 API，支持流畅的链式调用
- ✅ 类型安全，所有组件和属性都有强类型定义
- ✅ 支持所有飞书卡片 2.0 组件（按钮、输入框、选择器、日期选择器等）
- ✅ 支持布局系统（垂直/水平布局、分栏布局）
- ✅ 支持交互行为（回调、跳转、表单提交）
- ✅ 自动生成符合规范的 JSON
- ✅ 支持中文直出（非 Unicode 编码）
- ✅ 支持紧凑格式和格式化输出选项

## 快速开始

### 安装

项目已创建在 `LarkCardKit` 文件夹中，使用 .NET 10 开发。

### 基本用法

```csharp
using LarkCardKit.Builders;
using LarkCardKit.Enums;

// 创建简单卡片
var card = CardBuilder.Create()
    .Header(h => h.Title("欢迎使用飞书卡片 SDK"))
    .Body(b => b
        .PlainText("这是一个使用函数式 API 构建的卡片示例")
        .Div(d => d
            .Vertical()
            .VerticalSpacing("8px")
            .Button(btn => btn
                .Text("确认")
                .Type(ButtonType.Primary)
                .OnClick(new { action = "confirm" })))
    )
    .ToJson();

Console.WriteLine(card);
```

### JSON 输出格式

SDK 支持两种 JSON 输出格式：

```csharp
// 紧凑格式（默认）
var compactJson = card.ToJson();
// 输出：{"schema":"2.0","header":{"title":{"content":"欢迎"}}}

// 格式化格式（带缩进和换行）
var indentedJson = card.ToJson(indented: true);
// 输出：
// {
//   "schema": "2.0",
//   "header": {
//     "title": {
//       "content": "欢迎"
//     }
//   }
// }
```

**中文显示说明：**
- ✅ 中文字符直接显示，不会被转义为 Unicode 编码（如 `\u6B22\u8FCE`）
- ✅ 适用于调试、日志记录、人工阅读等场景

### 表单卡片

```csharp
var formCard = CardBuilder.Create()
    .Header(h => h.Title("用户信息收集"))
    .Body(b => b
        .Form(f => f
            .Name("userForm")
            .Input(i => i
                .Name("username")
                .Label("用户名：")
                .Placeholder("请输入用户名")
                .Required())
            .Select(s => s
                .Name("department")
                .Label("部门：")
                .Placeholder("请选择部门")
                .AddOption("tech", "技术部")
                .AddOption("sales", "销售部"))
            .ColumnSet(cs => cs
                .AddColumn(col => col
                    .Button(btn => btn
                        .Text("提交")
                        .Type(ButtonType.Primary)))
                .AddColumn(col => col
                    .Button(btn => btn
                        .Text("重置")
                        .Type(ButtonType.Default)))))
    )
    .ToJson();
```

### 分栏布局

```csharp
var columnCard = CardBuilder.Create()
    .Header(h => h.Title("分栏布局示例"))
    .Body(b => b
        .ColumnSet(cs => cs
            .AddColumn(col => col
                .Width("auto")
                .PlainText("**左侧内容**"))
            .AddColumn(col => col
                .Width("fill")
                .PlainText("**右侧内容**")))
        .Button(btn => btn
            .Text("了解更多")
            .Type(ButtonType.Primary)
            .OnClickUrl("https://example.com")))
    .ToJson();
```

## 组件支持

### 交互组件
- ✅ Button（按钮）- 支持 9 种类型、4 种尺寸、多种交互
- ✅ Input（输入框）- 支持文本、多行文本、密码类型
- ✅ Select（选择器）- 支持单选/多选、自定义选项
- ✅ DatePicker（日期选择器）- 支持日期选择
- ✅ Checkbox（勾选器）- 支持多选

### 容器组件
- ✅ Div（普通容器）- 支持垂直/水平布局
- ✅ Form（表单容器）- 支持异步提交
- ✅ ColumnSet（分栏）- 支持多列布局

### 文本和媒体
- ✅ PlainText（纯文本）
- ✅ Markdown（Markdown 文本）
- ✅ Image（图片）

## 项目结构

```
LarkCardKit/
├── Models/              # 数据模型
│   ├── Card.cs         # 卡片根模型
│   ├── CardHeader.cs   # 卡片头部
│   ├── CardBody.cs     # 卡片主体
│   └── Elements/       # 组件模型
├── Builders/           # 构建器
│   ├── CardBuilder.cs
│   ├── ButtonBuilder.cs
│   ├── InputBuilder.cs
│   └── ...
├── Enums/              # 枚举类型
│   ├── ButtonType.cs
│   ├── ButtonSize.cs
│   └── ...
├── Config/             # 配置
│   ├── CardConfig.cs
│   └── JsonOptions.cs
└── Behaviors/          # 交互行为
    └── Behaviors.cs
```

## 运行示例

```bash
# 运行示例项目
dotnet run --project Examples/LarkCardKit.Examples.csproj
```

## 开发状态

当前版本已完成核心功能：
- ✅ 项目初始化和基础架构
- ✅ 所有枚举类型定义
- ✅ 核心模型类
- ✅ JSON 序列化配置
- ✅ 函数式构建器 API
- ✅ 布局系统
- ✅ 所有交互组件
- ✅ 交互行为配置
- ✅ 表单容器
- ✅ 示例项目

待完成：
- ⏳ 模板系统（通知模板、表单模板等）
- ⏳ 完整的 XML 文档注释

## 技术栈

- .NET 10
- System.Text.Json 8.0
- C# 最新特性

## 许可证

MIT License
