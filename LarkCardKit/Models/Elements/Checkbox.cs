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
    public string? Name { get; set; }
    
    /// <summary>
    /// 是否必填
    /// </summary>
    [JsonPropertyName("required")]
    public bool? Required { get; set; }
    
    /// <summary>
    /// 是否禁用
    /// </summary>
    [JsonPropertyName("disabled")]
    public bool? Disabled { get; set; }

    /// <summary>
    /// 禁用提示
    /// </summary>
    [JsonPropertyName("disabled_tips")]
    public PlainText? DisabledTips { get; set; }

    /// <summary>
    /// 占位文本
    /// </summary>
    [JsonPropertyName("placeholder")]
    public PlainText? Placeholder { get; set; }
    
    /// <summary>
    /// 组件宽度
    /// </summary>
    [JsonPropertyName("width")]
    public string? Width { get; set; }
    
    /// <summary>
    /// 组件边框样式
    /// </summary>
    [JsonPropertyName("type")]
    public string? Type { get; set; }
    
    /// <summary>
    /// 选项列表
    /// </summary>
    [JsonPropertyName("options")]
    public List<CheckboxOption>? Options { get; set; }
    
    /// <summary>
    /// 初始选中值
    /// </summary>
    [JsonPropertyName("selected_values")]
    public List<string>? SelectedValues { get; set; }

    /// <summary>
    /// 交互行为列表
    /// </summary>
    [JsonPropertyName("behaviors")]
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
