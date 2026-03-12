using LarkCardKit.Builders;
using LarkCardKit.Models.Elements;
using Xunit;
using System.Text.Json;

namespace LarkCardKit.Tests.Components;

[Trait("Category", "Unit")]
[Trait("Category", "NewComponents")]
public class NewElementsTests
{
    private readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
    {
        WriteIndented = false,
        DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
    };

    public class LoopTests
    {
        [Fact]
        public void CreateBasicLoop_ShouldSucceed()
        {
            var loop = new LoopBuilder()
                .List(new List<object> { 1, 2, 3 })
                .Template(new PlainTextBuilder().Content("Item").Build())
                .Build();

            Assert.Equal("loop", loop.Tag);
            Assert.NotNull(loop.DataSource);
            Assert.NotNull(loop.Template);
        }

        [Fact]
        public void SetDataSource_ShouldSucceed()
        {
            var dataSource = new LoopDataSource
            {
                List = new List<object> { "item1", "item2", "item3" }
            };

            var loop = new LoopBuilder()
                .DataSource(dataSource)
                .Template(new PlainTextBuilder().Content("Test").Build())
                .Build();

            Assert.Equal(dataSource, loop.DataSource);
        }

        [Fact]
        public void SetListData_ShouldSucceed()
        {
            var items = new List<object>
            {
                new { name = "Item 1", value = 1 },
                new { name = "Item 2", value = 2 }
            };

            var loop = new LoopBuilder()
                .List(items)
                .Template(new PlainTextBuilder().Content("Template").Build())
                .Build();

            var json = JsonSerializer.Serialize(loop);
            Assert.Contains("\"list\"", json);
            Assert.Contains("Item 1", json);
        }

        [Fact]
        public void SetTemplate_ShouldSucceed()
        {
            var template = new PlainTextBuilder()
                .Content("Template Content")
                .ElementId("template-1")
                .Build();

            var loop = new LoopBuilder()
                .List(new List<object> { 1, 2, 3 })
                .Template(template)
                .Build();

            Assert.Equal(template, loop.Template);
            Assert.Equal("template-1", template.ElementId);
        }

        [Fact]
        public void SetElementId_ShouldSucceed()
        {
            var loop = new LoopBuilder()
                .List(new List<object> { 1, 2 })
                .Template(new PlainTextBuilder().Content("Item").Build())
                .ElementId("loop-1")
                .Build();

            Assert.Equal("loop-1", loop.ElementId);
        }
    }

    public class CollapsiblePanelTests
    {
        [Fact]
        public void CreateBasicCollapsiblePanel_ShouldSucceed()
        {
            var panel = new CollapsiblePanelBuilder()
                .Title("折叠面板标题")
                .Build();

            Assert.Equal("collapsible_panel", panel.Tag);
            Assert.NotNull(panel.Header);
            Assert.NotNull(panel.Header.Title);
        }

        [Fact]
        public void SetExpanded_ShouldSucceed()
        {
            var panel = new CollapsiblePanelBuilder()
                .Expanded(true)
                .Title("展开的面板")
                .Build();

            Assert.True(panel.Expanded);
        }

        [Fact]
        public void SetStandardIcon_ShouldSucceed()
        {
            var panel = new CollapsiblePanelBuilder()
                .Title("带图标的面板")
                .StandardIcon("icon-home", "blue")
                .Build();

            Assert.NotNull(panel.Header?.Icon);
            Assert.Equal("standard_icon", panel.Header.Icon.Tag);
            Assert.Equal("icon-home", panel.Header.Icon.Token);
            Assert.Equal("blue", panel.Header.Icon.Color);
        }

        [Fact]
        public void SetCustomIcon_ShouldSucceed()
        {
            var panel = new CollapsiblePanelBuilder()
                .Title("自定义图标")
                .CustomIcon("img_key_123")
                .Build();

            Assert.NotNull(panel.Header?.Icon);
            Assert.Equal("custom_icon", panel.Header.Icon.Tag);
            Assert.Equal("img_key_123", panel.Header.Icon.ImgKey);
        }

        [Fact]
        public void SetExpandedIcon_ShouldSucceed()
        {
            var icon = new HeaderIcon
            {
                Tag = "standard_icon",
                Token = "icon-arrow-up",
                Color = "green"
            };

            var panel = new CollapsiblePanelBuilder()
                .Title("动态图标")
                .ExpandedIcon(icon)
                .Build();

            Assert.NotNull(panel.Header?.ExpandedIcon);
            Assert.Equal("icon-arrow-up", panel.Header.ExpandedIcon.Token);
        }

