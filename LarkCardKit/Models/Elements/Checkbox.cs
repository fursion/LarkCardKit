using System.Text.Json.Serialization;
using LarkCardKit.Models.Behaviors;

namespace LarkCardKit.Models.Elements;

/// <summary>
/// 多选下拉组件
/// </summary>
public class Checkbox : Element
{
    /// <inheritdoc/>
    public override string Tag => "multi_select_static";
    
    /// <summary>
    /// 组件唯一标识，表单容器中必填
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
    /// 是否禁用
    /// </summary>
    [JsonPropertyName("disabled")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? Disabled { get; set; }
    
    /// <summary>
    /// 占位文本
    /// </summary>
    [JsonPropertyName("placeholder")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public PlainText? Placeholder { get; set; }
    
    /// <summary>
    /// 组件宽度
    /// </summary>
    [JsonPropertyName("width")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Width { get; set; }
    
    /// <summary>
    /// 组件边框样式
    /// </summary>
    [JsonPropertyName("type")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Type { get; set; }
    
    /// <summary>
    /// 选项列表
    /// </summary>
    [JsonPropertyName("options")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<CheckboxOption>? Options { get; set; }
    
    /// <summary>
    /// 初始选中值
    /// </summary>
    [JsonPropertyName("selected_values")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<string>? SelectedValues { get; set; }
    
    /// <summary>
    /// 交互行为列表
    /// </summary>
    [JsonPropertyName("behaviors")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<object>? Behaviors { get; set; }
}

/// <summary>
/// 多选下拉选项
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
