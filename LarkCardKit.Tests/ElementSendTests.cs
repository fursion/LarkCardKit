using LarkCardKit.Builders;
using Xunit;

namespace LarkCardKit.Tests;

[Trait("Category", "Integration")]
public class ElementSendTests : IntegrationTestBase
{
    [Fact]
    public async Task SendPlainTextCard_ShouldSucceed()
    {
        SkipIfNotConfigured();
        
        var card = CardBuilder.Create()
            .Header(h => h.Title("PlainText 元素测试"))
            .Body(b => b.PlainText("这是纯文本元素内容，测试 PlainText 组件发送。"))
            .Build();
        
        var messageId = await SendCardAsync("PlainText 元素", card);
        Assert.NotNull(messageId);
        
        await DelayAsync();
    }
    
    [Fact]
    public async Task SendMarkdownCard_ShouldSucceed()
    {
        SkipIfNotConfigured();
        
        var card = CardBuilder.Create()
            .Header(h => h.Title("Markdown 元素测试"))
            .Body(b => b
                .Markdown("**粗体文本** 和 *斜体文本*\n")
                .Markdown("- 列表项 1\n- 列表项 2\n- 列表项 3\n")
                .Markdown("`代码块` 和 [链接](https://feishu.cn)"))
            .Build();
        
        var messageId = await SendCardAsync("Markdown 元素", card);
        Assert.NotNull(messageId);
        
        await DelayAsync();
    }
    
    [Fact(Skip = "需要真实的图片 img_key，占位符 img_v2_xxxx 无法通过验证")]
    public async Task SendImageCard_ShouldSucceed()
    {
        SkipIfNotConfigured();
        
        var card = CardBuilder.Create()
            .Header(h => h.Title("Image 元素测试"))
            .Body(b => b
                .PlainText("图片元素测试（使用示例图片 Key）")
                .Image("img_v2_xxxx", i => i
                    .Alt("示例图片")
                    .Width("200px")))
            .Build();
        
        var messageId = await SendCardAsync("Image 元素", card);
        Assert.NotNull(messageId);
        
        await DelayAsync();
    }
    
    [Fact]
    public async Task SendMultiplePlainTextElements_ShouldSucceed()
    {
        SkipIfNotConfigured();
        
        var card = CardBuilder.Create()
            .Header(h => h.Title("多个 PlainText 元素测试"))
            .Body(b => b
                .PlainText("第一行文本")
                .PlainText("第二行文本")
                .PlainText("第三行文本"))
            .Build();
        
        var messageId = await SendCardAsync("多个 PlainText", card);
        Assert.NotNull(messageId);
        
        await DelayAsync();
    }
    
    [Fact]
    public async Task SendMixedElements_ShouldSucceed()
    {
        SkipIfNotConfigured();
        
        var card = CardBuilder.Create()
            .Header(h => h.Title("混合元素测试"))
            .Body(b => b
                .PlainText("纯文本内容")
                .Markdown("**Markdown 内容**")
                .PlainText("更多纯文本"))
            .Build();
        
        var messageId = await SendCardAsync("混合元素", card);
        Assert.NotNull(messageId);
        
        await DelayAsync();
    }
}
