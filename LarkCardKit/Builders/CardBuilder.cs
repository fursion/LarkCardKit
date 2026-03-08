using LarkCardKit.Models;
using LarkCardKit.Config;
using LarkCardKit.Services;
using LarkCardKit.Models.Elements;
using LarkCardKit.Templates;

namespace LarkCardKit.Builders;

/// <summary>
/// 卡片构建器
/// 使用流畅的链式调用 API 构建飞书卡片对象
/// </summary>
/// <example>
/// 以下示例演示如何使用 CardBuilder 创建卡片：
/// <code>
/// var card = CardBuilder.Create()
///     .Header(h => h.Title("欢迎"))
///     .Body(b => b
///         .PlainText("这是卡片内容")
///         .Button(btn => btn
///             .Text("点击")
///             .Type(ButtonType.Primary)))
///     .Build();
/// 
/// // 序列化为 JSON
/// var json = card.ToJson();
/// </code>
/// </example>
public class CardBuilder
{
    private readonly Card _card = new();
    private readonly TemplateParameterFiller _filler = new();
    private ElementFinder? _finder;
    private bool _templateApplied = false;
    
    /// <summary>
    /// 配置卡片设置
    /// </summary>
    /// <param name="configure">卡片配置构建器的配置动作</param>
    /// <returns>当前卡片构建器实例（支持链式调用）</returns>
    /// <example>
    /// <code>
    /// var card = CardBuilder.Create()
    ///     .Config(c => c
    ///         .UpdateMulti(true)
    ///         .StreamingMode(false))
    ///     .Build();
    /// </code>
    /// </example>
    public CardBuilder Config(Action<CardConfigBuilder> configure)
    {
        var builder = new CardConfigBuilder();
        configure(builder);
        _card.Config = builder.Build();
        return this;
    }
    
    /// <summary>
    /// 配置卡片头部
    /// </summary>
    /// <param name="configure">卡片头部构建器的配置动作</param>
    /// <returns>当前卡片构建器实例（支持链式调用）</returns>
    /// <example>
    /// <code>
    /// var card = CardBuilder.Create()
    ///     .Header(h => h
    ///         .Title("卡片标题")
    ///         .Padding("8px"))
    ///     .Build();
    /// </code>
    /// </example>
    public CardBuilder Header(Action<CardHeaderBuilder> configure)
    {
        var builder = new CardHeaderBuilder();
        configure(builder);
        _card.Header = builder.Build();
        return this;
    }
    
    public CardBuilder CardLink(Action<CardLinkBuilder> configure)
    {
        var builder = new CardLinkBuilder();
        configure(builder);
        _card.CardLink = builder.Build();
        return this;
    }
    
    public CardBuilder CardLink(string url)
    {
        _card.CardLink = new Models.CardLink { Url = url };
        return this;
    }

    public CardBuilder Fallback(Action<FallbackBuilder> configure)
    {
        var builder = new FallbackBuilder();
        configure(builder);
        _card.Fallback = builder.Build();
        return this;
    }

    public CardBuilder Fallback(string title, string content)
    {
        _card.Fallback = new Models.Fallback
        {
            Title = new Models.Elements.PlainText { Content = title },
            Content = new Models.Elements.PlainText { Content = content }
        };
        return this;
    }

    public CardBuilder Body(Action<CardBodyBuilder> configure)
    {
        var builder = new CardBodyBuilder(_card.Body);
        configure(builder);
        return this;
    }
    
    public CardBuilder SetParameter(string key, object? value)
    {
        _filler.SetParameter(key, value);
        _templateApplied = false;
        return this;
    }
    
    /// <summary>
    /// 批量设置模板参数（使用匿名对象）
    /// </summary>
    /// <param name="parameters">包含参数的匿名对象</param>
    /// <returns>当前卡片构建器实例（支持链式调用）</returns>
    /// <example>
    /// <code>
    /// card.SetParameters(new { userName = "张三", orderId = "123" });
    /// </code>
    /// </example>
    public CardBuilder SetParameters(object parameters)
    {
        _filler.SetParameters(parameters);
        _templateApplied = false;
        return this;
    }
    
    /// <summary>
    /// 批量设置模板参数（使用字典）
    /// </summary>
    /// <param name="parameters">参数字典</param>
    /// <returns>当前卡片构建器实例（支持链式调用）</returns>
    /// <example>
    /// <code>
    /// var dict = new Dictionary&lt;string, object?&gt;
    /// {
    ///     ["userName"] = "张三",
    ///     ["orderId"] = "123"
    /// };
    /// card.SetParameters(dict);
    /// </code>
    /// </example>
    public CardBuilder SetParameters(Dictionary<string, object?> parameters)
    {
        _filler.SetParameters(parameters);
        _templateApplied = false;
        return this;
    }
    
    private void ApplyTemplateParameters()
    {
        if (_templateApplied) return;
        
        if (_card.Header?.Title is PlainText headerTitle)
        {
            headerTitle.Content = _filler.FillString(headerTitle.Content);
        }
        
        foreach (var element in _card.Body.Elements)
        {
            ApplyTemplateToElement(element);
        }
        
        _templateApplied = true;
    }
    
