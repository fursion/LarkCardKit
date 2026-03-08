using LarkCardKit.Models;
using LarkCardKit.Models.Behaviors;
using LarkCardKit.Models.Elements;
using LarkCardKit.Templates;

namespace LarkCardKit.Builders;

/// <summary>
/// 选择器构建器，用于构建下拉选择组件
/// </summary>
/// <remarks>
/// 选择器支持单选和多选模式，可通过多种方式配置选项：
/// <list type="bullet">
///   <item><description>使用 <see cref="AddOption(string, string)"/> 逐个添加选项</description></item>
///   <item><description>使用 <see cref="AddOption(SelectOptionInfo)"/> 添加选项对象</description></item>
///   <item><description>使用 <see cref="AddOptions"/> 批量添加选项</description></item>
///   <item><description>使用 <see cref="Options{T}(IEnumerable{T}, Func{T, SelectOptionInfo})"/> 从泛型集合转换</description></item>
///   <item><description>使用 <see cref="OptionsFrom"/> 从模板参数获取选项</description></item>
/// </list>
/// </remarks>
/// <example>
/// 以下示例演示如何使用 SelectBuilder：
/// <code>
/// var select = new SelectBuilder()
///     .Name("city")
///     .Placeholder("请选择城市")
///     .AddOption("beijing", "北京")
///     .AddOption("shanghai", "上海")
///     .Build();
/// </code>
/// </example>
public class SelectBuilder
{
    private readonly Select _select = new();
    
    /// <summary>
    /// 设置选择器的唯一标识名称（表单容器中必填）
    /// </summary>
    /// <param name="name">选择器名称</param>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    public SelectBuilder Name(string name)
    {
        _select.Name = name;
        return this;
    }
    
    /// <summary>
    /// 设置是否必填
    /// </summary>
    /// <param name="required">是否必填，默认为 true</param>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    public SelectBuilder Required(bool required = true)
    {
        _select.Required = required;
        return this;
    }
    
    /// <summary>
    /// 设置是否为多选模式
    /// </summary>
    /// <param name="multi">是否多选，默认为 true</param>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    public SelectBuilder MultiSelect(bool multi = true)
    {
        _select.MultiSelect = multi;
        return this;
    }
    
    /// <summary>
    /// 设置占位提示文本
    /// </summary>
    /// <param name="placeholder">占位文本内容</param>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    public SelectBuilder Placeholder(string placeholder)
    {
        _select.Placeholder = new PlainText { Content = placeholder };
        return this;
    }
    
    /// <summary>
    /// 设置初始选中项
    /// </summary>
    /// <param name="option">初始选中项的值</param>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    public SelectBuilder InitialOption(string option)
    {
        _select.InitialOption = option;
        return this;
    }
    
    /// <summary>
    /// 设置选择器宽度
    /// </summary>
    /// <param name="width">
    /// 宽度值，支持预设值或自定义值
    /// <para>预设值："default", "fill"</para>
    /// <para>自定义值："100px", "200px" 等</para>
    /// </param>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    public SelectBuilder Width(string width)
    {
        _select.Width = width;
        return this;
    }
    
    /// <summary>
    /// 设置是否禁用
    /// </summary>
    /// <param name="disabled">是否禁用，默认为 true</param>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    public SelectBuilder Disabled(bool disabled = true)
    {
        _select.Disabled = disabled;
        return this;
    }
    
    /// <summary>
    /// 设置元素唯一标识
    /// </summary>
    /// <param name="id">元素 ID</param>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    public SelectBuilder ElementId(string id)
    {
        _select.ElementId = id;
        return this;
    }
    
    /// <summary>
    /// 设置外边距
    /// </summary>
    /// <param name="margin">
    /// 外边距值，支持范围 [0,99]px
    /// <para>示例："4px", "8px 12px", "4px 8px 4px 8px"</para>
    /// </param>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    public SelectBuilder Margin(string margin)
    {
        _select.Margin = margin;
        return this;
    }
    
    /// <summary>
    /// 添加单个选项
    /// </summary>
    /// <param name="value">选项值</param>
    /// <param name="text">选项显示文本</param>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    /// <example>
    /// <code>
    /// select.AddOption("beijing", "北京");
    /// </code>
    /// </example>
    public SelectBuilder AddOption(string value, string text)
    {
        _select.Options ??= new List<SelectOption>();
        _select.Options.Add(new SelectOption
        {
            Value = value,
            Text = new PlainText { Content = text }
        });
        return this;
    }
    
    /// <summary>
    /// 从模板参数键获取选项列表
    /// </summary>
    /// <param name="templateKey">模板参数键名</param>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    /// <remarks>
    /// 使用此方法时，选项列表将在调用 <see cref="CardBuilder.SetParameter"/> 时动态填充
    /// </remarks>
    public SelectBuilder OptionsFrom(string templateKey)
    {
        _select.OptionsTemplateKey = templateKey;
        return this;
    }
    
