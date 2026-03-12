using LarkCardKit.Models;
using LarkCardKit.Models.Elements;
using LarkCardKit.Templates;

namespace LarkCardKit;

/// <summary>
/// 模板参数填充工具类
/// 支持在卡片文本内容中使用 ${变量名} 格式的模板变量
/// </summary>
public static class TemplateHelper
{
    /// <summary>
    /// 使用参数字典填充卡片中的模板参数
    /// </summary>
    /// <param name="card">卡片对象</param>
    /// <param name="parameters">参数字典</param>
    public static void FillTemplate(this Card card, Dictionary<string, object?> parameters)
    {
        var filler = new TemplateParameterFiller();
        filler.SetParameters(parameters);
        card.FillTemplate(filler);
    }

    /// <summary>
    /// 使用 TemplateParameterFiller 填充卡片中的模板参数
    /// </summary>
    /// <param name="card">卡片对象</param>
    /// <param name="filler">模板参数填充器</param>
    public static void FillTemplate(this Card card, TemplateParameterFiller filler)
    {
        if (card.Header != null)
        {
            FillTemplateInElement(card.Header.Title, filler);
            FillTemplateInElement(card.Header.Subtitle, filler);
        }

        FillTemplateInElements(card.Body.Elements, filler);
    }

    /// <summary>
    /// 在元素中填充模板参数
    /// </summary>
    private static void FillTemplateInElement(Element? element, TemplateParameterFiller filler)
    {
        if (element == null) return;

        switch (element)
        {
            case PlainText plainText:
                plainText.Content = filler.FillString(plainText.Content);
                break;
            case Markdown markdown:
                markdown.Content = filler.FillString(markdown.Content);
                break;
            case MarkdownText markdownText:
                markdownText.Content = filler.FillString(markdownText.Content);
                break;
            case ColumnSet columnSet:
                foreach (var column in columnSet.Columns)
                {
                    FillTemplateInElements(column.Elements, filler);
                }
                break;
            case Form form:
                FillTemplateInElements(form.Elements, filler);
                break;
            case CollapsiblePanel collapsiblePanel:
                if (collapsiblePanel.Header != null)
                {
                    FillTemplateInElement(collapsiblePanel.Header.Title, filler);
                }
                FillTemplateInElements(collapsiblePanel.Elements, filler);
                break;
            case Loop loop:
                FillTemplateInElement(loop.Template, filler);
                break;
        }
    }

    /// <summary>
    /// 在元素列表中填充模板参数
    /// </summary>
    private static void FillTemplateInElements(List<Element> elements, TemplateParameterFiller filler)
    {
        if (elements == null) return;

        foreach (var element in elements)
        {
            FillTemplateInElement(element, filler);
        }
    }
}