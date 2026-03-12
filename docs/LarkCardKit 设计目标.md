# LarkCardKit 设计目标

> 飞书卡片多语言 DSL（领域特定语言）工具包

## 1. 项目愿景

LarkCardKit 旨在为开发者提供一套**简洁、类型安全、多语言支持**的飞书卡片构建工具。通过流畅的 Builder API 和强类型模型，让开发者无需手写 JSON 即可快速构建符合飞书卡片 2.0 规范的交互卡片。

### 核心价值

- **类型安全**：编译时类型检查，避免 JSON 手写错误
- **流畅 API**：链式调用，代码即文档，提升开发体验
- **多语言支持**：统一的 API 设计理念，覆盖主流后端/前端语言
- **完整覆盖**：支持飞书卡片 2.0 全部组件和属性

## 2. 目标语言

| 语言 | 状态 | 包名 | 适用场景 |
|------|------|------|----------|
| C# | ✅ 进行中 | `LarkCardKit` | .NET 后端服务、Azure Functions |
| TypeScript/JavaScript | 🚧 规划中 | `@larkcardkit/core` | Node.js 后端、前端应用 |
| Python | 📋 计划中 | `larkcardkit` | Django/FastAPI 后端、数据处理 |
| Go | 📋 计划中 | `github.com/larkcardkit/go-cardkit` | 云原生服务、微服务架构 |

## 3. 架构设计

### 3.1 分层架构

```
┌─────────────────────────────────────────────────────────┐
│                    应用层 (Application)                   │
│              用户代码调用 Builder API                     │
├─────────────────────────────────────────────────────────┤
│                    构建层 (Builder)                       │
│     CardBuilder, ButtonBuilder, FormBuilder, ...        │
├─────────────────────────────────────────────────────────┤
│                    模型层 (Model)                         │
│   Card, CardHeader, CardBody, Element, Behavior, ...    │
├─────────────────────────────────────────────────────────┤
│                    序列化层 (Serialization)               │
│              JSON 序列化/反序列化                         │
├─────────────────────────────────────────────────────────┤
│                    服务层 (Service)                       │
│         ElementFinder, TemplateFiller, ...              │
└─────────────────────────────────────────────────────────┘
```

### 3.2 核心模块

#### 模型层 (Models)
- **Card** - 卡片根对象
- **CardHeader** - 卡片头部
- **CardBody** - 卡片主体
- **CardConfig** - 卡片配置
- **Elements** - 所有组件模型（Button, Input, Div, ...）

#### 构建层 (Builders)
- **CardBuilder** - 卡片构建器（入口）
- **HeaderBuilder** - 头部构建器
- **BodyBuilder** - 主体构建器
- **ElementBuilder** - 各组件构建器（ButtonBuilder, InputBuilder, ...）

#### 枚举层 (Enums)
- **ButtonType** - 按钮类型
- **ButtonSize** - 按钮尺寸
- **InputType** - 输入框类型
- **AlignType** - 对齐方式
- **BehaviorType** - 交互行为类型
- ...

#### 服务层 (Services)
- **IElementFinder** - 元素查找服务
- **TemplateParameterFiller** - 模板参数填充

## 4. API 设计原则

### 4.1 链式调用

所有构建器采用 Fluent Builder 模式，支持链式调用：

```csharp
// C# 示例
var card = CardBuilder.Create()
    .Header(h => h
        .Title("欢迎使用 LarkCardKit")
        .Subtitle("构建飞书卡片从未如此简单")
        .Template(HeaderTemplate.Blue))
    .Body(b => b
        .PlainText("这是一段普通文本")
        .Button(btn => btn
            .Text("点击我")
            .Type(ButtonType.Primary)
            .Callback("click_action", new { id = 123 })))
    .Build();
```

```typescript
// TypeScript 示例（规划中）
const card = CardBuilder.create()
    .header(h => h
        .title("欢迎使用 LarkCardKit")
        .subtitle("构建飞书卡片从未如此简单")
        .template(HeaderTemplate.Blue))
    .body(b => b
        .plainText("这是一段普通文本")
        .button(btn => btn
            .text("点击我")
            .type(ButtonType.Primary)
            .callback("click_action", { id: 123 })))
    .build();
```

```python
# Python 示例（规划中）
card = (CardBuilder.create()
    .header(lambda h: h
        .title("欢迎使用 LarkCardKit")
        .subtitle("构建飞书卡片从未如此简单")
        .template(HeaderTemplate.BLUE))
    .body(lambda b: b
        .plain_text("这是一段普通文本")
        .button(lambda btn: btn
            .text("点击我")
            .type(ButtonType.PRIMARY)
            .callback("click_action", {"id": 123})))
    .build())
```