    private void ApplyTemplateToElement(Element element)
    {
        switch (element)
        {
            case PlainText plainText:
                plainText.Content = _filler.FillString(plainText.Content);
                break;
                
            case Markdown markdown:
                markdown.Content = _filler.FillString(markdown.Content);
                break;
                
            case Button button:
                ApplyTemplateToButton(button);
                break;
                
            case Input input:
                ApplyTemplateToInput(input);
                break;
                
            case Select select:
                ApplyTemplateToSelect(select);
                break;
                
            case Checkbox checkbox:
                ApplyTemplateToCheckbox(checkbox);
                break;
                
            case DatePicker datePicker:
                ApplyTemplateToDatePicker(datePicker);
                break;
                
            case PickerTime pickerTime:
                ApplyTemplateToPickerTime(pickerTime);
                break;
                
            case PickerDatetime pickerDatetime:
                ApplyTemplateToPickerDatetime(pickerDatetime);
                break;
                
            case Div div:
                ApplyTemplateToDiv(div);
                break;
                
            case Form form:
                ApplyTemplateToForm(form);
                break;
                
            case ColumnSet columnSet:
                ApplyTemplateToColumnSet(columnSet);
                break;
        }
    }
    
    private void ApplyTemplateToButton(Button button)
    {
        if (button.Text != null)
        {
            button.Text.Content = _filler.FillString(button.Text.Content);
        }
        
        if (button.Type != null && button.Type.Contains("${"))
        {
            button.Type = _filler.FillString(button.Type);
        }
        
        if (button.HoverTips != null)
        {
            button.HoverTips.Content = _filler.FillString(button.HoverTips.Content);
        }
        
        if (button.DisabledTips != null)
        {
            button.DisabledTips.Content = _filler.FillString(button.DisabledTips.Content);
        }
        
        if (button.Confirm != null)
        {
            ApplyTemplateToConfirmConfig(button.Confirm);
        }
    }
    
    private void ApplyTemplateToInput(Input input)
    {
        if (input.Placeholder != null)
        {
            input.Placeholder.Content = _filler.FillString(input.Placeholder.Content);
        }
        
        if (input.Label != null)
        {
            input.Label.Content = _filler.FillString(input.Label.Content);
        }
        
        if (input.DefaultValue != null)
        {
            input.DefaultValue = _filler.FillString(input.DefaultValue);
        }
        
        if (input.Disabled != null)
        {
            if (input.Disabled is string disabledStr && disabledStr.Contains("${"))
            {
                var filled = _filler.FillString(disabledStr);
                if (bool.TryParse(filled, out var boolValue))
                {
                    input.Disabled = boolValue;
                }
                else
                {
                    input.Disabled = filled;
                }
            }
        }
        
        if (input.DisabledTips != null)
        {
            input.DisabledTips.Content = _filler.FillString(input.DisabledTips.Content);
        }
        
        if (input.Confirm != null)
        {
            ApplyTemplateToConfirmConfig(input.Confirm);
        }
    }
    
    private void ApplyTemplateToSelect(Select select)
    {
        if (select.Placeholder != null)
        {
            select.Placeholder.Content = _filler.FillString(select.Placeholder.Content);
        }
        
        if (!string.IsNullOrEmpty(select.OptionsTemplateKey))
        {
            var rawOptions = _filler.GetValue(select.OptionsTemplateKey);
            if (rawOptions != null)
            {
                if (rawOptions is IEnumerable<SelectOptionInfo> optionInfos)
                {
                    select.Options = optionInfos.Select(info => new SelectOption
                    {
                        Value = info.Value,
                        Text = new PlainText { Content = info.Text }
                    }).ToList();
                }
                else if (rawOptions is IEnumerable<SelectOption> options)
                {
                    select.Options = options.ToList();
                }
            }
        }
        
        if (select.Options != null)
        {
            foreach (var option in select.Options)
            {
                if (option.Text != null)
                {
                    option.Text.Content = _filler.FillString(option.Text.Content);
                }
                option.Value = _filler.FillString(option.Value);
            }
        }
    }
    
    private void ApplyTemplateToCheckbox(Checkbox checkbox)
    {
        if (checkbox.Placeholder != null)
        {
            checkbox.Placeholder.Content = _filler.FillString(checkbox.Placeholder.Content);
        }
        
        if (checkbox.Options != null)
        {
            foreach (var option in checkbox.Options)
            {
                if (option.Text != null)
                {
                    option.Text.Content = _filler.FillString(option.Text.Content);
                }
                option.Value = _filler.FillString(option.Value);
            }
        }
    }
    
    private void ApplyTemplateToDatePicker(DatePicker datePicker)
    {
        if (datePicker.Placeholder != null)
        {
            datePicker.Placeholder.Content = _filler.FillString(datePicker.Placeholder.Content);
        }
        
        if (datePicker.InitialDate != null)
        {
            datePicker.InitialDate = _filler.FillString(datePicker.InitialDate);
        }
        
        if (datePicker.Confirm != null)
        {
            ApplyTemplateToConfirmConfig(datePicker.Confirm);
        }
    }
    
    private void ApplyTemplateToPickerTime(PickerTime pickerTime)
    {
        if (pickerTime.Placeholder != null)
        {
            pickerTime.Placeholder.Content = _filler.FillString(pickerTime.Placeholder.Content);
        }
        
        if (pickerTime.InitialTime != null)
        {
            pickerTime.InitialTime = _filler.FillString(pickerTime.InitialTime);
        }
        
        if (pickerTime.Confirm != null)
        {
            ApplyTemplateToConfirmConfig(pickerTime.Confirm);
        }
    }
    
