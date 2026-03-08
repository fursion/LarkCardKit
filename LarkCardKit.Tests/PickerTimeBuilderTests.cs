using LarkCardKit.Builders;
using Xunit;

namespace LarkCardKit.Tests;

public class PickerTimeBuilderTests
{
    [Fact]
    public void Build_BasicPickerTime_ShouldHaveCorrectTag()
    {
        var pickerTime = new PickerTimeBuilder().Build();
        
        Assert.Equal("picker_time", pickerTime.Tag);
    }
    
    [Fact]
    public void Build_WithName_ShouldSetName()
    {
        var pickerTime = new PickerTimeBuilder()
            .Name("meetingTime")
            .Build();
        
        Assert.Equal("meetingTime", pickerTime.Name);
    }
    
    [Fact]
    public void Build_WithPlaceholder_ShouldSetPlaceholder()
    {
        var pickerTime = new PickerTimeBuilder()
            .Placeholder("请选择时间")
            .Build();
        
        Assert.NotNull(pickerTime.Placeholder);
        Assert.Equal("请选择时间", pickerTime.Placeholder.Content);
    }
    
    [Fact]
    public void Build_WithInitialTime_ShouldSetInitialTime()
    {
        var pickerTime = new PickerTimeBuilder()
            .InitialTime("09:30")
            .Build();
        
        Assert.Equal("09:30", pickerTime.InitialTime);
    }
    
    [Fact]
    public void Build_WithRequired_ShouldSetRequired()
    {
        var pickerTime = new PickerTimeBuilder()
            .Required()
            .Build();
        
        Assert.True(pickerTime.Required);
    }
    
    [Fact]
    public void Build_WithDisabled_ShouldSetDisabled()
    {
        var pickerTime = new PickerTimeBuilder()
            .Disabled()
            .Build();
        
        Assert.True(pickerTime.Disabled);
    }
    
    [Fact]
    public void Build_WithWidth_ShouldSetWidth()
    {
        var pickerTime = new PickerTimeBuilder()
            .Width("fill")
            .Build();
        
        Assert.Equal("fill", pickerTime.Width);
    }
    
    [Fact]
    public void Build_WithConfirm_ShouldSetConfirm()
    {
        var pickerTime = new PickerTimeBuilder()
            .Confirm("确认选择", "确定选择这个时间吗？")
            .Build();
        
        Assert.NotNull(pickerTime.Confirm);
        Assert.Equal("确认选择", pickerTime.Confirm.Title?.Content);
        Assert.Equal("确定选择这个时间吗？", pickerTime.Confirm.Text?.Content);
    }
    
    [Fact]
    public void Build_WithOnChange_ShouldAddBehavior()
    {
        var pickerTime = new PickerTimeBuilder()
            .OnChange(new { action = "time_selected" })
            .Build();
        
        Assert.NotNull(pickerTime.Behaviors);
        Assert.Single(pickerTime.Behaviors);
    }
    
    [Fact]
    public void Build_WithAllProperties_ShouldSetAllProperties()
    {
        var pickerTime = new PickerTimeBuilder()
            .Name("timePicker1")
            .Placeholder("选择时间")
            .InitialTime("14:00")
            .Required()
            .Width("default")
            .ElementId("timePicker1")
            .Margin("8px 0")
            .Build();
        
        Assert.Equal("picker_time", pickerTime.Tag);
        Assert.Equal("timePicker1", pickerTime.Name);
        Assert.Equal("选择时间", pickerTime.Placeholder?.Content);
        Assert.Equal("14:00", pickerTime.InitialTime);
        Assert.True(pickerTime.Required);
        Assert.Equal("default", pickerTime.Width);
        Assert.Equal("timePicker1", pickerTime.ElementId);
        Assert.Equal("8px 0", pickerTime.Margin);
    }
    
    [Fact]
    public void Build_UsingCardBuilder_ShouldAddPickerTimeToCard()
    {
        var card = CardBuilder.Create()
            .Header(h => h.Title("时间选择测试"))
            .Body(b => b
                .PickerTime(pt => pt
                    .Name("startTime")
                    .Placeholder("请选择开始时间")))
            .Build();
        
        Assert.Single(card.Body.Elements);
        var element = card.Body.Elements[0] as Models.Elements.PickerTime;
        Assert.NotNull(element);
        Assert.Equal("picker_time", element.Tag);
        Assert.Equal("startTime", element.Name);
    }
    
    [Fact]
    public void Build_InForm_ShouldAddPickerTimeToForm()
    {
        var card = CardBuilder.Create()
            .Header(h => h.Title("表单时间选择"))
            .Body(b => b
                .Form(f => f
                    .Name("timeForm")
                    .Vertical()
                    .PickerTime(pt => pt
                        .Name("meetingTime")
                        .Placeholder("选择会议时间")
                        .Required())
                    .Button(btn => btn.Text("提交").Submit())))
            .Build();
        
        var form = card.Body.Elements[0] as Models.Elements.Form;
        Assert.NotNull(form);
        Assert.Equal(2, form.Elements.Count);
        
        var pickerTime = form.Elements[0] as Models.Elements.PickerTime;
        Assert.NotNull(pickerTime);
        Assert.Equal("meetingTime", pickerTime.Name);
        Assert.True(pickerTime.Required);
    }
}
