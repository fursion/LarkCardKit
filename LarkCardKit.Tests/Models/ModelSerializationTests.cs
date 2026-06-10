using System.Text.Json;
using FluentAssertions;
using LarkCardKit.Models;
using LarkCardKit.Models.Elements;
using LarkCardKit.Config;
using Xunit;

namespace LarkCardKit.Tests.Models;

/// <summary>
/// 模型序列化单元测试
/// </summary>
public class ModelSerializationTests
{
    private readonly JsonSerializerOptions _jsonOptions = JsonOptions.Default;

    #region Card Tests

    [Fact]
    public void Card_ShouldSerializeWithSchema2_0()
    {
        var card = new Card
        {
            Header = new CardHeader { Title = new PlainText("Test") },
            Body = new CardBody { Elements = new List<Element>() }
        };

        var json = JsonSerializer.Serialize(card, _jsonOptions);
        var doc = JsonDocument.Parse(json);

        doc.RootElement.GetProperty("schema").GetString().Should().Be("2.0");
    }

    [Fact]
    public void Card_WithAllProperties_ShouldSerializeCorrectly()
    {
        var card = new Card
        {
            Config = new CardConfig { WidthMode = "fill" },
            CardLink = new CardLink { Url = "https://example.com" },
            Header = new CardHeader { Title = new PlainText("Title") },
            Body = new CardBody { Elements = new List<Element>() },
            Fallback = new Fallback
            {
                Title = new PlainText("Fallback Title"),
                Content = new PlainText("Fallback Content"),
                Config = new CardConfig { WidthMode = "fill" },
                Elements = new List<Element>()
            }
        };

        var json = JsonSerializer.Serialize(card, _jsonOptions);
        var doc = JsonDocument.Parse(json);

        doc.RootElement.TryGetProperty("config", out _).Should().BeTrue();
        doc.RootElement.TryGetProperty("card_link", out _).Should().BeTrue();
        doc.RootElement.TryGetProperty("header", out _).Should().BeTrue();
        doc.RootElement.TryGetProperty("body", out _).Should().BeTrue();
        doc.RootElement.TryGetProperty("fallback", out _).Should().BeTrue();
    }

    #endregion

    #region Fallback Tests

    [Fact]
    public void Fallback_WithConfigAndElements_ShouldSerializeCorrectly()
    {
        var fallback = new Fallback
        {
            Title = new PlainText("Title"),
            Content = new PlainText("Content"),
            Config = new CardConfig { WidthMode = "fill" },
            Elements = new List<Element>
            {
                new Markdown { Content = "Fallback markdown" }
            }
        };

        var json = JsonSerializer.Serialize(fallback, _jsonOptions);
        var doc = JsonDocument.Parse(json);

        doc.RootElement.TryGetProperty("config", out _).Should().BeTrue();
        doc.RootElement.TryGetProperty("elements", out _).Should().BeTrue();
    }

    #endregion

    #region Button Tests

    [Fact]
    public void Button_ShouldSerializeElementId()
    {
        var button = new Button
        {
            ElementId = "btn-1",
            Text = new PlainText("Click Me"),
            Type = "primary",
            Size = "medium"
        };

        var json = JsonSerializer.Serialize(button, _jsonOptions);
        var doc = JsonDocument.Parse(json);

        doc.RootElement.GetProperty("tag").GetString().Should().Be("button");
        doc.RootElement.GetProperty("element_id").GetString().Should().Be("btn-1");
        doc.RootElement.GetProperty("type").GetString().Should().Be("primary");
    }

    [Fact]
    public void Button_WithDisabledTips_ShouldSerializeCorrectly()
    {
        var button = new Button
        {
            Text = new PlainText("Click"),
            Disabled = true,
            DisabledTips = new PlainText("Button is disabled")
        };

        var json = JsonSerializer.Serialize(button, _jsonOptions);
        var doc = JsonDocument.Parse(json);

        doc.RootElement.GetProperty("disabled").GetBoolean().Should().BeTrue();
        doc.RootElement.TryGetProperty("disabled_tips", out _).Should().BeTrue();
    }

    [Fact]
    public void Button_WithMargin_ShouldSerializeCorrectly()
    {
        var button = new Button
        {
            Text = new PlainText("Click"),
            Margin = "10px"
        };

        var json = JsonSerializer.Serialize(button, _jsonOptions);
        var doc = JsonDocument.Parse(json);

        doc.RootElement.GetProperty("margin").GetString().Should().Be("10px");
    }

