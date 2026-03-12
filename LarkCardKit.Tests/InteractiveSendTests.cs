using LarkCardKit.Builders;
using LarkCardKit.Enums;
using LarkCardKit.Models;
using Xunit;

namespace LarkCardKit.Tests;

[Trait("Category", "Integration")]
public class InteractiveSendTests : IntegrationTestBase
{
    [Fact]
    public async Task SendButton_ShouldSucceed()
    {
        SkipIfNotConfigured();
        
        var card = CardBuilder.Create()
            .Header(h => h.Title("Button 按钮测试").TextTag(t => t.Text("C#").Color("purple")))
            .Body(b => b
                .Button(btn => btn
                    .Text("默认按钮"))
                .Button(btn => btn
                    .Text("主要按钮")
                    .Type(ButtonType.Primary))
                .Button(btn => btn
                    .Text("危险按钮")
                    .Type(ButtonType.Danger)))
            .Build();
        
        var messageId = await SendCardAsync("Button 按钮", card);
        Assert.NotNull(messageId);
        
        await DelayAsync();
    }
    
    [Fact]
    public async Task SendInput_ShouldSucceed()
    {
        SkipIfNotConfigured();
        
        var card = CardBuilder.Create()
            .Header(h => h.Title("Input 输入框测试").TextTag(t => t.Text("C#").Color("purple")))
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
                    .Input(input => input
                        .Name("password")
                        .Label("密码")
                        .Placeholder("请输入密码")
                        .Type(InputType.Password))
                    .Button(btn => btn.Text("提交").Submit())))
            .Build();
        
        var messageId = await SendCardAsync("Input 输入框", card);
        Assert.NotNull(messageId);
        
        await DelayAsync();
    }
    
    [Fact]
    public async Task SendSelect_ShouldSucceed()
    {
        SkipIfNotConfigured();
        
        var card = CardBuilder.Create()
            .Header(h => h.Title("Select 选择器测试").TextTag(t => t.Text("C#").Color("purple")))
            .Body(b => b
                .Form(form => form
                    .Name("selectForm")
                    .Vertical()
                    .Select(select => select
                        .Name("city")
                        .Placeholder("请选择城市")
                        .AddOption("beijing", "北京")
                        .AddOption("shanghai", "上海")
                        .AddOption("guangzhou", "广州")
                        .AddOption("shenzhen", "深圳"))
                    .Button(btn => btn.Text("提交").Submit())))
            .Build();
        
        var messageId = await SendCardAsync("Select 选择器", card);
        Assert.NotNull(messageId);
        
        await DelayAsync();
    }
    
    [Fact]
    public async Task SendSelectWithOptionsInfo_ShouldSucceed()
    {
        SkipIfNotConfigured();
        
        var options = new[]
        {
            new SelectOptionInfo("opt1", "选项一", "这是选项一的描述"),
            new SelectOptionInfo("opt2", "选项二", "这是选项二的描述"),
            ("opt3", "选项三")
        };
        
        var card = CardBuilder.Create()
            .Header(h => h.Title("Select 使用 OptionsInfo 测试").TextTag(t => t.Text("C#").Color("purple")))
            .Body(b => b
                .Form(form => form
                    .Name("selectForm2")
                    .Vertical()
                    .Select(select => select
                        .Name("option")
                        .Placeholder("请选择")
                        .AddOptions(options))
                    .Button(btn => btn.Text("提交").Submit())))
            .Build();
        
        var messageId = await SendCardAsync("Select OptionsInfo", card);
        Assert.NotNull(messageId);
        
        await DelayAsync();
    }
    
    [Fact]
    public async Task SendMultiSelect_ShouldSucceed()
    {
        SkipIfNotConfigured();
        
        var card = CardBuilder.Create()
            .Header(h => h.Title("MultiSelect 多选测试").TextTag(t => t.Text("C#").Color("purple")))
            .Body(b => b
                .Form(form => form
                    .Name("multiSelectForm")
                    .Vertical()
                    .Select(select => select
                        .Name("hobbies")
                        .Placeholder("请选择爱好（可多选）")
                        .MultiSelect()
                        .AddOption("reading", "阅读")
                        .AddOption("gaming", "游戏")
                        .AddOption("music", "音乐")
                        .AddOption("sports", "运动"))
                    .Button(btn => btn.Text("提交").Submit())))
            .Build();
        
        var messageId = await SendCardAsync("MultiSelect 多选", card);
        Assert.NotNull(messageId);
        
        await DelayAsync();
    }
    
    [Fact]
    public async Task SendDatePicker_ShouldSucceed()
    {
        SkipIfNotConfigured();
        
        var card = CardBuilder.Create()
            .Header(h => h.Title("DatePicker 日期选择器测试").TextTag(t => t.Text("C#").Color("purple")))
            .Body(b => b
                .Form(form => form
                    .Name("dateForm")
                    .Vertical()
                    .DatePicker(dp => dp
                        .Name("birthday")
                        .Placeholder("请选择出生日期")
                        .InitialDate("2000-01-01"))
                    .Button(btn => btn.Text("提交").Submit())))
            .Build();
        
        var messageId = await SendCardAsync("DatePicker", card);
        Assert.NotNull(messageId);
        
        await DelayAsync();
    }
    
    [Fact]
    public async Task SendCheckbox_ShouldSucceed()
    {
        SkipIfNotConfigured();
        
        var card = CardBuilder.Create()
            .Header(h => h.Title("Checkbox 多选框测试").TextTag(t => t.Text("C#").Color("purple")))
            .Body(b => b
                .Form(form => form
                    .Name("checkboxForm")
                    .Vertical()
                    .Checkbox(cb => cb
                        .Name("interests")
                        .Placeholder("请选择兴趣")
                        .AddOption("tech", "技术")
                        .AddOption("design", "设计")
                        .AddOption("product", "产品")
                        .OnChange(new { action = "select" }))
                    .Button(btn => btn.Text("提交").Submit())))
            .Build();
        
        var messageId = await SendCardAsync("Checkbox", card);
        Assert.NotNull(messageId);
        
        await DelayAsync();
    }
    
    [Fact]
    public async Task SendCheckboxWithOptionsInfo_ShouldSucceed()
    {
        SkipIfNotConfigured();
        
        var options = new[]
        {
            new CheckboxOptionInfo("cb1", "复选框一"),
            new CheckboxOptionInfo("cb2", "复选框二"),
            ("cb3", "复选框三")
        };
        
        var card = CardBuilder.Create()
            .Header(h => h.Title("Checkbox 使用 OptionsInfo 测试").TextTag(t => t.Text("C#").Color("purple")))
            .Body(b => b
                .Form(form => form
                    .Name("checkboxForm2")
                    .Vertical()
                    .Checkbox(cb => cb
                        .Name("items")
                        .Placeholder("请选择项目")
                        .AddOptions(options))
                    .Button(btn => btn.Text("提交").Submit())))
            .Build();
        
        var messageId = await SendCardAsync("Checkbox OptionsInfo", card);
        Assert.NotNull(messageId);
        
        await DelayAsync();
    }
}
