using LarkCardKit.Builders;
using LarkCardKit.Enums;
using LarkCardKit.Models;
using LarkCardKit.Templates;
using Xunit;

namespace LarkCardKit.Tests;

[Trait("Category", "Integration")]
public class TemplateSendTests : IntegrationTestBase
{
    [Fact]
    public async Task SendTemplateParameterFill_ShouldSucceed()
    {
        SkipIfNotConfigured();
        
        var card = CardBuilder.Create()
            .Header(h => h.Title("欢迎 ${userName}").TextTag(t => t.Text("C#").Color("purple")))
            .Body(b => b
                .PlainText("您好，${userName}！")
                .Markdown("您的订单 **${orderId}** 已发货。")
                .PlainText("预计 ${deliveryDate:3天内} 送达"))
            .SetParameter("userName", "张三")
            .SetParameter("orderId", "ORD-2024-001")
            .SetParameter("deliveryDate", "明天")
            .Build();
        
        var messageId = await SendCardAsync("模板参数填充", card);
        Assert.NotNull(messageId);
        
        await DelayAsync();
    }
    
    [Fact]
    public async Task SendTemplateWithObjectParameters_ShouldSucceed()
    {
        SkipIfNotConfigured();
        
        var card = CardBuilder.Create()
            .Header(h => h.Title("用户信息: ${user.name}").TextTag(t => t.Text("C#").Color("purple")))
            .Body(b => b
                .PlainText("姓名: ${user.name}")
                .PlainText("部门: ${user.department}")
                .PlainText("邮箱: ${user.email}"))
            .SetParameter("user", new { name = "李四", department = "技术部", email = "lisi@example.com" })
            .Build();
        
        var messageId = await SendCardAsync("嵌套对象参数", card);
        Assert.NotNull(messageId);
        
        await DelayAsync();
    }
    
    [Fact]
    public async Task SendTemplateWithDefaultValue_ShouldSucceed()
    {
        SkipIfNotConfigured();
        
        var card = CardBuilder.Create()
            .Header(h => h.Title("默认值测试").TextTag(t => t.Text("C#").Color("purple")))
            .Body(b => b
                .PlainText("已提供参数: ${provided:未提供}")
                .PlainText("未提供参数: ${notProvided:使用默认值}"))
            .SetParameter("provided", "实际值")
            .Build();
        
        var messageId = await SendCardAsync("默认值模板", card);
        Assert.NotNull(messageId);
        
        await DelayAsync();
    }
    
    [Fact]
    public async Task SendTemplateWithAnonymousObject_ShouldSucceed()
    {
        SkipIfNotConfigured();
        
        var card = CardBuilder.Create()
            .Header(h => h.Title("${title}").TextTag(t => t.Text("C#").Color("purple")))
            .Body(b => b
                .PlainText("${content}")
                .Markdown("**状态**: ${status}"))
            .SetParameters(new
            {
                title = "匿名对象参数测试",
                content = "这是通过匿名对象传入的内容",
                status = "已完成"
            })
            .Build();
        
        var messageId = await SendCardAsync("匿名对象参数", card);
        Assert.NotNull(messageId);
        
        await DelayAsync();
    }
    
    [Fact]
    public async Task SendTemplateWithDictionary_ShouldSucceed()
    {
        SkipIfNotConfigured();
        
        var parameters = new Dictionary<string, object?>
        {
            ["title"] = "字典参数测试",
            ["count"] = 42,
            ["enabled"] = true
        };
        
        var card = CardBuilder.Create()
            .Header(h => h.Title("${title}").TextTag(t => t.Text("C#").Color("purple")))
            .Body(b => b
                .PlainText("数量: ${count}")
                .PlainText("启用状态: ${enabled}"))
            .SetParameters(parameters)
            .Build();
        
        var messageId = await SendCardAsync("字典参数", card);
        Assert.NotNull(messageId);
        
        await DelayAsync();
    }
    
    [Fact]
    public async Task SendConfigObjectTemplate_ShouldSucceed()
    {
        SkipIfNotConfigured();
        
        var card = CardBuilder.Create()
            .Header(h => h.Title("配置对象模板测试").TextTag(t => t.Text("C#").Color("purple")))
            .Body(b => b
                .Button(btn => btn
                    .Text("模板按钮类型")
                    .Type(new TemplateValue("buttonType", "default")))
                .Input(input => input
                    .Name("testInput")
                    .Placeholder("输入框")
                    .Disabled(new TemplateValue("isDisabled", "false"))))
            .SetParameter("buttonType", ButtonType.Primary)
            .SetParameter("isDisabled", false)
            .Build();
        
        var messageId = await SendCardAsync("配置对象模板", card);
        Assert.NotNull(messageId);
        
        await DelayAsync();
    }
    
    [Fact]
    public async Task SendOptionsFromTemplate_ShouldSucceed()
    {
        SkipIfNotConfigured();
        
        var options = new[]
        {
            new SelectOptionInfo("opt1", "选项一"),
            new SelectOptionInfo("opt2", "选项二"),
            new SelectOptionInfo("opt3", "选项三")
        };
        
        var card = CardBuilder.Create()
            .Header(h => h.Title("OptionsFrom 模板测试").TextTag(t => t.Text("C#").Color("purple")))
            .Body(b => b
                .Form(form => form
                    .Name("templateForm")
                    .Vertical()
                    .Select(select => select
                        .Name("dynamicOption")
                        .Placeholder("动态选项")
                        .OptionsFrom("dynamicOptions"))
                    .Button(btn => btn.Text("提交").Submit())))
            .SetParameter("dynamicOptions", options)
            .Build();
        
        var messageId = await SendCardAsync("OptionsFrom 模板", card);
        Assert.NotNull(messageId);
        
        await DelayAsync();
    }
}
