using LarkCardKit.Builders;
using Xunit;

namespace LarkCardKit.Tests;

public class TableBuilderTests
{
    [Fact]
    public void Build_BasicTable_ShouldHaveCorrectTag()
    {
        var table = new TableBuilder()
            .Column(col => col.Name("name").DataType("text"))
            .Row(row => row.TextCell("name", "测试"))
            .Build();
        
        Assert.Equal("table", table.Tag);
    }
    
    [Fact]
    public void Build_WithPageSize_ShouldSetPageSize()
    {
        var table = new TableBuilder()
            .PageSize(10)
            .Column(col => col.Name("name"))
            .Build();
        
        Assert.Equal(10, table.PageSize);
    }
    
    [Fact]
    public void Build_WithRowHeight_ShouldSetRowHeight()
    {
        var table = new TableBuilder()
            .RowHeight("auto")
            .Column(col => col.Name("name"))
            .Build();
        
        Assert.Equal("auto", table.RowHeight);
    }
    
    [Fact]
    public void Build_WithFreezeFirstColumn_ShouldSetFreezeFirstColumn()
    {
        var table = new TableBuilder()
            .FreezeFirstColumn()
            .Column(col => col.Name("name"))
            .Build();
        
        Assert.True(table.FreezeFirstColumn);
    }
    
    [Fact]
    public void Build_WithHeaderStyle_ShouldSetHeaderStyle()
    {
        var table = new TableBuilder()
            .HeaderStyle(hs => hs
                .BackgroundStyle("grey")
                .Bold()
                .TextAlign("center"))
            .Column(col => col.Name("name"))
            .Build();
        
        Assert.NotNull(table.HeaderStyle);
        Assert.Equal("grey", table.HeaderStyle.BackgroundStyle);
        Assert.True(table.HeaderStyle.Bold);
        Assert.Equal("center", table.HeaderStyle.TextAlign);
    }
    
    [Fact]
    public void Build_WithMultipleColumns_ShouldAddAllColumns()
    {
        var table = new TableBuilder()
            .Column(col => col.Name("name").DisplayName("名称").DataType("text"))
            .Column(col => col.Name("amount").DisplayName("金额").DataType("number"))
            .Column(col => col.Name("status").DisplayName("状态").DataType("options"))
            .Build();
        
        Assert.Equal(3, table.Columns?.Count);
        Assert.Equal("name", table.Columns![0].Name);
        Assert.Equal("amount", table.Columns![1].Name);
        Assert.Equal("status", table.Columns![2].Name);
    }
    
    [Fact]
    public void Build_WithNumberFormat_ShouldSetFormat()
    {
        var table = new TableBuilder()
            .Column(col => col
                .Name("amount")
                .DataType("number")
                .NumberFormat(f => f.Symbol("¥").Precision(2).Separator()))
            .Build();
        
        Assert.NotNull(table.Columns![0].Format);
        Assert.Equal("¥", table.Columns[0].Format!.Symbol);
        Assert.Equal(2, table.Columns[0].Format!.Precision);
        Assert.True(table.Columns[0].Format!.Separator);
    }
    
    [Fact]
    public void Build_WithDateFormat_ShouldSetDateFormat()
    {
        var table = new TableBuilder()
            .Column(col => col
                .Name("date")
                .DataType("date")
                .DateFormat("YYYY/MM/DD"))
            .Build();
        
        Assert.Equal("YYYY/MM/DD", table.Columns![0].DateFormat);
    }
    
    [Fact]
    public void Build_WithRow_ShouldAddRow()
    {
        var table = new TableBuilder()
            .Column(col => col.Name("name").DataType("text"))
            .Row(row => row.TextCell("name", "飞书科技"))
            .Build();
        
        Assert.Single(table.Rows!);
        Assert.Equal("飞书科技", table.Rows![0]["name"]);
    }
    
    [Fact]
    public void Build_WithMultipleRows_ShouldAddAllRows()
    {
        var table = new TableBuilder()
            .Column(col => col.Name("name").DataType("text"))
            .Row(row => row.TextCell("name", "飞书科技"))
            .Row(row => row.TextCell("name", "字节跳动"))
            .Row(row => row.TextCell("name", "抖音"))
            .Build();
        
        Assert.Equal(3, table.Rows?.Count);
    }
    
