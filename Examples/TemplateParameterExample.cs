using LarkCardKit.Builders;
using LarkCardKit.Enums;
using LarkCardKit.Models.Elements;

namespace LarkCardKit.Examples;

public static class TemplateParameterExample
{
    public static void Run()
    {
        Console.WriteLine("=== 模板参数填充示例 ===\n");

        Example1_BasicPlaceholder();
        Example2_DefaultValue();
        Example3_NestedProperties();
        Example4_ConfigObjectTemplate();
        Example5_ComplexTemplate();
    }

    private static void Example1_BasicPlaceholder()
    {
        Console.WriteLine("--- 示例 1: 基本占位符 ${key} ---");

        var builder = CardBuilder.Create()
            .Header(h => h.Title("欢迎您，${userName}！"))
            .Body(b => b
                .PlainText("您的订单号是：${orderId}")
                .PlainText("订单状态：${status}")
                .Button(btn => btn
                    .Text("查看详情")
                    .ElementId("detailBtn")
                    .Type(ButtonType.Primary)));

        Console.WriteLine("填充前:");
        Console.WriteLine(builder.ToJson(indented: true));
        Console.WriteLine();

        builder.SetParameter("userName", "张三");
        builder.SetParameter("orderId", "ORD-2024-001234");
        builder.SetParameter("status", "已发货");

        Console.WriteLine("填充后:");
        Console.WriteLine(builder.ToJson(indented: true));
        Console.WriteLine();
    }

    private static void Example2_DefaultValue()
    {
        Console.WriteLine("--- 示例 2: 默认值 ${key:default} ---");

        var builder = CardBuilder.Create()
            .Header(h => h.Title("通知消息"))
            .Body(b => b
                .PlainText("发送人：${sender:系统管理员}")
                .PlainText("发送时间：${sendTime:刚刚")
                .PlainText("优先级：${priority:普通}")
                .Markdown("消息内容：\n${content:暂无内容}"));

        Console.WriteLine("不提供参数（使用默认值）:");
        Console.WriteLine(builder.ToJson(indented: true));
        Console.WriteLine();

        var builder2 = CardBuilder.Create()
            .Header(h => h.Title("通知消息"))
            .Body(b => b
                .PlainText("发送人：${sender:系统管理员}")
                .PlainText("发送时间：${sendTime:刚刚")
                .PlainText("优先级：${priority:普通}")
                .Markdown("消息内容：\n${content:暂无内容}"));

        builder2.SetParameter("sender", "李四");
        builder2.SetParameter("priority", "紧急");
        builder2.SetParameter("content", "这是一条重要的通知消息！");

        Console.WriteLine("提供部分参数:");
        Console.WriteLine(builder2.ToJson(indented: true));
        Console.WriteLine();
    }

    private static void Example3_NestedProperties()
    {
        Console.WriteLine("--- 示例 3: 嵌套属性 ${user.name} ---");

        var builder = CardBuilder.Create()
            .Header(h => h.Title("用户信息卡片"))
            .Body(b => b
                .PlainText("姓名：${user.name}")
                .PlainText("部门：${user.department}")
                .PlainText("邮箱：${user.email}")
                .PlainText("经理：${user.manager.name:未指定}")
                .Button(btn => btn
                    .Text("联系 ${user.name}")
                    .ElementId("contactBtn")
                    .Type(ButtonType.Primary)));

        var userData = new
        {
            user = new
            {
                name = "王五",
                department = "技术研发部",
                email = "wangwu@company.com",
                manager = new
                {
                    name = "赵六"
                }
            }
        };

        builder.SetParameters(userData);

        Console.WriteLine(builder.ToJson(indented: true));
        Console.WriteLine();
    }

    private static void Example4_ConfigObjectTemplate()
    {
        Console.WriteLine("--- 示例 4: 配置对象模板 ---");

        var config = new
        {
            appName = "飞书卡片助手",
            version = "2.0.0",
            environment = "生产环境",
            features = new
            {
                darkMode = true,
                notifications = true,
                autoSave = false
            }
        };

        var builder = CardBuilder.Create()
            .Header(h => h.Title("${appName} v${version}"))
            .Body(b => b
                .PlainText("运行环境：${environment}")
                .Markdown("### 功能状态\n")
                .Div(d => d
                    .Vertical()
                    .PlainText("深色模式：${features.darkMode}")
                    .PlainText("消息通知：${features.notifications}")
                    .PlainText("自动保存：${features.autoSave}"))
                .Button(btn => btn
                    .Text("查看设置")
                    .ElementId("settingsBtn")
                    .Type(ButtonType.Default)));

        builder.SetParameters(config);

        Console.WriteLine(builder.ToJson(indented: true));
        Console.WriteLine();
    }

    private static void Example5_ComplexTemplate()
    {
        Console.WriteLine("--- 示例 5: 复杂模板场景 ---");

        var orderData = new Dictionary<string, object?>
        {
            ["orderNumber"] = "ORD-2024-009876",
            ["customerName"] = "测试公司",
            ["totalAmount"] = "¥12,580.00",
            ["items"] = 5,
            ["status"] = "配送中",
            ["estimatedArrival"] = "2024-01-15",
            ["trackingNumber"] = "SF1234567890",
            ["supportPhone"] = "400-123-4567"
        };

        var builder = CardBuilder.Create()
            .Header(h => h.Title("订单详情"))
            .Body(b => b
                .Div(d => d
                    .Vertical()
                    .VerticalSpacing("8px")
                    .PlainText("**订单号**：${orderNumber}")
                    .PlainText("**客户**：${customerName}")
                    .PlainText("**金额**：${totalAmount}")
                    .PlainText("**商品数**：${items} 件"))
                .Div(d => d
                    .Vertical()
                    .VerticalSpacing("8px")
                    .Margin("12px 0 0 0")
                    .PlainText("**物流状态**：${status}")
                    .PlainText("**预计到达**：${estimatedArrival:待定}")
                    .PlainText("**快递单号**：${trackingNumber:暂无}"))
                .ColumnSet(cs => cs
                    .Margin("16px 0 0 0")
                    .AddColumn(col => col
                        .Width("fill")
                        .Button(btn => btn
                            .Text("查看物流")
                            .ElementId("trackBtn")
                            .Type(ButtonType.Primary)))
                    .AddColumn(col => col
                        .Width("fill")
                        .Button(btn => btn
                            .Text("联系客服")
                            .ElementId("contactBtn")
                            .Type(ButtonType.Default)))));

        builder.SetParameters(orderData);

        Console.WriteLine(builder.ToJson(indented: true));
        Console.WriteLine();
    }
}
