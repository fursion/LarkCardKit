using LarkCardKit.Builders;
using LarkCardKit.Enums;
using LarkCardKit.Examples;

Console.WriteLine("╔════════════════════════════════════════════════════════════╗");
Console.WriteLine("║           飞书卡片 SDK (LarkCardKit) 示例程序               ║");
Console.WriteLine("╚════════════════════════════════════════════════════════════╝");
Console.WriteLine();

Console.WriteLine("请选择要运行的示例：");
Console.WriteLine("  1. 基础卡片示例");
Console.WriteLine("  2. 对象参数配置选项示例");
Console.WriteLine("  3. 组件查找、修改、替换示例");
Console.WriteLine("  4. 模板参数填充示例");
Console.WriteLine("  0. 运行所有示例");
Console.WriteLine();
Console.Write("请输入选项 (0-4): ");

var choice = Console.ReadLine();
Console.WriteLine();

switch (choice)
{
    case "1":
        RunBasicExamples();
        break;
    case "2":
        OptionConfigurationExample.Run();
        break;
    case "3":
        ElementOperationsExample.Run();
        break;
    case "4":
        TemplateParameterExample.Run();
        break;
    case "0":
    default:
        RunBasicExamples();
        OptionConfigurationExample.Run();
        ElementOperationsExample.Run();
        TemplateParameterExample.Run();
        break;
}

Console.WriteLine();
Console.WriteLine("✅ 示例运行完成！");
return;

static void RunBasicExamples()
{
    // 示例 1：简单的按钮卡片（紧凑格式）
    Console.WriteLine("=== 示例 1：简单按钮卡片（紧凑格式） ===");
var simpleCard = CardBuilder.Create()
    .Header(h => h.Title("欢迎使用飞书卡片 SDK"))
    .Body(b => b
        .PlainText("这是一个使用函数式 API 构建的卡片示例")
        .Div(d => d
            .Vertical()
            .VerticalSpacing("8px")
            .PlainText("请选择操作：")
            .Button(btn => btn
                .Text("确认").ElementId("confirmBtn")
                .Type(ButtonType.Primary)
                .OnClick(new { action = "confirm" }))
            .Button(btn => btn
                .Text("取消").ElementId("cancelBtn")
                .Type(ButtonType.Default)
                .OnClick(new { action = "cancel" })))
    )
    .ToJson();

Console.WriteLine(simpleCard);
Console.WriteLine();

// 示例 1-B：简单的按钮卡片（格式化格式）
Console.WriteLine("=== 示例 1-B：简单按钮卡片（格式化格式） ===");
var simpleCardIndented = CardBuilder.Create()
    .Header(h => h.Title("欢迎使用飞书卡片 SDK"))
    .Body(b => b
        .PlainText("这是一个使用函数式 API 构建的卡片示例")
        .Div(d => d
            .Vertical()
            .VerticalSpacing("8px")
            .PlainText("请选择操作：")
            .Button(btn => btn
                .Text("确认")
                .Type(ButtonType.Primary)
                .OnClick(new { action = "confirm" })))
    )
    .ToJson(indented: true);

Console.WriteLine(simpleCardIndented);
Console.WriteLine();

// 示例 2：表单卡片（格式化格式）
Console.WriteLine("=== 示例 2：表单卡片（格式化格式） ===");
var formCard = CardBuilder.Create()
    .Header(h => h.Title("用户信息收集"))
    .Body(b => b
        .Form(f => f
            .Name("userForm")
            .VerticalSpacing("12px")
            .Input(i => i
                .Name("username")
                .Label("用户名：")
                .Placeholder("请输入用户名")
                .Required())
            .Input(i => i
                .Name("email")
                .Label("邮箱：")
                .Placeholder("请输入邮箱")
                .Type(InputType.Text))
            .Select(s => s
                .Name("department")
                .Placeholder("请选择部门")
                .AddOption("tech", "技术部")
                .AddOption("sales", "销售部")
                .AddOption("hr", "人事部"))
            .ColumnSet(cs => cs
                .Margin("8px 0 0 0")
                .AddColumn(col => col
                    .Width("auto")
                    .Button(btn => btn
                        .Text("提交")
                        .Type(ButtonType.Primary)
                        .Width("fill")))
                .AddColumn(col => col
                    .Width("auto")
                    .Button(btn => btn
                        .Text("重置")
                        .Type(ButtonType.Default)
                        .Width("fill")))))
    )
    .ToJson(indented: true);

Console.WriteLine(formCard);
Console.WriteLine();

// 示例 3：分栏布局卡片（紧凑格式）
Console.WriteLine("=== 示例 3：分栏布局卡片（紧凑格式） ===");
var columnCard = CardBuilder.Create()
    .Header(h => h.Title("分栏布局示例"))
    .Body(b => b
        .PlainText("这是一个分栏布局的卡片")
        .ColumnSet(cs => cs
            .Margin("8px 0")
            .AddColumn(col => col
                .Width("auto")
                .PlainText("**左侧内容**\n这里是左侧的文本内容"))
            .AddColumn(col => col
                .Width("fill")
                .PlainText("**右侧内容**\n这里是右侧的文本内容，宽度自适应")))
        .Button(btn => btn
            .Text("了解更多")
            .Type(ButtonType.Primary)
            .OnClickUrl("https://example.com")))
    .ToJson();

Console.WriteLine(columnCard);
Console.WriteLine();

// 示例 3-B：分栏布局卡片（格式化格式）
Console.WriteLine("=== 示例 3-B：分栏布局卡片（格式化格式） ===");
var columnCardIndented = CardBuilder.Create()
    .Header(h => h.Title("分栏布局示例"))
    .Body(b => b
        .PlainText("这是一个分栏布局的卡片")
        .ColumnSet(cs => cs
            .Margin("8px 0")
            .AddColumn(col => col
                .Width("auto")
                .PlainText("**左侧内容**\n这里是左侧的文本内容"))
            .AddColumn(col => col
                .Width("fill")
                .PlainText("**右侧内容**\n这里是右侧的文本内容，宽度自适应")))
        .Button(btn => btn
            .Text("了解更多")
            .Type(ButtonType.Primary)
            .OnClickUrl("https://example.com")))
    .ToJson(indented: true);

Console.WriteLine(columnCardIndented);
Console.WriteLine();

Console.WriteLine("💡 提示：");
Console.WriteLine("  - 使用 .ToJson() 输出紧凑格式（默认）");
Console.WriteLine("  - 使用 .ToJson(true) 输出格式化格式（带缩进和换行）");
Console.WriteLine("  - 中文字符现在会直接显示，不会被转义为 Unicode 编码");
}
