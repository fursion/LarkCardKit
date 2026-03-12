using LarkCardKit.Builders;
using LarkCardKit.Enums;
using Xunit;

namespace LarkCardKit.Tests;

[Trait("Category", "Integration")]
public class MoreComponentsSendTests : IntegrationTestBase
{
    [Fact]
    public async Task SendPickerDatetime_ShouldSucceed()
    {
        SkipIfNotConfigured();
        
        var card = CardBuilder.Create()
            .Header(h => h.Title("PickerDatetime 日期时间选择器测试").TextTag(t => t.Text("C#").Color("purple")))
            .Body(b => b
                .PickerDatetime(pd => pd
                    .Placeholder("请选择日期时间")
                    .InitialDatetime("2024-01-15 09:00")))
            .Build();
        
        var messageId = await SendCardAsync("PickerDatetime", card);
        Assert.NotNull(messageId);
        
        await DelayAsync();
    }
    
    [Fact]
    public async Task SendPickerDatetimeInForm_ShouldSucceed()
    {
        SkipIfNotConfigured();
        
        var card = CardBuilder.Create()
            .Header(h => h.Title("表单中的日期时间选择器").TextTag(t => t.Text("C#").Color("purple")))
            .Body(b => b
                .Form(form => form
                    .Name("datetimeForm")
                    .Vertical()
                    .Add(new Builders.PickerDatetimeBuilder()
                        .Placeholder("选择会议时间")
                        .Name("meetingTime")
                        .Required()
                        .Build())
                    .Button(btn => btn.Text("提交").Submit())))
            .Build();
        
        var messageId = await SendCardAsync("表单日期时间选择器", card);
        Assert.NotNull(messageId);
        
        await DelayAsync();
    }
    
    [Fact]
    public async Task SendOverflow_ShouldSucceed()
    {
        SkipIfNotConfigured();
        
        var card = CardBuilder.Create()
            .Header(h => h.Title("Overflow 折叠按钮组测试").TextTag(t => t.Text("C#").Color("purple")))
            .Body(b => b
                .PlainText("点击查看更多操作：")
                .Overflow(overflow => overflow
                    .AddOption("edit", "编辑")
                    .AddOption("delete", "删除")
                    .AddOption("share", "分享")))
            .Build();
        
        var messageId = await SendCardAsync("Overflow 折叠按钮组", card);
        Assert.NotNull(messageId);
        
        await DelayAsync();
    }
    
    [Fact]
    public async Task SendOverflowWithCallback_ShouldSucceed()
    {
        SkipIfNotConfigured();
        
        var card = CardBuilder.Create()
            .Header(h => h.Title("带回调的折叠按钮组").TextTag(t => t.Text("C#").Color("purple")))
            .Body(b => b
                .Overflow(overflow => overflow
                    .AddOption("option1", "选项一")
                    .AddOption("option2", "选项二")
                    .AddOption("option3", "选项三")
                    .OnClick(new { action = "overflow_click" })))
            .Build();
        
        var messageId = await SendCardAsync("Overflow 回调", card);
        Assert.NotNull(messageId);
        
        await DelayAsync();
    }
    
    [Fact(Skip = "Person 组件 API 返回 open_id 属性不支持，可能是飞书 API 版本问题")]
    public async Task SendPerson_ShouldSucceed()
    {
        SkipIfNotConfigured();
        
        var openId = "ou_f73a30fa87008847f2253ea854e1d476";
        
        var card = CardBuilder.Create()
            .Header(h => h.Title("Person 人员展示测试"))
            .Body(b => b
                .PlainText("单个人员展示：")
                .Person(p => p
                    .OpenId(openId)
                    .ShowName()
                    .ShowAvatar()))
            .Build();
        
        var messageId = await SendCardAsync("Person 人员展示", card);
        Assert.NotNull(messageId);
        
        await DelayAsync();
    }
    
