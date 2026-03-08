using LarkCardKit.Builders;
using LarkCardKit.Enums;
using LarkCardKit.Models.Elements;

namespace LarkCardKit.Examples;

public static class ElementOperationsExample
{
    public static void Run()
    {
        Console.WriteLine("=== 组件查找、修改、替换示例 ===\n");

        Example1_FindElementById();
        Example2_ModifyElement();
        Example3_ReplaceElement();
        Example4_ComplexOperations();
    }

    private static void Example1_FindElementById()
    {
        Console.WriteLine("--- 示例 1: FindElementById 查找组件 ---");

        var builder = CardBuilder.Create()
            .Header(h => h.Title("查找组件示例"))
            .Body(b => b
                .PlainText("这是一段文本")
                .Button(btn => btn
                    .Text("点击我")
                    .ElementId("btn1")
                    .Type(ButtonType.Primary))
                .Div(d => d
                    .PlainText("Div 内的文本").ElementId("divText1")));

        var button = builder.FindElementById<Button>("btn1");
        if (button != null)
        {
            Console.WriteLine($"找到按钮: ElementId={button.ElementId}, Text={button.Text?.Content}");
        }

        var divText = builder.FindElementById<PlainText>("divText1");
        if (divText != null)
        {
            Console.WriteLine($"找到 Div 内文本: ElementId={divText.ElementId}, Content={divText.Content}");
        }

        var notFound = builder.FindElementById("nonexistent");
        Console.WriteLine($"查找不存在的元素: {(notFound == null ? "未找到" : "找到了")}");

        Console.WriteLine();
    }

    private static void Example2_ModifyElement()
    {
        Console.WriteLine("--- 示例 2: ModifyElement 修改组件 ---");

        var builder = CardBuilder.Create()
            .Header(h => h.Title("修改组件示例"))
            .Body(b => b
                .Div(d => d
                    .PlainText("原始文本内容").ElementId("text1"))
                .Button(btn => btn
                    .Text("原始按钮")
                    .ElementId("btn1")
                    .Type(ButtonType.Primary)));

        Console.WriteLine("修改前:");
        Console.WriteLine(builder.ToJson(indented: true));
        Console.WriteLine();

        builder.ModifyElement<PlainText>("text1", text =>
        {
            text.Content = "修改后的文本内容";
        });

        builder.ModifyElement<Button>("btn1", btn =>
        {
            btn.Text = new PlainText { Content = "修改后的按钮" };
            btn.Type = "danger";
        });

        Console.WriteLine("修改后:");
        Console.WriteLine(builder.ToJson(indented: true));
        Console.WriteLine();
    }

    private static void Example3_ReplaceElement()
    {
        Console.WriteLine("--- 示例 3: ReplaceElement 替换组件 ---");

        var builder = CardBuilder.Create()
            .Header(h => h.Title("替换组件示例"))
            .Body(b => b
                .Div(d => d
                    .PlainText("将被替换的文本").ElementId("text1"))
                .Button(btn => btn
                    .Text("将被替换的按钮")
                    .ElementId("btn1")
                    .Type(ButtonType.Default)));

        Console.WriteLine("替换前:");
        Console.WriteLine(builder.ToJson(indented: true));
        Console.WriteLine();

        var newMarkdown = new Markdown
        {
            ElementId = "text1",
            Content = "**这是新替换的 Markdown 内容**"
        };
        builder.ReplaceElement("text1", newMarkdown);

        var newButton = new Button
        {
            ElementId = "btn1",
            Text = new PlainText { Content = "新按钮" },
            Type = "primary"
        };
        builder.ReplaceElement("btn1", newButton);

        Console.WriteLine("替换后:");
        Console.WriteLine(builder.ToJson(indented: true));
        Console.WriteLine();
    }

    private static void Example4_ComplexOperations()
    {
        Console.WriteLine("--- 示例 4: 复杂操作 - 表单内组件操作 ---");

        var builder = CardBuilder.Create()
            .Header(h => h.Title("表单组件操作"))
            .Body(b => b
                .Form(f => f
                    .Name("userForm")
                    .Input(i => i
                        .Name("username")
                        .Placeholder("请输入用户名")
                        .ElementId("inputUsername"))
                    .Input(i => i
                        .Name("email")
                        .Placeholder("请输入邮箱")
                        .ElementId("inputEmail"))
                    .Select(s => s
                        .Name("department")
                        .Placeholder("请选择部门")
                        .ElementId("selectDept")
                        .AddOption("tech", "技术部")
                        .AddOption("sales", "销售部"))
                    .Button(btn => btn
                        .Text("提交")
                        .ElementId("submitBtn")
                        .Type(ButtonType.Primary))));

        Console.WriteLine("原始表单:");
        Console.WriteLine(builder.ToJson(indented: true));
        Console.WriteLine();

        builder.ModifyElement<Input>("inputUsername", input =>
        {
            input.Placeholder = new PlainText { Content = "请输入您的用户名（必填）" };
            input.Required = true;
        });

        builder.ModifyElement<Input>("inputEmail", input =>
        {
            input.Placeholder = new PlainText { Content = "例如：example@company.com" };
        });

        var selectBuilder = new SelectBuilder()
            .AddOption("hr", "人事部")
            .AddOption("finance", "财务部");
        builder.ReplaceElement("selectDept", selectBuilder.Build());

        builder.ModifyElement<Button>("submitBtn", btn =>
        {
            btn.Text = new PlainText { Content = "提交表单" };
        });

        Console.WriteLine("修改后的表单:");
        Console.WriteLine(builder.ToJson(indented: true));
        Console.WriteLine();
    }
}
