using LarkCardKit.Templates;
using Xunit;

namespace LarkCardKit.Tests;

public class TemplateParameterFillerTests
{
    [Fact]
    public void FillString_WithSinglePlaceholder_ShouldReplace()
    {
        var filler = new TemplateParameterFiller();
        filler.SetParameter("name", "张三");

        var result = filler.FillString("你好，${name}！");

        Assert.Equal("你好，张三！", result);
    }

    [Fact]
    public void FillString_WithMultiplePlaceholders_ShouldReplaceAll()
    {
        var filler = new TemplateParameterFiller();
        filler.SetParameter("firstName", "张");
        filler.SetParameter("lastName", "三");

        var result = filler.FillString("${firstName}${lastName}");

        Assert.Equal("张三", result);
    }

    [Fact]
    public void FillString_WithDefaultValue_ShouldUseDefault()
    {
        var filler = new TemplateParameterFiller();

        var result = filler.FillString("你好，${name:访客}！");

        Assert.Equal("你好，访客！", result);
    }

    [Fact]
    public void FillString_WithValueAndDefault_ShouldUseValue()
    {
        var filler = new TemplateParameterFiller();
        filler.SetParameter("name", "李四");

        var result = filler.FillString("你好，${name:访客}！");

        Assert.Equal("你好，李四！", result);
    }

    [Fact]
    public void FillString_WithUnmatchedPlaceholder_ShouldRemoveByDefault()
    {
        var filler = new TemplateParameterFiller();

        var result = filler.FillString("你好，${name}！");

        Assert.Equal("你好，！", result);
    }

    [Fact]
    public void FillString_WithKeepUnmatchedOption_ShouldKeepPlaceholder()
    {
        var options = new TemplateOptions { KeepUnmatchedPlaceholders = true };
        var filler = new TemplateParameterFiller(options);

        var result = filler.FillString("你好，${name}！");

        Assert.Equal("你好，${name}！", result);
    }

    [Fact]
    public void FillString_WithNullInput_ShouldReturnEmpty()
    {
        var filler = new TemplateParameterFiller();

        var result = filler.FillString(null);

        Assert.Equal(string.Empty, result);
    }

    [Fact]
    public void FillString_WithEmptyInput_ShouldReturnEmpty()
    {
        var filler = new TemplateParameterFiller();

        var result = filler.FillString("");

        Assert.Equal(string.Empty, result);
    }

    [Fact]
    public void FillString_WithNoPlaceholders_ShouldReturnOriginal()
    {
        var filler = new TemplateParameterFiller();
        filler.SetParameter("name", "张三");

        var result = filler.FillString("你好世界");

        Assert.Equal("你好世界", result);
    }

    [Fact]
    public void SetParameters_WithObject_ShouldSetAllProperties()
    {
        var filler = new TemplateParameterFiller();
        filler.SetParameters(new { Name = "张三", Age = 25 });

        Assert.Equal("张三", filler.GetValue("Name"));
        Assert.Equal(25, filler.GetValue<int>("Age"));
    }

    [Fact]
    public void SetParameters_WithDictionary_ShouldSetAllEntries()
    {
        var filler = new TemplateParameterFiller();
        var dict = new Dictionary<string, object?>
        {
            ["key1"] = "value1",
            ["key2"] = 123
        };
        filler.SetParameters(dict);

        Assert.Equal("value1", filler.GetValue("key1"));
        Assert.Equal(123, filler.GetValue<int>("key2"));
    }

    [Fact]
    public void SetParameters_WithNullObject_ShouldNotThrow()
    {
        var filler = new TemplateParameterFiller();

        var exception = Record.Exception(() => filler.SetParameters((object?)null));

        Assert.Null(exception);
    }

    [Fact]
    public void GetValue_WithNestedProperty_ShouldReturnNestedValue()
    {
        var filler = new TemplateParameterFiller();
        var user = new { Name = "张三", Address = new { City = "北京", Zip = "100000" } };
        filler.SetParameter("user", user);

        var userObj = filler.GetValue("user");
        Assert.NotNull(userObj);
        
        var result = filler.FillString("${user.Name}, ${user.Address.City}");
        Assert.Equal("张三, 北京", result);
    }