    #endregion

    #region Input Tests

    [Fact]
    public void Input_Disabled_ShouldBeBoolean()
    {
        var input = new Input
        {
            Name = "input-1",
            Disabled = true,
            DisabledTips = new PlainText("Input disabled")
        };

        var json = JsonSerializer.Serialize(input, _jsonOptions);
        var doc = JsonDocument.Parse(json);

        doc.RootElement.GetProperty("disabled").ValueKind.Should().Be(JsonValueKind.True);
    }

    [Fact]
    public void Input_WithAllProperties_ShouldSerializeCorrectly()
    {
        var input = new Input
        {
            ElementId = "input-1",
            Name = "username",
            Required = true,
            Placeholder = new PlainText("Enter username"),
            Label = new PlainText("Username"),
            InputType = "text",
            MaxLength = 100,
            Width = "fill",
            Margin = "5px"
        };

        var json = JsonSerializer.Serialize(input, _jsonOptions);
        var doc = JsonDocument.Parse(json);

        doc.RootElement.GetProperty("tag").GetString().Should().Be("input");
        doc.RootElement.GetProperty("element_id").GetString().Should().Be("input-1");
        doc.RootElement.GetProperty("name").GetString().Should().Be("username");
    }

    #endregion

    #region Select Tests

    [Fact]
    public void Select_WithDisabledTips_ShouldSerializeCorrectly()
    {
        var select = new Select
        {
            Name = "select-1",
            Placeholder = new PlainText("Select an option"),
            Options = new List<SelectOption>
            {
                new() { Value = "opt1", Text = new PlainText("Option 1") }
            },
            Disabled = true,
            DisabledTips = new PlainText("Select disabled")
        };

        var json = JsonSerializer.Serialize(select, _jsonOptions);
        var doc = JsonDocument.Parse(json);

        doc.RootElement.TryGetProperty("disabled_tips", out _).Should().BeTrue();
    }

    [Fact]
    public void Select_MultiSelect_ShouldHaveCorrectTag()
    {
        var select = new Select
        {
            Name = "multi-select",
            MultiSelect = true,
            Options = new List<SelectOption>()
        };

        var json = JsonSerializer.Serialize(select, _jsonOptions);
        var doc = JsonDocument.Parse(json);

        doc.RootElement.GetProperty("tag").GetString().Should().Be("multi_select_static");
    }

    #endregion

    #region DatePicker Tests

    [Fact]
    public void DatePicker_WithAllProperties_ShouldSerializeCorrectly()
    {
        var datePicker = new DatePicker
        {
            ElementId = "date-1",
            Name = "birthday",
            InitialDate = "2024-01-01",
            Placeholder = new PlainText("Select date"),
            Disabled = false,
            DisabledTips = new PlainText("Date picker disabled"),
            Margin = "10px"
        };

        var json = JsonSerializer.Serialize(datePicker, _jsonOptions);
        var doc = JsonDocument.Parse(json);

        doc.RootElement.GetProperty("tag").GetString().Should().Be("date_picker");
        doc.RootElement.GetProperty("element_id").GetString().Should().Be("date-1");
        doc.RootElement.GetProperty("initial_date").GetString().Should().Be("2024-01-01");
    }

    #endregion

    #region PickerTime Tests

    [Fact]
    public void PickerTime_WithAllProperties_ShouldSerializeCorrectly()
    {
        var pickerTime = new PickerTime
        {
            ElementId = "time-1",
            Name = "start-time",
            InitialTime = "09:00",
            Placeholder = new PlainText("Select time"),
            Margin = "5px"
        };

        var json = JsonSerializer.Serialize(pickerTime, _jsonOptions);
        var doc = JsonDocument.Parse(json);

        doc.RootElement.GetProperty("tag").GetString().Should().Be("picker_time");
        doc.RootElement.GetProperty("initial_time").GetString().Should().Be("09:00");
    }

    #endregion

    #region PickerDatetime Tests

