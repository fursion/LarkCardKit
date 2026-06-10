using LarkCardKit.Models;
using LarkCardKit.Models.Elements;

namespace LarkCardKit.Services;

/// <summary>
/// 元素查找器实现
/// 提供通过 ID 或 tag 查找和修改卡片组件的能力
/// </summary>
/// <example>
/// <code>
/// var finder = new ElementFinder(card);
///
/// // 查找组件
/// var button = finder.FindElementById("btn1");
///
/// // 修改组件
/// finder.ModifyElementById("btn1", el => {
///     if (el is Button btn) btn.Disabled = true;
/// });
///
/// // 批量修改
/// finder.UpdateAllByTag("button", el => {
///     if (el is Button btn) btn.Disabled = true;
/// });
/// </code>
/// </example>
public class ElementFinder : IElementFinder
{
    private readonly Card _card;

    public ElementFinder(Card card)
    {
        _card = card ?? throw new ArgumentNullException(nameof(card));
    }

    /// <inheritdoc />
    public Element? FindElementById(string elementId)
    {
        if (string.IsNullOrEmpty(elementId))
            return null;

        return FindElementRecursive(_card.Body.Elements, elementId);
    }

    /// <inheritdoc />
    public T? FindElementById<T>(string elementId) where T : Element
    {
        var element = FindElementById(elementId);
        return element as T;
    }

    /// <inheritdoc />
    public bool TryFindElementById(string elementId, out Element? element)
    {
        element = FindElementById(elementId);
        return element != null;
    }

    /// <inheritdoc />
    public bool ModifyElementById(string elementId, Action<Element> modify)
    {
        var element = FindElementById(elementId);
        if (element == null)
            return false;

        modify(element);
        return true;
    }

    /// <inheritdoc />
    public bool ModifyElementById<T>(string elementId, Action<T> modify) where T : Element
    {
        var element = FindElementById<T>(elementId);
        if (element == null)
            return false;

        modify(element);
        return true;
    }

    /// <inheritdoc />
    public IEnumerable<Element> FindAllElementsByTag(string tag)
    {
        var results = new List<Element>();
        FindElementsByTagRecursive(_card.Body.Elements, tag, results);
        return results;
    }

    /// <inheritdoc />
    public int UpdateAllByTag(string tag, Action<Element> modify)
    {
        var elements = FindAllElementsByTag(tag).ToList();
        foreach (var element in elements)
        {
            modify(element);
        }
        return elements.Count;
    }

    private Element? FindElementRecursive(List<Element> elements, string elementId)
    {
        foreach (var element in elements)
        {
            if (element.ElementId == elementId)
                return element;

            var found = element switch
            {
                TextDiv textDiv => FindInTextDiv(textDiv, elementId),
                PlainTextElement plainTextElement => FindInPlainTextElement(plainTextElement, elementId),
                Form form => FindInForm(form, elementId),
                ColumnSet columnSet => FindInColumnSet(columnSet, elementId),
                _ => null
            };

            if (found != null)
                return found;
        }

        return null;
    }

    private Element? FindInTextDiv(TextDiv textDiv, string elementId)
    {
        if (textDiv.Text != null)
        {
            if (textDiv.Text.ElementId == elementId)
                return textDiv.Text;

            var foundInText = FindInNestedElement(textDiv.Text, elementId);
            if (foundInText != null)
                return foundInText;
        }

        return null;
    }

    private Element? FindInPlainTextElement(PlainTextElement plainTextElement, string elementId)
    {
        if (plainTextElement.Elements != null && plainTextElement.Elements.Count > 0)
        {
            return FindElementRecursive(plainTextElement.Elements, elementId);
        }

        if (plainTextElement.Text != null)
        {
            if (plainTextElement.Text.ElementId == elementId)
                return plainTextElement.Text;

            var foundInText = FindInNestedElement(plainTextElement.Text, elementId);
            if (foundInText != null)
                return foundInText;
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
            TextDiv textDiv => FindInTextDiv(textDiv, elementId),
            PlainTextElement plainTextElement => FindInPlainTextElement(plainTextElement, elementId),
            Form form => FindInForm(form, elementId),
            ColumnSet columnSet => FindInColumnSet(columnSet, elementId),
            _ => null
        };
    }

    private void FindElementsByTagRecursive(List<Element> elements, string tag, List<Element> results)
    {
        foreach (var element in elements)
        {
            if (element.Tag == tag)
                results.Add(element);

            switch (element)
            {
                case TextDiv textDiv:
                    if (textDiv.Text != null && textDiv.Text.Tag == tag)
                        results.Add(textDiv.Text);
                    FindElementsByTagInNestedElement(textDiv.Text, tag, results);
                    break;

                case PlainTextElement plainTextElement:
                    if (plainTextElement.Elements != null)
                    {
                        FindElementsByTagRecursive(plainTextElement.Elements, tag, results);
                    }
                    if (plainTextElement.Text != null && plainTextElement.Text.Tag == tag)
                        results.Add(plainTextElement.Text);
                    FindElementsByTagInNestedElement(plainTextElement.Text, tag, results);
                    break;

                case Form form:
                    FindElementsByTagRecursive(form.Elements, tag, results);
                    break;

                case ColumnSet columnSet:
                    foreach (var column in columnSet.Columns)
                    {
                        if (column.Tag == tag)
                            results.Add(column);
                        FindElementsByTagRecursive(column.Elements, tag, results);
                    }
                    break;
            }
        }
    }

    private void FindElementsByTagInNestedElement(Element? element, string tag, List<Element> results)
    {
        if (element == null) return;

        switch (element)
        {
            case TextDiv textDiv:
                if (textDiv.Text != null && textDiv.Text.Tag == tag)
                    results.Add(textDiv.Text);
                FindElementsByTagInNestedElement(textDiv.Text, tag, results);
                break;

            case PlainTextElement plainTextElement:
                if (plainTextElement.Elements != null)
                {
                    FindElementsByTagRecursive(plainTextElement.Elements, tag, results);
                }
                if (plainTextElement.Text != null && plainTextElement.Text.Tag == tag)
                    results.Add(plainTextElement.Text);
                FindElementsByTagInNestedElement(plainTextElement.Text, tag, results);
                break;

            case Form form:
                FindElementsByTagRecursive(form.Elements, tag, results);
                break;

            case ColumnSet columnSet:
                foreach (var column in columnSet.Columns)
                {
                    if (column.Tag == tag)
                        results.Add(column);
                    FindElementsByTagRecursive(column.Elements, tag, results);
                }
                break;
        }
    }
}