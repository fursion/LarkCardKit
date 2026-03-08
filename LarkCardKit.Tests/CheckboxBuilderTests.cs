using LarkCardKit.Builders;
using LarkCardKit.Models;
using Xunit;

namespace LarkCardKit.Tests;

public class CheckboxBuilderTests
{
    [Fact]
    public void AddOption_WithStringParameters_ShouldAddOption()
    {
        var checkbox = new CheckboxBuilder()
            .AddOption("value1", "文本1")
            .Build();

        Assert.Single(checkbox.Options);
        Assert.Equal("value1", checkbox.Options[0].Value);
        Assert.Equal("文本1", checkbox.Options[0].Text?.Content);
    }

    [Fact]
    public void AddOption_WithCheckboxOptionInfo_ShouldAddOption()
    {
        var optionInfo = new CheckboxOptionInfo("value2", "文本2");
        var checkbox = new CheckboxBuilder()
            .AddOption(optionInfo)
            .Build();

        Assert.Single(checkbox.Options);
        Assert.Equal("value2", checkbox.Options[0].Value);
        Assert.Equal("文本2", checkbox.Options[0].Text?.Content);
    }

    [Fact]
    public void AddOptions_WithIEnumerable_ShouldAddMultipleOptions()
    {
        var options = new List<CheckboxOptionInfo>
        {
            new("v1", "t1"),
            new("v2", "t2"),
            new("v3", "t3")
        };

        var checkbox = new CheckboxBuilder()
            .AddOptions(options)
            .Build();

        Assert.Equal(3, checkbox.Options?.Count);
        Assert.Equal("v1", checkbox.Options![0].Value);
        Assert.Equal("v2", checkbox.Options[1].Value);
        Assert.Equal("v3", checkbox.Options[2].Value);
    }

    [Fact]
    public void Options_WithSelector_ShouldAddOptions()
    {
        var items = new List<(string Id, string Name)>
        {
            ("id1", "名称1"),
            ("id2", "名称2")
        };

        var checkbox = new CheckboxBuilder()
            .Options(items, x => new CheckboxOptionInfo(x.Id, x.Name))
            .Build();

        Assert.Equal(2, checkbox.Options?.Count);
        Assert.Equal("id1", checkbox.Options![0].Value);
        Assert.Equal("名称1", checkbox.Options[0].Text?.Content);
    }

    [Fact]
    public void Options_WithValueAndTextSelectors_ShouldAddOptions()
    {
        var items = new List<TestItem>
        {
            new() { Code = "A", Label = "选项A" },
            new() { Code = "B", Label = "选项B" }
        };

        var checkbox = new CheckboxBuilder()
            .Options(items, x => x.Code, x => x.Label)
            .Build();

        Assert.Equal(2, checkbox.Options?.Count);
        Assert.Equal("A", checkbox.Options![0].Value);
        Assert.Equal("选项A", checkbox.Options[0].Text?.Content);
    }

    [Fact]
    public void BasicProperties_ShouldBeSetCorrectly()
    {
        var checkbox = new CheckboxBuilder()
            .Name("testCheckbox")
            .Required(true)
            .Placeholder("请选择")
            .Width("300px")
            .ElementId("checkbox-id")
            .Margin("8px")
            .Build();

        Assert.Equal("testCheckbox", checkbox.Name);
        Assert.True(checkbox.Required);
        Assert.Equal("请选择", checkbox.Placeholder?.Content);
        Assert.Equal("300px", checkbox.Width);
        Assert.Equal("checkbox-id", checkbox.ElementId);
        Assert.Equal("8px", checkbox.Margin);
    }

    [Fact]
    public void InitialSelected_ShouldSetSelectedValues()
    {
        var checkbox = new CheckboxBuilder()
            .AddOption("v1", "t1")
            .AddOption("v2", "t2")
            .AddOption("v3", "t3")
            .InitialSelected("v1", "v3")
            .Build();

        Assert.NotNull(checkbox.SelectedValues);
        Assert.Equal(2, checkbox.SelectedValues.Count);
        Assert.Contains("v1", checkbox.SelectedValues);
        Assert.Contains("v3", checkbox.SelectedValues);
    }

    [Fact]
    public void OnChange_ShouldAddBehavior()
    {
        var checkbox = new CheckboxBuilder()
            .OnChange(new { action = "update" })
            .Build();

        Assert.NotNull(checkbox.Behaviors);
        Assert.Single(checkbox.Behaviors);
    }

    [Fact]
    public void EmptyOptions_ShouldBeNull()
    {
        var checkbox = new CheckboxBuilder()
            .Name("emptyCheckbox")
            .Build();

        Assert.Null(checkbox.Options);
    }

    [Fact]
    public void MultipleAddOptionCalls_ShouldAccumulateOptions()
    {
        var checkbox = new CheckboxBuilder()
            .AddOption("v1", "t1")
            .AddOption("v2", "t2")
            .AddOption("v3", "t3")
            .Build();

        Assert.Equal(3, checkbox.Options?.Count);
    }

    [Fact]
    public void MixedOptionMethods_ShouldWorkTogether()
    {
        var checkbox = new CheckboxBuilder()
            .AddOption("v1", "t1")
            .AddOption(new CheckboxOptionInfo("v2", "t2"))
            .AddOptions(new[] { new CheckboxOptionInfo("v3", "t3") })
            .Build();

        Assert.Equal(3, checkbox.Options?.Count);
    }

    [Fact]
    public void InitialSelected_WithNoOptions_ShouldStillSetValues()
    {
        var checkbox = new CheckboxBuilder()
            .InitialSelected("v1", "v2")
            .Build();

        Assert.NotNull(checkbox.SelectedValues);
        Assert.Equal(2, checkbox.SelectedValues.Count);
    }

    [Fact]
    public void ImplicitConversion_ShouldWorkWithTuple()
    {
        CheckboxOptionInfo option = ("value", "text");
        
        Assert.Equal("value", option.Value);
        Assert.Equal("text", option.Text);
    }

    private class TestItem
    {
        public string Code { get; set; } = string.Empty;
        public string Label { get; set; } = string.Empty;
    }
}
