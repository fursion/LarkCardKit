using LarkCardKit.Builders;
using LarkCardKit.Models.Elements;
using Xunit;
using System.Text.Json;

namespace LarkCardKit.Tests.Components;

[Trait("Category", "Unit")]
public class BasicElementsTests
{
    private readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
    {
        WriteIndented = false,
        DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
    };

    public class PlainTextTests
    {
        [Fact]
        public void CreateBasicPlainText_ShouldSucceed()
        {
            var plainText = new PlainTextBuilder()
                .Content("Hello World")
                .Build();

            Assert.Equal("plain_text", plainText.Tag);
            Assert.Equal("Hello World", plainText.Content);
        }

        [Fact]
        public void SetAllProperties_ShouldSucceed()
        {
            var plainText = new PlainTextBuilder()
                .Content("测试文本")
                .TextSize("large")
                .TextColor("blue")
                .TextAlign("center")
                .Notation(true)
                .Width("200px")
                .ElementId("test-plain-1")
                .Margin("16px")
                .Build();

            Assert.Equal("large", plainText.TextSize);
            Assert.Equal("blue", plainText.TextColor);
            Assert.Equal("center", plainText.TextAlign);
            Assert.True(plainText.Notation);
            Assert.Equal("200px", plainText.Width);
            Assert.Equal("test-plain-1", plainText.ElementId);
            Assert.Equal("16px", plainText.Margin);
        }

        [Fact]
        public void SerializeToJson_ShouldContainAllProperties()
        {
            var plainText = new PlainTextBuilder()
                .Content("JSON 测试")
                .TextSize("normal")
                .ElementId("json-test")
                .Build();

            var json = JsonSerializer.Serialize(plainText);
            
            Assert.Contains("\"Tag\":\"plain_text\"", json);
            Assert.Contains("JSON", json);
            Assert.Contains("\"text_size\":\"normal\"", json);
            Assert.Contains("\"element_id\":\"json-test\"", json);
        }
    }

    public class MarkdownTests
    {
        [Fact]
        public void CreateBasicMarkdown_ShouldSucceed()
        {
            var markdown = new MarkdownBuilder()
                .Content("# Hello\n**粗体**和*斜体*")
                .Build();

            Assert.Equal("lark_md", markdown.Tag);
            Assert.Equal("# Hello\n**粗体**和*斜体*", markdown.Content);
        }

        [Fact]
        public void SetAllProperties_ShouldSucceed()
        {
            var markdown = new MarkdownBuilder()
                .Content("测试 Markdown")
                .TextSize("large")
                .TextColor("red")
                .TextAlign("right")
                .Icon("icon-home", "blue")
                .ElementId("test-md-1")
                .Margin("8px")
                .Build();

            Assert.Equal("large", markdown.TextSize);
            Assert.Equal("red", markdown.TextColor);
            Assert.Equal("right", markdown.TextAlign);
            Assert.Equal("test-md-1", markdown.ElementId);
            Assert.Equal("8px", markdown.Margin);
        }

        [Fact]
        public void SupportMarkdownSyntax_ShouldSucceed()
        {
            var markdown = new MarkdownBuilder()
                .Content("**粗体** *斜体* ~~删除线~~ [链接](https://example.com)")
                .Build();

            Assert.Contains("**粗体**", markdown.Content);
            Assert.Contains("*斜体*", markdown.Content);
            Assert.Contains("~~删除线~~", markdown.Content);
        }
    }

    public class DivTests
    {
        [Fact]
        public void CreateBasicDiv_ShouldSucceed()
        {
            var div = new TextDivBuilder()
                .Text("Div 容器内容")
                .Build();

            Assert.Equal("div", div.Tag);
            Assert.NotNull(div.Text);
            Assert.Equal("Div 容器内容", div.Text.Content);
        }

        [Fact]
        public void SetLayoutProperties_ShouldSucceed()
        {
            var div = new TextDivBuilder()
                .Direction("horizontal")
                .Padding("16px")
                .VerticalSpacing("8px")
                .HorizontalSpacing("12px")
                .HorizontalAlign(LarkCardKit.Enums.AlignType.Center)
                .VerticalAlign(LarkCardKit.Enums.AlignType.Center)
                .Build();

            Assert.Equal("horizontal", div.Direction);
            Assert.Equal("16px", div.Padding);
            Assert.Equal("8px", div.VerticalSpacing);
            Assert.Equal("12px", div.HorizontalSpacing);
        }

        [Fact]
        public void VerticalLayout_ShouldSucceed()
        {
            var div = new TextDivBuilder()
                .Vertical()
                .Text("垂直布局文本")
                .Build();

            Assert.Equal("vertical", div.Direction);
            Assert.NotNull(div.Text);
        }

        [Fact]
        public void HorizontalLayout_ShouldSucceed()
        {
            var div = new TextDivBuilder()
                .Horizontal()
                .Text("水平布局文本")
                .Build();

            Assert.Equal("horizontal", div.Direction);
            Assert.NotNull(div.Text);
        }

        [Fact]
        public void AddMultipleElements_ShouldUseCardBodyBuilder()
        {
            // 使用 CardBodyBuilder 来添加多个文本元素
            var card = CardBuilder.Create()
                .Body(b => b
                    .PlainText("文本")
                    .Markdown("**Markdown**"))
                .Build();

            Assert.NotNull(card.Body);
            Assert.Equal(2, card.Body.Elements.Count);
        }
    }

    public class SerializationTests
    {
        private readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
        {
            WriteIndented = false,
            DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
        };

        [Fact]
        public void SerializeNestedStructure_ShouldSucceed()
        {
            var div = new TextDivBuilder()
                .Text("外层")
                .Build();

            var json = JsonSerializer.Serialize(div);

            Assert.Contains("div", json);
            Assert.Contains("plain_text", json);
        }

        [Fact]
        public void IgnoreNullValues_ShouldSucceed()
        {
            var plainText = new PlainTextBuilder()
                .Content("测试")
                .Build();

            var json = JsonSerializer.Serialize(plainText, _jsonOptions);
            
            Assert.DoesNotContain("null", json);
        }

        [Fact]
        public void IncludeAllSetProperties_ShouldSucceed()
        {
            var markdown = new MarkdownBuilder()
                .Content("完整测试")
                .TextSize("large")
                .TextColor("blue")
                .ElementId("full-test")
                .Build();

            var json = JsonSerializer.Serialize(markdown);

            Assert.Contains("content", json);
            Assert.Contains("\"text_size\":\"large\"", json);
            Assert.Contains("\"text_color\":\"blue\"", json);
            Assert.Contains("\"element_id\":\"full-test\"", json);
        }
    }
}