    [Fact]
    public void PickerDatetime_WithAllProperties_ShouldSerializeCorrectly()
    {
        var pickerDatetime = new PickerDatetime
        {
            ElementId = "datetime-1",
            Name = "meeting-time",
            InitialDatetime = "2024-01-01 09:00",
            Placeholder = new PlainText("Select datetime"),
            Margin = "10px"
        };

        var json = JsonSerializer.Serialize(pickerDatetime, _jsonOptions);
        var doc = JsonDocument.Parse(json);

        doc.RootElement.GetProperty("tag").GetString().Should().Be("picker_datetime");
        doc.RootElement.GetProperty("element_id").GetString().Should().Be("datetime-1");
    }

    #endregion

    #region Checkbox Tests

    [Fact]
    public void Checkbox_WithDisabledTips_ShouldSerializeCorrectly()
    {
        var checkbox = new Checkbox
        {
            Name = "checkbox-1",
            Options = new List<CheckboxOption>
            {
                new() { Value = "opt1", Text = new PlainText("Option 1") }
            },
            Disabled = true,
            DisabledTips = new PlainText("Checkbox disabled"),
            Margin = "5px"
        };

        var json = JsonSerializer.Serialize(checkbox, _jsonOptions);
        var doc = JsonDocument.Parse(json);

        doc.RootElement.GetProperty("tag").GetString().Should().Be("multi_select_static");
        doc.RootElement.TryGetProperty("disabled_tips", out _).Should().BeTrue();
    }

    #endregion

    #region Overflow Tests

    [Fact]
    public void Overflow_WithAllProperties_ShouldSerializeCorrectly()
    {
        var overflow = new Overflow
        {
            ElementId = "overflow-1",
            Options = new List<OverflowOption>
            {
                new() { Value = "edit", Text = new PlainText("Edit") },
                new() { Value = "delete", Text = new PlainText("Delete") }
            },
            Disabled = true,
            DisabledTips = new PlainText("Overflow disabled"),
            Margin = "5px"
        };

        var json = JsonSerializer.Serialize(overflow, _jsonOptions);
        var doc = JsonDocument.Parse(json);

        doc.RootElement.GetProperty("tag").GetString().Should().Be("overflow");
        doc.RootElement.GetProperty("element_id").GetString().Should().Be("overflow-1");
    }

    #endregion

    #region Image Tests

    [Fact]
    public void Image_WithAllProperties_ShouldSerializeCorrectly()
    {
        var image = new Image
        {
            ElementId = "img-1",
            ImgKey = "img_v2_xxxx",
            Alt = new PlainText("Image description"),
            Size = "medium",
            Margin = "10px"
        };

        var json = JsonSerializer.Serialize(image, _jsonOptions);
        var doc = JsonDocument.Parse(json);

        doc.RootElement.GetProperty("tag").GetString().Should().Be("img");
        doc.RootElement.GetProperty("element_id").GetString().Should().Be("img-1");
        doc.RootElement.GetProperty("img_key").GetString().Should().Be("img_v2_xxxx");
    }

    #endregion

    #region Markdown Tests

    [Fact]
    public void Markdown_WithAllProperties_ShouldSerializeCorrectly()
    {
        var markdown = new Markdown
        {
            ElementId = "md-1",
            Content = "**Bold** and *italic*",
            TextSize = "notation",
            TextColor = "red",
            Margin = "5px"
        };

        var json = JsonSerializer.Serialize(markdown, _jsonOptions);
        var doc = JsonDocument.Parse(json);

        doc.RootElement.GetProperty("tag").GetString().Should().Be("markdown");
        doc.RootElement.GetProperty("element_id").GetString().Should().Be("md-1");
        doc.RootElement.GetProperty("content").GetString().Should().Be("**Bold** and *italic*");
    }

    #endregion

    #region Hr Tests

    [Fact]
    public void Hr_WithAllProperties_ShouldSerializeCorrectly()
    {
        var hr = new Hr
        {
            ElementId = "hr-1",
            Margin = "10px"
        };

        var json = JsonSerializer.Serialize(hr, _jsonOptions);
        var doc = JsonDocument.Parse(json);

        doc.RootElement.GetProperty("tag").GetString().Should().Be("hr");
        doc.RootElement.GetProperty("element_id").GetString().Should().Be("hr-1");
    }

    #endregion

    #region Chart Tests

