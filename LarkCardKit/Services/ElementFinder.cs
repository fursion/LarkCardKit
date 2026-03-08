using LarkCardKit.Models;
using LarkCardKit.Models.Elements;

namespace LarkCardKit.Services;

public class ElementFinder : IElementFinder
{
    private readonly Card _card;

    public ElementFinder(Card card)
    {
        _card = card ?? throw new ArgumentNullException(nameof(card));
    }

    public Element? FindElementById(string elementId)
    {
        if (string.IsNullOrEmpty(elementId))
            return null;

        return FindElementRecursive(_card.Body.Elements, elementId);
    }

    public T? FindElementById<T>(string elementId) where T : Element
    {
        var element = FindElementById(elementId);
        return element as T;
    }

    public bool TryFindElementById(string elementId, out Element? element)
    {
        element = FindElementById(elementId);
        return element != null;
    }

    private Element? FindElementRecursive(List<Element> elements, string elementId)
    {
        foreach (var element in elements)
        {
            if (element.ElementId == elementId)
                return element;

            var found = element switch
            {
                Div div => FindInDiv(div, elementId),
                Form form => FindInForm(form, elementId),
                ColumnSet columnSet => FindInColumnSet(columnSet, elementId),
                _ => null
            };

            if (found != null)
                return found;
        }

        return null;
    }

    private Element? FindInDiv(Div div, string elementId)
    {
        if (div.Text != null)
        {
            if (div.Text.ElementId == elementId)
                return div.Text;

            var foundInText = FindInNestedElement(div.Text, elementId);
            if (foundInText != null)
                return foundInText;
        }

        if (div.Elements != null && div.Elements.Count > 0)
        {
            var found = FindElementRecursive(div.Elements, elementId);
            if (found != null)
                return found;
        }

        return null;
    }

    private Element? FindInForm(Form form, string elementId)
    {
        if (form.Elements.Count > 0)
        {
            return FindElementRecursive(form.Elements, elementId);
        }

        return null;
    }

    private Element? FindInColumnSet(ColumnSet columnSet, string elementId)
    {
        foreach (var column in columnSet.Columns)
        {
            if (column.ElementId == elementId)
                return column;

            if (column.Elements.Count > 0)
            {
                var found = FindElementRecursive(column.Elements, elementId);
                if (found != null)
                    return found;
            }
        }

        return null;
    }

    private Element? FindInNestedElement(Element element, string elementId)
    {
        return element switch
        {
            Div div => FindInDiv(div, elementId),
            Form form => FindInForm(form, elementId),
            ColumnSet columnSet => FindInColumnSet(columnSet, elementId),
            _ => null
        };
    }
}
