using LarkCardKit.Builders;
using Xunit;

namespace LarkCardKit.Tests;

public class HrBuilderTests
{
    [Fact]
    public void Build_BasicHr_ShouldHaveCorrectTag()
    {
        var hr = new HrBuilder().Build();
        
        Assert.Equal("hr", hr.Tag);
    }
    
    [Fact]
    public void Build_WithElementId_ShouldSetElementId()
    {
        var hr = new HrBuilder()
            .ElementId("divider1")
            .Build();
        
        Assert.Equal("divider1", hr.ElementId);
    }
    
    [Fact]
    public void Build_WithMargin_ShouldSetMargin()
    {
        var hr = new HrBuilder()
            .Margin("8px 0")
            .Build();
        
        Assert.Equal("8px 0", hr.Margin);
    }
    
    [Fact]
    public void Build_WithAllProperties_ShouldSetAllProperties()
    {
        var hr = new HrBuilder()
            .ElementId("hr1")
            .Margin("12px 0")
            .Build();
        
        Assert.Equal("hr", hr.Tag);
        Assert.Equal("hr1", hr.ElementId);
        Assert.Equal("12px 0", hr.Margin);
    }
    
    [Fact]
    public void Build_UsingCardBuilder_ShouldAddHrToCard()
    {
        var card = CardBuilder.Create()
            .Header(h => h.Title("测试卡片"))
            .Body(b => b
                .PlainText("内容上方")
                .Hr()
                .PlainText("内容下方"))
            .Build();
        
        Assert.Equal(3, card.Body.Elements.Count);
        Assert.Equal("div", card.Body.Elements[0].Tag);
        Assert.Equal("hr", card.Body.Elements[1].Tag);
        Assert.Equal("div", card.Body.Elements[2].Tag);
    }
    
    [Fact]
    public void Build_UsingCardBuilderWithConfigure_ShouldAddConfiguredHr()
    {
        var card = CardBuilder.Create()
            .Header(h => h.Title("测试卡片"))
            .Body(b => b
                .PlainText("内容上方")
                .Hr(hr => hr.Margin("16px 0").ElementId("myHr"))
                .PlainText("内容下方"))
            .Build();
        
        var hrElement = card.Body.Elements[1] as Models.Elements.Hr;
        Assert.NotNull(hrElement);
        Assert.Equal("hr", hrElement.Tag);
        Assert.Equal("16px 0", hrElement.Margin);
        Assert.Equal("myHr", hrElement.ElementId);
    }
}
