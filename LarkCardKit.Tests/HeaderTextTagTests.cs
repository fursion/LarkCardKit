using LarkCardKit.Builders;
using LarkCardKit.Models.Elements;
using Xunit;

namespace LarkCardKit.Tests;

public class HeaderTextTagTests
{
    [Fact]
    public void TextTagBuilder_Build_ShouldHaveCorrectTag()
    {
        var textTag = new TextTagBuilder()
            .Text("测试标签")
            .Build();

        Assert.Equal("text_tag", textTag.Tag);
        Assert.Equal("测试标签", (textTag.Text as PlainText)?.Content);
    }

    [Fact]
    public void TextTagBuilder_WithColor_ShouldSetColor()
    {
        var textTag = new TextTagBuilder()
            .Text("进行中")
            .Color("blue")
            .Build();

        Assert.Equal("进行中", (textTag.Text as PlainText)?.Content);
        Assert.Equal("blue", textTag.Color);
    }
    
    [Fact]
    public void TextTagBuilder_WithElementId_ShouldSetElementId()
    {
        var textTag = new TextTagBuilder()
            .Text("标签")
            .ElementId("tag1")
            .Build();
        
        Assert.Equal("tag1", textTag.ElementId);
    }
    
    [Fact]
    public void CardHeaderBuilder_WithSubtitle_ShouldSetSubtitle()
    {
        var card = CardBuilder.Create()
            .Header(h => h
                .Title("主标题")
                .Subtitle("副标题"))
            .Build();
        
        Assert.NotNull(card.Header);
        Assert.NotNull(card.Header.Subtitle);
    }
    
    [Fact]
    public void CardHeaderBuilder_WithTemplate_ShouldSetTemplate()
    {
        var card = CardBuilder.Create()
            .Header(h => h
                .Title("标题")
                .Template("blue"))
            .Build();
        
        Assert.Equal("blue", card.Header?.Template);
    }
    
    [Fact]
    public void CardHeaderBuilder_WithSingleTextTag_ShouldAddTextTag()
    {
        var card = CardBuilder.Create()
            .Header(h => h
                .Title("任务状态")
                .TextTag(t => t.Text("进行中").Color("blue")))
            .Build();

        Assert.NotNull(card.Header?.TextTagList);
        Assert.Single(card.Header.TextTagList);
        Assert.Equal("进行中", (card.Header.TextTagList[0].Text as PlainText)?.Content);
        Assert.Equal("blue", card.Header.TextTagList[0].Color);
    }

    [Fact]
    public void CardHeaderBuilder_WithMultipleTextTags_ShouldAddAllTags()
    {
        var card = CardBuilder.Create()
            .Header(h => h
                .Title("项目状态")
                .TextTag(t => t.Text("进行中").Color("blue"))
                .TextTag(t => t.Text("紧急").Color("red"))
                .TextTag(t => t.Text("重要").Color("orange")))
            .Build();

        Assert.NotNull(card.Header?.TextTagList);
        Assert.Equal(3, card.Header.TextTagList.Count);
        Assert.Equal("进行中", (card.Header.TextTagList[0].Text as PlainText)?.Content);
        Assert.Equal("紧急", (card.Header.TextTagList[1].Text as PlainText)?.Content);
        Assert.Equal("重要", (card.Header.TextTagList[2].Text as PlainText)?.Content);
    }
    
    [Fact]
    public void CardHeaderBuilder_WithAllProperties_ShouldSetAllProperties()
    {
        var card = CardBuilder.Create()
            .Header(h => h
                .Title("完整标题")
                .Subtitle("完整副标题")
                .Template("green")
                .Padding("16px")
                .TextTag(t => t.Text("已完成").Color("green").ElementId("statusTag")))
            .Build();
        
        Assert.NotNull(card.Header);
        Assert.Equal("green", card.Header.Template);
        Assert.Equal("16px", card.Header.Padding);
        Assert.NotNull(card.Header.Subtitle);
        Assert.NotNull(card.Header.TextTagList);
        Assert.Single(card.Header.TextTagList);
        Assert.Equal("statusTag", card.Header.TextTagList[0].ElementId);
    }
    
    [Theory]
    [InlineData("neutral")]
    [InlineData("blue")]
    [InlineData("turquoise")]
    [InlineData("lime")]
    [InlineData("orange")]
    [InlineData("violet")]
    [InlineData("indigo")]
    [InlineData("wathet")]
    [InlineData("green")]
    [InlineData("yellow")]
    [InlineData("red")]
    [InlineData("purple")]
    [InlineData("carmine")]
    public void TextTagBuilder_WithValidColors_ShouldSetColors(string color)
    {
        var textTag = new TextTagBuilder()
            .Text("标签")
            .Color(color)
            .Build();
        
        Assert.Equal(color, textTag.Color);
    }
    
    [Theory]
    [InlineData("blue")]
    [InlineData("wathet")]
    [InlineData("turquoise")]
    [InlineData("green")]
    [InlineData("yellow")]
    [InlineData("orange")]
    [InlineData("red")]
    [InlineData("carmine")]
    [InlineData("violet")]
    [InlineData("purple")]
    [InlineData("indigo")]
    [InlineData("grey")]
    [InlineData("default")]
    public void CardHeaderBuilder_WithValidTemplates_ShouldSetTemplates(string template)
    {
        var card = CardBuilder.Create()
            .Header(h => h
                .Title("标题")
                .Template(template))
            .Build();
        
        Assert.Equal(template, card.Header?.Template);
    }
}
