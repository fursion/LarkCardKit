# LarkCardKit 类型对齐实现方案

## 背景

本方案旨在更新 C# 实现代码以匹配飞书卡片 2.0 标准文档，主要涉及：

1. 修正组件属性的类型定义
2. 完善 ElementConverter 反序列化支持
3. 实现模板参数填充机制

## 文本类型体系

### 类型定义

| 类名 | Tag | 用途 |
|------|-----|------|
| `PlainText` | `plain_text` | 纯文本组件 & 文本属性 |
| `Markdown` | `markdown` | Markdown 组件（body 元素） |
| `MarkdownText` | `lark_md` | TextObject 的 lark_md 模式 |

### 使用场景

**TextObject 类型**（支持 `plain_text` 和 `lark_md` 两种模式）:
- `CardHeader.Title`, `CardHeader.Subtitle` - 使用 `Element` 类型
- `CollapsiblePanelHeader.Title` - 使用 `Element` 类型
- `TextTag.Text` - 使用 `Element` 类型

**PlainText 类型**（固定 `tag: plain_text`）:
- `Input.placeholder`, `Input.label`, `Input.disabled_tips`
- `Button.text`, `Button.hover_tips`, `Button.disabled_tips`
- `Select*.placeholder`, `Select*.label`, `Select*.disabled_tips`
- `Image.alt`, `Image.title`

## 实现变更

### 1. CollapsiblePanel 类型修正

**文件**: `LarkCardKit/Models/Elements/CollapsiblePanel.cs`

```csharp
// 修改前
[JsonPropertyName("title")]
public PlainText? Title { get; set; }

// 修改后
/// <summary>
/// 面板标题（支持 plain_text 和 lark_md 两种模式）
/// </summary>
[JsonPropertyName("title")]
public Element? Title { get; set; }
```

### 2. TextTag 类型修正

**文件**: `LarkCardKit/Models/Elements/TextTag.cs`

```csharp
// 修改前
[JsonPropertyName("text")]
public PlainText? Text { get; set; }

// 修改后
/// <summary>
/// 标签文本内容（支持 plain_text 和 lark_md 两种模式）
/// </summary>
[JsonPropertyName("text")]
public Element? Text { get; set; }
```

### 3. ElementConverter 更新

**文件**: `LarkCardKit/Converters/ElementConverter.cs`

添加 `lark_md` 的反序列化支持：

```csharp
return tag switch
{
    "plain_text" => JsonSerializer.Deserialize<PlainText>(json, options),
    "markdown" => JsonSerializer.Deserialize<Markdown>(json, options),
    "lark_md" => JsonSerializer.Deserialize<MarkdownText>(json, options),  // 新增
    // ... 其他类型
};
```

### 4. 模板参数填充工具类

**文件**: `LarkCardKit/TemplateHelper.cs`（新建）

提供卡片级别的模板参数填充功能，集成现有的 `TemplateParameterFiller` 类。

#### 使用示例

```csharp
// 使用字典参数
var card = new Card
{
    Header = new CardHeader
    {
        Title = new PlainText { Content = "你好，${name}！" }
    }
};

card.FillTemplate(new Dictionary<string, object?>
{
    ["name"] = "张三"
});

// 使用 TemplateParameterFiller
var filler = new TemplateParameterFiller();
filler.SetParameter("name", "张三");
filler.SetParameter("user", new { Name = "李四", City = "北京" });

card.FillTemplate(filler);
```

#### 支持的模板语法

- 基本变量：`${variable_name}`
- 嵌套属性：`${user.name}`
- 默认值：`${name:访客}`
- 未匹配处理：默认移除，可通过 `TemplateOptions.KeepUnmatchedPlaceholders` 保留原格式

#### 遍历填充位置

1. `Card.Header.Title.Content`
2. `Card.Header.Subtitle.Content`
3. `Card.Body.Elements` 中所有 `PlainText`、`Markdown`、`MarkdownText` 的 `Content`
4. 递归处理容器组件（`ColumnSet`、`Form`、`CollapsiblePanel`、`Loop`）内的元素

## 变更文件清单

| 文件路径 | 操作类型 | 说明 |
|----------|----------|------|
| `LarkCardKit/Models/Elements/CollapsiblePanel.cs` | 修改 | Title 类型改为 Element |
| `LarkCardKit/Models/Elements/TextTag.cs` | 修改 | Text 类型改为 Element |
| `LarkCardKit/Converters/ElementConverter.cs` | 修改 | 添加 lark_md 反序列化 |
| `LarkCardKit/TemplateHelper.cs` | 新建 | 模板参数填充工具类 |

## 兼容性说明

- `Element?` 类型是 `PlainText` 的扩展，现有使用 `PlainText` 的代码仍然兼容
- 反序列化时新增 `lark_md` 支持，不影响现有 JSON 解析

## 测试验证

- 单元测试：231 个测试全部通过
- 构建状态：成功，无警告无错误

---

*文档更新日期：2026-03-11*