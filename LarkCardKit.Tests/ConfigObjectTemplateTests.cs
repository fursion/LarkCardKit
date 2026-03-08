using LarkCardKit.Builders;
using LarkCardKit.Enums;
using LarkCardKit.Models.Elements;
using Xunit;

namespace LarkCardKit.Tests;

public class ConfigObjectTemplateTests
{
    [Fact]
    public void TemplateValue_EnumValue_ShouldConvertCorrectly()
    {
        var templateValue = new LarkCardKit.Templates.TemplateValue("buttonType");
        
        Assert.Equal("buttonType", templateValue.Key);
        Assert.Null(templateValue.DefaultValue);
    }

    [Fact]
    public void TemplateValue_WithDefault_ShouldSetDefault()
    {
        var templateValue = LarkCardKit.Templates.TemplateValue.WithDefault("key", "default");
        
        Assert.Equal("key", templateValue.Key);
        Assert.Equal("default", templateValue.DefaultValue);
    }

    [Fact]
    public void TemplateValue_ImplicitConversionFromString_ShouldWork()
    {
        LarkCardKit.Templates.TemplateValue templateValue = "myKey";
        
        Assert.Equal("myKey", templateValue.Key);
    }

    [Fact]
    public void CardBuilder_SetParameter_WithEnum_ShouldWork()
    {
        var card = CardBuilder.Create()
            .SetParameter("title", "测试标题")
            .Header(h => h.Title("${title}"))
            .Build();

        var title = card.Header?.Title as PlainText;
        Assert.NotNull(title);
        Assert.Equal("测试标题", title.Content);
    }

    [Fact]
    public void CardBuilder_SetParameter_WithBool_ShouldWork()
    {
        var filler = new LarkCardKit.Templates.TemplateParameterFiller();
        filler.SetParameter("enabled", true);

        var result = filler.GetValue<bool>("enabled");

        Assert.True(result);
    }

    [Fact]
    public void CardBuilder_SetParameter_WithComplexObject_ShouldWork()
    {
        var config = new
        {
            Title = "卡片标题",
            UpdateMulti = true,
            StreamingMode = false
        };

        var card = CardBuilder.Create()
            .SetParameters(config)
            .Header(h => h.Title("${Title}"))
            .Config(c => c
                .UpdateMulti(config.UpdateMulti)
                .StreamingMode(config.StreamingMode))
            .Build();

        var title = card.Header?.Title as PlainText;
        Assert.NotNull(title);
        Assert.Equal("卡片标题", title.Content);
        Assert.True(card.Config?.UpdateMulti);
        Assert.False(card.Config?.StreamingMode);
    }

    [Fact]
    public void TemplateParameterFiller_TypeConversion_IntToString()
    {
        var filler = new LarkCardKit.Templates.TemplateParameterFiller();
        filler.SetParameter("count", 42);

        var result = filler.FillString("数量：${count}");

        Assert.Equal("数量：42", result);
    }

    [Fact]
    public void TemplateParameterFiller_TypeConversion_BoolToString()
    {
        var filler = new LarkCardKit.Templates.TemplateParameterFiller();
        filler.SetParameter("active", true);

        var result = filler.FillString("状态：${active}");

        Assert.Equal("状态：True", result);
    }

    [Fact]
    public void TemplateParameterFiller_TypeConversion_DateTimeToString()
    {
        var filler = new LarkCardKit.Templates.TemplateParameterFiller();
        var date = new DateTime(2024, 1, 15);
        filler.SetParameter("date", date);

        var result = filler.FillString("日期：${date}");

        Assert.Contains("2024", result);
    }

    [Fact]
    public void TemplateParameterFiller_TypeConversion_StringToInt()
    {
        var filler = new LarkCardKit.Templates.TemplateParameterFiller();
        filler.SetParameter("number", "123");

        var result = filler.GetValue<int>("number");

        Assert.Equal(123, result);
    }

    [Fact]
    public void TemplateParameterFiller_TypeConversion_StringToBool()
    {
        var filler = new LarkCardKit.Templates.TemplateParameterFiller();
        filler.SetParameter("flag", "true");

        var result = filler.GetValue<bool>("flag");

        Assert.True(result);
    }

    [Fact]
    public void TemplateParameterFiller_TypeConversion_StringToEnum()
    {
        var filler = new LarkCardKit.Templates.TemplateParameterFiller();
        filler.SetParameter("size", "CropCenter");

        var result = filler.GetValue<ImageSize>("size");

        Assert.Equal(ImageSize.CropCenter, result);
    }

    [Fact]
    public void TemplateParameterFiller_TypeConversion_IntToEnum()
    {
        var filler = new LarkCardKit.Templates.TemplateParameterFiller();
        filler.SetParameter("type", 1);

        var result = filler.GetValue<ButtonType>("type");

        Assert.Equal((ButtonType)1, result);
    }