```go
// Go 示例（规划中）
card := NewCardBuilder().
    Header(func(h *HeaderBuilder) {
        h.Title("欢迎使用 LarkCardKit").
          Subtitle("构建飞书卡片从未如此简单").
          Template(HeaderTemplateBlue)
    }).
    Body(func(b *BodyBuilder) {
        b.PlainText("这是一段普通文本").
          Button(func(btn *ButtonBuilder) {
              btn.Text("点击我").
                   Type(ButtonTypePrimary).
                   Callback("click_action", map[string]any{"id": 123})
          })
    }).
    Build()
```

### 4.2 类型安全

- 所有枚举值使用强类型定义
- 必填字段在构建时校验
- 可选字段使用 `Option<T>` 或可空类型

### 4.3 语义化命名

| 类别 | 命名规范 | 示例 |
|------|----------|------|
| 模型类 | PascalCase | `Card`, `CardHeader`, `Button` |
| 构建器 | xxxBuilder | `CardBuilder`, `ButtonBuilder` |
| 枚举 | PascalCase | `ButtonType`, `AlignType` |
| 方法 | PascalCase (C#) / camelCase (TS/Go) | `.Title()`, `.Build()` |

### 4.4 统一入口

所有构建从 `CardBuilder.Create()` / `CardBuilder.create()` 开始，最终调用 `.Build()` 产出卡片对象。

## 5. 组件支持矩阵

### 5.1 容器类组件

| 组件 | JSON Tag | C# | TypeScript | Python | Go |
|------|----------|----|-----------|--------|----|
| 分栏 | `column_set` | ✅ | 🚧 | 📋 | 📋 |
| 表单容器 | `form` | ✅ | 🚧 | 📋 | 📋 |
| 交互容器 | `interactive_container` | ✅ | 🚧 | 📋 | 📋 |
| 折叠面板 | `collapsible_panel` | ✅ | 📋 | 📋 | 📋 |

### 5.2 展示类组件

| 组件 | JSON Tag | C# | TypeScript | Python | Go |
|------|----------|----|-----------|--------|----|
| 标题 | `header` | ✅ | 🚧 | 📋 | 📋 |
| 普通文本 | `div` | ✅ | 🚧 | 📋 | 📋 |
| 富文本 | `markdown` | ✅ | 🚧 | 📋 | 📋 |
| 图片 | `img` | ✅ | 🚧 | 📋 | 📋 |
| 多图混排 | `img_combination` | 📋 | 📋 | 📋 | 📋 |
| 人员 | `person` | 📋 | 📋 | 📋 | 📋 |
| 人员列表 | `person_list` | 📋 | 📋 | 📋 | 📋 |
| 图表 | `chart` | ✅ | 📋 | 📋 | 📋 |
| 表格 | `table` | ✅ | 📋 | 📋 | 📋 |
| 分割线 | `hr` | ✅ | 🚧 | 📋 | 📋 |

### 5.3 交互类组件

| 组件 | JSON Tag | C# | TypeScript | Python | Go |
|------|----------|----|-----------|--------|----|
| 输入框 | `input` | ✅ | 🚧 | 📋 | 📋 |
| 按钮 | `button` | ✅ | 🚧 | 📋 | 📋 |
| 折叠按钮组 | `overflow` | ✅ | 📋 | 📋 | 📋 |
| 下拉选择-单选 | `select_static` | ✅ | 🚧 | 📋 | 📋 |
| 下拉选择-多选 | `multi_select_static` | ✅ | 🚧 | 📋 | 📋 |
| 人员选择-单选 | `select_person` | ✅ | 🚧 | 📋 | 📋 |
| 人员选择-多选 | `multi_select_person` | ✅ | 🚧 | 📋 | 📋 |
| 日期选择器 | `date_picker` | ✅ | 🚧 | 📋 | 📋 |
| 时间选择器 | `picker_time` | ✅ | 🚧 | 📋 | 📋 |
| 日期时间选择器 | `picker_datetime` | ✅ | 🚧 | 📋 | 📋 |
| 多图选择 | `select_img` | 📋 | 📋 | 📋 | 📋 |
| 勾选器 | `checker` | ✅ | 📋 | 📋 | 📋 |

> 图例：✅ 已实现 | 🚧 进行中 | 📋 计划中

## 6. 高级特性

### 6.1 模板参数填充

支持在卡片中使用变量占位符，运行时动态填充：

```csharp
var template = CardBuilder.Create()
    .Header(h => h.Title("${title}"))
    .Body(b => b.PlainText("${content}"))
    .Build();

var card = template.Fill(new Dictionary<string, object>
{
    ["title"] = "动态标题",
    ["content"] = "动态内容"
});
```

### 6.2 元素查找与修改

支持按 `element_id` 或条件查找并修改卡片元素：

```csharp
var card = CardBuilder.Create()
    .Body(b => b
        .PlainText("文本", id: "text_1")
        .Button("按钮", id: "btn_1"))
    .Build();

// 查找并修改
card.FindElement("text_1")
    .As<PlainText>()
    .Content = "修改后的文本";
```

### 6.3 JSON 序列化

内置 JSON 序列化支持，输出符合飞书规范的 JSON：

```csharp
// 序列化为 JSON 字符串
string json = card.ToJson();

// 序列化为 JsonElement（用于 API 调用）
JsonElement element = card.ToJsonElement();

// 美化输出
string prettyJson = card.ToJson(indent: true);
```

### 6.4 验证支持

构建时自动验证必填字段和值范围：

```csharp
var card = CardBuilder.Create()
    .Body(b => b
        .Input(input => input
            .Name("username")           // 必填
            .MaxLength(100)))           // 自动验证范围
    .Build();  // 构建时验证
```

## 7. 目录结构

```
LarkCardKit/
├── docs/                           # 文档
│   ├── 飞书卡片 2.0 标准文档.md
│   ├── LarkCardKit 设计目标.md
│   └── 组件支持情况.md
│
├── LarkCardKit/                    # C# 实现
│   ├── Models/                     # 模型定义
│   │   ├── Card.cs
│   │   ├── CardHeader.cs
│   │   ├── CardBody.cs
│   │   └── Elements/
│   ├── Builders/                   # 构建器
│   │   ├── CardBuilder.cs
│   │   ├── HeaderBuilder.cs
│   │   └── ...
│   ├── Enums/                      # 枚举定义
│   ├── Services/                   # 服务层
│   ├── Templates/                  # 模板功能
│   └── Config/                     # 配置
│
├── TypeScript/                     # TypeScript 实现
│   ├── packages/
│   │   └── core/                   # @larkcardkit/core
│   └── examples/
│
├── python/                         # Python 实现（规划）
│   └── larkcardkit/
│
├── go/                             # Go 实现（规划）
│   └── cardkit/
│
├── LarkCardKit.Tests/              # 单元测试
├── Examples/                       # 使用示例
└── LarkKit/                        # 飞书 API 客户端（可选）
```

## 8. 开发路线图

### Phase 1: C# 核心实现 ✅ (当前)
- [x] 核心模型定义
- [x] CardBuilder 基础框架
- [x] 主要组件构建器
- [x] 枚举类型定义
- [x] JSON 序列化
- [x] 单元测试框架
- [ ] 所有组件完整覆盖

### Phase 2: TypeScript/JavaScript 实现 🚧
- [ ] 项目初始化 (monorepo)
- [ ] 类型定义
- [ ] Builder 实现
- [ ] Node.js 兼容测试

### Phase 3: Python 实现 📋
- [ ] 项目初始化
- [ ] 类型注解 (Python 3.10+)
- [ ] Builder 实现
- [ ] PyPI 发布

### Phase 4: Go 实现 📋
- [ ] 项目初始化
- [ ] 类型定义
- [ ] Builder 实现
- [ ] Go Modules 发布

### Phase 5: 文档与生态 📋
- [ ] 完整 API 文档
- [ ] 交互式 Playground
- [ ] 在线代码生成器
- [ ] VS Code 插件

## 9. 包发布计划

### NuGet (C#)
```
LarkCardKit
├── LarkCardKit.Core          # 核心库
├── LarkCardKit.AspNetCore   # ASP.NET Core 集成
└── LarkCardKit.Extensions    # 扩展功能
```

### npm (TypeScript/JavaScript)
```
@larkcardkit/core             # 核心库
@larkcardkit/react            # React 组件（可选）
@larkcardkit/preview          # 预览工具（可选）
```

### PyPI (Python)
```
larkcardkit                   # 核心库
larkcardkit[fastapi]          # FastAPI 集成
```

### Go Modules
```
github.com/larkcardkit/go-cardkit
```

## 10. 参考资源

- [飞书开放平台 - 卡片消息](https://open.feishu.cn/document/ukTMukTMukTM/ucTM5YjL3ETO24yNxkjN)
- [飞书卡片搭建工具](https://open.feishu.cn/cardkit)
- [卡片 JSON 2.0 结构文档](./飞书卡片%202.0%20标准文档.md)

---

*文档版本：1.0*
*最后更新：2026-03-11*