using LarkCardKit.Builders;
using LarkCardKit.Models.Elements;
using Xunit;

namespace LarkCardKit.Tests;

public class CardBuilderElementModifierTests
{
    [Fact]
    public void ModifyElement_ExistingElement_ShouldModifySuccessfully()
    {
        var builder = CardBuilder.Create()
            .Body(b => b
                .Button(btn => btn
                    .Text("原始文本")
                    .ElementId("btn1")));

        var result = builder.ModifyElement("btn1", e =>
        {
            if (e is Button button)
            {
                button.Text = new PlainText { Content = "修改后文本" };
            }
        });

        Assert.True(result);
        var button = builder.FindElementById<Button>("btn1");
        Assert.NotNull(button);
        Assert.Equal("修改后文本", button.Text?.Content);
    }

    [Fact]
    public void ModifyElement_NonExistentElement_ShouldReturnFalse()
    {
        var builder = CardBuilder.Create()
            .Body(b => b.PlainText("内容"));

        var result = builder.ModifyElement("nonexistent", e => { });

        Assert.False(result);
    }

    [Fact]
    public void ModifyElement_Generic_ShouldModifyTypedElement()
    {
        var builder = CardBuilder.Create()
            .Body(b => b
                .Input(i => i
                    .Name("username")
                    .Placeholder("请输入")
                    .ElementId("input1")));

        var result = builder.ModifyElement<Input>("input1", input =>
        {
            input.Placeholder = new PlainText { Content = "请输入用户名" };
            input.Required = true;
        });

        Assert.True(result);
        var input = builder.FindElementById<Input>("input1");
        Assert.NotNull(input);
        Assert.Equal("请输入用户名", input.Placeholder?.Content);
        Assert.True(input.Required);
    }

    [Fact]
    public void ModifyElement_Generic_WrongType_ShouldReturnFalse()
    {
        var builder = CardBuilder.Create()
            .Body(b => b
                .Button(btn => btn.Text("按钮").ElementId("btn1")));

        var result = builder.ModifyElement<Input>("btn1", input =>
        {
            input.Placeholder = new PlainText { Content = "占位符" };
        });

        Assert.False(result);
    }

    [Fact]
    public void TryModifyElement_ShouldWorkSameAsModifyElement()
    {
        var builder = CardBuilder.Create()
            .Body(b => b
                .Select(s => s
                    .Name("selector")
                    .ElementId("select1")
                    .AddOption("v1", "选项1")));

        var result = builder.TryModifyElement<Select>("select1", select =>
        {
            select.Options ??= new List<SelectOption>();
            select.Options.Add(new SelectOption
            {
                Value = "v2",
                Text = new PlainText { Content = "选项2" }
            });
        });

        Assert.True(result);
        var select = builder.FindElementById<Select>("select1");
        Assert.NotNull(select);
        Assert.Equal(2, select.Options?.Count);
    }

    [Fact]
    public void ModifyElement_InDiv_ShouldModifySuccessfully()
    {
        var pt = new PlainText { Content = "原始内容", ElementId = "text1" };
        var builder = CardBuilder.Create()
            .Body(b => b
                .Div(d => d
                    .Vertical()
                    .Add(pt)));

        var result = builder.ModifyElement<PlainText>("text1", text =>
        {
            text.Content = "修改后内容";
        });

        Assert.True(result);
        var plainText = builder.FindElementById<PlainText>("text1");
        Assert.NotNull(plainText);
        Assert.Equal("修改后内容", plainText.Content);
    }

    [Fact]
    public void ModifyElement_InForm_ShouldModifySuccessfully()
    {
        var builder = CardBuilder.Create()
            .Body(b => b
                .Form(f => f
                    .Name("form1")
                    .Input(i => i
                        .Name("email")
                        .ElementId("emailInput"))));

        var result = builder.ModifyElement<Input>("emailInput", input =>
        {
            input.Placeholder = new PlainText { Content = "请输入邮箱" };
        });

        Assert.True(result);
        var input = builder.FindElementById<Input>("emailInput");
        Assert.NotNull(input);
        Assert.Equal("请输入邮箱", input.Placeholder?.Content);
    }

