using System.Text.Json;
using FluentAssertions;
using LarkCardKit.Builders;
using LarkCardKit.Models;
using LarkCardKit.Models.Elements;
using LarkCardKit.Config;
using Xunit;

namespace LarkCardKit.Tests.Cards;

/// <summary>
/// 完整卡片构建测试 - 模拟真实场景
/// </summary>
public class CompleteCardTests
{
    private readonly JsonSerializerOptions _jsonOptions = JsonOptions.Default;

    /// <summary>
    /// 测试：构建一个简单的通知卡片
    /// </summary>
    [Fact]
    public void ShouldBuildNotificationCard()
    {
        var card = CardBuilder.Create()
            .WithTitle("系统通知")
            .WithSubtitle("2024-01-15 10:30")
            .WithIcon("blue")
            .AddMarkdown("您的申请已提交成功，请等待审批。")
            .AddHr()
            .AddMarkdown("**申请编号**: APP-2024-001\n**申请人**: 张三")
            .Build();

        var json = JsonSerializer.Serialize(card, _jsonOptions);
        var doc = JsonDocument.Parse(json);

        doc.RootElement.GetProperty("schema").GetString().Should().Be("2.0");
        doc.RootElement.GetProperty("header").GetProperty("title").GetProperty("content").GetString().Should().Be("系统通知");
        doc.RootElement.GetProperty("body").GetProperty("elements").GetArrayLength().Should().Be(3);
    }

    /// <summary>
    /// 测试：构建一个表单卡片
    /// </summary>
    [Fact]
    public void ShouldBuildFormCard()
    {
        var card = CardBuilder.Create()
            .WithTitle("用户信息表单")
            .WithWidthMode("fill")
            .AddForm(form => form
                .WithName("user-form")
                .AddInput(InputBuilder.Create()
                    .WithName("name")
                    .WithLabel("姓名")
                    .WithPlaceholder("请输入姓名")
                    .WithRequired(true)
                    .Build())
                .AddInput(InputBuilder.Create()
                    .WithName("email")
                    .WithLabel("邮箱")
                    .WithPlaceholder("请输入邮箱")
                    .WithInputType("email")
                    .WithRequired(true)
                    .Build())
                .AddSelect(SelectBuilder.Create()
                    .WithName("department")
                    .WithLabel("部门")
                    .WithPlaceholder("请选择部门")
                    .AddOption("tech", "技术部")
                    .AddOption("product", "产品部")
                    .AddOption("design", "设计部")
                    .Build())
                .AddButton(ButtonBuilder.Create()
                    .WithText("提交")
                    .WithType("primary")
                    .Build())
                .AddButton(ButtonBuilder.Create()
                    .WithText("取消")
                    .WithType("default")
                    .Build()))
            .Build();

        var json = JsonSerializer.Serialize(card, _jsonOptions);
        var doc = JsonDocument.Parse(json);

        doc.RootElement.GetProperty("body").GetProperty("elements")[0].GetProperty("tag").GetString().Should().Be("form");
    }

    /// <summary>
    /// 测试：构建一个审批卡片
    /// </summary>
    [Fact]
    public void ShouldBuildApprovalCard()
    {
        var card = CardBuilder.Create()
            .WithTitle("请假申请")
            .WithSubtitle("申请人: 李四")
            .WithIcon("blue")
            .AddMarkdown("### 申请详情")
            .AddMarkdown("**请假类型**: 年假\n**开始时间**: 2024-01-20\n**结束时间**: 2024-01-22\n**请假天数**: 3天")
            .AddHr()
            .AddMarkdown("### 请假事由")
            .AddMarkdown("家中有事需要处理，望批准。")
            .AddHr()
            .AddColumnSet(columnSet => columnSet
                .AddColumn(col => col
                    .WithWidth("50%")
                    .AddButton(ButtonBuilder.Create()
                        .WithText("批准")
                        .WithType("primary")
                        .WithCallback("approve", new { action = "approve" })
                        .Build()))
                .AddColumn(col => col
                    .WithWidth("50%")
                    .AddButton(ButtonBuilder.Create()
                        .WithText("拒绝")
                        .WithType("danger")
                        .WithCallback("reject", new { action = "reject" })
                        .Build())))
            .Build();

        var json = JsonSerializer.Serialize(card, _jsonOptions);
        var doc = JsonDocument.Parse(json);

        json.Should().Contain("approve");
        json.Should().Contain("reject");
    }