    [Fact]
    public void Chart_WithAllProperties_ShouldSerializeCorrectly()
    {
        var chart = new Chart
        {
            ElementId = "chart-1",
            ChartType = "line",
            Data = new { values = new[] { 1, 2, 3 } },
            Width = "fill",
            Margin = "10px"
        };

        var json = JsonSerializer.Serialize(chart, _jsonOptions);
        var doc = JsonDocument.Parse(json);

        doc.RootElement.GetProperty("tag").GetString().Should().Be("chart");
        doc.RootElement.GetProperty("element_id").GetString().Should().Be("chart-1");
    }

    #endregion

    #region Table Tests

    [Fact]
    public void Table_WithAllProperties_ShouldSerializeCorrectly()
    {
        var table = new Table
        {
            Columns = new List<TableColumn>
            {
                new() { Name = "col1", Width = "100px" }
            },
            Rows = new List<Dictionary<string, object?>>
            {
                new() { ["col1"] = "Value 1" }
            },
            Margin = "10px"
        };

        var json = JsonSerializer.Serialize(table, _jsonOptions);
        var doc = JsonDocument.Parse(json);

        doc.RootElement.GetProperty("tag").GetString().Should().Be("table");
        doc.RootElement.TryGetProperty("columns", out _).Should().BeTrue();
        doc.RootElement.TryGetProperty("rows", out _).Should().BeTrue();
    }

    #endregion

    #region Person Tests

    [Fact]
    public void Person_WithAllProperties_ShouldSerializeCorrectly()
    {
        var person = new Person
        {
            ElementId = "person-1",
            OpenId = "ou_xxxx",
            ShowName = true,
            Margin = "5px"
        };

        var json = JsonSerializer.Serialize(person, _jsonOptions);
        var doc = JsonDocument.Parse(json);

        doc.RootElement.GetProperty("tag").GetString().Should().Be("person");
        doc.RootElement.GetProperty("element_id").GetString().Should().Be("person-1");
    }

    #endregion

    #region PersonList Tests

    [Fact]
    public void PersonList_WithAllProperties_ShouldSerializeCorrectly()
    {
        var personList = new PersonList
        {
            ElementId = "person-list-1",
            OpenIds = new List<string> { "ou_xxxx", "ou_yyyy" },
            Margin = "5px"
        };

        var json = JsonSerializer.Serialize(personList, _jsonOptions);
        var doc = JsonDocument.Parse(json);

        doc.RootElement.GetProperty("tag").GetString().Should().Be("person_list");
        doc.RootElement.GetProperty("element_id").GetString().Should().Be("person-list-1");
    }

    #endregion

    #region ColumnSet Tests

    [Fact]
    public void ColumnSet_WithFlexMode_ShouldSerializeCorrectly()
    {
        var columnSet = new ColumnSet
        {
            Columns = new List<Column>
            {
                new() { Elements = new List<Element>() }
            },
            FlexMode = "flow",
            HorizontalSpacing = "small"
        };

        var json = JsonSerializer.Serialize(columnSet, _jsonOptions);
        var doc = JsonDocument.Parse(json);

        doc.RootElement.GetProperty("tag").GetString().Should().Be("column_set");
        doc.RootElement.GetProperty("flex_mode").GetString().Should().Be("flow");
    }

    #endregion

    #region CollapsiblePanel Tests

    [Fact]
    public void CollapsiblePanel_WithAllProperties_ShouldSerializeCorrectly()
    {
        var panel = new CollapsiblePanel
        {
            ElementId = "panel-1",
            Expanded = false,
            Margin = "10px",
            Padding = "5px",
            Header = new CollapsiblePanelHeader
            {
                Title = new PlainText("Panel Title")
            },
            Elements = new List<Element>
            {
                new Markdown { Content = "Panel content" }
            }
        };

        var json = JsonSerializer.Serialize(panel, _jsonOptions);
        var doc = JsonDocument.Parse(json);

        doc.RootElement.GetProperty("tag").GetString().Should().Be("collapsible_panel");
        doc.RootElement.GetProperty("element_id").GetString().Should().Be("panel-1");
        doc.RootElement.GetProperty("margin").GetString().Should().Be("10px");
        doc.RootElement.GetProperty("padding").GetString().Should().Be("5px");
    }

    #endregion

    #region InteractiveContainer Tests