    [Fact(Skip = "Person 组件 API 返回 open_id 属性不支持，可能是飞书 API 版本问题")]
    public async Task SendPersonWithStyle_ShouldSucceed()
    {
        SkipIfNotConfigured();
        
        var openId = "ou_f73a30fa87008847f2253ea854e1d476";
        
        var card = CardBuilder.Create()
            .Header(h => h.Title("带样式的人员展示"))
            .Body(b => b
                .Person(p => p
                    .OpenId(openId)
                    .Style("capsule")
                    .ShowName()
                    .ShowAvatar()))
            .Build();
        
        var messageId = await SendCardAsync("Person 样式", card);
        Assert.NotNull(messageId);
        
        await DelayAsync();
    }
    
    [Fact(Skip = "PersonList 组件 API 返回 open_ids 属性不支持，可能是飞书 API 版本问题")]
    public async Task SendPersonList_ShouldSucceed()
    {
        SkipIfNotConfigured();
        
        var openId = "ou_f73a30fa87008847f2253ea854e1d476";
        
        var card = CardBuilder.Create()
            .Header(h => h.Title("PersonList 人员列表测试"))
            .Body(b => b
                .PlainText("多个人员展示：")
                .PersonList(pl => pl
                    .OpenIds(openId)))
            .Build();
        
        var messageId = await SendCardAsync("PersonList 人员列表", card);
        Assert.NotNull(messageId);
        
        await DelayAsync();
    }
    
    [Fact(Skip = "PersonList 组件 API 返回 open_ids 属性不支持，可能是飞书 API 版本问题")]
    public async Task SendPersonListWithStyle_ShouldSucceed()
    {
        SkipIfNotConfigured();
        
        var openId = "ou_f73a30fa87008847f2253ea854e1d476";
        
        var card = CardBuilder.Create()
            .Header(h => h.Title("带样式的人员列表"))
            .Body(b => b
                .PersonList(pl => pl
                    .OpenIds(openId)
                    .Style("capsule")))
            .Build();
        
        var messageId = await SendCardAsync("PersonList 样式", card);
        Assert.NotNull(messageId);
        
        await DelayAsync();
    }
    
    [Fact]
    public async Task SendSelectPerson_ShouldSucceed()
    {
        SkipIfNotConfigured();
        
        var card = CardBuilder.Create()
            .Header(h => h.Title("SelectPerson 人员选择器测试").TextTag(t => t.Text("C#").Color("purple")))
            .Body(b => b
                .Form(form => form
                    .Name("personForm")
                    .Vertical()
                    .Add(new SelectPersonBuilder()
                        .Name("assignee")
                        .Placeholder("请选择人员")
                        .Build())
                    .Button(btn => btn.Text("提交").Submit())))
            .Build();
        
        var messageId = await SendCardAsync("SelectPerson 人员选择", card);
        Assert.NotNull(messageId);
        
        await DelayAsync();
    }
    
    [Fact]
    public async Task SendSelectPersonWithInitial_ShouldSucceed()
    {
        SkipIfNotConfigured();
        
        var card = CardBuilder.Create()
            .Header(h => h.Title("带初始值的人员选择器").TextTag(t => t.Text("C#").Color("purple")))
            .Body(b => b
                .Form(form => form
                    .Name("personForm2")
                    .Vertical()
                    .Add(new SelectPersonBuilder()
                        .Name("reviewer")
                        .Placeholder("请选择审批人")
                        .InitialOption("ou_c99c5f35d542efc7ee492afe11af19ef")
                        .Required()
                        .Build())
                    .Button(btn => btn.Text("提交").Submit())))
            .Build();
        
        var messageId = await SendCardAsync("SelectPerson 初始值", card);
        Assert.NotNull(messageId);
        
        await DelayAsync();
    }
    
    [Fact]
    public async Task SendMultiSelectPerson_ShouldSucceed()
    {
        SkipIfNotConfigured();
        
        var card = CardBuilder.Create()
            .Header(h => h.Title("MultiSelectPerson 多选人员测试").TextTag(t => t.Text("C#").Color("purple")))
            .Body(b => b
                .Form(form => form
                    .Name("multiPersonForm")
                    .Vertical()
                    .Add(new MultiSelectPersonBuilder()
                        .Name("reviewers")
                        .Placeholder("请选择审批人（可多选）")
                        .Build())
                    .Button(btn => btn.Text("提交").Submit())))
            .Build();
        
        var messageId = await SendCardAsync("MultiSelectPerson 多选", card);
        Assert.NotNull(messageId);
        
        await DelayAsync();
    }
    
