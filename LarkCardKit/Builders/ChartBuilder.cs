using LarkCardKit.Models.Elements;

namespace LarkCardKit.Builders;

/// <summary>
/// 图表构建器
/// </summary>
public class ChartBuilder
{
    private readonly Chart _chart = new();

    /// <summary>
    /// 设置图表类型
    /// </summary>
    public ChartBuilder ChartType(string type)
    {
        _chart.ChartType = type;
        return this;
    }

    /// <summary>
    /// 设置图表数据
    /// </summary>
    public ChartBuilder Data(object data)
    {
        _chart.Data = data;
        return this;
    }

    /// <summary>
    /// 设置图表宽度
    /// </summary>
    public ChartBuilder Width(string width)
    {
        _chart.Width = width;
        return this;
    }

    /// <summary>
    /// 设置元素 ID
    /// </summary>
    public ChartBuilder ElementId(string id)
    {
        _chart.ElementId = id;
        return this;
    }

    /// <summary>
    /// 设置外边距
    /// </summary>
    public ChartBuilder Margin(string margin)
    {
        _chart.Margin = margin;
        return this;
    }

    /// <summary>
    /// 构建 Chart 对象
    /// </summary>
    public Chart Build()
    {
        return _chart;
    }
}
