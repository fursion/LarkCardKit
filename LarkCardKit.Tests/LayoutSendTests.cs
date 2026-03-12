using LarkCardKit.Builders;
using LarkCardKit.Enums;
using Xunit;

namespace LarkCardKit.Tests;

[Trait("Category", "Integration")]
public class LayoutSendTests : IntegrationTestBase
{
    [Fact]
    public async Task SendDivContainer_ShouldSucceed()
    {
        SkipIfNotConfigured();
        
        var card = CardBuilder.Create()
            .Header(h => h.Title("Div 容器测试").TextTag(t => t.Text("C#").Color("purple")))
            .Body(b => b
                .Div(div => div
                    .Text("这是 Div 容器中的文本内容")))
            .Build();
        
        var messageId = await SendCardAsync("Div 容器", card);
        Assert.NotNull(messageId);
        
        await DelayAsync();
    }
    
    [Fact]
    public async Task SendDivWithMarkdown_ShouldSucceed()
    {
        SkipIfNotConfigured();

        var card = CardBuilder.Create()
            .Header(h => h.Title("Div + Markdown 测试").TextTag(t => t.Text("C#").Color("purple")))
            .Body(b => b
                .Markdown("**粗体标题**\n- 项目 1\n- 项目 2"))
            .Build();

        var messageId = await SendCardAsync("Div + Markdown", card);
        Assert.NotNull(messageId);

        await DelayAsync();
    }
    
    [Fact]
    public async Task SendColumnSetTwoColumns_ShouldSucceed()
    {
        SkipIfNotConfigured();
        
        var card = CardBuilder.Create()
            .Header(h => h.Title("ColumnSet 两栏布局测试").TextTag(t => t.Text("C#").Color("purple")))
            .Body(b => b
                .ColumnSet(cs => cs
                    .HorizontalSpacing("8px")
                    .AddColumn(col => col
                        .Width("50%")
                        .PlainText("左侧列内容"))
                    .AddColumn(col => col
                        .Width("50%")
                        .PlainText("右侧列内容"))))
            .Build();
        
        var messageId = await SendCardAsync("ColumnSet 两栏", card);
        Assert.NotNull(messageId);
        
        await DelayAsync();
    }
    
    [Fact]
    public async Task SendColumnSetThreeColumns_ShouldSucceed()
    {
        SkipIfNotConfigured();
        
        var card = CardBuilder.Create()
            .Header(h => h.Title("ColumnSet 三栏布局测试").TextTag(t => t.Text("C#").Color("purple")))
            .Body(b => b
                .ColumnSet(cs => cs
                    .AddColumn(col => col.Width("33%").Markdown("**列 1**\n第一列内容"))
                    .AddColumn(col => col.Width("33%").Markdown("**列 2**\n第二列内容"))
                    .AddColumn(col => col.Width("34%").Markdown("**列 3**\n第三列内容"))))
            .Build();
        
        var messageId = await SendCardAsync("ColumnSet 三栏", card);
        Assert.NotNull(messageId);
        
        await DelayAsync();
    }
    
    [Fact]
    public async Task SendFormContainer_ShouldSucceed()
    {
        SkipIfNotConfigured();
        
        var card = CardBuilder.Create()
            .Header(h => h.Title("Form 容器测试").TextTag(t => t.Text("C#").Color("purple")))
            .Body(b => b
                .Form(form => form
                    .Name("testForm")
                    .Vertical()
                    .Input(input => input
                        .Name("field1")
                        .Label("字段 1")
                        .Placeholder("请输入"))
                    .Button(btn => btn.Text("提交").Submit())))
            .Build();
        
        var messageId = await SendCardAsync("Form 容器", card);
        Assert.NotNull(messageId);
        
        await DelayAsync();
    }
    
    [Fact]
    public async Task SendNestedLayout_ShouldSucceed()
    {
        SkipIfNotConfigured();
        
        var card = CardBuilder.Create()
            .Header(h => h.Title("嵌套布局测试").TextTag(t => t.Text("C#").Color("purple")))
            .Body(b => b
                .ColumnSet(cs => cs
                    .AddColumn(col => col
                        .Width("50%")
                        .PlainText("左侧内容"))
                    .AddColumn(col => col
                        .Width("50%")
                        .PlainText("右侧内容"))))
            .Build();
        
        var messageId = await SendCardAsync("嵌套布局", card);
        Assert.NotNull(messageId);
        
        await DelayAsync();
    }
}