    private void ApplyTemplateToPickerDatetime(PickerDatetime pickerDatetime)
    {
        if (pickerDatetime.Placeholder != null)
        {
            pickerDatetime.Placeholder.Content = _filler.FillString(pickerDatetime.Placeholder.Content);
        }
        
        if (pickerDatetime.InitialDatetime != null)
        {
            pickerDatetime.InitialDatetime = _filler.FillString(pickerDatetime.InitialDatetime);
        }
        
        if (pickerDatetime.Confirm != null)
        {
            ApplyTemplateToConfirmConfig(pickerDatetime.Confirm);
        }
    }
    
    private void ApplyTemplateToDiv(Div div)
    {
        if (div.Text != null)
        {
            ApplyTemplateToElement(div.Text);
        }
        
        if (div.Elements != null)
        {
            foreach (var child in div.Elements)
            {
                ApplyTemplateToElement(child);
            }
        }
    }
    
    private void ApplyTemplateToForm(Form form)
    {
        foreach (var element in form.Elements)
        {
            ApplyTemplateToElement(element);
        }
    }
    
    private void ApplyTemplateToColumnSet(ColumnSet columnSet)
    {
        foreach (var column in columnSet.Columns)
        {
            foreach (var element in column.Elements)
            {
                ApplyTemplateToElement(element);
            }
        }
    }
    
    private void ApplyTemplateToConfirmConfig(ConfirmConfig confirm)
    {
        if (confirm.Title != null)
        {
            confirm.Title.Content = _filler.FillString(confirm.Title.Content);
        }
        
        if (confirm.Text != null)
        {
            confirm.Text.Content = _filler.FillString(confirm.Text.Content);
        }
    }
    
    /// <summary>
    /// 构建卡片对象
    /// </summary>
    /// <returns>完整的 <see cref="Card"/> 对象</returns>
    /// <example>
    /// <code>
    /// var card = CardBuilder.Create()
    ///     .Header(h => h.Title("标题"))
    ///     .Build();
    /// </code>
    /// </example>
    public Card Build()
    {
        ApplyTemplateParameters();
        return _card;
    }
    
    /// <summary>
    /// 根据元素 ID 查找元素
    /// </summary>
    /// <param name="elementId">元素唯一标识</param>
    /// <returns>找到的元素，如果未找到则返回 null</returns>
    /// <example>
    /// <code>
    /// var element = card.FindElementById("btn1");
    /// </code>
    /// </example>
    public Element? FindElementById(string elementId)
    {
        _finder ??= new ElementFinder(_card);
        return _finder.FindElementById(elementId);
    }
    
    /// <summary>
    /// 根据元素 ID 查找指定类型的元素
    /// </summary>
    /// <typeparam name="T">元素类型</typeparam>
    /// <param name="elementId">元素唯一标识</param>
    /// <returns>找到的元素，如果未找到或类型不匹配则返回 null</returns>
    /// <example>
    /// <code>
    /// var button = card.FindElementById&lt;Button&gt;("btn1");
    /// </code>
    /// </example>
    public T? FindElementById<T>(string elementId) where T : Element
    {
        _finder ??= new ElementFinder(_card);
        return _finder.FindElementById<T>(elementId);
    }
    
    /// <summary>
    /// 尝试根据元素 ID 查找元素
    /// </summary>
    /// <param name="elementId">元素唯一标识</param>
    /// <param name="element">找到的元素，如果未找到则为 null</param>
    /// <returns>是否找到元素</returns>
    public bool TryFindElementById(string elementId, out Element? element)
    {
        _finder ??= new ElementFinder(_card);
        return _finder.TryFindElementById(elementId, out element);
    }
    
    /// <summary>
    /// 修改指定 ID 的元素
    /// </summary>
    /// <param name="elementId">元素唯一标识</param>
    /// <param name="modify">修改操作</param>
    /// <returns>是否成功修改（元素存在时返回 true）</returns>
    /// <example>
    /// <code>
    /// card.ModifyElement("btn1", element => 
    /// {
    ///     if (element is Button btn)
    ///         btn.Text = new PlainText { Content = "新文本" };
    /// });
    /// </code>
    /// </example>
    public bool ModifyElement(string elementId, Action<Element> modify)
    {
        var element = FindElementById(elementId);
        if (element == null)
            return false;
        
        modify(element);
        return true;
    }
    
    /// <summary>
    /// 修改指定 ID 和类型的元素
    /// </summary>
    /// <typeparam name="T">元素类型</typeparam>
    /// <param name="elementId">元素唯一标识</param>
    /// <param name="modify">修改操作</param>
    /// <returns>是否成功修改（元素存在且类型匹配时返回 true）</returns>
    /// <example>
    /// <code>
    /// card.ModifyElement&lt;Button&gt;("btn1", btn => 
    /// {
    ///     btn.Text = new PlainText { Content = "新文本" };
    /// });
    /// </code>
    /// </example>
    public bool ModifyElement<T>(string elementId, Action<T> modify) where T : Element
    {
        var element = FindElementById<T>(elementId);
        if (element == null)
            return false;
        
        modify(element);
        return true;
    }
    