    /// <summary>
    /// 添加单个选项（使用 SelectOptionInfo 对象）
    /// </summary>
    /// <param name="option">选项信息对象</param>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    /// <example>
    /// <code>
    /// select.AddOption(new SelectOptionInfo("beijing", "北京"));
    /// // 或使用元组隐式转换
    /// SelectOptionInfo option = ("shanghai", "上海");
    /// select.AddOption(option);
    /// </code>
    /// </example>
    public SelectBuilder AddOption(SelectOptionInfo option)
    {
        _select.Options ??= new List<SelectOption>();
        _select.Options.Add(new SelectOption
        {
            Value = option.Value,
            Text = new PlainText { Content = option.Text }
        });
        return this;
    }
    
    /// <summary>
    /// 批量添加选项
    /// </summary>
    /// <param name="options">选项信息集合</param>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    /// <example>
    /// <code>
    /// var options = new List&lt;SelectOptionInfo&gt;
    /// {
    ///     new SelectOptionInfo("beijing", "北京"),
    ///     new SelectOptionInfo("shanghai", "上海")
    /// };
    /// select.AddOptions(options);
    /// </code>
    /// </example>
    public SelectBuilder AddOptions(IEnumerable<SelectOptionInfo> options)
    {
        _select.Options ??= new List<SelectOption>();
        foreach (var option in options)
        {
            _select.Options.Add(new SelectOption
            {
                Value = option.Value,
                Text = new PlainText { Content = option.Text }
            });
        }
        return this;
    }
    
    /// <summary>
    /// 从泛型集合转换并添加选项
    /// </summary>
    /// <typeparam name="T">集合元素类型</typeparam>
    /// <param name="items">数据源集合</param>
    /// <param name="selector">选择器函数，将元素转换为 SelectOptionInfo</param>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    /// <example>
    /// <code>
    /// var users = new List&lt;User&gt; { new User { Id = "1", Name = "张三" } };
    /// select.Options(users, u => new SelectOptionInfo(u.Id, u.Name));
    /// </code>
    /// </example>
    public SelectBuilder Options<T>(IEnumerable<T> items, Func<T, SelectOptionInfo> selector)
    {
        _select.Options ??= new List<SelectOption>();
        foreach (var item in items)
        {
            var option = selector(item);
            _select.Options.Add(new SelectOption
            {
                Value = option.Value,
                Text = new PlainText { Content = option.Text }
            });
        }
        return this;
    }
    
    /// <summary>
    /// 从泛型集合转换并添加选项（使用独立的选择器函数）
    /// </summary>
    /// <typeparam name="T">集合元素类型</typeparam>
    /// <param name="items">数据源集合</param>
    /// <param name="valueSelector">值选择器函数</param>
    /// <param name="textSelector">文本选择器函数</param>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    /// <example>
    /// <code>
    /// var users = new List&lt;User&gt; { new User { Id = "1", Name = "张三" } };
    /// select.Options(users, u => u.Id, u => u.Name);
    /// </code>
    /// </example>
    public SelectBuilder Options<T>(IEnumerable<T> items, Func<T, string> valueSelector, Func<T, string> textSelector)
    {
        _select.Options ??= new List<SelectOption>();
        foreach (var item in items)
        {
            _select.Options.Add(new SelectOption
            {
                Value = valueSelector(item),
                Text = new PlainText { Content = textSelector(item) }
            });
        }
        return this;
    }
    
    /// <summary>
    /// 设置选择变更时的回调数据
    /// </summary>
    /// <param name="callbackData">回调数据对象</param>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    public SelectBuilder OnChange(object callbackData)
    {
        _select.Behaviors ??= new List<object>();
        _select.Behaviors.Add(new CallbackBehavior { Value = callbackData });
        return this;
    }
    
    /// <summary>
    /// 构建选择器对象
    /// </summary>
    /// <returns>完整的 <see cref="Select"/> 对象</returns>
    public Select Build() => _select;
}

/// <summary>
/// 日期选择器构建器，用于构建日期选择组件
/// </summary>
/// <example>
/// 以下示例演示如何使用 DatePickerBuilder：
/// <code>
/// var datePicker = new DatePickerBuilder()
///     .Name("birthday")
///     .Placeholder("请选择日期")
///     .InitialDate("2024-01-01")
///     .Build();
/// </code>
/// </example>
public class DatePickerBuilder
{
    private readonly DatePicker _datePicker = new();
    
    /// <summary>
    /// 设置日期选择器的唯一标识名称（表单容器中必填）
    /// </summary>
    /// <param name="name">日期选择器名称</param>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    public DatePickerBuilder Name(string name)
    {
        _datePicker.Name = name;
        return this;
    }
    