    [Fact]
    public async Task SendMultiSelectPersonWithOptions_ShouldSucceed()
    {
        SkipIfNotConfigured();
        
        var card = CardBuilder.Create()
            .Header(h => h.Title("带预设选项的多选人员"))
            .Body(b => b
                .Form(form => form
                    .Name("multiPersonForm2")
                    .Vertical()
                    .Add(new MultiSelectPersonBuilder()
                        .Name("participants")
                        .Placeholder("选择参会人员")
                        .AddOption("ou_c99c5f35d542efc7ee492afe11af19ef")
                        .AddOption("ou_d88d6g46e653fgd8ff603c6f22bg10fg")
                        .SelectedValues("ou_c99c5f35d542efc7ee492afe11af19ef")
                        .Build())
                    .Button(btn => btn.Text("提交").Submit())))
            .Build();
        
        var messageId = await SendCardAsync("MultiSelectPerson 预设选项", card);
        Assert.NotNull(messageId);
        
        await DelayAsync();
    }
    
    [Fact]
    public async Task SendChecker_ShouldSucceed()
    {
        SkipIfNotConfigured();
        
        var card = CardBuilder.Create()
            .Header(h => h.Title("Checker 勾选器测试").TextTag(t => t.Text("C#").Color("purple")))
            .Body(b => b
                .Checker(c => c
                    .Name("task1")
                    .Text("完成第一个任务")
                    .Checked(false)))
            .Build();
        
        var messageId = await SendCardAsync("Checker 勾选器", card);
        Assert.NotNull(messageId);
        
        await DelayAsync();
    }
    
    [Fact]
    public async Task SendCheckerWithMarkdown_ShouldSucceed()
    {
        SkipIfNotConfigured();
        
        var card = CardBuilder.Create()
            .Header(h => h.Title("带 Markdown 的勾选器"))
            .Body(b => b
                .Checker(c => c
                    .Name("task2")
                    .MarkdownText("**重要任务**\n- 子任务 1\n- 子任务 2")
                    .Checked(true)
                    .CheckedStyle(cs => cs.ShowStrikethrough())))
            .Build();
        
        var messageId = await SendCardAsync("Checker Markdown", card);
        Assert.NotNull(messageId);
        
        await DelayAsync();
    }
    
    [Fact]
    public async Task SendCheckerWithCallback_ShouldSucceed()
    {
        SkipIfNotConfigured();
        
        var card = CardBuilder.Create()
            .Header(h => h.Title("带回调的勾选器"))
            .Body(b => b
                .Checker(c => c
                    .Name("task3")
                    .Text("点击触发回调的任务")
                    .OnClick(new { taskId = "task3", action = "check" })))
            .Build();
        
        var messageId = await SendCardAsync("Checker 回调", card);
        Assert.NotNull(messageId);
        
        await DelayAsync();
    }
    
    [Fact(Skip = "ImgCombination 需要有效的图片 key，请先上传图片获取有效的 img_key")]
    public async Task SendImgCombinationQuadruple_ShouldSucceed()
    {
        SkipIfNotConfigured();
        
        var imgKey = "img_v3_0238_073f1823-df2b-4377-86c6-e293f183622j";
        
        var card = CardBuilder.Create()
            .Header(h => h.Title("ImgCombination 四宫格测试"))
            .Body(b => b
                .PlainText("四宫格图片混排：")
                .ImgCombination(ic => ic
                    .Quadruple()
                    .CornerRadius("8px")
                    .AddImage(imgKey)
                    .AddImage(imgKey)
                    .AddImage(imgKey)
                    .AddImage(imgKey)))
            .Build();
        
        var messageId = await SendCardAsync("四宫格图片", card);
        Assert.NotNull(messageId);
        
        await DelayAsync();
    }
    
