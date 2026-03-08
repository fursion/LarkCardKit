using System.Text.Json;
using LarkCardKit.Builders;
using LarkCardKit.Config;
using LarkCardKit.Enums;
using LarkKit;
using LarkKit.Core.Config;
using LarkKit.Models.Data;
using LarkKit.Models.Params;
using LarkKit.Models.Requests;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

Console.WriteLine("=== LarkCardKit DSL 全组件测试 ===\n");

var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";
Console.WriteLine($"当前环境：{environment}\n");

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true)
    .Build();

var loggerFactory = LoggerFactory.Create(builder =>
{
    builder.AddConsole().AddFilter("Microsoft", LogLevel.Warning).AddFilter("System", LogLevel.Warning);
    var logLevel = environment == "Development" ? LogLevel.Debug : LogLevel.Information;
    builder.SetMinimumLevel(logLevel);
});

var logger = loggerFactory.CreateLogger<LarkClient>();

var appId = configuration["Lark:AppId"];
var appSecret = configuration["Lark:AppSecret"];

if (string.IsNullOrEmpty(appId) || appId == "your_app_id")
{
    Console.WriteLine("❌ 请先在 appsettings.json 中配置 AppId");
    return;
}

if (string.IsNullOrEmpty(appSecret) || appSecret == "your_app_secret")
{
    Console.WriteLine("❌ 请先在 appsettings.json 中配置 AppSecret");
    return;
}

var options = new LarkOptions
{
    AppId = appId,
    AppSecret = appSecret,
    BaseUrl = configuration["Lark:BaseUrl"] ?? "https://open.feishu.cn",
    Timeout = int.Parse(configuration["Lark:Timeout"] ?? "30")
};

var client = new LarkClient(options, logger, loggerFactory);

