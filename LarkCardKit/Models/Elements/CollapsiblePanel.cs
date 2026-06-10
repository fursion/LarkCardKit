using System.Text.Json.Serialization;
using LarkCardKit.Models.Elements;

namespace LarkCardKit.Models.Elements;

/// <summary>
/// 折叠面板组件
/// </summary>
public class CollapsiblePanel : Element
{
    /// <inheritdoc/>
    public override string Tag => "collapsible_panel";

    /// <summary>
    /// 元素唯一标识
    /// </summary>
    [JsonPropertyName("element_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public new string? ElementId { get; set; }

    /// <summary>
    /// 是否展开
    /// </summary>
    [JsonPropertyName("expanded")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? Expanded { get; set; }

    /// <summary>
    /// 外边距
    /// </summary>
    [JsonPropertyName("margin")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public new string? Margin { get; set; }

    /// <summary>
    /// 内边距
    /// </summary>
    [JsonPropertyName("padding")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Padding { get; set; }

    /// <summary>
    /// 面板头部配置
    /// </summary>
    [JsonPropertyName("header")]
    public CollapsiblePanelHeader? Header { get; set; }

    /// <summary>
    /// 子元素列表
    /// </summary>
    [JsonPropertyName("elements")]
    public List<Element> Elements { get; set; } = new();
}

/// <summary>
/// 折叠面板头部配置
/// </summary>
public class CollapsiblePanelHeader
{
    /// <summary>
    /// 面板标题（支持 plain_text 和 lark_md 两种模式）
    /// </summary>
    [JsonPropertyName("title")]
    public Element? Title { get; set; }
    
    /// <summary>
    /// 头部图标
    /// </summary>
    [JsonPropertyName("icon")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public HeaderIcon? Icon { get; set; }
    
    /// <summary>
    /// 展开时的图标
    /// </summary>
    [JsonPropertyName("expanded_icon")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public HeaderIcon? ExpandedIcon { get; set; }
}

/// <summary>
/// 头部图标配置
/// </summary>
public class HeaderIcon
{
    /// <summary>
    /// 图标类型：standard_icon 或 custom_icon
    /// </summary>
    [JsonPropertyName("tag")]
    public string Tag { get; set; } = string.Empty;
    
    /// <summary>
    /// 标准图标 token
    /// </summary>
    [JsonPropertyName("token")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Token { get; set; }
    
    /// <summary>
    /// 图标颜色
    /// </summary>
    [JsonPropertyName("color")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Color { get; set; }
    
    /// <summary>
    /// 自定义图片 key
    /// </summary>
    [JsonPropertyName("img_key")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? ImgKey { get; set; }
}
