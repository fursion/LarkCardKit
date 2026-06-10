using System.Text.Json;
using FluentAssertions;
using LarkCardKit.Builders;
using LarkCardKit.Models;
using LarkCardKit.Models.Elements;
using LarkCardKit.Config;
using Xunit;

namespace LarkCardKit.Tests.Builders;

/// <summary>
/// Builder 集成测试
/// </summary>
public class BuilderIntegrationTests
{
    private readonly JsonSerializerOptions _jsonOptions = JsonOptions.Default;

    #region CardBuilder Tests

    [Fact]
    public void CardBuilder_ShouldBuildCardWithHeaderAndBody()
    {
        var card = CardBuilder.Create()
            .WithTitle("Test Card")
            .WithSubtitle("Subtitle")
            .AddMarkdown("This is **bold** text")
            .Build();

        card.Should().NotBeNull();
        card.Header.Should().NotBeNull();
        card.Header!.Title.Should().NotBeNull();
        card.Body.Should().NotBeNull();
        card.Body!.Elements.Should().HaveCount(1);
    }

    [Fact]
    public void CardBuilder_ShouldBuildCardWithConfig()
    {
        var card = CardBuilder.Create()
            .WithWidthMode("fill")
            .WithEnableForward(true)
            .WithTitle("Card")
            .Build();

        card.Config.Should().NotBeNull();
        card.Config!.WidthMode.Should().Be("fill");
        card.Config.EnableForward.Should().BeTrue();
    }

    [Fact]
    public void CardBuilder_ShouldBuildCardWithCardLink()
    {
        var card = CardBuilder.Create()
            .WithTitle("Card")
            .WithCardLink("https://example.com", "https://pc.example.com")
            .Build();

        card.CardLink.Should().NotBeNull();
        card.CardLink!.Url.Should().Be("https://example.com");
        card.CardLink.PcUrl.Should().Be("https://pc.example.com");
    }

    #endregion

    #region ButtonBuilder Tests

    [Fact]
    public void ButtonBuilder_ShouldBuildButtonWithAllProperties()
    {
        var button = ButtonBuilder.Create()
            .WithText("Click Me")
            .WithType("primary")
            .WithSize("large")
            .WithWidth("fill")
            .WithElementId("btn-submit")
            .WithMargin("10px")
            .WithDisabled(true)
            .WithDisabledTips("Button is disabled")
            .Build();

        button.Should().NotBeNull();
        button.Tag.Should().Be("button");
        button.Text!.Content.Should().Be("Click Me");
        button.Type.Should().Be("primary");
        button.Size.Should().Be("large");
        button.Width.Should().Be("fill");
        button.ElementId.Should().Be("btn-submit");
        button.Margin.Should().Be("10px");
        button.Disabled.Should().BeTrue();
        button.DisabledTips!.Content.Should().Be("Button is disabled");
    }

    [Fact]
    public void ButtonBuilder_ShouldBuildButtonWithCallbackBehavior()
    {
        var button = ButtonBuilder.Create()
            .WithText("Submit")
            .WithCallback("submit_action", new { key = "value" })
            .Build();

        button.Behaviors.Should().NotBeNull();
        button.Behaviors!.Should().HaveCount(1);
    }

    #endregion

    #region InputBuilder Tests

    [Fact]
    public void InputBuilder_ShouldBuildInputWithAllProperties()
    {
        var input = InputBuilder.Create()
            .WithName("username")
            .WithPlaceholder("Enter username")
            .WithLabel("Username")
            .WithRequired(true)
            .WithMaxLength(50)
            .WithWidth("fill")
            .WithElementId("input-username")
            .WithMargin("5px")
            .Build();

        input.Should().NotBeNull();
        input.Tag.Should().Be("input");
        input.Name.Should().Be("username");
        input.Placeholder!.Content.Should().Be("Enter username");
        input.Label!.Content.Should().Be("Username");
        input.Required.Should().BeTrue();
        input.MaxLength.Should().Be(50);
        input.ElementId.Should().Be("input-username");
        input.Margin.Should().Be("5px");
    }

    [Fact]
    public void InputBuilder_ShouldBuildMultilineInput()
    {
        var input = InputBuilder.Create()
            .WithName("description")
            .WithInputType("multiline")
            .WithRows(5)
            .WithAutoResize(true)
            .WithMaxRows(10)
            .Build();

        input.InputType.Should().Be("multiline");
        input.Rows.Should().Be(5);
        input.AutoResize.Should().BeTrue();
        input.MaxRows.Should().Be(10);
    }