try
{
    Console.WriteLine("正在初始化客户端...");
    await client.InitializeAsync();
    Console.WriteLine("✓ 客户端初始化成功\n");

    var email = configuration["TestSettings:TestEmail"] ?? "test@example.com";
    Console.WriteLine($"发送目标邮箱：{email}\n");

    // ========================================
    // 测试 1: Div 组件 - 文本
    // ========================================
    Console.WriteLine("=== 测试 1: Div 组件 - 文本 ===");
    var divCard = CardBuilder.Create()
        .Header(h => h.Title("Div 组件测试"))
        .Body(b => b
            .Div(div => div
                .Text("这是 Div 组件的文本内容")))
        .Build();

    await SendCardAsync("Div 组件", divCard.ToJson());

    // ========================================
    // 测试 2: Div 组件 - 多个文本
    // ========================================
    Console.WriteLine("=== 测试 2: Div 组件 - 多个文本 ===");
    var divVerticalCard = CardBuilder.Create()
        .Header(h => h.Title("多个 Div 组件"))
        .Body(b => b
            .Div(div => div.Text("第一行文本"))
            .Div(div => div.Text("第二行文本"))
            .Div(div => div.Text("第三行文本")))
        .Build();

    await SendCardAsync("Div 多文本", divVerticalCard.ToJson());

    // ========================================
    // 测试 3: Markdown 元素
    // ========================================
    Console.WriteLine("=== 测试 3: Markdown 元素 ===");
    var divMarkdownCard = CardBuilder.Create()
        .Header(h => h.Title("Markdown 文本测试"))
        .Body(b => b
            .Markdown("**粗体** *斜体*\n- 列表项 1\n- 列表项 2"))
        .Build();

    await SendCardAsync("Markdown 元素", divMarkdownCard.ToJson());

    // ========================================
    // 测试 4: ColumnSet 分栏组件
    // ========================================
    Console.WriteLine("=== 测试 4: ColumnSet 分栏组件 ===");
    var columnSetCard = CardBuilder.Create()
        .Header(h => h.Title("ColumnSet 分栏测试"))
        .Body(b => b
            .ColumnSet(cs => cs
                .HorizontalSpacing("8px")
                .AddColumn(col => col
                    .Width("50%")
                    .Markdown("左侧列内容"))
                .AddColumn(col => col
                    .Width("50%")
                    .Markdown("右侧列内容"))))
        .Build();

    await SendCardAsync("ColumnSet 分栏", columnSetCard.ToJson());

    // ========================================
    // 测试 5: ColumnSet 三栏布局
    // ========================================
    Console.WriteLine("=== 测试 5: ColumnSet 三栏布局 ===");
    var threeColumnCard = CardBuilder.Create()
        .Header(h => h.Title("三栏布局示例"))
        .Body(b => b
            .ColumnSet(cs => cs
                .AddColumn(col => col.Width("33%").Markdown("列 1"))
                .AddColumn(col => col.Width("33%").Markdown("列 2"))
                .AddColumn(col => col.Width("34%").Markdown("列 3"))))
        .Build();

    await SendCardAsync("三栏布局", threeColumnCard.ToJson());

    // ========================================
    // 测试 6: Form 表单组件 - Input
    // ========================================
    Console.WriteLine("=== 测试 6: Form 表单组件 - Input ===");
    var inputFormCard = CardBuilder.Create()
        .Header(h => h.Title("Form 表单 - Input 测试"))
        .Body(b => b
            .Form(form => form
                .Name("inputForm")
                .Vertical()
                .Input(input => input
                    .Name("username")
                    .Label("用户名")
                    .Placeholder("请输入用户名")
                    .Required())
                .Input(input => input
                    .Name("email")
                    .Label("邮箱")
                    .Placeholder("请输入邮箱")
                    .Type(InputType.Text))
                .Button(btn => btn.Text("提交").Submit())))
        .Build();

    await SendCardAsync("Form - Input", inputFormCard.ToJson());

    // ========================================
    // 测试 7: Form 表单组件 - Select
    // ========================================
    Console.WriteLine("=== 测试 7: Form 表单组件 - Select ===");
    var selectFormCard = CardBuilder.Create()
        .Header(h => h.Title("Form 表单 - Select 测试"))
        .Body(b => b
            .Form(form => form
                .Name("selectForm")
                .Vertical()
                .Select(select => select
                    .Name("city")
                    .Placeholder("请选择城市")
                    .AddOption("beijing", "北京")
                    .AddOption("shanghai", "上海")
                    .AddOption("guangzhou", "广州"))
                .Button(btn => btn.Text("提交").Submit())))
        .Build();

    await SendCardAsync("Form - Select", selectFormCard.ToJson());

    // ========================================
    // 测试 8: Form 表单组件 - DatePicker
    // ========================================
    Console.WriteLine("=== 测试 8: Form 表单组件 - DatePicker ===");
    var datePickerCard = CardBuilder.Create()
        .Header(h => h.Title("Form 表单 - DatePicker 测试"))
        .Body(b => b
            .Form(form => form
                .Name("dateForm")
                .Vertical()
                .DatePicker(dp => dp
                    .Name("birthday")
                    .Placeholder("请选择日期")
                    .InitialDate("2000-01-01"))
                .Button(btn => btn.Text("提交").Submit())))
        .Build();

    await SendCardAsync("Form - DatePicker", datePickerCard.ToJson());

    // ========================================
    // 测试 9: Form 表单组件 - Checkbox（多选下拉）
    // ========================================
    Console.WriteLine("=== 测试 9: Form 表单组件 - Checkbox（多选下拉） ===");
    var checkboxCard = CardBuilder.Create()
        .Header(h => h.Title("Form 表单 - 多选下拉测试"))
        .Body(b => b
            .Form(form => form
                .Name("checkboxForm")
                .Vertical()
                .Checkbox(cb => cb
                    .Name("hobbies")
                    .Placeholder("请选择爱好")
                    .AddOption("reading", "阅读")
                    .AddOption("gaming", "游戏")
                    .AddOption("music", "音乐")
                    .OnChange(new { action = "select" }))
                .Button(btn => btn.Text("提交").Submit())))
        .Build();

    await SendCardAsync("Form - 多选下拉", checkboxCard.ToJson());

    // ========================================
    // 测试 10: 综合表单
    // ========================================
    Console.WriteLine("=== 测试 10: 综合表单 ===");
    var complexFormCard = CardBuilder.Create()
        .Header(h => h.Title("用户信息收集"))
        .Body(b => b
            .Div(div => div.Text("请填写以下信息："))
            .Form(form => form
                .Name("userForm")
                .Vertical()
                .Input(input => input
                    .Name("name")
                    .Label("姓名")
                    .Placeholder("请输入姓名")
                    .Required())
                .Select(select => select
                    .Name("gender")
                    .Placeholder("请选择")
                    .AddOption("male", "男")
                    .AddOption("female", "女"))
                .DatePicker(dp => dp
                    .Name("birthday")
                    .Placeholder("请选择日期")
                    .InitialDate("2000-01-01"))
                .Checkbox(cb => cb
                    .Name("interests")
                    .Placeholder("请选择兴趣")
                    .AddOption("tech", "技术")
                    .AddOption("design", "设计")
                    .AddOption("product", "产品")
                    .OnChange(new { action = "select" }))
                .Button(btn => btn.Text("提交").Submit())))
        .Build();

    await SendCardAsync("综合表单", complexFormCard.ToJson());

    // ========================================
    // 测试 11: 复杂布局
    // ========================================
    Console.WriteLine("=== 测试 11: 复杂布局 ===");
    var complexLayoutCard = CardBuilder.Create()
        .Header(h => h.Title("复杂布局示例"))
        .Body(b => b
            .ColumnSet(cs => cs
                .AddColumn(col => col
                    .Width("33%")
                    .Markdown("列 1"))
                .AddColumn(col => col
                    .Width("33%")
                    .Markdown("列 2"))
                .AddColumn(col => col
                    .Width("34%")
                    .Markdown("列 3")))
            .Div(div => div.Text("分隔线下方内容")))
        .Build();

    await SendCardAsync("复杂布局", complexLayoutCard.ToJson());

    // ========================================
    // 测试 12: 纯文本和 Markdown 元素
    // ========================================
    Console.WriteLine("=== 测试 12: Markdown 元素 ===");
    var textMarkdownCard = CardBuilder.Create()
        .Header(h => h.Title("Markdown 测试"))
        .Body(b => b
            .Markdown("这是普通文本")
            .Markdown("**这是 Markdown 元素**\n- 支持列表\n- 支持*斜体*和**粗体**"))
        .Build();

    await SendCardAsync("Markdown", textMarkdownCard.ToJson());

    Console.WriteLine("\n=== 所有组件测试完成！请检查飞书客户端 ===");

    async Task SendCardAsync(string testName, string cardJson)
    {
        var request = new MessageCreateRequest
        {
            Data = new MessageCreateData
            {
                ReceiveId = email,
                MsgType = "interactive",
                Content = cardJson
            },
            Params = new MessageCreateParams { ReceiveIdType = "email" }
        };

        var response = await client.Im.Message.CreateAsync(request);
        Console.WriteLine($"✓ {testName} 发送成功！消息 ID: {response.MessageId}\n");
        await Task.Delay(500);
    }
}
catch (Exception ex)
{
    Console.WriteLine($"\n❌ 发生错误：{ex.Message}");
    if (ex is LarkKit.Core.Exception.LarkException larkEx)
    {
        Console.WriteLine($"错误码：{larkEx.ErrorCode}");
        Console.WriteLine($"排查建议：{larkEx.Suggestion}");
    }
    logger.LogError(ex, "发送消息时发生错误");
    Console.WriteLine($"\n详细错误：{ex}");
}
finally
{
    client.Dispose();
    loggerFactory.Dispose();
    Console.WriteLine("\n客户端已释放");
}
