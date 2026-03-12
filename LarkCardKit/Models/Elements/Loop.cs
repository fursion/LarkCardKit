using System.Text.Json.Serialization;

namespace LarkCardKit.Models.Elements;

/// <summary>
/// 循环容器组件
/// </summary>
public class Loop : Element
{
    /// <inheritdoc/>
    public override string Tag => "loop";
    
    /// <summary>
    /// 数据源
    /// </summary>
    [JsonPropertyName("data_source")]
    public LoopDataSource? DataSource { get; set; }
    
    /// <summary>
    /// 循环模板
    /// </summary>
    [JsonPropertyName("template")]
    public Element? Template { get; set; }
}

/// <summary>
/// 循环容器数据源
/// </summary>
public class LoopDataSource
{
    /// <summary>
    /// 列表数据
    /// </summary>
    [JsonPropertyName("list")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<object>? List { get; set; }
    
    /// <summary>
    /// 其他自定义属性
    /// </summary>
    [JsonExtensionData]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Dictionary<string, object>? AdditionalData { get; set; }
}