    /// <summary>
    /// 尝试修改指定 ID 和类型的元素
    /// </summary>
    /// <typeparam name="T">元素类型</typeparam>
    /// <param name="elementId">元素唯一标识</param>
    /// <param name="modify">修改操作</param>
    /// <returns>是否成功修改</returns>
    public bool TryModifyElement<T>(string elementId, Action<T> modify) where T : Element
    {
        return ModifyElement(elementId, modify);
    }
    
    /// <summary>
    /// 替换指定 ID 的元素
    /// </summary>
    /// <param name="elementId">要替换的元素 ID</param>
    /// <param name="newElement">新元素</param>
    /// <returns>是否成功替换</returns>
    /// <example>
    /// <code>
    /// card.ReplaceElement("text1", new Markdown { Content = "**新内容**" });
    /// </code>
    /// </example>
    public bool ReplaceElement(string elementId, Element newElement)
    {
        return TryReplaceElementInternal(elementId, newElement);
    }
    
    /// <summary>
    /// 尝试替换指定 ID 的元素
    /// </summary>
    /// <param name="elementId">要替换的元素 ID</param>
    /// <param name="newElement">新元素</param>
    /// <returns>是否成功替换</returns>
    public bool TryReplaceElement(string elementId, Element newElement)
    {
        return TryReplaceElementInternal(elementId, newElement);
    }
    
    private bool TryReplaceElementInternal(string elementId, Element newElement)
    {
        return ReplaceElementRecursive(_card.Body.Elements, elementId, newElement);
    }
    
    private bool ReplaceElementRecursive(List<Element> elements, string elementId, Element newElement)
    {
        for (int i = 0; i < elements.Count; i++)
        {
            if (elements[i].ElementId == elementId)
            {
                elements[i] = newElement;
                return true;
            }
            
            var replaced = elements[i] switch
            {
                Div div => ReplaceInDiv(div, elementId, newElement),
                Form form => ReplaceInForm(form, elementId, newElement),
                ColumnSet columnSet => ReplaceInColumnSet(columnSet, elementId, newElement),
                _ => false
            };
            
            if (replaced)
                return true;
        }
        
        return false;
    }
    
    private bool ReplaceInDiv(Div div, string elementId, Element newElement)
    {
        if (div.Text != null && div.Text.ElementId == elementId)
        {
            div.Text = newElement as PlainText ?? 
                       newElement as Markdown ?? 
                       div.Text;
            return true;
        }
        
        if (div.Elements != null && div.Elements.Count > 0)
        {
            return ReplaceElementRecursive(div.Elements, elementId, newElement);
        }
        
        return false;
    }
    
    private bool ReplaceInForm(Form form, string elementId, Element newElement)
    {
        if (form.Elements.Count > 0)
        {
            return ReplaceElementRecursive(form.Elements, elementId, newElement);
        }
        
        return false;
    }
    
    private bool ReplaceInColumnSet(ColumnSet columnSet, string elementId, Element newElement)
    {
        for (int i = 0; i < columnSet.Columns.Count; i++)
        {
            var column = columnSet.Columns[i];
            if (column.ElementId == elementId)
            {
                if (newElement is Column newColumn)
                {
                    columnSet.Columns[i] = newColumn;
                    return true;
                }
                return false;
            }
            
            if (column.Elements.Count > 0)
            {
                if (ReplaceElementRecursive(column.Elements, elementId, newElement))
                    return true;
            }
        }
        
        return false;
    }
    
    /// <summary>
    /// 构建并序列化为 JSON 字符串（紧凑格式）
    /// </summary>
    /// <returns>紧凑格式的 JSON 字符串</returns>
    /// <example>
    /// <code>
    /// var json = CardBuilder.Create()
    ///     .Header(h => h.Title("标题"))
    ///     .ToJson();
    /// </code>
    /// </example>
    public string ToJson()
    {
        ApplyTemplateParameters();
        return _card.ToJson();
    }
    
    /// <summary>
    /// 构建并序列化为 JSON 字符串（可指定格式）
    /// </summary>
    /// <param name="indented">
    /// 是否格式化输出
    /// <list type="bullet">
    ///   <item><description><c>true</c> - 输出带缩进和换行的格式化 JSON</description></item>
    ///   <item><description><c>false</c> - 输出紧凑格式的 JSON（默认）</description></item>
    /// </list>
    /// </param>
    /// <returns>JSON 字符串</returns>
    /// <example>
    /// <code>
    /// // 紧凑格式
    /// var json = card.ToJson();
    /// 
    /// // 格式化格式
    /// var jsonIndented = card.ToJson(indented: true);
    /// </code>
    /// </example>
    public string ToJson(bool indented)
    {
        ApplyTemplateParameters();
        return _card.ToJson(indented);
    }
    
    /// <summary>
    /// 创建卡片构建器实例
    /// </summary>
    /// <returns>新的 <see cref="CardBuilder"/> 实例</returns>
    /// <example>
    /// <code>
    /// var builder = CardBuilder.Create();
    /// </code>
    /// </example>
    public static CardBuilder Create() => new();
}

/// <summary>
/// 卡片配置构建器
/// 用于构建卡片的配置对象
/// </summary>
public class CardConfigBuilder
{
    private readonly CardConfig _config = new();

    public CardConfigBuilder UpdateMulti(bool value)
    {
        _config.UpdateMulti = value;
        return this;
    }

    public CardConfigBuilder StreamingMode(bool value)
    {
        _config.StreamingMode = value;
        return this;
    }