    [Fact]
    public void Build_WithDifferentCellTypes_ShouldSetCorrectValues()
    {
        var table = new TableBuilder()
            .Column(col => col.Name("text").DataType("text"))
            .Column(col => col.Name("number").DataType("number"))
            .Column(col => col.Name("date").DataType("date"))
            .Column(col => col.Name("option").DataType("options"))
            .Column(col => col.Name("persons").DataType("persons"))
            .Column(col => col.Name("markdown").DataType("markdown"))
            .Row(row => row
                .TextCell("text", "文本内容")
                .NumberCell("number", 123.45)
                .DateCell("date", 1606101072000L)
                .OptionCell("option", "进行中", "blue")
                .PersonsCell("persons", "ou_xxx", "ou_yyy")
                .MarkdownCell("markdown", "**粗体**"))
            .Build();
        
        var row = table.Rows![0];
        Assert.Equal("文本内容", row["text"]);
        Assert.Equal(123.45, row["number"]);
        Assert.Equal(1606101072000L, row["date"]);
        Assert.NotNull(row["option"]);
        Assert.NotNull(row["persons"]);
        Assert.Equal("**粗体**", row["markdown"]);
    }
    
    [Fact]
    public void Build_WithOptionCell_ShouldCreateCorrectStructure()
    {
        var table = new TableBuilder()
            .Column(col => col.Name("status").DataType("options"))
            .Row(row => row.OptionCell("status", "已完成", "green"))
            .Build();
        
        var option = table.Rows![0]["status"] as List<Dictionary<string, string>>;
        Assert.NotNull(option);
        Assert.Single(option);
        Assert.Equal("已完成", option[0]["text"]);
        Assert.Equal("green", option[0]["color"]);
    }
    
    [Fact]
    public void Build_WithOptionsCell_ShouldCreateMultipleOptions()
    {
        var table = new TableBuilder()
            .Column(col => col.Name("tags").DataType("options"))
            .Row(row => row.OptionsCell("tags", 
                ("标签1", "blue"),
                ("标签2", "red"),
                ("标签3", "green")))
            .Build();
        
        var options = table.Rows![0]["tags"] as List<Dictionary<string, string>>;
        Assert.NotNull(options);
        Assert.Equal(3, options.Count);
    }
    
    [Fact]
    public void Build_WithLinkCell_ShouldCreateMarkdownLink()
    {
        var table = new TableBuilder()
            .Column(col => col.Name("link").DataType("lark_md"))
            .Row(row => row.LinkCell("link", "点击这里", "https://feishu.cn"))
            .Build();
        
        Assert.Equal("[点击这里](https://feishu.cn)", table.Rows![0]["link"]);
    }
    
    [Fact]
    public void Build_UsingCardBuilder_ShouldAddTableToCard()
    {
        var card = CardBuilder.Create()
            .Header(h => h.Title("数据表"))
            .Body(b => b
                .Table(table => table
                    .PageSize(5)
                    .Column(col => col.Name("name").DisplayName("名称"))
                    .Row(row => row.TextCell("name", "测试"))))
            .Build();
        
        Assert.Single(card.Body.Elements);
        var tableElement = card.Body.Elements[0] as Models.Elements.Table;
        Assert.NotNull(tableElement);
        Assert.Equal("table", tableElement.Tag);
    }
    
    [Fact]
    public void Build_CompleteTable_ShouldSetAllProperties()
    {
        var table = new TableBuilder()
            .ElementId("table1")
            .Margin("8px 0")
            .PageSize(10)
            .RowHeight("auto")
            .RowMaxHeight("50px")
            .FreezeFirstColumn()
            .HeaderStyle(hs => hs
                .TextAlign("left")
                .TextSize("normal")
                .BackgroundStyle("grey")
                .TextColor("grey")
                .Bold(true)
                .Lines(1))
            .Column(col => col
                .Name("customer_name")
                .DisplayName("客户名称")
                .Width("auto")
                .DataType("text")
                .VerticalAlign("center")
                .HorizontalAlign("left"))
            .Column(col => col
                .Name("amount")
                .DisplayName("金额")
                .Width("120px")
                .DataType("number")
                .NumberFormat(f => f.Symbol("¥").Precision(2).Separator()))
            .Row(row => row
                .TextCell("customer_name", "飞书科技")
                .NumberCell("amount", 168.5))
            .Build();
        
        Assert.Equal("table1", table.ElementId);
        Assert.Equal("8px 0", table.Margin);
        Assert.Equal(10, table.PageSize);
        Assert.Equal("auto", table.RowHeight);
        Assert.Equal("50px", table.RowMaxHeight);
        Assert.True(table.FreezeFirstColumn);
        Assert.NotNull(table.HeaderStyle);
        Assert.Equal(2, table.Columns?.Count);
        Assert.Single(table.Rows!);
    }
}