        [Fact]
        public void AddElements_ShouldSucceed()
        {
            var panel = new CollapsiblePanelBuilder()
                .Title("内容面板")
                .AddElement(new PlainTextBuilder().Content("内容 1").Build())
                .AddElement(new PlainTextBuilder().Content("内容 2").Build())
                .Build();

            Assert.Equal(2, panel.Elements.Count);
        }

        [Fact]
        public void SetMargin_ShouldSucceed()
        {
            var panel = new CollapsiblePanelBuilder()
                .Title("带边距的面板")
                .Margin("16px")
                .Build();

            Assert.Equal("16px", panel.Margin);
        }
    }

    public class ChartTests
    {
        [Fact]
        public void CreateBasicChart_ShouldSucceed()
        {
            var chartData = new
            {
                labels = new[] { "一月", "二月", "三月" },
                datasets = new[]
                {
                    new { label = "销售额", data = new[] { 100, 200, 150 } }
                }
            };

            var chart = new ChartBuilder()
                .ChartType("line")
                .Data(chartData)
                .Build();

            Assert.Equal("chart", chart.Tag);
            Assert.Equal("line", chart.ChartType);
            Assert.NotNull(chart.Data);
        }

        [Fact]
        public void SetChartType_ShouldSucceed()
        {
            var chart = new ChartBuilder()
                .ChartType("bar")
                .Data(new { })
                .Build();

            Assert.Equal("bar", chart.ChartType);
        }

        [Fact]
        public void SupportMultipleChartTypes_ShouldSucceed()
        {
            var types = new[] { "line", "bar", "pie", "area", "wordcloud" };

            foreach (var type in types)
            {
                var chart = new ChartBuilder()
                    .ChartType(type)
                    .Data(new { })
                    .Build();

                Assert.Equal(type, chart.ChartType);
            }
        }

        [Fact]
        public void SetChartData_ShouldSucceed()
        {
            var data = new
            {
                categories = new[] { "A", "B", "C" },
                series = new[] { 10, 20, 30 }
            };

            var chart = new ChartBuilder()
                .ChartType("column")
                .Data(data)
                .Build();

            Assert.Equal(data, chart.Data);
        }

        [Fact]
        public void SetChartWidth_ShouldSucceed()
        {
            var chart = new ChartBuilder()
                .ChartType("pie")
                .Data(new { })
                .Width("400px")
                .Build();

            Assert.Equal("400px", chart.Width);
        }

        [Fact]
        public void SetElementId_ShouldSucceed()
        {
            var chart = new ChartBuilder()
                .ChartType("line")
                .Data(new { })
                .ElementId("sales-chart")
                .Build();

            Assert.Equal("sales-chart", chart.ElementId);
        }
    }

    public class SelectImgTests
    {
        [Fact]
        public void CreateBasicSelectImg_ShouldSucceed()
        {
            var selectImg = new SelectImgBuilder()
                .Name("imageSelector")
                .Placeholder("请选择图片")
                .Build();

            Assert.Equal("select_img", selectImg.Tag);
            Assert.Equal("imageSelector", selectImg.Name);
        }

        [Fact]
        public void SetSingleMode_ShouldSucceed()
        {
            var selectImg = new SelectImgBuilder()
                .Name("singleSelect")
                .Single()
                .Build();

            Assert.Equal("single", selectImg.SelectMode);
        }

        [Fact]
        public void SetMultiMode_ShouldSucceed()
        {
            var selectImg = new SelectImgBuilder()
                .Name("multiSelect")
                .Multi()
                .Build();

            Assert.Equal("multi", selectImg.SelectMode);
        }

        [Fact]
        public void AddOptions_ShouldSucceed()
        {
            var selectImg = new SelectImgBuilder()
                .Name("imagePicker")
                .Option("img_key_1", "图片 1")
                .Option("img_key_2", "图片 2")
                .Option("img_key_3")
                .Build();

            Assert.NotNull(selectImg.Options);
            Assert.Equal(3, selectImg.Options.Count);
            Assert.Equal("img_key_1", selectImg.Options[0].Value);
            Assert.Equal("图片 1", selectImg.Options[0].Text?.Content);
        }