    /// <summary>
    /// 测试：构建一个数据展示卡片
    /// </summary>
    [Fact]
    public void ShouldBuildDataDisplayCard()
    {
        var card = CardBuilder.Create()
            .WithTitle("销售数据报表")
            .WithSubtitle("2024年1月")
            .AddTable(table => table
                .AddColumn("product", "产品名称", "150px")
                .AddColumn("sales", "销售额", "100px")
                .AddColumn("growth", "增长率", "80px")
                .AddRow(new Dictionary<string, object?> { ["product"] = "产品A", ["sales"] = "¥100,000", ["growth"] = "+15%" })
                .AddRow(new Dictionary<string, object?> { ["product"] = "产品B", ["sales"] = "¥80,000", ["growth"] = "+10%" })
                .AddRow(new Dictionary<string, object?> { ["product"] = "产品C", ["sales"] = "¥50,000", ["growth"] = "-5%" }))
            .AddHr()
            .AddChart(chart => chart
                .WithChartType("bar")
                .WithData(new
                {
                    categories = new[] { "产品A", "产品B", "产品C" },
                    values = new[] { 100, 80, 50 }
                })
                .WithWidth("fill"))
            .Build();

        var json = JsonSerializer.Serialize(card, _jsonOptions);
        var doc = JsonDocument.Parse(json);

        doc.RootElement.GetProperty("body").GetProperty("elements")[0].GetProperty("tag").GetString().Should().Be("table");
        doc.RootElement.GetProperty("body").GetProperty("elements")[2].GetProperty("tag").GetString().Should().Be("chart");
    }

    /// <summary>
    /// 测试：构建一个人员选择卡片
    /// </summary>
    [Fact]
    public void ShouldBuildPersonSelectionCard()
    {
        var card = CardBuilder.Create()
            .WithTitle("选择审批人")
            .AddSelectPerson(selectPerson => selectPerson
                .WithName("approver")
                .WithLabel("审批人")
                .WithPlaceholder("请选择审批人"))
            .AddHr()
            .AddMultiSelectPerson(multiSelectPerson => multiSelectPerson
                .WithName("cc_users")
                .WithLabel("抄送人")
                .WithPlaceholder("请选择抄送人"))
            .AddHr()
            .AddButton(ButtonBuilder.Create()
                .WithText("提交")
                .WithType("primary")
                .WithWidth("fill"))
            .Build();

        var json = JsonSerializer.Serialize(card, _jsonOptions);

        json.Should().Contain("select_person");
        json.Should().Contain("multi_select_person");
    }

    /// <summary>
    /// 测试：构建一个日期时间选择卡片
    /// </summary>
    [Fact]
    public void ShouldBuildDateTimePickerCard()
    {
        var card = CardBuilder.Create()
            .WithTitle("会议预约")
            .AddInput(InputBuilder.Create()
                .WithName("title")
                .WithLabel("会议主题")
                .WithPlaceholder("请输入会议主题")
                .WithRequired(true)
                .Build())
            .AddDatePicker(datePicker => datePicker
                .WithName("date")
                .WithLabel("会议日期")
                .WithPlaceholder("请选择日期"))
            .AddPickerTime(pickerTime => pickerTime
                .WithName("start_time")
                .WithLabel("开始时间")
                .WithPlaceholder("请选择开始时间"))
            .AddPickerTime(pickerTime => pickerTime
                .WithName("end_time")
                .WithLabel("结束时间")
                .WithPlaceholder("请选择结束时间"))
            .AddHr()
            .AddButton(ButtonBuilder.Create()
                .WithText("预约会议")
                .WithType("primary")
                .WithWidth("fill"))
            .Build();

        var json = JsonSerializer.Serialize(card, _jsonOptions);

        json.Should().Contain("date_picker");
        json.Should().Contain("picker_time");
    }

