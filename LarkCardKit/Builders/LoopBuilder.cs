using LarkCardKit.Models.Elements;

namespace LarkCardKit.Builders;

/// <summary>
/// 循环容器构建器
/// </summary>
public class LoopBuilder
{
    private readonly Loop _loop = new();

    /// <summary>
    /// 设置数据源
    /// </summary>
    public LoopBuilder DataSource(LoopDataSource dataSource)
    {
        _loop.DataSource = dataSource;
        return this;
    }

    /// <summary>
    /// 设置列表数据
    /// </summary>
    public LoopBuilder List(List<object> list)
    {
        if (_loop.DataSource == null)
        {
            _loop.DataSource = new LoopDataSource();
        }
        _loop.DataSource.List = list;
        return this;
    }

    /// <summary>
    /// 设置循环模板
    /// </summary>
    public LoopBuilder Template(Element template)
    {
        _loop.Template = template;
        return this;
    }

    /// <summary>
    /// 设置元素 ID
    /// </summary>
    public LoopBuilder ElementId(string id)
    {
        _loop.ElementId = id;
        return this;
    }

    /// <summary>
    /// 设置外边距
    /// </summary>
    public LoopBuilder Margin(string margin)
    {
        _loop.Margin = margin;
        return this;
    }

    /// <summary>
    /// 构建 Loop 对象
    /// </summary>
    public Loop Build()
    {
        return _loop;
    }
}