    /// <summary>
    /// 设置是否必填
    /// </summary>
    /// <param name="required">是否必填，默认为 true</param>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    public DatePickerBuilder Required(bool required = true)
    {
        _datePicker.Required = required;
        return this;
    }
    
    /// <summary>
    /// 设置占位提示文本
    /// </summary>
    /// <param name="placeholder">占位文本内容</param>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    public DatePickerBuilder Placeholder(string placeholder)
    {
        _datePicker.Placeholder = new PlainText { Content = placeholder };
        return this;
    }
    
    /// <summary>
    /// 设置初始日期
    /// </summary>
    /// <param name="date">初始日期，格式为 yyyy-MM-dd</param>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    public DatePickerBuilder InitialDate(string date)
    {
        _datePicker.InitialDate = date;
        return this;
    }
    
    /// <summary>
    /// 设置日期选择器宽度
    /// </summary>
    /// <param name="width">宽度值，如 "default", "fill", "200px"</param>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    public DatePickerBuilder Width(string width)
    {
        _datePicker.Width = width;
        return this;
    }
    
    /// <summary>
    /// 设置是否禁用
    /// </summary>
    /// <param name="disabled">是否禁用，默认为 true</param>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    public DatePickerBuilder Disabled(bool disabled = true)
    {
        _datePicker.Disabled = disabled;
        return this;
    }
    
    /// <summary>
    /// 设置元素唯一标识
    /// </summary>
    /// <param name="id">元素 ID</param>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    public DatePickerBuilder ElementId(string id)
    {
        _datePicker.ElementId = id;
        return this;
    }
    
    /// <summary>
    /// 设置外边距
    /// </summary>
    /// <param name="margin">外边距值，如 "4px", "8px 12px"</param>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    public DatePickerBuilder Margin(string margin)
    {
        _datePicker.Margin = margin;
        return this;
    }
    
    /// <summary>
    /// 设置二次确认弹窗
    /// </summary>
    /// <param name="title">确认弹窗标题</param>
    /// <param name="text">确认弹窗内容</param>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    public DatePickerBuilder Confirm(string title, string text)
    {
        _datePicker.Confirm = new ConfirmConfig
        {
            Title = new PlainText { Content = title },
            Text = new PlainText { Content = text }
        };
        return this;
    }
    
    /// <summary>
    /// 设置日期变更时的回调数据
    /// </summary>
    /// <param name="callbackData">回调数据对象</param>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    public DatePickerBuilder OnChange(object callbackData)
    {
        _datePicker.Behaviors ??= new List<object>();
        _datePicker.Behaviors.Add(new CallbackBehavior { Value = callbackData });
        return this;
    }
    
    /// <summary>
    /// 构建日期选择器对象
    /// </summary>
    /// <returns>完整的 <see cref="DatePicker"/> 对象</returns>
    public DatePicker Build() => _datePicker;
}

/// <summary>
/// 勾选器构建器，用于构建多选下拉组件
/// </summary>
/// <remarks>
/// 勾选器支持多选模式，可通过多种方式配置选项：
/// <list type="bullet">
///   <item><description>使用 <see cref="AddOption(string, string)"/> 逐个添加选项</description></item>
///   <item><description>使用 <see cref="AddOption(CheckboxOptionInfo)"/> 添加选项对象</description></item>
///   <item><description>使用 <see cref="AddOptions"/> 批量添加选项</description></item>
///   <item><description>使用 <see cref="Options{T}(IEnumerable{T}, Func{T, CheckboxOptionInfo})"/> 从泛型集合转换</description></item>
/// </list>
/// </remarks>
/// <example>
/// 以下示例演示如何使用 CheckboxBuilder：
/// <code>
/// var checkbox = new CheckboxBuilder()
///     .Name("skills")
///     .Placeholder("请选择技能")
///     .AddOption("csharp", "C#")
///     .AddOption("typescript", "TypeScript")
///     .InitialSelected("csharp")
///     .Build();
/// </code>
/// </example>
public class CheckboxBuilder
{
    private readonly Checkbox _checkbox = new();
    
    /// <summary>
    /// 设置勾选器的唯一标识名称（表单容器中必填）
    /// </summary>
    /// <param name="name">勾选器名称</param>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    public CheckboxBuilder Name(string name)
    {
        _checkbox.Name = name;
        return this;
    }
    
    /// <summary>
    /// 设置是否必填
    /// </summary>
    /// <param name="required">是否必填，默认为 true</param>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    public CheckboxBuilder Required(bool required = true)
    {
        _checkbox.Required = required;
        return this;
    }
    