    public CardConfigBuilder StreamingConfig(Action<StreamingConfigBuilder> configure)
    {
        var builder = new StreamingConfigBuilder();
        configure(builder);
        _config.StreamingConfig = builder.Build();
        return this;
    }

    public CardConfigBuilder Summary(string content)
    {
        _config.Summary = new StreamingSummary { Content = content };
        return this;
    }

    public CardConfigBuilder Summary(string content, Dictionary<string, string> i18nContent)
    {
        _config.Summary = new StreamingSummary { Content = content, I18nContent = i18nContent };
        return this;
    }

    public CardConfigBuilder Locales(params string[] locales)
    {
        _config.Locales = locales;
        return this;
    }

    public CardConfigBuilder EnableForward(bool value)
    {
        _config.EnableForward = value;
        return this;
    }

    public CardConfigBuilder WidthMode(string mode)
    {
        _config.WidthMode = mode;
        return this;
    }

    public CardConfigBuilder UseCustomTranslation(bool value)
    {
        _config.UseCustomTranslation = value;
        return this;
    }

    public CardConfigBuilder EnableForwardInteraction(bool value)
    {
        _config.EnableForwardInteraction = value;
        return this;
    }

    public CardConfigBuilder Style(Action<CardStyleBuilder> configure)
    {
        var builder = new CardStyleBuilder();
        configure(builder);
        _config.Style = builder.Build();
        return this;
    }

    public CardConfig Build() => _config;
}

public class StreamingConfigBuilder
{
    private readonly StreamingConfig _config = new();

    public StreamingConfigBuilder PrintFrequencyMs(object value)
    {
        _config.PrintFrequencyMs = value;
        return this;
    }

    public StreamingConfigBuilder PrintStep(object value)
    {
        _config.PrintStep = value;
        return this;
    }

    public StreamingConfigBuilder PrintStrategy(string strategy)
    {
        _config.PrintStrategy = strategy;
        return this;
    }

    public StreamingConfig Build() => _config;
}

public class CardStyleBuilder
{
    private readonly CardStyle _style = new();

    public CardStyleBuilder TextSize(object value)
    {
        _style.TextSize = value;
        return this;
    }

    public CardStyleBuilder Color(object value)
    {
        _style.Color = value;
        return this;
    }

    public CardStyle Build() => _style;
}

/// <summary>
/// 卡片头部构建器
/// 用于构建卡片的头部（标题和图标）
/// </summary>
public class CardHeaderBuilder
{
    private readonly CardHeader _header = new();
    
    /// <summary>
    /// 设置卡片标题
    /// </summary>
    /// <param name="title">标题文本内容</param>
    /// <returns>当前头部构建器实例（支持链式调用）</returns>
    /// <example>
    /// <code>
    /// var header = new CardHeaderBuilder()
    ///     .Title("欢迎使用飞书卡片")
    ///     .Build();
    /// </code>
    /// </example>
    public CardHeaderBuilder Title(string title)
    {
        _header.Title = new Models.Elements.PlainText { Content = title };
        return this;
    }
    
    /// <summary>
    /// 设置卡片副标题
    /// </summary>
    /// <param name="subtitle">副标题文本内容</param>
    /// <returns>当前头部构建器实例（支持链式调用）</returns>
    public CardHeaderBuilder Subtitle(string subtitle)
    {
        _header.Subtitle = new Models.Elements.PlainText { Content = subtitle };
        return this;
    }
    
    /// <summary>
    /// 设置标题主题样式颜色
    /// </summary>
    /// <param name="template">
    /// 主题颜色枚举值
    /// <para>可选值：blue, wathet, turquoise, green, yellow, orange, red, carmine, violet, purple, indigo, grey, default</para>
    /// </param>
    /// <returns>当前头部构建器实例（支持链式调用）</returns>
    public CardHeaderBuilder Template(string template)
    {
        _header.Template = template;
        return this;
    }
    
    /// <summary>
    /// 添加标题后缀标签
    /// </summary>
    /// <param name="configure">后缀标签构建器配置</param>
    /// <returns>当前头部构建器实例（支持链式调用）</returns>
    /// <remarks>
    /// 最多可添加 3 个标签，超出不展示。
    /// </remarks>
    /// <example>
    /// <code>
    /// var card = CardBuilder.Create()
    ///     .Header(h => h
    ///         .Title("任务状态")
    ///         .TextTag(t => t.Text("进行中").Color("blue")))
    ///     .Build();
    /// </code>
    /// </example>
    public CardHeaderBuilder TextTag(Action<TextTagBuilder> configure)
    {
        var builder = new TextTagBuilder();
        configure(builder);
        _header.TextTagList ??= new List<Models.Elements.TextTag>();
        _header.TextTagList.Add(builder.Build());
        return this;
    }
    
    /// <summary>
    /// 设置头部内边距
    /// </summary>
    /// <param name="padding">
    /// 内边距值，支持范围 [0,99]px
    /// <para>示例："4px", "8px 12px", "4px 8px 4px 8px"</para>
    /// </param>
    /// <returns>当前头部构建器实例（支持链式调用）</returns>
    public CardHeaderBuilder Padding(string padding)
    {
        _header.Padding = padding;
        return this;
    }
    