    #endregion

    #region SelectBuilder Tests

    [Fact]
    public void SelectBuilder_ShouldBuildSelectWithOptions()
    {
        var select = SelectBuilder.Create()
            .WithName("country")
            .WithPlaceholder("Select country")
            .WithLabel("Country")
            .AddOption("us", "United States")
            .AddOption("cn", "China")
            .AddOption("jp", "Japan")
            .WithInitialOption("cn")
            .WithElementId("select-country")
            .WithMargin("10px")
            .Build();

        select.Should().NotBeNull();
        select.Tag.Should().Be("select_static");
        select.Name.Should().Be("country");
        select.Options.Should().HaveCount(3);
        select.InitialOption.Should().Be("cn");
        select.ElementId.Should().Be("select-country");
    }

    [Fact]
    public void SelectBuilder_ShouldBuildMultiSelect()
    {
        var select = SelectBuilder.Create()
            .WithName("tags")
            .WithMultiSelect(true)
            .AddOption("tag1", "Tag 1")
            .AddOption("tag2", "Tag 2")
            .Build();

        select.Tag.Should().Be("multi_select_static");
    }

    #endregion

    #region DatePickerBuilder Tests

    [Fact]
    public void DatePickerBuilder_ShouldBuildDatePicker()
    {
        var datePicker = DatePickerBuilder.Create()
            .WithName("birthday")
            .WithPlaceholder("Select your birthday")
            .WithLabel("Birthday")
            .WithInitialDate("2024-01-01")
            .WithElementId("date-birthday")
            .WithMargin("10px")
            .Build();

        datePicker.Should().NotBeNull();
        datePicker.Tag.Should().Be("date_picker");
        datePicker.Name.Should().Be("birthday");
        datePicker.InitialDate.Should().Be("2024-01-01");
        datePicker.ElementId.Should().Be("date-birthday");
    }

    #endregion

    #region PickerTimeBuilder Tests

    [Fact]
    public void PickerTimeBuilder_ShouldBuildPickerTime()
    {
        var pickerTime = PickerTimeBuilder.Create()
            .WithName("start_time")
            .WithPlaceholder("Select start time")
            .WithInitialTime("09:00")
            .WithElementId("time-start")
            .Build();

        pickerTime.Should().NotBeNull();
        pickerTime.Tag.Should().Be("picker_time");
        pickerTime.Name.Should().Be("start_time");
        pickerTime.InitialTime.Should().Be("09:00");
    }

    #endregion

    #region PickerDatetimeBuilder Tests

    [Fact]
    public void PickerDatetimeBuilder_ShouldBuildPickerDatetime()
    {
        var pickerDatetime = PickerDatetimeBuilder.Create()
            .WithName("meeting_time")
            .WithPlaceholder("Select meeting time")
            .WithInitialDatetime("2024-01-01 09:00")
            .WithElementId("datetime-meeting")
            .Build();

        pickerDatetime.Should().NotBeNull();
        pickerDatetime.Tag.Should().Be("picker_datetime");
        pickerDatetime.Name.Should().Be("meeting_time");
        pickerDatetime.InitialDatetime.Should().Be("2024-01-01 09:00");
    }

    #endregion

    #region ImageBuilder Tests

    [Fact]
    public void ImageBuilder_ShouldBuildImage()
    {
        var image = ImageBuilder.Create()
            .WithImgKey("img_v2_xxxx")
            .WithAlt("Image description")
            .WithTitle("Image title")
            .WithSize("medium")
            .WithElementId("img-1")
            .WithMargin("10px")
            .Build();

        image.Should().NotBeNull();
        image.Tag.Should().Be("img");
        image.ImgKey.Should().Be("img_v2_xxxx");
        image.Alt!.Content.Should().Be("Image description");
        image.Size.Should().Be("medium");
        image.ElementId.Should().Be("img-1");
    }

    #endregion

    #region MarkdownBuilder Tests

    [Fact]
    public void MarkdownBuilder_ShouldBuildMarkdown()
    {
        var markdown = MarkdownBuilder.Create()
            .WithContent("**Bold** and *italic*")
            .WithTextSize("notation")
            .WithTextColor("red")
            .WithTextAlign("center")
            .WithElementId("md-1")
            .WithMargin("5px")
            .Build();

        markdown.Should().NotBeNull();
        markdown.Tag.Should().Be("markdown");
        markdown.Content.Should().Be("**Bold** and *italic*");
        markdown.TextSize.Should().Be("notation");
        markdown.TextColor.Should().Be("red");
        markdown.TextAlign.Should().Be("center");
        markdown.ElementId.Should().Be("md-1");
    }