    [Fact]
    public void TemplateParameterFiller_ComplexObject_NestedAccess()
    {
        var filler = new LarkCardKit.Templates.TemplateParameterFiller();
        var user = new UserInfo
        {
            Name = "张三",
            Email = "zhangsan@example.com",
            Profile = new UserProfile
            {
                Department = "技术部",
                Level = 5
            }
        };
        filler.SetParameter("user", user);

        var userObj = filler.GetValue("user");
        Assert.NotNull(userObj);
        
        var result = filler.FillString("用户：${user.Name}，部门：${user.Profile.Department}");
        Assert.Equal("用户：张三，部门：技术部", result);
    }

    [Fact]
    public void TemplateParameterFiller_ComplexObject_InTemplate()
    {
        var filler = new LarkCardKit.Templates.TemplateParameterFiller();
        var order = new
        {
            OrderId = "ORD-001",
            Customer = new
            {
                Name = "李四",
                Phone = "13800138000"
            },
            Items = 3
        };
        filler.SetParameter("order", order);

        var result = filler.FillString("订单${order.OrderId}，客户：${order.Customer.Name}，商品数：${order.Items}");

        Assert.Equal("订单ORD-001，客户：李四，商品数：3", result);
    }

    [Fact]
    public void TemplateParameterFiller_NullableValue_ShouldHandleNull()
    {
        var filler = new LarkCardKit.Templates.TemplateParameterFiller();
        filler.SetParameter("value", null);

        var result = filler.GetValue("value");

        Assert.Null(result);
    }

    [Fact]
    public void TemplateParameterFiller_DictionaryValue_ShouldWork()
    {
        var filler = new LarkCardKit.Templates.TemplateParameterFiller();
        var dict = new Dictionary<string, object>
        {
            ["key1"] = "value1",
            ["key2"] = 100
        };
        filler.SetParameter("config", dict);

        var result = filler.GetValue<Dictionary<string, object>>("config");

        Assert.NotNull(result);
        Assert.Equal("value1", result["key1"]);
        Assert.Equal(100, result["key2"]);
    }

    [Fact]
    public void TemplateParameterFiller_ListValue_ShouldWork()
    {
        var filler = new LarkCardKit.Templates.TemplateParameterFiller();
        var list = new List<string> { "a", "b", "c" };
        filler.SetParameter("items", list);

        var result = filler.GetValue<List<string>>("items");

        Assert.NotNull(result);
        Assert.Equal(3, result.Count);
        Assert.Equal("a", result[0]);
    }

    [Fact]
    public void TemplateParameterFiller_DoubleConversion_ShouldWork()
    {
        var filler = new LarkCardKit.Templates.TemplateParameterFiller();
        filler.SetParameter("price", "99.99");

        var result = filler.GetValue<double>("price");

        Assert.Equal(99.99, result, 2);
    }

    [Fact]
    public void TemplateParameterFiller_DecimalConversion_ShouldWork()
    {
        var filler = new LarkCardKit.Templates.TemplateParameterFiller();
        filler.SetParameter("amount", "1234.56");

        var result = filler.GetValue<decimal>("amount");

        Assert.Equal(1234.56m, result);
    }

    [Fact]
    public void CardBuilder_SetParameters_Dictionary_ShouldWork()
    {
        var parameters = new Dictionary<string, object?>
        {
            ["title"] = "动态标题",
            ["content"] = "动态内容"
        };

        var card = CardBuilder.Create()
            .SetParameters(parameters)
            .Header(h => h.Title("${title}"))
            .Body(b => b.PlainText("${content}"))
            .Build();

        var title = card.Header?.Title as PlainText;
        Assert.NotNull(title);
        Assert.Equal("动态标题", title.Content);
    }

    [Fact]
    public void CardBuilder_SetParameters_Object_ShouldWork()
    {
        var card = CardBuilder.Create()
            .SetParameters(new { Title = "对象标题", Subtitle = "副标题" })
            .Header(h => h.Title("${Title}"))
            .Body(b => b.PlainText("${Subtitle}"))
            .Build();

        var title = card.Header?.Title as PlainText;
        Assert.NotNull(title);
        Assert.Equal("对象标题", title.Content);
    }

    [Fact]
    public void TemplateParameterFiller_EmptyStringDefault_ShouldWork()
    {
        var filler = new LarkCardKit.Templates.TemplateParameterFiller();

        var result = filler.FillString("值：${missing:}");

        Assert.Equal("值：", result);
    }

    [Fact]
    public void TemplateParameterFiller_SpecialCharactersInDefault_ShouldWork()
    {
        var filler = new LarkCardKit.Templates.TemplateParameterFiller();

        var result = filler.FillString("值：${missing:默认值（特殊）}");

        Assert.Equal("值：默认值（特殊）", result);
    }

    private class UserInfo
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public UserProfile? Profile { get; set; }
    }

    private class UserProfile
    {
        public string Department { get; set; } = string.Empty;
        public int Level { get; set; }
    }
}
