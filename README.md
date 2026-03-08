# LarkCardKit - 飞书卡片构建器 SDK

[![NuGet](https://img.shields.io/nuget/v/LarkCardKit.svg)](https://www.nuget.org/packages/LarkCardKit/)
[![NuGet Downloads](https://img.shields.io/nuget/dt/LarkCardKit.svg)](https://www.nuget.org/packages/LarkCardKit/)
[![License](https://img.shields.io/github/license/fursion/LarkCardKit.svg)](https://github.com/fursion/LarkCardKit/blob/main/LICENSE)
[![.NET](https://img.shields.io/badge/.NET-10.0-512BD4.svg)](https://dotnet.microsoft.com/)

基于飞书卡片 JSON 2.0 规范的 C# 函数式 UI 搭建 SDK

## 功能特性

- ✅ 完全符合飞书卡片 JSON 2.0 规范
- ✅ 函数式 API，支持流畅的链式调用
- ✅ 类型安全，所有组件和属性都有强类型定义
- ✅ 支持 30+ 飞书卡片 2.0 组件（按钮、输入框、选择器、日期选择器、表格等）
- ✅ 支持布局系统（垂直/水平布局、分栏布局）
- ✅ 支持交互行为（回调、跳转、表单提交）
- ✅ 自动生成符合规范的 JSON
- ✅ 支持中文直出（非 Unicode 编码）
- ✅ 支持紧凑格式和格式化输出选项
- ✅ 支持模板参数填充

## 安装

通过 NuGet 安装：

```bash
dotnet add package LarkCardKit --version 1.0.0
```

或在 Package Manager Console 中：

```powershell
Install-Package LarkCardKit -Version 1.0.0
```

## 快速开始

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
                .OnCallback(new { action = "confirm" })))
    )
    .ToJson();

Console.WriteLine(card);
```

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
                .Option("tech", "技术部")
                .Option("sales", "销售部"))
            .ColumnSet(cs => cs
                .AddColumn(col => col
                    .Button(btn => btn
                        .Text("提交")
                        .Type(ButtonType.Primary)
                        .FormSubmit()))
                .AddColumn(col => col
                    .Button(btn => btn
                        .Text("重置")
                        .Type(ButtonType.Default)
                        .FormReset())))))
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
            .OnClick("https://example.com")))
    .ToJson();
```

### 表格组件

```csharp
var tableCard = CardBuilder.Create()
    .Header(h => h.Title("数据报表"))
    .Body(b => b
        .Table(t => t
            .PageSize(5)
            .Column(c => c.Name("name").Display("姓名").DataType("text"))
            .Column(c => c.Name("score").Display("分数").DataType("number"))
            .Row(r => r.Cells("张三", 95))
            .Row(r => r.Cells("李四", 88))))
    .ToJson();
```

### 模板参数填充

```csharp
var templateCard = CardBuilder.Create()
    .Header(h => h.Title("欢迎 {{name}}"))
    .Body(b => b
        .Markdown("您有 {{count}} 条新消息")
        .Button(btn => btn
            .Text("查看详情")
            .OnClick("{{detail_url}}")))
    .FillTemplate(new
    {
        name = "张三",
        count = 5,
        detail_url = "https://example.com/detail"
    })
    .ToJson();
```

### JSON 输出格式

SDK 支持两种 JSON 输出格式：

```csharp
// 紧凑格式（默认）
var compactJson = card.ToJson();

// 格式化格式（带缩进和换行）
var indentedJson = card.ToJson(indented: true);
```

**中文显示说明：**
- ✅ 中文字符直接显示，不会被转义为 Unicode 编码
- ✅ 适用于调试、日志记录、人工阅读等场景

## 组件支持

### 总体统计

| 指标 | 数值 |
|------|------|
| 已实现组件 | 30 个 |
| 总体覆盖率 | **96.8%** |

### 交互组件

| 组件 | 说明 | 状态 |
|------|------|------|
| Button | 按钮 - 支持 9 种类型、4 种尺寸 | ✅ |
| Input | 输入框 - 支持文本、多行文本、密码 | ✅ |
| Select | 选择器 - 支持单选/多选 | ✅ |
| DatePicker | 日期选择器 | ✅ |
| PickerTime | 时间选择器 | ✅ |
| PickerDatetime | 日期时间选择器 | ✅ |
| Checkbox | 勾选框 | ✅ |
| SelectPerson | 人员选择器（单选） | ✅ |
| MultiSelectPerson | 人员选择器（多选） | ✅ |
| Overflow | 折叠按钮组 | ✅ |
| Checker | 勾选器 | ✅ |

### 容器组件

| 组件 | 说明 | 状态 |
|------|------|------|
| Div | 普通容器 - 支持垂直/水平布局 | ✅ |
| Form | 表单容器 - 支持异步提交 | ✅ |
| ColumnSet | 分栏布局 | ✅ |
| InteractiveContainer | 交互容器 | ✅ |

### 文本和媒体

| 组件 | 说明 | 状态 |
|------|------|------|
| PlainText | 纯文本 | ✅ |
| Markdown | Markdown 文本 | ✅ |
| Image | 图片 | ✅ |
| ImgCombination | 多图混排（二/三/四/六/九宫格） | ✅ |

### 展示组件

| 组件 | 说明 | 状态 |
|------|------|------|
| Table | 表格 | ✅ |
| Hr | 分割线 | ✅ |
| Person | 人员展示 | ✅ |
| PersonList | 人员列表 | ✅ |
| TextTag | 文本标签 | ✅ |
| Chart | 图表 | ❌ 计划中 |

## 项目结构

```
LarkCardKit/
├── Models/                    # 数据模型
│   ├── Card.cs               # 卡片根模型
│   ├── CardHeader.cs         # 卡片头部
│   ├── CardBody.cs           # 卡片主体
│   ├── CardLink.cs           # 卡片链接
│   ├── CardStyle.cs          # 卡片样式
│   └── Elements/             # 组件模型
│       ├── Button.cs
│       ├── Input.cs
│       ├── Select.cs
│       └── ...
├── Builders/                  # 构建器
│   ├── CardBuilder.cs        # 卡片构建器
│   ├── ButtonBuilder.cs      # 按钮构建器
│   ├── InputBuilder.cs       # 输入框构建器
│   └── ...
├── Enums/                     # 枚举类型
│   ├── ButtonType.cs
│   ├── ButtonSize.cs
│   └── ...
├── Config/                    # 配置
│   ├── CardConfig.cs
│   └── JsonOptions.cs
├── Services/                  # 服务
│   ├── ElementFinder.cs      # 元素查找器
│   └── IElementFinder.cs
├── Templates/                 # 模板
│   └── TemplateParameterFiller.cs
└── Converters/                # 转换器
    └── ElementConverter.cs
```

## 运行示例

```bash
# 运行示例项目
dotnet run --project Examples/LarkCardKit.Examples.csproj

# 运行测试
dotnet test LarkCardKit.Tests/LarkCardKit.Tests.csproj
```

## 技术栈

- .NET 10
- System.Text.Json
- C# 最新特性

## 相关链接

- [NuGet 包](https://www.nuget.org/packages/LarkCardKit/)
- [GitHub 仓库](https://github.com/fursion/LarkCardKit)
- [飞书卡片 2.0 开发文档](https://open.feishu.cn/document/client-docs/bot-v3/card-card-creation/card-creation-introduction)
- [组件支持情况](docs/组件支持情况.md)

## 许可证

[MIT License](LICENSE)
