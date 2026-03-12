using LarkCardKit.Models.Elements;

namespace LarkCardKit.Services;

/// <summary>
/// 元素查找器接口
/// 提供通过 ID 查找和修改卡片组件的能力
/// </summary>
public interface IElementFinder
{
    /// <summary>
    /// 通过 ID 查找元素
    /// </summary>
    /// <param name="elementId">元素唯一标识</param>
    /// <returns>找到的元素，如果未找到则返回 null</returns>
    Element? FindElementById(string elementId);

    /// <summary>
    /// 通过 ID 查找指定类型的元素
    /// </summary>
    /// <typeparam name="T">元素类型</typeparam>
    /// <param name="elementId">元素唯一标识</param>
    /// <returns>找到的元素，如果未找到或类型不匹配则返回 null</returns>
    T? FindElementById<T>(string elementId) where T : Element;

    /// <summary>
    /// 尝试通过 ID 查找元素
    /// </summary>
    /// <param name="elementId">元素唯一标识</param>
    /// <param name="element">找到的元素，如果未找到则为 null</param>
    /// <returns>是否找到元素</returns>
    bool TryFindElementById(string elementId, out Element? element);

    /// <summary>
    /// 通过 ID 修改元素
    /// </summary>
    /// <param name="elementId">元素唯一标识</param>
    /// <param name="modify">修改操作</param>
    /// <returns>是否成功修改（元素存在时返回 true）</returns>
    bool ModifyElementById(string elementId, Action<Element> modify);

    /// <summary>
    /// 通过 ID 修改指定类型的元素
    /// </summary>
    /// <typeparam name="T">元素类型</typeparam>
    /// <param name="elementId">元素唯一标识</param>
    /// <param name="modify">修改操作</param>
    /// <returns>是否成功修改（元素存在且类型匹配时返回 true）</returns>
    bool ModifyElementById<T>(string elementId, Action<T> modify) where T : Element;

    /// <summary>
    /// 通过 tag 查找所有元素
    /// </summary>
    /// <param name="tag">元素 tag</param>
    /// <returns>找到的元素列表</returns>
    IEnumerable<Element> FindAllElementsByTag(string tag);

    /// <summary>
    /// 批量修改所有指定 tag 的元素
    /// </summary>
    /// <param name="tag">元素 tag</param>
    /// <param name="modify">修改操作</param>
    /// <returns>修改的元素数量</returns>
    int UpdateAllByTag(string tag, Action<Element> modify);
}