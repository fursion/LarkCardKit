using System.Text.Json.Serialization;
using LarkCardKit.Config;
using LarkCardKit.Models.Elements;

namespace LarkCardKit.Models;

/// <summary>
/// 飞书卡片根模型
/// 表示一张完整的飞书卡片消息，符合飞书卡片 JSON 2.0 规范
/// </summary>
/// <example>
/// 以下示例演示如何创建卡片对象：
/// <code>
/// var card = new Card
/// {
///     Header = new CardHeader { Title = new PlainText { Content = "标题" } },
///     Body = new CardBody
///     {
///         Elements = { new PlainText { Content = "内容" } }
///     }
/// };
/// </code>
/// </example>
public class Card
{
    /// <summary>
    /// 卡片 JSON 结构版本
    /// 固定为 "2.0"，表示使用飞书卡片 2.0 规范
    /// </summary>
    [JsonPropertyName("schema")]
    public string Schema { get; set; } = "2.0";
    
    /// <summary>
    /// 卡片配置
    /// 包含卡片的更新策略、流式更新等配置项
    /// </summary>
    [JsonPropertyName("config")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public CardConfig? Config { get; set; }
    
    /// <summary>
    /// 卡片链接配置
    /// 设置点击卡片时的跳转链接
    /// </summary>
    [JsonPropertyName("card_link")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public object? CardLink { get; set; }
    
    /// <summary>
    /// 卡片头部
    /// 显示卡片的标题和图标，位于卡片顶部
    /// </summary>
    [JsonPropertyName("header")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public CardHeader? Header { get; set; }
    
    /// <summary>
    /// 卡片主体
    /// 包含卡片的所有内容元素（文本、按钮、表单等）
    /// </summary>
    [JsonPropertyName("body")]
    public CardBody Body { get; set; } = new();
    
    /// <summary>
    /// 降级配置
    /// 当客户端不支持卡片 2.0 时的降级显示内容
    /// </summary>
    [JsonPropertyName("fallback")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public object? Fallback { get; set; }
}
