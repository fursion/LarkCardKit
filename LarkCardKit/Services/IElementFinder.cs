using LarkCardKit.Models.Elements;

namespace LarkCardKit.Services;

public interface IElementFinder
{
    Element? FindElementById(string elementId);
    T? FindElementById<T>(string elementId) where T : Element;
    bool TryFindElementById(string elementId, out Element? element);
}