    [Fact]
    public void InteractiveContainer_WithAllProperties_ShouldSerializeCorrectly()
    {
        var container = new InteractiveContainer
        {
            ElementId = "container-1",
            Width = "fill",
            Margin = "10px",
            Elements = new List<Element>
            {
                new Markdown { Content = "Content" }
            }
        };

        var json = JsonSerializer.Serialize(container, _jsonOptions);
        var doc = JsonDocument.Parse(json);

        doc.RootElement.GetProperty("tag").GetString().Should().Be("interactive_container");
        doc.RootElement.GetProperty("element_id").GetString().Should().Be("container-1");
    }

    #endregion

    #region SelectPerson Tests

    [Fact]
    public void SelectPerson_WithAllProperties_ShouldSerializeCorrectly()
    {
        var selectPerson = new SelectPerson
        {
            ElementId = "select-person-1",
            Name = "approver",
            Placeholder = new PlainText("Select person"),
            Disabled = true,
            DisabledTips = new PlainText("Select disabled"),
            Margin = "5px"
        };

        var json = JsonSerializer.Serialize(selectPerson, _jsonOptions);
        var doc = JsonDocument.Parse(json);

        doc.RootElement.GetProperty("tag").GetString().Should().Be("select_person");
        doc.RootElement.GetProperty("element_id").GetString().Should().Be("select-person-1");
    }

    #endregion

    #region MultiSelectPerson Tests

    [Fact]
    public void MultiSelectPerson_WithAllProperties_ShouldSerializeCorrectly()
    {
        var multiSelectPerson = new MultiSelectPerson
        {
            ElementId = "multi-select-person-1",
            Name = "reviewers",
            Placeholder = new PlainText("Select reviewers"),
            Disabled = true,
            DisabledTips = new PlainText("Select disabled"),
            Margin = "5px"
        };

        var json = JsonSerializer.Serialize(multiSelectPerson, _jsonOptions);
        var doc = JsonDocument.Parse(json);

        doc.RootElement.GetProperty("tag").GetString().Should().Be("multi_select_person");
        doc.RootElement.GetProperty("element_id").GetString().Should().Be("multi-select-person-1");
    }

    #endregion

    #region SelectImg Tests

    [Fact]
    public void SelectImg_WithAllProperties_ShouldSerializeCorrectly()
    {
        var selectImg = new SelectImg
        {
            ElementId = "select-img-1",
            Name = "avatar",
            Placeholder = new PlainText("Select image"),
            SelectMode = "single",
            Disabled = true,
            DisabledTips = new PlainText("Select disabled"),
            Margin = "5px"
        };

        var json = JsonSerializer.Serialize(selectImg, _jsonOptions);
        var doc = JsonDocument.Parse(json);

        doc.RootElement.GetProperty("tag").GetString().Should().Be("select_img");
        doc.RootElement.GetProperty("element_id").GetString().Should().Be("select-img-1");
    }

    #endregion

    #region Checker Tests

    [Fact]
    public void Checker_WithAllProperties_ShouldSerializeCorrectly()
    {
        var checker = new Checker
        {
            ElementId = "checker-1",
            Name = "todo-item",
            Checked = true,
            Text = new PlainText("Task completed"),
            Margin = "5px",
            Disabled = true,
            DisabledTips = new PlainText("Checker disabled")
        };

        var json = JsonSerializer.Serialize(checker, _jsonOptions);
        var doc = JsonDocument.Parse(json);

        doc.RootElement.GetProperty("tag").GetString().Should().Be("checker");
        doc.RootElement.GetProperty("element_id").GetString().Should().Be("checker-1");
    }

    #endregion

    #region PlainText Tests

    [Fact]
    public void PlainText_ShouldSerializeWithTag()
    {
        var plainText = new PlainText("Hello World");

        var json = JsonSerializer.Serialize(plainText, _jsonOptions);
        var doc = JsonDocument.Parse(json);

        doc.RootElement.GetProperty("tag").GetString().Should().Be("plain_text");
        doc.RootElement.GetProperty("content").GetString().Should().Be("Hello World");
    }

    #endregion

    #region MarkdownText Tests

    [Fact]
    public void MarkdownText_ShouldSerializeWithTag()
    {
        var markdownText = new MarkdownText("**Bold** text");

        var json = JsonSerializer.Serialize(markdownText, _jsonOptions);
        var doc = JsonDocument.Parse(json);

        doc.RootElement.GetProperty("tag").GetString().Should().Be("lark_md");
        doc.RootElement.GetProperty("content").GetString().Should().Be("**Bold** text");
    }

    #endregion
}
