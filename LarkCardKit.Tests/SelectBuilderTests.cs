using LarkCardKit.Builders;
using LarkCardKit.Models;
using Xunit;

namespace LarkCardKit.Tests;

public class SelectBuilderTests
{
    [Fact]
    public void AddOption_WithStringParameters_ShouldAddOption()
    {
        var select = new SelectBuilder()
            .AddOption("value1", "文本1")
            .Build();

        Assert.Single(select.Options);
        Assert.Equal("value1", select.Options[0].Value);
        Assert.Equal("文本1", select.Options[0].Text?.Content);
    }

    [Fact]
    public void AddOption_WithSelectOptionInfo_ShouldAddOption()
    {
        var optionInfo = new SelectOptionInfo("value2", "文本2", "描述");
        var select = new SelectBuilder()
            .AddOption(optionInfo)
            .Build();

        Assert.Single(select.Options);
        Assert.Equal("value2", select.Options[0].Value);
        Assert.Equal("文本2", select.Options[0].Text?.Content);
    }

    [Fact]
    public void AddOptions_WithIEnumerable_ShouldAddMultipleOptions()
    {
        var options = new List<SelectOptionInfo>
        {
            new("v1", "t1"),
            new("v2", "t2"),
            new("v3", "t3")
        };

        var select = new SelectBuilder()
            .AddOptions(options)
            .Build();

        Assert.Equal(3, select.Options?.Count);
        Assert.Equal("v1", select.Options![0].Value);
        Assert.Equal("v2", select.Options[1].Value);
        Assert.Equal("v3", select.Options[2].Value);
    }

    [Fact]
    public void Options_WithSelector_ShouldAddOptions()
    {
        var items = new List<(string Id, string Name)>
        {
            ("id1", "名称1"),
            ("id2", "名称2")
        };

        var select = new SelectBuilder()
            .Options(items, x => new SelectOptionInfo(x.Id, x.Name))
            .Build();

        Assert.Equal(2, select.Options?.Count);
        Assert.Equal("id1", select.Options![0].Value);
        Assert.Equal("名称1", select.Options[0].Text?.Content);
    }

    [Fact]
    public void Options_WithValueAndTextSelectors_ShouldAddOptions()
    {
        var items = new List<TestItem>
        {
            new() { Code = "A", Label = "选项A" },
            new() { Code = "B", Label = "选项B" }
        };

        var select = new SelectBuilder()
            .Options(items, x => x.Code, x => x.Label)
            .Build();

        Assert.Equal(2, select.Options?.Count);
        Assert.Equal("A", select.Options![0].Value);
        Assert.Equal("选项A", select.Options[0].Text?.Content);
    }

    [Fact]
    public void BasicProperties_ShouldBeSetCorrectly()
    {
        var select = new SelectBuilder()
            .Name("testSelect")
            .Required(true)
            .MultiSelect(true)
            .Placeholder("请选择")
            .InitialOption("opt1")
            .Width("200px")
            .Disabled(true)
            .ElementId("select-id")
            .Margin("10px")
            .Build();

        Assert.Equal("testSelect", select.Name);
        Assert.True(select.Required);
        Assert.True(select.MultiSelect);
        Assert.Equal("请选择", select.Placeholder?.Content);
        Assert.Equal("opt1", select.InitialOption);
        Assert.Equal("200px", select.Width);
        Assert.True(select.Disabled);
        Assert.Equal("select-id", select.ElementId);
        Assert.Equal("10px", select.Margin);
    }

    [Fact]
    public void OptionsFrom_ShouldSetTemplateKey()
    {
        var select = new SelectBuilder()
            .OptionsFrom("dynamicOptions")
            .Build();

        Assert.Equal("dynamicOptions", select.OptionsTemplateKey);
    }

    [Fact]
    public void OnChange_ShouldAddBehavior()
    {
        var select = new SelectBuilder()
            .OnChange(new { action = "submit" })
            .Build();

        Assert.NotNull(select.Behaviors);
        Assert.Single(select.Behaviors);
    }

    [Fact]
    public void EmptyOptions_ShouldBeNull()
    {
        var select = new SelectBuilder()
            .Name("emptySelect")
            .Build();

        Assert.Null(select.Options);
    }

    [Fact]
    public void MultipleAddOptionCalls_ShouldAccumulateOptions()
    {
        var select = new SelectBuilder()
            .AddOption("v1", "t1")
            .AddOption("v2", "t2")
            .AddOption("v3", "t3")
            .Build();

        Assert.Equal(3, select.Options?.Count);
    }

    [Fact]
    public void MixedOptionMethods_ShouldWorkTogether()
    {
        var select = new SelectBuilder()
            .AddOption("v1", "t1")
            .AddOption(new SelectOptionInfo("v2", "t2"))
            .AddOptions(new[] { new SelectOptionInfo("v3", "t3") })
            .Build();

        Assert.Equal(3, select.Options?.Count);
    }

    private class TestItem
    {
        public string Code { get; set; } = string.Empty;
        public string Label { get; set; } = string.Empty;
    }
}