    public CardHeaderBuilder Icon(string token, string? color = null)
    {
        _header.Icon = new Models.HeaderIcon
        {
            Tag = "standard_icon",
            Token = token,
            Color = color
        };
        return this;
    }
    
    public CardHeaderBuilder CustomIcon(string imgKey)
    {
        _header.Icon = new Models.HeaderIcon
        {
            Tag = "custom_icon",
            ImgKey = imgKey
        };
        return this;
    }
    
    public CardHeader Build() => _header;
}

/// <summary>
/// 卡片主体构建器
/// 用于构建卡片的主体内容（包含所有元素）
/// </summary>
public class CardBodyBuilder
{
    private readonly CardBody _body;
    
    /// <summary>
    /// 初始化卡片主体构建器
    /// </summary>
    /// <param name="body">卡片主体对象</param>
    public CardBodyBuilder(CardBody body)
    {
        _body = body;
    }
    
    /// <summary>
    /// 设置垂直间距
    /// </summary>
    /// <param name="spacing">
    /// 间距值，支持预设值或自定义像素值
    /// <para>预设值："small"(4px), "medium"(8px), "large"(12px), "extra_large"(16px)</para>
    /// <para>自定义值："4px", "12px" 等，支持范围 [0,99]px</para>
    /// </param>
    /// <returns>当前主体构建器实例（支持链式调用）</returns>
    public CardBodyBuilder VerticalSpacing(string spacing)
    {
        _body.VerticalSpacing = spacing;
        return this;
    }
    
    /// <summary>
    /// 设置水平间距
    /// </summary>
    /// <param name="spacing">
    /// 间距值，支持预设值或自定义像素值
    /// <para>预设值："small"(4px), "medium"(8px), "large"(12px), "extra_large"(16px)</para>
    /// <para>自定义值："4px", "12px" 等，支持范围 [0,99]px</para>
    /// </param>
    /// <returns>当前主体构建器实例（支持链式调用）</returns>
    public CardBodyBuilder HorizontalSpacing(string spacing)
    {
        _body.HorizontalSpacing = spacing;
        return this;
    }
    
    /// <summary>
    /// 设置内边距
    /// </summary>
    /// <param name="padding">
    /// 内边距值，支持范围 [0,99]px
    /// <para>示例："4px", "8px 12px", "4px 8px 4px 8px"</para>
    /// </param>
    /// <returns>当前主体构建器实例（支持链式调用）</returns>
    public CardBodyBuilder Padding(string padding)
    {
        _body.Padding = padding;
        return this;
    }
    
    /// <summary>
    /// 添加元素到卡片主体
    /// </summary>
    /// <param name="element">要添加的卡片元素</param>
    /// <returns>当前主体构建器实例（支持链式调用）</returns>
    /// <example>
    /// <code>
    /// var body = new CardBodyBuilder(body)
    ///     .AddElement(new PlainText { Content = "文本" })
    ///     .AddElement(new Button { Text = new PlainText { Content = "按钮" } });
    /// </code>
    /// </example>
    public CardBodyBuilder AddElement(Models.Elements.Element element)
    {
        _body.Elements.Add(element);
        return this;
    }
    
    /// <summary>
    /// 添加容器元素
    /// </summary>
    /// <param name="configure">容器构建器的配置动作</param>
    /// <returns>当前主体构建器实例（支持链式调用）</returns>
    /// <example>
    /// <code>
    /// var card = CardBuilder.Create()
    ///     .Body(b => b
    ///         .Div(d => d
    ///             .Vertical()
    ///             .PlainText("内容")))
    ///     .Build();
    /// </code>
    /// </example>
    public CardBodyBuilder Div(Action<DivBuilder> configure)
    {
        var builder = new DivBuilder();
        configure(builder);
        _body.Elements.Add(builder.Build());
        return this;
    }
    
    /// <summary>
    /// 添加按钮元素
    /// </summary>
    /// <param name="configure">按钮构建器的配置动作</param>
    /// <returns>当前主体构建器实例（支持链式调用）</returns>
    /// <example>
    /// <code>
    /// var card = CardBuilder.Create()
    ///     .Body(b => b
    ///         .Button(btn => btn
    ///             .Text("点击")
    ///             .Type(ButtonType.Primary)))
    ///     .Build();
    /// </code>
    /// </example>
    public CardBodyBuilder Button(Action<ButtonBuilder> configure)
    {
        var builder = new ButtonBuilder();
        configure(builder);
        _body.Elements.Add(builder.Build());
        return this;
    }
    
    /// <summary>
    /// 添加输入框元素
    /// </summary>
    /// <param name="configure">输入框构建器的配置动作</param>
    /// <returns>当前主体构建器实例（支持链式调用）</returns>
    /// <example>
    /// <code>
    /// var card = CardBuilder.Create()
    ///     .Body(b => b
    ///         .Input(i => i
    ///             .Name("username")
    ///             .Placeholder("请输入用户名")))
    ///     .Build();
    /// </code>
    /// </example>
    public CardBodyBuilder Input(Action<InputBuilder> configure)
    {
        var builder = new InputBuilder();
        configure(builder);
        _body.Elements.Add(builder.Build());
        return this;
    }
    
    /// <summary>
    /// 添加选择器元素
    /// </summary>
    /// <param name="configure">选择器构建器的配置动作</param>
    /// <returns>当前主体构建器实例（支持链式调用）</returns>
    public CardBodyBuilder Select(Action<SelectBuilder> configure)
    {
        var builder = new SelectBuilder();
        configure(builder);
        _body.Elements.Add(builder.Build());
        return this;
    }
    