    /// <summary>
    /// 测试：构建一个折叠面板卡片
    /// </summary>
    [Fact]
    public void ShouldBuildCollapsiblePanelCard()
    {
        var card = CardBuilder.Create()
            .WithTitle("帮助文档")
            .AddCollapsiblePanel(panel => panel
                .WithTitle("如何创建申请？")
                .WithExpanded(true)
                .AddMarkdown("1. 点击右上角「新建申请」按钮\n2. 填写申请表单\n3. 提交等待审批"))
            .AddCollapsiblePanel(panel => panel
                .WithTitle("如何查看审批进度？")
                .WithExpanded(false)
                .AddMarkdown("在「我的申请」页面可以查看所有申请的审批进度。"))
            .AddCollapsiblePanel(panel => panel
                .WithTitle("如何撤回申请？")
                .WithExpanded(false)
                .AddMarkdown("在申请详情页面点击「撤回」按钮即可撤回申请。"))
            .Build();

        var json = JsonSerializer.Serialize(card, _jsonOptions);
        var doc = JsonDocument.Parse(json);

        var elements = doc.RootElement.GetProperty("body").GetProperty("elements");
        elements.GetArrayLength().Should().Be(3);
        elements[0].GetProperty("tag").GetString().Should().Be("collapsible_panel");
    }

    /// <summary>
    /// 测试：构建一个交互容器卡片
    /// </summary>
    [Fact]
    public void ShouldBuildInteractiveContainerCard()
    {
        var card = CardBuilder.Create()
            .WithTitle("任务列表")
            .AddInteractiveContainer(container => container
                .WithWidth("fill")
                .AddChecker(checker => checker
                    .WithName("task1")
                    .WithText("完成文档编写")
                    .WithChecked(true))
                .AddChecker(checker => checker
                    .WithName("task2")
                    .WithText("提交代码审核")
                    .WithChecked(false))
                .AddChecker(checker => checker
                    .WithName("task3")
                    .WithText("部署到测试环境")
                    .WithChecked(false)))
            .Build();

        var json = JsonSerializer.Serialize(card, _jsonOptions);

        json.Should().Contain("interactive_container");
        json.Should().Contain("checker");
    }

    /// <summary>
    /// 测试：构建一个图片选择卡片
    /// </summary>
    [Fact]
    public void ShouldBuildSelectImgCard()
    {
        var card = CardBuilder.Create()
            .WithTitle("选择头像")
            .AddSelectImg(selectImg => selectImg
                .WithName("avatar")
                .WithLabel("头像")
                .WithPlaceholder("请选择头像")
                .WithSelectMode("single")
                .AddOption("img_v2_avatar1", "头像1")
                .AddOption("img_v2_avatar2", "头像2")
                .AddOption("img_v2_avatar3", "头像3"))
            .AddHr()
            .AddButton(ButtonBuilder.Create()
                .WithText("确认选择")
                .WithType("primary"))
            .Build();

        var json = JsonSerializer.Serialize(card, _jsonOptions);

        json.Should().Contain("select_img");
    }

    /// <summary>
    /// 测试：构建一个带全局跳转的卡片
    /// </summary>
    [Fact]
    public void ShouldBuildCardWithCardLink()
    {
        var card = CardBuilder.Create()
            .WithTitle("点击卡片跳转")
            .WithCardLink(
                "https://mobile.example.com/detail",
                "https://pc.example.com/detail",
                "https://ios.example.com/detail",
                "https://android.example.com/detail")
            .AddMarkdown("点击卡片任意位置可跳转到详情页面。")
            .Build();

        var json = JsonSerializer.Serialize(card, _jsonOptions);
        var doc = JsonDocument.Parse(json);

        var cardLink = doc.RootElement.GetProperty("card_link");
        cardLink.GetProperty("url").GetString().Should().Be("https://mobile.example.com/detail");
        cardLink.GetProperty("pc_url").GetString().Should().Be("https://pc.example.com/detail");
    }

    /// <summary>
    /// 测试：构建一个带降级内容的卡片
    /// </summary>
    [Fact]
    public void ShouldBuildCardWithFallback()
    {
        var card = CardBuilder.Create()
            .WithTitle("新功能卡片")
            .AddMarkdown("这是新版本才支持的内容。")
            .WithFallback(fallback => fallback
                .WithTitle("旧版本提示")
                .WithContent("请升级到最新版本以查看完整内容。"))
            .Build();

        var json = JsonSerializer.Serialize(card, _jsonOptions);
        var doc = JsonDocument.Parse(json);

        doc.RootElement.TryGetProperty("fallback", out _).Should().BeTrue();
    }

