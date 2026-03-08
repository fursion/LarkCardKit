using LarkCardKit.Builders;
using Xunit;

namespace LarkCardKit.Tests;

[Trait("Category", "Integration")]
public class NewComponentsSendTests : IntegrationTestBase
{
    [Fact]
    public async Task SendHr_ShouldSucceed()
    {
        SkipIfNotConfigured();
        
        var card = CardBuilder.Create()
            .Header(h => h.Title("分割线测试"))
            .Body(b => b
                .PlainText("内容上方")
                .Hr()
                .PlainText("内容下方"))
            .Build();
        
        var messageId = await SendCardAsync("分割线", card);
        Assert.NotNull(messageId);
        
        await DelayAsync();
    }
    
    [Fact]
    public async Task SendHrWithMargin_ShouldSucceed()
    {
        SkipIfNotConfigured();
        
        var card = CardBuilder.Create()
            .Header(h => h.Title("带边距的分割线测试"))
            .Body(b => b
                .PlainText("内容上方")
                .Hr(hr => hr.Margin("16px 0"))
                .PlainText("内容下方"))
            .Build();
        
        var messageId = await SendCardAsync("带边距分割线", card);
        Assert.NotNull(messageId);
        
        await DelayAsync();
    }
    
    [Fact]
    public async Task SendPickerTime_ShouldSucceed()
    {
        SkipIfNotConfigured();
        
        var card = CardBuilder.Create()
            .Header(h => h.Title("时间选择器测试"))
            .Body(b => b
                .PickerTime(pt => pt
                    .Placeholder("请选择时间")
                    .InitialTime("09:00")))
            .Build();
        
        var messageId = await SendCardAsync("时间选择器", card);
        Assert.NotNull(messageId);
        
        await DelayAsync();
    }
    
    [Fact]
    public async Task SendPickerTimeInForm_ShouldSucceed()
    {
        SkipIfNotConfigured();
        
        var card = CardBuilder.Create()
            .Header(h => h.Title("表单中的时间选择器"))
            .Body(b => b
                .Form(form => form
                    .Name("timeForm")
                    .Vertical()
                    .PickerTime(pt => pt
                        .Name("startTime")
                        .Placeholder("选择开始时间")
                        .Required())
                    .PickerTime(pt => pt
                        .Name("endTime")
                        .Placeholder("选择结束时间")
                        .InitialTime("18:00"))
                    .Button(btn => btn.Text("提交").Submit())))
            .Build();
        
        var messageId = await SendCardAsync("表单时间选择器", card);
        Assert.NotNull(messageId);
        
        await DelayAsync();
    }
    
    [Fact]
    public async Task SendCardWithHrAndPickerTime_ShouldSucceed()
    {
        SkipIfNotConfigured();
        
        var card = CardBuilder.Create()
            .Header(h => h.Title("综合测试：分割线 + 时间选择器"))
            .Body(b => b
                .PlainText("请选择会议时间：")
                .Hr(hr => hr.Margin("8px 0"))
                .PickerTime(pt => pt
                    .Placeholder("选择会议时间")
                    .InitialTime("14:00"))
                .Hr(hr => hr.Margin("8px 0"))
                .Markdown("选择完成后请点击确认"))
            .Build();
        
        var messageId = await SendCardAsync("综合测试", card);
        Assert.NotNull(messageId);
        
        await DelayAsync();
    }
    
    [Fact]
    public async Task SendHeaderWithTextTags_ShouldSucceed()
    {
        SkipIfNotConfigured();
        
        var card = CardBuilder.Create()
            .Header(h => h
                .Title("任务状态")
                .Subtitle("项目进度跟踪")
                .Template("blue")
                .TextTag(t => t.Text("进行中").Color("blue"))
                .TextTag(t => t.Text("紧急").Color("red")))
            .Body(b => b
                .PlainText("这是一个带有后缀标签的卡片标题"))
            .Build();
        
        var messageId = await SendCardAsync("后缀标签测试", card);
        Assert.NotNull(messageId);
        
        await DelayAsync();
    }
    
    [Fact]
    public async Task SendHeaderWithThreeTextTags_ShouldSucceed()
    {
        SkipIfNotConfigured();
        
        var card = CardBuilder.Create()
            .Header(h => h
                .Title("项目状态")
                .Template("green")
                .TextTag(t => t.Text("已完成").Color("green"))
                .TextTag(t => t.Text("已审核").Color("turquoise"))
                .TextTag(t => t.Text("已归档").Color("neutral")))
            .Body(b => b
                .PlainText("项目已完成所有流程"))
            .Build();
        
        var messageId = await SendCardAsync("三个后缀标签", card);
        Assert.NotNull(messageId);
        
        await DelayAsync();
    }
    
    [Fact]
    public async Task SendHeaderWithTemplateAndSubtitle_ShouldSucceed()
    {
        SkipIfNotConfigured();
        
        var card = CardBuilder.Create()
            .Header(h => h
                .Title("系统通知")
                .Subtitle("2024-01-15 10:30")
                .Template("orange"))
            .Body(b => b
                .Markdown("**重要提醒**\n系统将于今晚 22:00 进行维护"))
            .Build();
        
        var messageId = await SendCardAsync("主题和副标题", card);
        Assert.NotNull(messageId);
        
        await DelayAsync();
    }
    
