using LarkCardKit.Builders;
using LarkCardKit.Enums;
using LarkCardKit.Models.Elements;
using Xunit;

namespace LarkCardKit.Tests;

public class CardBuilderElementReplacerTests
{
    [Fact]
    public void ReplaceElement_TopLevelElement_ShouldReplaceSuccessfully()
    {
        var builder = CardBuilder.Create()
            .Body(b => b
                .Button(btn => btn
                    .Text("原始按钮")
                    .ElementId("btn1")));

        var newButton = new Button
        {
            ElementId = "btn1",
            Text = new PlainText { Content = "新按钮" }
        };

        var result = builder.ReplaceElement("btn1", newButton);

        Assert.True(result);
        var button = builder.FindElementById<Button>("btn1");
        Assert.NotNull(button);
        Assert.Equal("新按钮", button.Text?.Content);
    }

    [Fact]
    public void ReplaceElement_NonExistentElement_ShouldReturnFalse()
    {
        var builder = CardBuilder.Create()
            .Body(b => b.PlainText("内容"));

        var newElement = new PlainText { Content = "新内容", ElementId = "new" };
        var result = builder.ReplaceElement("nonexistent", newElement);

        Assert.False(result);
    }

    [Fact]
    public void ReplaceElement_WithDifferentType_ShouldReplaceSuccessfully()
    {
        var pt = new PlainText { Content = "原始文本", ElementId = "text1" };
        
        var builder = CardBuilder.Create()
            .Body(b => b.AddElement(pt));

        var newMarkdown = new Markdown
        {
            ElementId = "text1",
            Content = "**Markdown内容**"
        };

        var result = builder.ReplaceElement("text1", newMarkdown);

        Assert.True(result);
        var element = builder.FindElementById("text1");
        Assert.NotNull(element);
        Assert.IsType<Markdown>(element);
    }

    [Fact]
    public void TryReplaceElement_ShouldWorkSameAsReplaceElement()
    {
        var builder = CardBuilder.Create()
            .Body(b => b
                .Input(i => i
                    .Name("input1")
                    .ElementId("inp1")));

        var newInput = new Input
        {
            ElementId = "inp1",
            Name = "newInput",
            Placeholder = new PlainText { Content = "新占位符" }
        };

        var result = builder.TryReplaceElement("inp1", newInput);

        Assert.True(result);
        var input = builder.FindElementById<Input>("inp1");
        Assert.NotNull(input);
        Assert.Equal("newInput", input.Name);
    }

    [Fact]
    public void ReplaceElement_InDiv_ShouldReplaceSuccessfully()
    {
        var builder = CardBuilder.Create()
            .Body(b => b
                .Div(d => d
                    .Vertical()
                    .Button(btn => btn
                        .Text("Div中的按钮")
                        .ElementId("divBtn"))));

        var newSelect = new Select
        {
            ElementId = "divBtn",
            Name = "newSelect",
            Placeholder = new PlainText { Content = "选择" }
        };

        var result = builder.ReplaceElement("divBtn", newSelect);

        Assert.True(result);
        var element = builder.FindElementById("divBtn");
        Assert.NotNull(element);
        Assert.IsType<Select>(element);
    }

    [Fact]
    public void ReplaceElement_InForm_ShouldReplaceSuccessfully()
    {
        var builder = CardBuilder.Create()
            .Body(b => b
                .Form(f => f
                    .Name("form1")
                    .Input(i => i
                        .Name("oldInput")
                        .ElementId("formInput"))));

        var newCheckbox = new Checkbox
        {
            ElementId = "formInput",
            Name = "newCheckbox"
        };

        var result = builder.ReplaceElement("formInput", newCheckbox);

        Assert.True(result);
        var element = builder.FindElementById("formInput");
        Assert.NotNull(element);
        Assert.IsType<Checkbox>(element);
    }

    [Fact]
    public void ReplaceElement_InColumnSet_ShouldReplaceSuccessfully()
    {
        var builder = CardBuilder.Create()
            .Body(b => b
                .ColumnSet(cs => cs
                    .AddColumn(col => col
                        .Width("50%")
                        .Button(btn => btn
                            .Text("列按钮")
                            .ElementId("colBtn")))));

        var newButton = new Button
        {
            ElementId = "colBtn",
            Text = new PlainText { Content = "替换后的列按钮" }
        };

        var result = builder.ReplaceElement("colBtn", newButton);

        Assert.True(result);
        var button = builder.FindElementById<Button>("colBtn");
        Assert.NotNull(button);
        Assert.Equal("替换后的列按钮", button.Text?.Content);
    }

