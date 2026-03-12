using LarkCardKit.Builders;
using LarkCardKit.Models.Elements;
using Xunit;

namespace LarkCardKit.Tests;

public class CardBuilderElementFinderTests
{
    [Fact]
    public void FindElementById_TopLevelElement_ShouldFindElement()
    {
        var builder = CardBuilder.Create()
            .Body(b => b
                .PlainText("文本内容")
                .Button(btn => btn
                    .Text("按钮")
                    .ElementId("btn1")));

        var element = builder.FindElementById("btn1");

        Assert.NotNull(element);
        Assert.IsType<Button>(element);
        var button = (Button)element;
        Assert.Equal("按钮", button.Text?.Content);
    }

    [Fact]
    public void FindElementById_NonExistentElement_ShouldReturnNull()
    {
        var builder = CardBuilder.Create()
            .Body(b => b
                .PlainText("文本")
                .Button(btn => btn.Text("按钮").ElementId("btn1")));

        var element = builder.FindElementById("nonexistent");

        Assert.Null(element);
    }

    [Fact]
    public void FindElementById_InDiv_ShouldFindElement()
    {
        // 手动创建 PlainTextElement 并添加子元素
        var divElement = new PlainTextElement
        {
            Direction = "vertical",
            Elements = new List<Element>
            {
                new PlainText { Content = "内容1" },
                new Button { Text = new PlainText { Content = "按钮" }, ElementId = "divBtn" }
            }
        };

        var builder = CardBuilder.Create()
            .Body(b => b.AddElement(divElement));

        var element = builder.FindElementById("divBtn");

        Assert.NotNull(element);
        Assert.IsType<Button>(element);
    }

    [Fact]
    public void FindElementById_InForm_ShouldFindElement()
    {
        var builder = CardBuilder.Create()
            .Body(b => b
                .Form(f => f
                    .Name("myForm")
                    .Input(i => i
                        .Name("username")
                        .ElementId("input1"))));

        var element = builder.FindElementById("input1");

        Assert.NotNull(element);
        Assert.IsType<Input>(element);
    }

    [Fact]
    public void FindElementById_InColumnSet_ShouldFindElement()
    {
        var builder = CardBuilder.Create()
            .Body(b => b
                .ColumnSet(cs => cs
                    .AddColumn(col => col
                        .Width("50%")
                        .ElementId("col1")
                        .Button(btn => btn
                            .Text("列按钮")
                            .ElementId("colBtn")))));

        var colElement = builder.FindElementById("col1");
        var btnElement = builder.FindElementById("colBtn");

        Assert.NotNull(colElement);
        Assert.NotNull(btnElement);
        Assert.IsType<Column>(colElement);
        Assert.IsType<Button>(btnElement);
    }

    [Fact]
    public void FindElementById_Generic_ShouldReturnTypedElement()
    {
        var builder = CardBuilder.Create()
            .Body(b => b
                .Select(s => s
                    .Name("selector")
                    .ElementId("select1")
                    .AddOption("v1", "选项1")));

        var select = builder.FindElementById<Select>("select1");

        Assert.NotNull(select);
        Assert.Equal("selector", select.Name);
        Assert.Single(select.Options!);
    }

    [Fact]
    public void FindElementById_Generic_WrongType_ShouldReturnNull()
    {
        var builder = CardBuilder.Create()
            .Body(b => b
                .Button(btn => btn
                    .Text("按钮")
                    .ElementId("btn1")));

        var input = builder.FindElementById<Input>("btn1");

        Assert.Null(input);
    }

    [Fact]
    public void TryFindElementById_ExistingElement_ShouldReturnTrue()
    {
        var builder = CardBuilder.Create()
            .Body(b => b
                .Button(btn => btn.Text("按钮").ElementId("btn1")));

        var found = builder.TryFindElementById("btn1", out var element);

        Assert.True(found);
        Assert.NotNull(element);
        Assert.IsType<Button>(element);
    }

    [Fact]
    public void TryFindElementById_NonExistentElement_ShouldReturnFalse()
    {
        var builder = CardBuilder.Create()
            .Body(b => b.PlainText("内容"));

        var found = builder.TryFindElementById("nonexistent", out var element);

        Assert.False(found);
        Assert.Null(element);
    }

    [Fact]
    public void FindElementById_NestedDiv_ShouldFindElement()
    {
        var nestedDiv = new PlainTextElement
        {
            Direction = "vertical",
            Elements = new List<Element>
            {
                new Button { Text = new PlainText { Content = "深层按钮" }, ElementId = "deepBtn" }
            }
        };

        var outerDiv = new PlainTextElement
        {
            Direction = "vertical",
            Elements = new List<Element> { nestedDiv }
        };

        var builder = CardBuilder.Create()
            .Body(b => b.AddElement(outerDiv));

        var element = builder.FindElementById("deepBtn");

        Assert.NotNull(element);
        Assert.IsType<Button>(element);
    }

    [Fact]
    public void FindElementById_EmptyCard_ShouldReturnNull()
    {
        var builder = CardBuilder.Create();

        var element = builder.FindElementById("any");

        Assert.Null(element);
    }

    [Fact]
    public void FindElementById_MultipleMatches_ShouldReturnFirst()
    {
        var builder = CardBuilder.Create()
            .Body(b => b
                .Button(btn => btn.Text("按钮1").ElementId("btn"))
                .Button(btn => btn.Text("按钮2").ElementId("btn")));

        var element = builder.FindElementById("btn");

        Assert.NotNull(element);
        var button = (Button)element;
        Assert.Equal("按钮1", button.Text?.Content);
    }

    [Fact]
    public void FindElementById_InCheckbox_ShouldFindElement()
    {
        var builder = CardBuilder.Create()
            .Body(b => b
                .Checkbox(cb => cb
                    .Name("checkbox1")
                    .ElementId("cb1")
                    .AddOption("v1", "选项1")));

        var element = builder.FindElementById("cb1");

        Assert.NotNull(element);
        Assert.IsType<Checkbox>(element);
    }

    [Fact]
    public void FindElementById_InDatePicker_ShouldFindElement()
    {
        var builder = CardBuilder.Create()
            .Body(b => b
                .DatePicker(dp => dp
                    .Name("date1")
                    .ElementId("dp1")));

        var element = builder.FindElementById("dp1");

        Assert.NotNull(element);
        Assert.IsType<DatePicker>(element);
    }

    [Fact]
    public void FindElementById_ComplexNestedStructure_ShouldFindElement()
    {
        var nestedInput = new Input { Name = "input1", ElementId = "nestedInput" };
        var form = new Form
        {
            Name = "form1",
            Elements = new List<Element> { nestedInput }
        };

        var outerDiv = new PlainTextElement
        {
            Direction = "vertical",
            Elements = new List<Element>
            {
                new ColumnSet
                {
                    Columns = new List<Column>
                    {
                        new() { Width = "100%", Elements = new List<Element> { form } }
                    }
                }
            }
        };

        var builder = CardBuilder.Create()
            .Body(b => b
                .AddElement(outerDiv)
                .Button(btn => btn.Text("按钮").ElementId("btn1")));

        var input = builder.FindElementById("nestedInput");
        var button = builder.FindElementById("btn1");

        Assert.NotNull(input);
        Assert.IsType<Input>(input);
        Assert.NotNull(button);
        Assert.IsType<Button>(button);
    }
}
