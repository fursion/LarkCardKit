using System.Text.Json.Serialization;
using LarkCardKit.Models.Behaviors;

namespace LarkCardKit.Models.Elements;

/// <summary>
/// 选择器组件
/// </summary>
public class Select : Element
{
    /// <inheritdoc/>
    public override string Tag => "select";
    
    /// <summary>
    /// 选择器唯一标识，表单容器中必填
    /// </summary>
    [JsonPropertyName("name")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Name { get; set; }
    
    /// <summary>
    /// 是否必填
    /// </summary>
    [JsonPropertyName("required")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? Required { get; set; }
    
    /// <summary>
    /// 是否多选
    /// </summary>
    [JsonPropertyName("multi_select")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? MultiSelect { get; set; }
    
    /// <summary>
    /// 占位文本
    /// </summary>
    [JsonPropertyName("placeholder")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public PlainText? Placeholder { get; set; }
    
    /// <summary>
    /// 文本标签
    /// </summary>
    [JsonPropertyName("label")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public PlainText? Label { get; set; }
    
    /// <summary>
    /// 初始选项
    /// </summary>
    [JsonPropertyName("initial_option")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? InitialOption { get; set; }
    
    /// <summary>
    /// 选项列表
    /// </summary>
    [JsonPropertyName("options")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<SelectOption>? Options { get; set; }
    
    /// <summary>
    /// 选择器宽度
    /// </summary>
    [JsonPropertyName("width")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Width { get; set; }
    
    /// <summary>
    /// 是否禁用
    /// </summary>
    [JsonPropertyName("disabled")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? Disabled { get; set; }
    
    /// <summary>
    /// 交互行为列表
    /// </summary>
    [JsonPropertyName("behaviors")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<object>? Behaviors { get; set; }
}

/// <summary>
/// 选择器选项
/// </summary>
public class SelectOption
{
    /// <summary>
    /// 选项值
    /// </summary>
    [JsonPropertyName("value")]
    public string Value { get; set; } = string.Empty;
    
    /// <summary>
    /// 选项文本
    /// </summary>
    [JsonPropertyName("text")]
    public PlainText? Text { get; set; }
}