    [Fact]
    public void ReplaceElement_NestedStructure_ShouldReplaceSuccessfully()
    {
        var nestedInput = new Input { Name = "nestedInput", ElementId = "deepInput" };
        var nestedDiv = new Div
        {
            Direction = "vertical",
            Elements = new List<Element> { nestedInput }
        };
        var outerDiv = new Div
        {
            Direction = "vertical",
            Elements = new List<Element> { nestedDiv }
        };

        var builder = CardBuilder.Create()
            .Body(b => b.AddElement(outerDiv));

        var newInput = new Input
        {
            ElementId = "deepInput",
            Name = "replacedInput",
            Placeholder = new PlainText { Content = "替换后的输入框" }
        };

        var result = builder.ReplaceElement("deepInput", newInput);

        Assert.True(result);
        var input = builder.FindElementById<Input>("deepInput");
        Assert.NotNull(input);
        Assert.Equal("replacedInput", input.Name);
    }

    [Fact]
    public void ReplaceElement_EmptyCard_ShouldReturnFalse()
    {
        var builder = CardBuilder.Create();

        var newElement = new PlainText { Content = "内容" };
        var result = builder.ReplaceElement("any", newElement);

        Assert.False(result);
    }

    [Fact]
    public void ReplaceElement_MultipleElements_ShouldReplaceCorrectOne()
    {
        var builder = CardBuilder.Create()
            .Body(b => b
                .Button(btn => btn.Text("按钮1").ElementId("btn1"))
                .Button(btn => btn.Text("按钮2").ElementId("btn2"))
                .Button(btn => btn.Text("按钮3").ElementId("btn3")));

        var newButton = new Button
        {
            ElementId = "btn2",
            Text = new PlainText { Content = "替换的按钮2" }
        };

        var result = builder.ReplaceElement("btn2", newButton);

        Assert.True(result);
        var btn1 = builder.FindElementById<Button>("btn1");
        var btn2 = builder.FindElementById<Button>("btn2");
        var btn3 = builder.FindElementById<Button>("btn3");
        
        Assert.NotNull(btn1);
        Assert.Equal("按钮1", btn1.Text?.Content);
        Assert.NotNull(btn2);
        Assert.Equal("替换的按钮2", btn2.Text?.Content);
        Assert.NotNull(btn3);
        Assert.Equal("按钮3", btn3.Text?.Content);
    }

    [Fact]
    public void ReplaceElement_WithSelect_ShouldWork()
    {
        var pt = new PlainText { Content = "文本", ElementId = "text1" };
        
        var builder = CardBuilder.Create()
            .Body(b => b.AddElement(pt));

        var newSelect = new Select
        {
            ElementId = "text1",
            Name = "newSelect",
            Options = new List<SelectOption>
            {
                new() { Value = "v1", Text = new PlainText { Content = "选项1" } }
            }
        };

        var result = builder.ReplaceElement("text1", newSelect);

        Assert.True(result);
        var select = builder.FindElementById<Select>("text1");
        Assert.NotNull(select);
        Assert.Single(select.Options!);
    }

    [Fact]
    public void ReplaceElement_WithDatePicker_ShouldWork()
    {
        var builder = CardBuilder.Create()
            .Body(b => b
                .Input(i => i.Name("input1").ElementId("inp1")));

        var newDatePicker = new DatePicker
        {
            ElementId = "inp1",
            Name = "datePicker",
            InitialDate = "2024-12-25"
        };

        var result = builder.ReplaceElement("inp1", newDatePicker);

        Assert.True(result);
        var dp = builder.FindElementById<DatePicker>("inp1");
        Assert.NotNull(dp);
        Assert.Equal("2024-12-25", dp.InitialDate);
    }

    [Fact]
    public void ReplaceElement_ComplexNestedInMultipleContainers_ShouldWork()
    {
        var submitBtn = new Button { Text = new PlainText { Content = "提交" }, ElementId = "submitBtn" };
        var column = new Column
        {
            Width = "100%",
            Elements = new List<Element> { submitBtn }
        };
        var columnSet = new ColumnSet
        {
            Columns = new List<Column> { column }
        };

        var builder = CardBuilder.Create()
            .Body(b => b
                .Div(d => d
                    .Vertical()
                    .PlainText("顶部文本"))
                .Form(f => f
                    .Name("form")
                    .Input(i => i.Name("email").ElementId("email"))
                    .Select(s => s.Name("country").ElementId("country")))
                .AddElement(columnSet));

        var newButton = new Button
        {
            ElementId = "submitBtn",
            Text = new PlainText { Content = "发送" },
            Type = ButtonType.Primary.ToString().ToLower()
        };

        var result = builder.ReplaceElement("submitBtn", newButton);

        Assert.True(result);
        var button = builder.FindElementById<Button>("submitBtn");
        Assert.NotNull(button);
        Assert.Equal("发送", button.Text?.Content);
        Assert.Equal("primary", button.Type);
    }
}