    #endregion

    #region DivBuilder Tests

    [Fact]
    public void DivBuilder_ShouldBuildDiv()
    {
        var div = DivBuilder.Create()
            .WithText("Plain text content")
            .WithElementId("div-1")
            .WithMargin("10px")
            .Build();

        div.Should().NotBeNull();
        div.Tag.Should().Be("div");
        div.Text!.Content.Should().Be("Plain text content");
        div.ElementId.Should().Be("div-1");
    }

    #endregion

    #region HrBuilder Tests

    [Fact]
    public void HrBuilder_ShouldBuildHr()
    {
        var hr = HrBuilder.Create()
            .WithElementId("hr-1")
            .WithMargin("10px")
            .Build();

        hr.Should().NotBeNull();
        hr.Tag.Should().Be("hr");
        hr.ElementId.Should().Be("hr-1");
        hr.Margin.Should().Be("10px");
    }

    #endregion

    #region ChartBuilder Tests

    [Fact]
    public void ChartBuilder_ShouldBuildChart()
    {
        var chart = ChartBuilder.Create()
            .WithChartType("line")
            .WithData(new { values = new[] { 1, 2, 3 } })
            .WithWidth("fill")
            .WithElementId("chart-1")
            .Build();

        chart.Should().NotBeNull();
        chart.Tag.Should().Be("chart");
        chart.ChartType.Should().Be("line");
        chart.Width.Should().Be("fill");
        chart.ElementId.Should().Be("chart-1");
    }

    #endregion

    #region TableBuilder Tests

    [Fact]
    public void TableBuilder_ShouldBuildTable()
    {
        var table = TableBuilder.Create()
            .AddColumn("name", "Name", "100px")
            .AddColumn("age", "Age", "50px")
            .AddRow(new Dictionary<string, object?> { ["name"] = "Alice", ["age"] = 25 })
            .AddRow(new Dictionary<string, object?> { ["name"] = "Bob", ["age"] = 30 })
            .WithElementId("table-1")
            .Build();

        table.Should().NotBeNull();
        table.Tag.Should().Be("table");
        table.Columns.Should().HaveCount(2);
        table.Rows.Should().HaveCount(2);
    }

    #endregion

    #region PersonBuilder Tests

    [Fact]
    public void PersonBuilder_ShouldBuildPerson()
    {
        var person = PersonBuilder.Create()
            .WithOpenId("ou_xxxx")
            .WithShowName(true)
            .WithShowAvatar(true)
            .WithElementId("person-1")
            .Build();

        person.Should().NotBeNull();
        person.Tag.Should().Be("person");
        person.OpenId.Should().Be("ou_xxxx");
        person.ShowName.Should().BeTrue();
    }

    #endregion

    #region PersonListBuilder Tests

    [Fact]
    public void PersonListBuilder_ShouldBuildPersonList()
    {
        var personList = PersonListBuilder.Create()
            .AddOpenId("ou_xxxx")
            .AddOpenId("ou_yyyy")
            .WithElementId("person-list-1")
            .Build();

        personList.Should().NotBeNull();
        personList.Tag.Should().Be("person_list");
        personList.OpenIds.Should().HaveCount(2);
    }

    #endregion

    #region OverflowBuilder Tests

    [Fact]
    public void OverflowBuilder_ShouldBuildOverflow()
    {
        var overflow = OverflowBuilder.Create()
            .AddOption("edit", "Edit")
            .AddOption("delete", "Delete")
            .WithElementId("overflow-1")
            .WithMargin("5px")
            .Build();

        overflow.Should().NotBeNull();
        overflow.Tag.Should().Be("overflow");
        overflow.Options.Should().HaveCount(2);
        overflow.ElementId.Should().Be("overflow-1");
    }

    #endregion

    #region SelectPersonBuilder Tests

    [Fact]
    public void SelectPersonBuilder_ShouldBuildSelectPerson()
    {
        var selectPerson = SelectPersonBuilder.Create()
            .WithName("approver")
            .WithPlaceholder("Select approver")
            .WithLabel("Approver")
            .WithElementId("select-person-1")
            .Build();

        selectPerson.Should().NotBeNull();
        selectPerson.Tag.Should().Be("select_person");
        selectPerson.Name.Should().Be("approver");
    }

