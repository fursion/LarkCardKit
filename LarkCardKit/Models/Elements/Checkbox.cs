using System.Text.Json.Serialization;
using LarkCardKit.Models.Behaviors;

namespace LarkCardKit.Models.Elements;

/// <summary>
/// 勾选器组件
/// </summary>
public class Checkbox : Element
{
    /// <inheritdoc/>
    public override string Tag => "checkbox";
    
    /// <summary>
    /// 勾选器唯一标识，表单容器中必填
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
    /// 选项列表
    /// </summary>
    [JsonPropertyName("options")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<CheckboxOption>? Options { get; set; }
    
    /// <summary>
    /// 初始选中值
    /// </summary>
    [JsonPropertyName("initial_selected_options")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<string>? InitialSelectedOptions { get; set; }
    
    /// <summary>
    /// 交互行为列表
    /// </summary>
    [JsonPropertyName("behaviors")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<object>? Behaviors { get; set; }
}

/// <summary>
/// 勾选器选项
/// </summary>
public class CheckboxOption
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
