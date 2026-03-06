using System.Text.Json.Serialization;

namespace LarkCardKit.Models.Elements;

/// <summary>
/// 卡片元素基类
/// 所有卡片组件的抽象基类
/// </summary>
public abstract class Element
{
    /// <summary>
    /// 元素标签（组件类型）
    /// </summary>
    [JsonPropertyName("tag")]
    public abstract string Tag { get; }
    
    /// <summary>
    /// 元素唯一标识，用于操作组件
    /// </summary>
    [JsonPropertyName("element_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? ElementId { get; set; }
    
    /// <summary>
    /// 外边距，支持范围 [-99,99]px
    /// </summary>
    [JsonPropertyName("margin")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Margin { get; set; }
}