    /// <summary>
    /// 添加日期选择器元素
    /// </summary>
    /// <param name="configure">日期选择器构建器的配置动作</param>
    /// <returns>当前主体构建器实例（支持链式调用）</returns>
    public CardBodyBuilder DatePicker(Action<DatePickerBuilder> configure)
    {
        var builder = new DatePickerBuilder();
        configure(builder);
        _body.Elements.Add(builder.Build());
        return this;
    }
    
    /// <summary>
    /// 添加勾选器元素
    /// </summary>
    /// <param name="configure">勾选器构建器的配置动作</param>
    /// <returns>当前主体构建器实例（支持链式调用）</returns>
    public CardBodyBuilder Checkbox(Action<CheckboxBuilder> configure)
    {
        var builder = new CheckboxBuilder();
        configure(builder);
        _body.Elements.Add(builder.Build());
        return this;
    }
    
    /// <summary>
    /// 添加表单容器元素
    /// </summary>
    /// <param name="configure">表单构建器的配置动作</param>
    /// <returns>当前主体构建器实例（支持链式调用）</returns>
    /// <example>
    /// <code>
    /// var card = CardBuilder.Create()
    ///     .Body(b => b
    ///         .Form(f => f
    ///             .Name("myForm")
    ///             .Input(i => i.Name("user").Required())))
    ///     .Build();
    /// </code>
    /// </example>
    public CardBodyBuilder Form(Action<FormBuilder> configure)
    {
        var builder = new FormBuilder();
        configure(builder);
        _body.Elements.Add(builder.Build());
        return this;
    }
    
    /// <summary>
    /// 添加分栏容器元素
    /// </summary>
    /// <param name="configure">分栏构建器的配置动作</param>
    /// <returns>当前主体构建器实例（支持链式调用）</returns>
    public CardBodyBuilder ColumnSet(Action<ColumnSetBuilder> configure)
    {
        var builder = new ColumnSetBuilder();
        configure(builder);
        _body.Elements.Add(builder.Build());
        return this;
    }
    
    /// <summary>
    /// 添加图片元素
    /// </summary>
    /// <param name="imgKey">图片的 img_key</param>
    /// <param name="configure">图片构建器的配置动作（可选）</param>
    /// <returns>当前主体构建器实例（支持链式调用）</returns>
    /// <example>
    /// <code>
    /// var card = CardBuilder.Create()
    ///     .Body(b => b
    ///         .Image("img_xxx", i => i
    ///             .Size(ImageSize.CropCenter)
    ///             .Width("200px")))
    ///     .Build();
    /// </code>
    /// </example>
    public CardBodyBuilder Image(string imgKey, Action<ImageBuilder>? configure = null)
    {
        var builder = new ImageBuilder(imgKey);
        configure?.Invoke(builder);
        _body.Elements.Add(builder.Build());
        return this;
    }
    
    /// <summary>
    /// 添加纯文本元素
    /// </summary>
    /// <param name="content">文本内容</param>
    /// <returns>当前主体构建器实例（支持链式调用）</returns>
    /// <example>
    /// <code>
    /// var card = CardBuilder.Create()
    ///     .Body(b => b
    ///         .PlainText("这是文本内容"))
    ///     .Build();
    /// </code>
    /// </example>
    public CardBodyBuilder PlainText(string content)
    {
        _body.Elements.Add(new Models.Elements.Div
        {
            Text = new Models.Elements.PlainText { Content = content }
        });
        return this;
    }
    
    public CardBodyBuilder PlainText(string content, Action<PlainTextBuilder> configure)
    {
        var builder = new PlainTextBuilder().Content(content);
        configure(builder);
        _body.Elements.Add(new Models.Elements.Div
        {
            Text = builder.Build()
        });
        return this;
    }
    
    /// <summary>
    /// 添加 Markdown 元素
    /// </summary>
    /// <param name="content">Markdown 内容</param>
    /// <returns>当前主体构建器实例（支持链式调用）</returns>
    /// <example>
    /// <code>
    /// var card = CardBuilder.Create()
    ///     .Body(b => b
    ///         .Markdown("**粗体文本**\n- 列表项 1\n- 列表项 2"))
    ///     .Build();
    /// </code>
    /// </example>
    public CardBodyBuilder Markdown(string content)
    {
        _body.Elements.Add(new Models.Elements.Div
        {
            Text = new Models.Elements.Markdown { Content = content }
        });
        return this;
    }
    
    public CardBodyBuilder Markdown(string content, Action<MarkdownBuilder> configure)
    {
        var builder = new MarkdownBuilder().Content(content);
        configure(builder);
        _body.Elements.Add(new Models.Elements.Div
        {
            Text = builder.Build()
        });
        return this;
    }
    
    /// <summary>
    /// 添加分割线元素
    /// </summary>
    /// <returns>当前主体构建器实例（支持链式调用）</returns>
    /// <example>
    /// <code>
    /// var card = CardBuilder.Create()
    ///     .Body(b => b
    ///         .PlainText("内容上方")
    ///         .Hr()
    ///         .PlainText("内容下方"))
    ///     .Build();
    /// </code>
    /// </example>
    public CardBodyBuilder Hr()
    {
        _body.Elements.Add(new Models.Elements.Hr());
        return this;
    }
    