    #endregion

    #region MultiSelectPersonBuilder Tests

    [Fact]
    public void MultiSelectPersonBuilder_ShouldBuildMultiSelectPerson()
    {
        var multiSelectPerson = MultiSelectPersonBuilder.Create()
            .WithName("reviewers")
            .WithPlaceholder("Select reviewers")
            .WithElementId("multi-select-person-1")
            .Build();

        multiSelectPerson.Should().NotBeNull();
        multiSelectPerson.Tag.Should().Be("multi_select_person");
        multiSelectPerson.Name.Should().Be("reviewers");
    }

    #endregion

    #region SelectImgBuilder Tests

    [Fact]
    public void SelectImgBuilder_ShouldBuildSelectImg()
    {
        var selectImg = SelectImgBuilder.Create()
            .WithName("avatar")
            .WithPlaceholder("Select avatar")
            .WithSelectMode("single")
            .AddOption("img_v2_xxxx", "Image 1")
            .WithElementId("select-img-1")
            .Build();

        selectImg.Should().NotBeNull();
        selectImg.Tag.Should().Be("select_img");
        selectImg.Name.Should().Be("avatar");
        selectImg.SelectMode.Should().Be("single");
        selectImg.Options.Should().HaveCount(1);
    }

    #endregion

    #region CheckerBuilder Tests

    [Fact]
    public void CheckerBuilder_ShouldBuildChecker()
    {
        var checker = CheckerBuilder.Create()
            .WithName("todo-item")
            .WithChecked(true)
            .WithText("Task completed")
            .WithElementId("checker-1")
            .WithMargin("5px")
            .Build();

        checker.Should().NotBeNull();
        checker.Tag.Should().Be("checker");
        checker.Name.Should().Be("todo-item");
        checker.Checked.Should().BeTrue();
    }

    #endregion

    #region CollapsiblePanelBuilder Tests

    [Fact]
    public void CollapsiblePanelBuilder_ShouldBuildCollapsiblePanel()
    {
        var panel = CollapsiblePanelBuilder.Create()
            .WithTitle("Panel Title")
            .WithExpanded(false)
            .WithElementId("panel-1")
            .WithMargin("10px")
            .WithPadding("5px")
            .AddMarkdown("Panel content")
            .Build();

        panel.Should().NotBeNull();
        panel.Tag.Should().Be("collapsible_panel");
        panel.ElementId.Should().Be("panel-1");
        panel.Margin.Should().Be("10px");
        panel.Padding.Should().Be("5px");
        panel.Elements.Should().HaveCount(1);
    }

    #endregion

    #region InteractiveContainerBuilder Tests

    [Fact]
    public void InteractiveContainerBuilder_ShouldBuildInteractiveContainer()
    {
        var container = InteractiveContainerBuilder.Create()
            .WithWidth("fill")
            .WithElementId("container-1")
            .WithMargin("10px")
            .AddMarkdown("Container content")
            .Build();

        container.Should().NotBeNull();
        container.Tag.Should().Be("interactive_container");
        container.ElementId.Should().Be("container-1");
        container.Margin.Should().Be("10px");
        container.Elements.Should().HaveCount(1);
    }

    #endregion

    #region FormBuilder Tests

    [Fact]
    public void FormBuilder_ShouldBuildForm()
    {
        var form = FormBuilder.Create()
            .WithName("user-form")
            .AddInput(InputBuilder.Create().WithName("name").WithLabel("Name").Build())
            .AddInput(InputBuilder.Create().WithName("email").WithLabel("Email").Build())
            .AddButton(ButtonBuilder.Create().WithText("Submit").Build())
            .Build();

        form.Should().NotBeNull();
        form.Tag.Should().Be("form");
        form.Name.Should().Be("user-form");
        form.Elements.Should().HaveCount(3);
    }

    #endregion

    #region Serialization Tests

    [Fact]
    public void BuiltCard_ShouldSerializeToJson()
    {
        var card = CardBuilder.Create()
            .WithTitle("Test Card")
            .AddMarkdown("Content")
            .AddButton(ButtonBuilder.Create().WithText("Click").Build())
            .Build();

        var json = JsonSerializer.Serialize(card, _jsonOptions);

        json.Should().NotBeNullOrEmpty();
        json.Should().Contain("\"schema\":\"2.0\"");
        json.Should().Contain("\"tag\":\"markdown\"");
        json.Should().Contain("\"tag\":\"button\"");
    }

    #endregion
}