    [Fact]
    public void GetValue_WithNonExistentNestedProperty_ShouldReturnNull()
    {
        var filler = new TemplateParameterFiller();
        filler.SetParameter("user", new { Name = "张三" });

        var result = filler.GetValue("user.NonExistent");

        Assert.Null(result);
    }

    [Fact]
    public void FillString_WithNestedPlaceholder_ShouldReplace()
    {
        var filler = new TemplateParameterFiller();
        var user = new { Name = "李四", Address = new { City = "上海" } };
        filler.SetParameter("user", user);

        var result = filler.FillString("用户：${user.Name}，城市：${user.Address.City}");

        Assert.Equal("用户：李四，城市：上海", result);
    }

    [Fact]
    public void GetValue_Generic_ShouldReturnTypedValue()
    {
        var filler = new TemplateParameterFiller();
        filler.SetParameter("count", 42);

        var result = filler.GetValue<int>("count");

        Assert.Equal(42, result);
    }

    [Fact]
    public void GetValue_Generic_WithStringConversion_ShouldConvert()
    {
        var filler = new TemplateParameterFiller();
        filler.SetParameter("number", "123");

        var result = filler.GetValue<int>("number");

        Assert.Equal(123, result);
    }

    [Fact]
    public void GetValue_Generic_WithEnumConversion_ShouldConvert()
    {
        var filler = new TemplateParameterFiller();
        filler.SetParameter("status", "Active");

        var result = filler.GetValue<TestStatus>("status");

        Assert.Equal(TestStatus.Active, result);
    }

    [Fact]
    public void GetValue_Generic_WithBoolStringConversion_ShouldConvert()
    {
        var filler = new TemplateParameterFiller();
        filler.SetParameter("enabled", "true");

        var result = filler.GetValue<bool>("enabled");

        Assert.True(result);
    }

    [Fact]
    public void GetValue_WithNonExistentKey_ShouldReturnNull()
    {
        var filler = new TemplateParameterFiller();

        var result = filler.GetValue("nonexistent");

        Assert.Null(result);
    }

    [Fact]
    public void GetValue_Generic_WithNonExistentKey_ShouldReturnDefault()
    {
        var filler = new TemplateParameterFiller();

        var result = filler.GetValue<int>("nonexistent");

        Assert.Equal(0, result);
    }

    [Fact]
    public void HasParameter_ExistingParameter_ShouldReturnTrue()
    {
        var filler = new TemplateParameterFiller();
        filler.SetParameter("key", "value");

        var result = filler.HasParameter("key");

        Assert.True(result);
    }

    [Fact]
    public void HasParameter_NonExistentParameter_ShouldReturnFalse()
    {
        var filler = new TemplateParameterFiller();

        var result = filler.HasParameter("nonexistent");

        Assert.False(result);
    }

    [Fact]
    public void Clear_ShouldRemoveAllParameters()
    {
        var filler = new TemplateParameterFiller();
        filler.SetParameter("key1", "value1");
        filler.SetParameter("key2", "value2");

        filler.Clear();

        Assert.False(filler.HasParameter("key1"));
        Assert.False(filler.HasParameter("key2"));
    }

    [Fact]
    public void SetParameter_ShouldOverwriteExistingValue()
    {
        var filler = new TemplateParameterFiller();
        filler.SetParameter("key", "value1");
        filler.SetParameter("key", "value2");

        var result = filler.GetValue("key");

        Assert.Equal("value2", result);
    }

    [Fact]
    public void FillString_ComplexTemplate_ShouldReplaceAllPlaceholders()
    {
        var filler = new TemplateParameterFiller();
        filler.SetParameters(new
        {
            Title = "通知",
            UserName = "王五",
            Date = "2024-01-15",
            Count = 5
        });

        var template = "【${Title}】尊敬的${UserName}，您有${Count}条新消息，日期：${Date}";
        var result = filler.FillString(template);

        Assert.Equal("【通知】尊敬的王五，您有5条新消息，日期：2024-01-15", result);
    }

    [Fact]
    public void FillString_MixedWithDefaults_ShouldWork()
    {
        var filler = new TemplateParameterFiller();
        filler.SetParameter("name", "张三");

        var result = filler.FillString("姓名：${name}，年龄：${age:未知}");

        Assert.Equal("姓名：张三，年龄：未知", result);
    }

    private enum TestStatus
    {
        Inactive,
        Active
    }
}