    /// <summary>
    /// 测试：构建一个人员展示卡片
    /// </summary>
    [Fact]
    public void ShouldBuildPersonDisplayCard()
    {
        var card = CardBuilder.Create()
            .WithTitle("项目成员")
            .AddMarkdown("### 项目负责人")
            .AddPerson(person => person
                .WithOpenId("ou_leader")
                .WithShowName(true)
                .WithShowAvatar(true))
            .AddHr()
            .AddMarkdown("### 团队成员")
            .AddPersonList(personList => personList
                .AddOpenId("ou_member1")
                .AddOpenId("ou_member2")
                .AddOpenId("ou_member3"))
            .Build();

        var json = JsonSerializer.Serialize(card, _jsonOptions);

        json.Should().Contain("\"tag\":\"person\"");
        json.Should().Contain("\"tag\":\"person_list\"");
    }

    /// <summary>
    /// 测试：构建一个溢出菜单卡片
    /// </summary>
    [Fact]
    public void ShouldBuildOverflowMenuCard()
    {
        var card = CardBuilder.Create()
            .WithTitle("文档管理")
            .AddMarkdown("### 项目需求文档.pdf")
            .AddOverflow(overflow => overflow
                .AddOption("view", "查看")
                .AddOption("download", "下载")
                .AddOption("share", "分享")
                .AddOption("delete", "删除"))
            .Build();

        var json = JsonSerializer.Serialize(card, _jsonOptions);

        json.Should().Contain("overflow");
    }

    /// <summary>
    /// 测试：构建一个多图混排卡片
    /// </summary>
    [Fact]
    public void ShouldBuildImgCombinationCard()
    {
        var card = CardBuilder.Create()
            .WithTitle("产品展示")
            .AddImgCombination(combination => combination
                .WithCombinationMode("normal")
                .WithCornerRadius("8px")
                .AddImage("img_v2_product1")
                .AddImage("img_v2_product2")
                .AddImage("img_v2_product3"))
            .Build();

        var json = JsonSerializer.Serialize(card, _jsonOptions);

        json.Should().Contain("img_combination");
    }

    /// <summary>
    /// 测试：构建一个禁用状态的表单卡片
    /// </summary>
    [Fact]
    public void ShouldBuildDisabledFormCard()
    {
        var card = CardBuilder.Create()
            .WithTitle("已提交的表单（只读）")
            .AddInput(InputBuilder.Create()
                .WithName("name")
                .WithLabel("姓名")
                .WithDefaultValue("张三")
                .WithDisabled(true)
                .WithDisabledTips("表单已提交，不可修改")
                .Build())
            .AddSelect(SelectBuilder.Create()
                .WithName("department")
                .WithLabel("部门")
                .AddOption("tech", "技术部")
                .WithInitialOption("tech")
                .WithDisabled(true)
                .WithDisabledTips("表单已提交，不可修改")
                .Build())
            .Build();

        var json = JsonSerializer.Serialize(card, _jsonOptions);
        var doc = JsonDocument.Parse(json);

        var elements = doc.RootElement.GetProperty("body").GetProperty("elements");
        elements[0].GetProperty("disabled").GetBoolean().Should().BeTrue();
        elements[1].GetProperty("disabled").GetBoolean().Should().BeTrue();
    }

    /// <summary>
    /// 测试：完整卡片 JSON 输出格式验证
    /// </summary>
    [Fact]
    public void CardJson_ShouldHaveCorrectFormat()
    {
        var card = CardBuilder.Create()
            .WithTitle("格式验证卡片")
            .WithSubtitle("副标题")
            .WithIcon("blue")
            .WithWidthMode("fill")
            .WithEnableForward(true)
            .AddMarkdown("内容")
            .Build();

        var json = JsonSerializer.Serialize(card, _jsonOptions);
        var doc = JsonDocument.Parse(json);

        // 验证根结构
        doc.RootElement.GetProperty("schema").GetString().Should().Be("2.0");
        doc.RootElement.TryGetProperty("config", out _).Should().BeTrue();
        doc.RootElement.TryGetProperty("header", out _).Should().BeTrue();
        doc.RootElement.TryGetProperty("body", out _).Should().BeTrue();

        // 验证 header 结构
        var header = doc.RootElement.GetProperty("header");
        header.GetProperty("title").GetProperty("tag").GetString().Should().Be("lark_md");

        // 验证 body 结构
        var body = doc.RootElement.GetProperty("body");
        body.GetProperty("elements").GetArrayLength().Should().Be(1);
    }
}