        [Fact]
        public void SetInitialOptions_ShouldSucceed()
        {
            var selectImg = new SelectImgBuilder()
                .Name("preselected")
                .Option("img_1")
                .Option("img_2")
                .InitialOptions(new List<string> { "img_1" })
                .Build();

            Assert.NotNull(selectImg.InitialOptions);
            Assert.Single(selectImg.InitialOptions);
            Assert.Equal("img_1", selectImg.InitialOptions[0]);
        }

        [Fact]
        public void SetLabel_ShouldSucceed()
        {
            var selectImg = new SelectImgBuilder()
                .Name("labeled")
                .Label("选择封面图片")
                .Build();

            Assert.NotNull(selectImg.Label);
            Assert.Equal("选择封面图片", selectImg.Label.Content);
        }

        [Fact]
        public void SetPlaceholder_ShouldSucceed()
        {
            var selectImg = new SelectImgBuilder()
                .Placeholder("点击选择图片")
                .Build();

            Assert.NotNull(selectImg.Placeholder);
            Assert.Equal("点击选择图片", selectImg.Placeholder.Content);
        }

        [Fact]
        public void SetWidth_ShouldSucceed()
        {
            var selectImg = new SelectImgBuilder()
                .Name("wide")
                .Width("400px")
                .Build();

            Assert.Equal("400px", selectImg.Width);
        }

        [Fact]
        public void SetDisabled_ShouldSucceed()
        {
            var selectImg = new SelectImgBuilder()
                .Name("disabled")
                .Disabled(true)
                .Build();

            Assert.True(selectImg.Disabled);
        }

        [Fact]
        public void SetConfirm_ShouldSucceed()
        {
            var selectImg = new SelectImgBuilder()
                .Name("confirm")
                .Confirm("确认选择", "确定要使用这张图片吗？")
                .Build();

            Assert.NotNull(selectImg.Confirm);
            Assert.Equal("确认选择", selectImg.Confirm.Title.Content);
            Assert.Equal("确定要使用这张图片吗？", selectImg.Confirm.Text.Content);
        }

        [Fact]
        public void SetRequired_ShouldSucceed()
        {
            var selectImg = new SelectImgBuilder()
                .Name("required")
                .Required(true)
                .Build();

            Assert.True(selectImg.Required);
        }
    }

    public class IntegrationTests
    {
        [Fact]
        public void LoopInDiv_ShouldSucceed()
        {
            var loop = new LoopBuilder()
                .List(new List<object> { 1, 2, 3 })
                .Template(new PlainTextBuilder().Content("Item").Build())
                .Build();

            var json = JsonSerializer.Serialize(loop);
            Assert.Contains("loop", json);
        }

        [Fact]
        public void CollapsiblePanelWithMultipleElements_ShouldSucceed()
        {
            var panel = new CollapsiblePanelBuilder()
                .Title("详细信息")
                .Expanded(true)
                .AddElement(new PlainTextBuilder().Content("详情 1").Build())
                .AddElement(new PlainTextBuilder().Content("详情 2").Build())
                .AddElement(new PlainTextBuilder().Content("详情 3").Build())
                .Build();

            Assert.Equal(3, panel.Elements.Count);
        }

        [Fact]
        public void ChartWithComplexData_ShouldSucceed()
        {
            var complexData = new
            {
                title = "销售趋势",
                xAxis = new[] { "1 月", "2 月", "3 月", "4 月" },
                series = new[]
                {
                    new { name = "产品 A", data = new[] { 100, 150, 200, 180 } },
                    new { name = "产品 B", data = new[] { 80, 120, 160, 140 } }
                }
            };

            var chart = new ChartBuilder()
                .ChartType("multi_line")
                .Data(complexData)
                .Width("600px")
                .Build();

            Assert.Equal(complexData, chart.Data);
        }

        [Fact]
        public void SelectImgMultiSelect_ShouldSucceed()
        {
            var selectImg = new SelectImgBuilder()
                .Name("gallery")
                .Label("选择展示图片")
                .Multi()
                .Option("img_1", "图片 1")
                .Option("img_2", "图片 2")
                .Option("img_3", "图片 3")
                .InitialOptions(new List<string> { "img_1", "img_3" })
                .Required(true)
                .Build();

            Assert.Equal("multi", selectImg.SelectMode);
            Assert.True(selectImg.Required);
            Assert.NotNull(selectImg.InitialOptions);
            Assert.Equal(2, selectImg.InitialOptions.Count);
        }
    }
}
