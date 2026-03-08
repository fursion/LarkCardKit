using LarkCardKit.Models.Elements;

namespace LarkCardKit.Builders;

/// <summary>
/// 分割线构建器
/// </summary>
/// <remarks>
/// 分割线组件是一条长横线，用于分割卡片的内容，使呈现内容更清晰。
/// </remarks>
/// <example>
/// 以下示例演示如何使用 HrBuilder：
/// <code>
/// var hr = new HrBuilder()
///     .Margin("8px 0")
///     .ElementId("divider1")
///     .Build();
/// </code>
/// </example>
public class HrBuilder
{
    private readonly Hr _hr = new();

    /// <summary>
    /// 设置元素唯一标识
    /// </summary>
    /// <param name="id">元素 ID</param>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    public HrBuilder ElementId(string id)
    {
        _hr.ElementId = id;
        return this;
    }

    /// <summary>
    /// 设置外边距
    /// </summary>
    /// <param name="margin">外边距值，如 "4px", "8px 12px"</param>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    public HrBuilder Margin(string margin)
    {
        _hr.Margin = margin;
        return this;
    }

    /// <summary>
    /// 构建分割线对象
    /// </summary>
    /// <returns>完整的 <see cref="Hr"/> 对象</returns>
    public Hr Build() => _hr;
}
