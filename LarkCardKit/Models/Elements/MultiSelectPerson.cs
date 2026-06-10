using System.Text.Json.Serialization;
using LarkCardKit.Models.Behaviors;

namespace LarkCardKit.Models.Elements;

public class MultiSelectPerson : Element
{
    public override string Tag => "multi_select_person";

    [JsonPropertyName("name")]
    public string? Name { get; set; }
    
    [JsonPropertyName("required")]
    public bool? Required { get; set; }
    
    [JsonPropertyName("placeholder")]
    public PlainText? Placeholder { get; set; }
    
    /// <summary>
    /// 文本标签
    /// </summary>
    [JsonPropertyName("label")]
    public PlainText? Label { get; set; }
    
    [JsonPropertyName("selected_values")]
    public List<string>? SelectedValues { get; set; }
    
    [JsonPropertyName("options")]
    public List<PersonOption>? Options { get; set; }
    
    [JsonPropertyName("width")]
    public string? Width { get; set; }
    
    [JsonPropertyName("disabled")]
    public bool? Disabled { get; set; }

    /// <summary>
    /// 禁用提示
    /// </summary>
    [JsonPropertyName("disabled_tips")]
    public PlainText? DisabledTips { get; set; }

    [JsonPropertyName("confirm")]
    public ConfirmConfig? Confirm { get; set; }
    
    [JsonPropertyName("behaviors")]
    public List<object>? Behaviors { get; set; }

    }

public class PersonOption
{
    [JsonPropertyName("value")]
    public string Value { get; set; } = string.Empty;
}