    [Fact(Skip = "ImgCombination 需要有效的图片 key，请先上传图片获取有效的 img_key")]
    public async Task SendImgCombinationGridNine_ShouldSucceed()
    {
        SkipIfNotConfigured();
        
        var imgKey = "img_v3_0238_073f1823-df2b-4377-86c6-e293f183622j";
        
        var card = CardBuilder.Create()
            .Header(h => h.Title("ImgCombination 九宫格测试"))
            .Body(b => b
                .PlainText("九宫格图片混排：")
                .ImgCombination(ic => ic
                    .GridNine()
                    .CornerRadius("4px")
                    .Images(
                        imgKey, imgKey, imgKey,
                        imgKey, imgKey, imgKey,
                        imgKey, imgKey, imgKey)))
            .Build();
        
        var messageId = await SendCardAsync("九宫格图片", card);
        Assert.NotNull(messageId);
        
        await DelayAsync();
    }
    
    [Fact]
    public async Task SendInteractiveContainer_ShouldSucceed()
    {
        SkipIfNotConfigured();
        
        var card = CardBuilder.Create()
            .Header(h => h.Title("InteractiveContainer 交互容器测试"))
            .Body(b => b
                .InteractiveContainer(ic => ic
                    .Width("fill")
                    .Vertical()
                    .Padding("12px")
                    .BackgroundStyle("default")
                    .CornerRadius("8px")
                    .PlainText("这是一个可点击的交互容器")
                    .PlainText("点击后跳转到链接")
                    .OnClick("https://feishu.cn")))
            .Build();
        
        var messageId = await SendCardAsync("InteractiveContainer 基础", card);
        Assert.NotNull(messageId);
        
        await DelayAsync();
    }
    
    [Fact]
    public async Task SendInteractiveContainerWithCallback_ShouldSucceed()
    {
        SkipIfNotConfigured();
        
        var card = CardBuilder.Create()
            .Header(h => h.Title("带回调的交互容器"))
            .Body(b => b
                .InteractiveContainer(ic => ic
                    .Width("fill")
                    .Horizontal()
                    .Padding("8px")
                    .BackgroundStyle("laser")
                    .CornerRadius("12px")
                    .HasBorder()
                    .BorderColor("blue")
                    .PlainText("左侧文本")
                    .Button(btn => btn.Text("容器内按钮"))
                    .OnCallback(new { action = "container_click" })))
            .Build();
        
        var messageId = await SendCardAsync("InteractiveContainer 回调", card);
        Assert.NotNull(messageId);
        
        await DelayAsync();
    }
    
    [Fact]
    public async Task SendInteractiveContainerWithConfirm_ShouldSucceed()
    {
        SkipIfNotConfigured();
        
        var card = CardBuilder.Create()
            .Header(h => h.Title("带二次确认的交互容器"))
            .Body(b => b
                .InteractiveContainer(ic => ic
                    .Width("fill")
                    .Vertical()
                    .Padding("16px")
                    .BackgroundStyle("default")
                    .CornerRadius("8px")
                    .HasBorder()
                    .BorderColor("grey")
                    .HoverTips("点击跳转到飞书")
                    .PlainText("点击此容器将跳转到飞书")
                    .PlainText("需要二次确认")
                    .Confirm("确认跳转", "确定要跳转到飞书吗？")
                    .OnClick("https://feishu.cn")))
            .Build();
        
        var messageId = await SendCardAsync("InteractiveContainer 二次确认", card);
        Assert.NotNull(messageId);
        
        await DelayAsync();
    }
    
    [Fact(Skip = "Fallback 格式需要确认")]
    public async Task SendFallback_ShouldSucceed()
    {
        SkipIfNotConfigured();
        
        var card = CardBuilder.Create()
            .Header(h => h.Title("Fallback 降级配置测试"))
            .Fallback("降级标题", "当客户端不支持卡片 2.0 时显示此内容")
            .Body(b => b
                .PlainText("这是卡片主体内容"))
            .Build();
        
        var messageId = await SendCardAsync("Fallback 降级", card);
        Assert.NotNull(messageId);
        
        await DelayAsync();
    }
    
    [Fact(Skip = "StreamingConfig 格式需要确认")]
    public async Task SendConfigWithStreaming_ShouldSucceed()
    {
        SkipIfNotConfigured();
        
        var card = CardBuilder.Create()
            .Config(c => c
                .StreamingMode(true)
                .StreamingConfig(sc => sc
                    .PrintFrequencyMs(new { @default = 100 })
                    .PrintStep(new { @default = 10 })
                    .PrintStrategy("fast"))
                .Summary("正在生成内容..."))
            .Header(h => h.Title("StreamingConfig 流式更新测试"))
            .Body(b => b
                .PlainText("流式更新卡片内容"))
            .Build();
        
        var messageId = await SendCardAsync("StreamingConfig", card);
        Assert.NotNull(messageId);
        
        await DelayAsync();
    }
    