    [Fact]
    public void ModifyElement_InColumnSet_ShouldModifySuccessfully()
    {
        var builder = CardBuilder.Create()
            .Body(b => b
                .ColumnSet(cs => cs
                    .AddColumn(col => col
                        .Width("50%")
                        .Button(btn => btn
                            .Text("列按钮")
                            .ElementId("colBtn")))));

        var result = builder.ModifyElement<Button>("colBtn", button =>
        {
            button.Text = new PlainText { Content = "修改后的列按钮" };
        });

        Assert.True(result);
        var button = builder.FindElementById<Button>("colBtn");
        Assert.NotNull(button);
        Assert.Equal("修改后的列按钮", button.Text?.Content);
    }

    [Fact]
    public void ModifyElement_MultipleProperties_ShouldModifyAll()
    {
        var builder = CardBuilder.Create()
            .Body(b => b
                .Checkbox(cb => cb
                    .Name("checkbox1")
                    .ElementId("cb1")
                    .AddOption("v1", "选项1")));

        var result = builder.ModifyElement<Checkbox>("cb1", checkbox =>
        {
            checkbox.Name = "modifiedCheckbox";
            checkbox.Required = true;
            checkbox.Options ??= new List<CheckboxOption>();
            checkbox.Options.Add(new CheckboxOption
            {
                Value = "v2",
                Text = new PlainText { Content = "选项2" }
            });
        });

        Assert.True(result);
        var checkbox = builder.FindElementById<Checkbox>("cb1");
        Assert.NotNull(checkbox);
        Assert.Equal("modifiedCheckbox", checkbox.Name);
        Assert.True(checkbox.Required);
        Assert.Equal(2, checkbox.Options?.Count);
    }

    [Fact]
    public void ModifyElement_NestedStructure_ShouldModifySuccessfully()
    {
        var nestedMd = new Markdown { Content = "原始Markdown", ElementId = "md1" };
        var nestedDiv = new Div
        {
            Direction = "vertical",
            Elements = new List<Element> { nestedMd }
        };
        var outerDiv = new Div
        {
            Direction = "vertical",
            Elements = new List<Element> { nestedDiv }
        };

        var builder = CardBuilder.Create()
            .Body(b => b.AddElement(outerDiv));

        var result = builder.ModifyElement<Markdown>("md1", markdown =>
        {
            markdown.Content = "**修改后的Markdown**";
        });

        Assert.True(result);
        var md = builder.FindElementById<Markdown>("md1");
        Assert.NotNull(md);
        Assert.Equal("**修改后的Markdown**", md.Content);
    }

    [Fact]
    public void ModifyElement_EmptyCard_ShouldReturnFalse()
    {
        var builder = CardBuilder.Create();

        var result = builder.ModifyElement("any", e => { });

        Assert.False(result);
    }

    [Fact]
    public void ModifyElement_AfterBuild_ShouldStillModify()
    {
        var builder = CardBuilder.Create()
            .Body(b => b
                .Button(btn => btn
                    .Text("按钮")
                    .ElementId("btn1")));

        builder.Build();
        
        var result = builder.ModifyElement<Button>("btn1", btn =>
        {
            btn.Text = new PlainText { Content = "修改后" };
        });

        Assert.True(result);
        var button = builder.FindElementById<Button>("btn1");
        Assert.NotNull(button);
        Assert.Equal("修改后", button.Text?.Content);
    }

    [Fact]
    public void ModifyElement_DatePicker_ShouldModifySuccessfully()
    {
        var builder = CardBuilder.Create()
            .Body(b => b
                .DatePicker(dp => dp
                    .Name("date1")
                    .ElementId("dp1")
                    .Placeholder("选择日期")));

        var result = builder.ModifyElement<DatePicker>("dp1", datePicker =>
        {
            datePicker.Placeholder = new PlainText { Content = "请选择日期" };
            datePicker.InitialDate = "2024-01-01";
        });

        Assert.True(result);
        var dp = builder.FindElementById<DatePicker>("dp1");
        Assert.NotNull(dp);
        Assert.Equal("请选择日期", dp.Placeholder?.Content);
        Assert.Equal("2024-01-01", dp.InitialDate);
    }
}
