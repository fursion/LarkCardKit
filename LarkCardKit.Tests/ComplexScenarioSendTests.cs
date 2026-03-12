using LarkCardKit.Builders;
using LarkCardKit.Enums;
using LarkCardKit.Models;
using LarkCardKit.Models.Elements;
using LarkCardKit.Templates;
using Xunit;

namespace LarkCardKit.Tests;

[Trait("Category", "Integration")]
public class ComplexScenarioSendTests : IntegrationTestBase
{
    [Fact]
    public async Task SendComplexForm_ShouldSucceed()
    {
        SkipIfNotConfigured();
        
        var card = CardBuilder.Create()
            .Header(h => h.Title("用户信息收集表单").TextTag(t => t.Text("C#").Color("purple")))
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
                        .Placeholder("请选择性别")
                        .AddOption("male", "男")
                        .AddOption("female", "女"))
                    .DatePicker(dp => dp
                        .Name("birthday")
                        .Placeholder("请选择出生日期")
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
        
        var messageId = await SendCardAsync("综合表单", card);
        Assert.NotNull(messageId);
        
        await DelayAsync();
    }
    
    [Fact]
    public async Task SendComplexLayout_ShouldSucceed()
    {
        SkipIfNotConfigured();
        
        var card = CardBuilder.Create()
            .Header(h => h.Title("复杂布局示例").TextTag(t => t.Text("C#").Color("purple")))
            .Body(b => b
                .ColumnSet(cs => cs
                    .AddColumn(col => col
                        .Width("33%")
                        .Markdown("**列 1**\n第一列内容"))
                    .AddColumn(col => col
                        .Width("33%")
                        .Markdown("**列 2**\n第二列内容"))
                    .AddColumn(col => col
                        .Width("34%")
                        .Markdown("**列 3**\n第三列内容")))
                .Div(div => div.Text("分隔线下方内容"))
                .ColumnSet(cs => cs
                    .AddColumn(col => col
                        .Width("50%")
                        .Button(btn => btn.Text("左侧按钮").Type(ButtonType.Primary)))
                    .AddColumn(col => col
                        .Width("50%")
                        .Button(btn => btn.Text("右侧按钮").Type(ButtonType.Danger)))))
            .Build();
        
        var messageId = await SendCardAsync("复杂布局", card);
        Assert.NotNull(messageId);
        
        await DelayAsync();
    }
    
    [Fact]
    public async Task SendCardWithFindAndModify_ShouldSucceed()
    {
        SkipIfNotConfigured();
        
        var builder = CardBuilder.Create()
            .Header(h => h.Title("查找修改测试").TextTag(t => t.Text("C#").Color("purple")))
            .Body(b => b
                .PlainText("原始文本")
                .Button(btn => btn
                    .Text("原始按钮")
                    .ElementId("testButton")));
        
        var found = builder.FindElementById("testButton");
        Assert.NotNull(found);
        
        var modified = builder.ModifyElement<Button>("testButton", btn =>
        {
            btn.Text = new PlainText { Content = "修改后的按钮" };
        });
        Assert.True(modified);
        
        var card = builder.Build();
        var messageId = await SendCardAsync("查找修改", card);
        Assert.NotNull(messageId);
        
        await DelayAsync();
    }
    
    [Fact]
    public async Task SendCardWithReplace_ShouldSucceed()
    {
        SkipIfNotConfigured();
        
        var builder = CardBuilder.Create()
            .Header(h => h.Title("替换测试").TextTag(t => t.Text("C#").Color("purple")))
            .Body(b => b
                .PlainText("这段文本将被保留")
                .Button(btn => btn
                    .Text("原始按钮")
                    .ElementId("replaceButton")));
        
        var newButton = new Button
        {
            Text = new PlainText { Content = "替换后的按钮" },
            Type = ButtonType.Primary.ToString().ToLower()
        };
        
        var replaced = builder.ReplaceElement("replaceButton", newButton);
        Assert.True(replaced);
        
        var card = builder.Build();
        var messageId = await SendCardAsync("替换组件", card);
        Assert.NotNull(messageId);
        
        await DelayAsync();
    }
    
    [Fact]
    public async Task SendNestedComponentOperations_ShouldSucceed()
    {
        SkipIfNotConfigured();
        
        var builder = CardBuilder.Create()
            .Header(h => h.Title("嵌套组件操作测试").TextTag(t => t.Text("C#").Color("purple")))
            .Body(b => b
                .Form(form => form
                    .Name("nestedForm")
                    .Vertical()
                    .Input(input => input
                        .Name("field1")
                        .Placeholder("字段 1")
                        .ElementId("nestedInput"))
                    .Button(btn => btn.Text("提交").Submit())));
        
        var found = builder.FindElementById("nestedInput");
        Assert.NotNull(found);
        Assert.IsType<Input>(found);
        
        var modified = builder.ModifyElement<Input>("nestedInput", input =>
        {
            input.Placeholder = new PlainText { Content = "修改后的占位符" };
        });
        Assert.True(modified);
        
        var card = builder.Build();
        var messageId = await SendCardAsync("嵌套组件操作", card);
        Assert.NotNull(messageId);
        
        await DelayAsync();
    }
    
    [Fact]
    public async Task SendCardWithAllNewFeatures_ShouldSucceed()
    {
        SkipIfNotConfigured();
        
        var users = new[]
        {
            new { Id = "1", Name = "张三" },
            new { Id = "2", Name = "李四" },
            new { Id = "3", Name = "王五" }
        };
        
        var builder = CardBuilder.Create()
            .Header(h => h.Title("全部新功能测试 - ${title}").TextTag(t => t.Text("C#").Color("purple")))
            .Body(b => b
                .PlainText("测试对象参数配置选项：")
                .Form(form => form
                    .Name("allFeaturesForm")
                    .Vertical()
                    .Select(select => select
                        .Name("userSelect")
                        .Placeholder("选择用户")
                        .Options(users, u => new SelectOptionInfo(u.Id, u.Name)))
                    .Checkbox(cb => cb
                        .Name("features")
                        .Placeholder("选择功能")
                        .AddOptions(new[]
                        {
                            new CheckboxOptionInfo("feat1", "功能一"),
                            new CheckboxOptionInfo("feat2", "功能二")
                        }))
                    .Button(btn => btn
                        .Text("提交")
                        .Type(new TemplateValue("buttonType"))
                        .Submit()
                        .ElementId("submitButton")))
                .ColumnSet(cs => cs
                    .AddColumn(col => col
                        .Width("50%")
                        .Markdown("左侧内容"))
                    .AddColumn(col => col
                        .Width("50%")
                        .Markdown("右侧内容"))))
            .SetParameter("title", "综合测试")
            .SetParameter("buttonType", ButtonType.Primary);
        
        var found = builder.FindElementById("submitButton");
        Assert.NotNull(found);
        
        var card = builder.Build();
        var messageId = await SendCardAsync("全部新功能", card);
        Assert.NotNull(messageId);
        
        await DelayAsync();
    }
}