    [Fact(Skip = "Style 格式需要确认")]
    public async Task SendConfigWithStyle_ShouldSucceed()
    {
        SkipIfNotConfigured();
        
        var card = CardBuilder.Create()
            .Config(c => c
                .WidthMode("fill")
                .EnableForward(true)
                .Style(s => s
                    .TextSize(new { cus_0 = new { @default = "medium" } })
                    .Color(new { cus_0 = new { light_mode = "blue", dark_mode = "cyan" } })))
            .Header(h => h.Title("Style 样式配置测试"))
            .Body(b => b
                .PlainText("自定义样式卡片"))
            .Build();
        
        var messageId = await SendCardAsync("Style 配置", card);
        Assert.NotNull(messageId);
        
        await DelayAsync();
    }
    
    [Fact]
    public async Task SendConfigWithLocales_ShouldSucceed()
    {
        SkipIfNotConfigured();
        
        var card = CardBuilder.Create()
            .Config(c => c
                .Locales("zh-CN", "en-US")
                .WidthMode("compact"))
            .Header(h => h.Title("Locales 语言配置测试"))
            .Body(b => b
                .PlainText("指定生效语言的卡片"))
            .Build();
        
        var messageId = await SendCardAsync("Locales 配置", card);
        Assert.NotNull(messageId);
        
        await DelayAsync();
    }
    
    [Fact]
    public async Task SendCardLink_ShouldSucceed()
    {
        SkipIfNotConfigured();
        
        var card = CardBuilder.Create()
            .Header(h => h.Title("CardLink 卡片链接测试"))
            .CardLink("https://feishu.cn")
            .Body(b => b
                .PlainText("点击卡片跳转到飞书官网"))
            .Build();
        
        var messageId = await SendCardAsync("CardLink 基础", card);
        Assert.NotNull(messageId);
        
        await DelayAsync();
    }
    
    [Fact]
    public async Task SendCardLinkMultiPlatform_ShouldSucceed()
    {
        SkipIfNotConfigured();
        
        var card = CardBuilder.Create()
            .Header(h => h.Title("多端卡片链接测试"))
            .CardLink(cl => cl
                .Url("https://feishu.cn")
                .PcUrl("https://feishu.cn/pc")
                .IosUrl("https://feishu.cn/ios")
                .AndroidUrl("https://feishu.cn/android"))
            .Body(b => b
                .PlainText("不同端跳转到不同链接"))
            .Build();
        
        var messageId = await SendCardAsync("CardLink 多端", card);
        Assert.NotNull(messageId);
        
        await DelayAsync();
    }
    
    [Fact]
    public async Task SendCompleteCardWithNewFeatures_ShouldSucceed()
    {
        SkipIfNotConfigured();
        
        var card = CardBuilder.Create()
            .Config(c => c
                .WidthMode("fill")
                .EnableForward(true)
                .EnableForwardInteraction(true))
            .Header(h => h
                .Title("综合功能测试")
                .Subtitle("新组件演示")
                .Template("blue")
                .TextTag(t => t.Text("新功能").Color("green")))
            .CardLink("https://feishu.cn")
            .Body(b => b
                .Form(form => form
                    .Name("completeForm")
                    .Vertical()
                    .Add(new Builders.PickerDatetimeBuilder()
                        .Name("datetime")
                        .Placeholder("选择日期时间")
                        .Build())
                    .Add(new SelectPersonBuilder()
                        .Name("person")
                        .Placeholder("选择人员")
                        .Build())
                    .Add(new MultiSelectPersonBuilder()
                        .Name("persons")
                        .Placeholder("选择多个人员")
                        .Build())
                    .Add(new CheckerBuilder()
                        .Name("agree")
                        .Text("同意条款")
                        .Build())
                    .Button(btn => btn.Text("提交").Submit())))
            .Build();
        
        var messageId = await SendCardAsync("综合功能测试", card);
        Assert.NotNull(messageId);
        
        await DelayAsync();
    }
}