    /// <summary>
    /// 设置元素唯一标识
    /// </summary>
    /// <param name="id">元素 ID</param>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    public CheckboxBuilder ElementId(string id)
    {
        _checkbox.ElementId = id;
        return this;
    }
    
    /// <summary>
    /// 设置外边距
    /// </summary>
    /// <param name="margin">外边距值，如 "4px", "8px 12px"</param>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    public CheckboxBuilder Margin(string margin)
    {
        _checkbox.Margin = margin;
        return this;
    }
    
    /// <summary>
    /// 添加单个选项
    /// </summary>
    /// <param name="value">选项值</param>
    /// <param name="text">选项显示文本</param>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    public CheckboxBuilder AddOption(string value, string text)
    {
        _checkbox.Options ??= new List<CheckboxOption>();
        _checkbox.Options.Add(new CheckboxOption
        {
            Value = value,
            Text = new PlainText { Content = text }
        });
        return this;
    }
    
    /// <summary>
    /// 添加单个选项（使用 CheckboxOptionInfo 对象）
    /// </summary>
    /// <param name="option">选项信息对象</param>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    public CheckboxBuilder AddOption(CheckboxOptionInfo option)
    {
        _checkbox.Options ??= new List<CheckboxOption>();
        _checkbox.Options.Add(new CheckboxOption
        {
            Value = option.Value,
            Text = new PlainText { Content = option.Text }
        });
        return this;
    }
    
    /// <summary>
    /// 批量添加选项
    /// </summary>
    /// <param name="options">选项信息集合</param>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    public CheckboxBuilder AddOptions(IEnumerable<CheckboxOptionInfo> options)
    {
        _checkbox.Options ??= new List<CheckboxOption>();
        foreach (var option in options)
        {
            _checkbox.Options.Add(new CheckboxOption
            {
                Value = option.Value,
                Text = new PlainText { Content = option.Text }
            });
        }
        return this;
    }
    
    /// <summary>
    /// 从泛型集合转换并添加选项
    /// </summary>
    /// <typeparam name="T">集合元素类型</typeparam>
    /// <param name="items">数据源集合</param>
    /// <param name="selector">选择器函数，将元素转换为 CheckboxOptionInfo</param>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    public CheckboxBuilder Options<T>(IEnumerable<T> items, Func<T, CheckboxOptionInfo> selector)
    {
        _checkbox.Options ??= new List<CheckboxOption>();
        foreach (var item in items)
        {
            var option = selector(item);
            _checkbox.Options.Add(new CheckboxOption
            {
                Value = option.Value,
                Text = new PlainText { Content = option.Text }
            });
        }
        return this;
    }
    
    /// <summary>
    /// 从泛型集合转换并添加选项（使用独立的选择器函数）
    /// </summary>
    /// <typeparam name="T">集合元素类型</typeparam>
    /// <param name="items">数据源集合</param>
    /// <param name="valueSelector">值选择器函数</param>
    /// <param name="textSelector">文本选择器函数</param>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    public CheckboxBuilder Options<T>(IEnumerable<T> items, Func<T, string> valueSelector, Func<T, string> textSelector)
    {
        _checkbox.Options ??= new List<CheckboxOption>();
        foreach (var item in items)
        {
            _checkbox.Options.Add(new CheckboxOption
            {
                Value = valueSelector(item),
                Text = new PlainText { Content = textSelector(item) }
            });
        }
        return this;
    }
    
    /// <summary>
    /// 设置初始选中值
    /// </summary>
    /// <param name="values">初始选中的选项值数组</param>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    public CheckboxBuilder InitialSelected(params string[] values)
    {
        _checkbox.SelectedValues = values.ToList();
        return this;
    }
    
    /// <summary>
    /// 设置占位提示文本
    /// </summary>
    /// <param name="placeholder">占位文本内容</param>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    public CheckboxBuilder Placeholder(string placeholder)
    {
        _checkbox.Placeholder = new PlainText { Content = placeholder };
        return this;
    }
    
    /// <summary>
    /// 设置勾选器宽度
    /// </summary>
    /// <param name="width">宽度值，如 "default", "fill", "200px"</param>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    public CheckboxBuilder Width(string width)
    {
        _checkbox.Width = width;
        return this;
    }
    
    /// <summary>
    /// 设置选择变更时的回调数据
    /// </summary>
    /// <param name="callbackData">回调数据对象</param>
    /// <returns>当前构建器实例（支持链式调用）</returns>
    public CheckboxBuilder OnChange(object callbackData)
    {
        _checkbox.Behaviors ??= new List<object>();
        _checkbox.Behaviors.Add(new CallbackBehavior { Value = callbackData });
        return this;
    }
    
    /// <summary>
    /// 构建勾选器对象
    /// </summary>
    /// <returns>完整的 <see cref="Checkbox"/> 对象</returns>
    public Checkbox Build() => _checkbox;
}
