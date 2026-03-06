using LarkCardKit.Models;
using LarkCardKit.Config;

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
    
    /// <summary>
    /// 配置卡片主体
    /// </summary>
    /// <param name="configure">卡片主体构建器的配置动作</param>
    /// <returns>当前卡片构建器实例（支持链式调用）</returns>
    /// <example>
    /// <code>
    /// var card = CardBuilder.Create()
    ///     .Body(b => b
    ///         .PlainText("内容")
    ///         .Div(d => d
    ///             .Vertical()
    ///             .PlainText("子内容")))
    ///     .Build();
    /// </code>
    /// </example>
    public CardBuilder Body(Action<CardBodyBuilder> configure)
    {
        var builder = new CardBodyBuilder(_card.Body);
        configure(builder);
        return this;
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
    public Card Build() => _card;
    
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
    public string ToJson() => _card.ToJson();
    
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
    public string ToJson(bool indented) => _card.ToJson(indented);
    
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
    
    /// <summary>
    /// 设置是否为共享卡片
    /// </summary>
    /// <param name="value">
    /// 共享卡片设置
    /// <list type="bullet">
    ///   <item><description><c>true</c> - 共享卡片（更新对所有人可见）</description></item>
    ///   <item><description><c>false</c> - 独享卡片（更新仅对自己可见）</description></item>
    /// </list>
    /// </param>
    /// <returns>当前配置构建器实例（支持链式调用）</returns>
    /// <remarks>
    /// 飞书卡片 2.0 规范中，默认值为 <c>true</c>，且暂时仅支持共享卡片。
    /// </remarks>
    public CardConfigBuilder UpdateMulti(bool value)
    {
        _config.UpdateMulti = value;
        return this;
    }
    
    /// <summary>
    /// 设置是否启用流式更新模式
    /// </summary>
    /// <param name="value">是否启用流式更新</param>
    /// <returns>当前配置构建器实例（支持链式调用）</returns>
    /// <remarks>
    /// 流式更新模式用于支持 AI 生成等场景的文本流式输出。
    /// </remarks>
    public CardConfigBuilder StreamingMode(bool value)
    {
        _config.StreamingMode = value;
        return this;
    }
    
    /// <summary>
    /// 构建卡片配置对象
    /// </summary>
    /// <returns>完整的 <see cref="CardConfig"/> 对象</returns>
    public CardConfig Build() => _config;
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
    
    /// <summary>
    /// 构建卡片头部对象
    /// </summary>
    /// <returns>完整的 <see cref="CardHeader"/> 对象</returns>
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
        _body.Elements.Add(new Models.Elements.PlainText { Content = content });
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
        _body.Elements.Add(new Models.Elements.Markdown { Content = content });
        return this;
    }
}
