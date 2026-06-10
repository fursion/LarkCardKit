using System.Text.Json.Serialization;
using LarkCardKit.Models.Behaviors;

namespace LarkCardKit.Models.Elements;

/// <summary>
/// 多图选择组件
/// </summary>
public class SelectImg : Element
{
    /// <inheritdoc/>
    public override string Tag => "select_img";

    /// <summary>
    /// 元素唯一标识
    /// </summary>
    [JsonPropertyName("element_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public new string? ElementId { get; set; }

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
    /// 选择模式：single-单选，multi-多选
    /// </summary>
    [JsonPropertyName("select_mode")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? SelectMode { get; set; }
    
    /// <summary>
    /// 初始选中图片 key 列表
    /// </summary>
    [JsonPropertyName("initial_options")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<string>? InitialOptions { get; set; }
    
    /// <summary>
    /// 图片选项列表
    /// </summary>
    [JsonPropertyName("options")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<SelectImgOption>? Options { get; set; }
    
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
    /// 禁用提示
    /// </summary>
    [JsonPropertyName("disabled_tips")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public PlainText? DisabledTips { get; set; }

    /// <summary>
    /// 二次确认配置
    /// </summary>
    [JsonPropertyName("confirm")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ConfirmConfig? Confirm { get; set; }
    
    /// <summary>
    /// 交互行为列表
    /// </summary>
    [JsonPropertyName("behaviors")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<object>? Behaviors { get; set; }

    /// <summary>
    /// 外边距
    /// </summary>
    [JsonPropertyName("margin")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public new string? Margin { get; set; }
}

/// <summary>
/// 多图选择选项
/// </summary>
public class SelectImgOption
{
    /// <summary>
    /// 图片的 Key
    /// </summary>
    [JsonPropertyName("value")]
    public string Value { get; set; } = string.Empty;
    
    /// <summary>
    /// 图片说明文本
    /// </summary>
    [JsonPropertyName("text")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public PlainText? Text { get; set; }
}