    [Fact]
    public async Task SendBasicTable_ShouldSucceed()
    {
        SkipIfNotConfigured();
        
        var card = CardBuilder.Create()
            .Header(h => h.Title("客户数据表"))
            .Body(b => b
                .Table(table => table
                    .PageSize(5)
                    .Column(col => col.Name("name").DisplayName("客户名称").DataType("text"))
                    .Column(col => col.Name("amount").DisplayName("金额(万元)").DataType("number"))
                    .Row(row => row.TextCell("name", "飞书科技").NumberCell("amount", 168))
                    .Row(row => row.TextCell("name", "字节跳动").NumberCell("amount", 256.8))))
            .Build();
        
        var messageId = await SendCardAsync("基本表格", card);
        Assert.NotNull(messageId);
        
        await DelayAsync();
    }
    
    [Fact]
    public async Task SendTableWithOptions_ShouldSucceed()
    {
        SkipIfNotConfigured();
        
        var card = CardBuilder.Create()
            .Header(h => h.Title("项目状态表"))
            .Body(b => b
                .Table(table => table
                    .PageSize(5)
                    .HeaderStyle(hs => hs.BackgroundStyle("grey").Bold())
                    .Column(col => col.Name("project").DisplayName("项目名称").DataType("text"))
                    .Column(col => col.Name("status").DisplayName("状态").DataType("options"))
                    .Column(col => col.Name("priority").DisplayName("优先级").DataType("options"))
                    .Row(row => row
                        .TextCell("project", "飞书卡片 SDK")
                        .OptionCell("status", "进行中", "blue")
                        .OptionCell("priority", "高", "red"))
                    .Row(row => row
                        .TextCell("project", "API 文档")
                        .OptionCell("status", "已完成", "green")
                        .OptionCell("priority", "中", "orange"))))
            .Build();
        
        var messageId = await SendCardAsync("选项标签表格", card);
        Assert.NotNull(messageId);
        
        await DelayAsync();
    }
    
    [Fact]
    public async Task SendTableWithNumberFormat_ShouldSucceed()
    {
        SkipIfNotConfigured();
        
        var card = CardBuilder.Create()
            .Header(h => h.Title("销售数据表"))
            .Body(b => b
                .Table(table => table
                    .PageSize(5)
                    .FreezeFirstColumn()
                    .Column(col => col.Name("product").DisplayName("产品").DataType("text").Width("120px"))
                    .Column(col => col.Name("revenue").DisplayName("收入").DataType("number")
                        .NumberFormat(f => f.Symbol("¥").Precision(2).Separator()))
                    .Column(col => col.Name("growth").DisplayName("增长率").DataType("number")
                        .NumberFormat(f => f.Precision(1)))
                    .Row(row => row
                        .TextCell("product", "企业版")
                        .NumberCell("revenue", 1688888.88)
                        .NumberCell("growth", 25.5))
                    .Row(row => row
                        .TextCell("product", "专业版")
                        .NumberCell("revenue", 888888.50)
                        .NumberCell("growth", 18.3))))
            .Build();
        
        var messageId = await SendCardAsync("数字格式表格", card);
        Assert.NotNull(messageId);
        
        await DelayAsync();
    }
    
    [Fact]
    public async Task SendCompleteTable_ShouldSucceed()
    {
        SkipIfNotConfigured();
        
        var card = CardBuilder.Create()
            .Header(h => h
                .Title("完整表格示例")
                .Template("blue"))
            .Body(b => b
                .Table(table => table
                    .ElementId("customerTable")
                    .PageSize(5)
                    .RowHeight("auto")
                    .RowMaxHeight("50px")
                    .FreezeFirstColumn()
                    .HeaderStyle(hs => hs
                        .TextAlign("left")
                        .BackgroundStyle("grey")
                        .Bold())
                    .Column(col => col
                        .Name("customer_name")
                        .DisplayName("客户名称")
                        .DataType("text")
                        .Width("auto"))
                    .Column(col => col
                        .Name("customer_scale")
                        .DisplayName("客户规模")
                        .DataType("options"))
                    .Column(col => col
                        .Name("customer_arr")
                        .DisplayName("ARR(万元)")
                        .DataType("number")
                        .NumberFormat(f => f.Symbol("¥").Precision(2).Separator()))
                    .Row(row => row
                        .TextCell("customer_name", "飞书科技")
                        .OptionCell("customer_scale", "S2", "blue")
                        .NumberCell("customer_arr", 168.5))
                    .Row(row => row
                        .TextCell("customer_name", "字节跳动")
                        .OptionCell("customer_scale", "S1", "red")
                        .NumberCell("customer_arr", 256.8))
                    .Row(row => row
                        .TextCell("customer_name", "抖音集团")
                        .OptionsCell("customer_scale", ("S1", "red"), ("KA", "orange"))
                        .NumberCell("customer_arr", 388.25))))
            .Build();
        
        var messageId = await SendCardAsync("完整表格", card);
        Assert.NotNull(messageId);
        
        await DelayAsync();
    }
}