    /// <summary>
    /// 添加分割线元素（带配置）
    /// </summary>
    /// <param name="configure">分割线构建器的配置动作</param>
    /// <returns>当前主体构建器实例（支持链式调用）</returns>
    /// <example>
    /// <code>
    /// var card = CardBuilder.Create()
    ///     .Body(b => b
    ///         .PlainText("内容上方")
    ///         .Hr(hr => hr.Margin("8px 0"))
    ///         .PlainText("内容下方"))
    ///     .Build();
    /// </code>
    /// </example>
    public CardBodyBuilder Hr(Action<HrBuilder> configure)
    {
        var builder = new HrBuilder();
        configure(builder);
        _body.Elements.Add(builder.Build());
        return this;
    }
    
    /// <summary>
    /// 添加时间选择器元素
    /// </summary>
    /// <param name="configure">时间选择器构建器的配置动作</param>
    /// <returns>当前主体构建器实例（支持链式调用）</returns>
    /// <example>
    /// <code>
    /// var card = CardBuilder.Create()
    ///     .Body(b => b
    ///         .PickerTime(pt => pt
    ///             .Name("meetingTime")
    ///             .Placeholder("请选择时间")
    ///             .InitialTime("09:00")))
    ///     .Build();
    /// </code>
    /// </example>
    public CardBodyBuilder PickerTime(Action<PickerTimeBuilder> configure)
    {
        var builder = new PickerTimeBuilder();
        configure(builder);
        _body.Elements.Add(builder.Build());
        return this;
    }
    
    public CardBodyBuilder PickerDatetime(Action<PickerDatetimeBuilder> configure)
    {
        var builder = new PickerDatetimeBuilder();
        configure(builder);
        _body.Elements.Add(builder.Build());
        return this;
    }
    
    public CardBodyBuilder Person(Action<PersonBuilder> configure)
    {
        var builder = new PersonBuilder();
        configure(builder);
        _body.Elements.Add(builder.Build());
        return this;
    }
    
    public CardBodyBuilder PersonList(Action<PersonListBuilder> configure)
    {
        var builder = new PersonListBuilder();
        configure(builder);
        _body.Elements.Add(builder.Build());
        return this;
    }
    
    public CardBodyBuilder SelectPerson(Action<SelectPersonBuilder> configure)
    {
        var builder = new SelectPersonBuilder();
        configure(builder);
        _body.Elements.Add(builder.Build());
        return this;
    }
    
    public CardBodyBuilder MultiSelectPerson(Action<MultiSelectPersonBuilder> configure)
    {
        var builder = new MultiSelectPersonBuilder();
        configure(builder);
        _body.Elements.Add(builder.Build());
        return this;
    }
    
    public CardBodyBuilder Overflow(Action<OverflowBuilder> configure)
    {
        var builder = new OverflowBuilder();
        configure(builder);
        _body.Elements.Add(builder.Build());
        return this;
    }
    
    public CardBodyBuilder Checker(Action<CheckerBuilder> configure)
    {
        var builder = new CheckerBuilder();
        configure(builder);
        _body.Elements.Add(builder.Build());
        return this;
    }
    
    public CardBodyBuilder ImgCombination(Action<ImgCombinationBuilder> configure)
    {
        var builder = new ImgCombinationBuilder();
        configure(builder);
        _body.Elements.Add(builder.Build());
        return this;
    }
    
    /// <summary>
    /// 添加表格元素
    /// </summary>
    /// <param name="configure">表格构建器的配置动作</param>
    /// <returns>当前主体构建器实例（支持链式调用）</returns>
    /// <remarks>
    /// 注意事项：
    /// <list type="bullet">
    ///   <item><description>单张卡片最多支持放置五个表格组件</description></item>
    ///   <item><description>表格组件不可被内嵌在其它组件内，只可放在卡片根节点下</description></item>
    /// </list>
    /// </remarks>
    /// <example>
    /// <code>
    /// var card = CardBuilder.Create()
    ///     .Header(h => h.Title("客户数据表"))
    ///     .Body(b => b
    ///         .Table(table => table
    ///             .PageSize(5)
    ///             .Column(col => col.Name("name").DisplayName("客户名称").DataType("text"))
    ///             .Column(col => col.Name("amount").DisplayName("金额").DataType("number"))
    ///             .Row(row => row.TextCell("name", "飞书科技").NumberCell("amount", 168))))
    ///     .Build();
    /// </code>
    /// </example>
    public CardBodyBuilder Table(Action<TableBuilder> configure)
    {
        var builder = new TableBuilder();
        configure(builder);
        _body.Elements.Add(builder.Build());
        return this;
    }

    public CardBodyBuilder InteractiveContainer(Action<InteractiveContainerBuilder> configure)
    {
        var builder = new InteractiveContainerBuilder();
        configure(builder);
        _body.Elements.Add(builder.Build());
        return this;
    }
}

public class FallbackBuilder
{
    private readonly Fallback _fallback = new();

    public FallbackBuilder Title(string title)
    {
        _fallback.Title = new PlainText { Content = title };
        return this;
    }

    public FallbackBuilder Content(string content)
    {
        _fallback.Content = new PlainText { Content = content };
        return this;
    }

    public Fallback Build() => _fallback;
}
