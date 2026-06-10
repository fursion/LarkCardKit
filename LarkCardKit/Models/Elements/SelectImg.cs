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
    /// 选择器唯一标识，表单容器中必填
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }
    
    /// <summary>
    /// 是否必填
    /// </summary>
    [JsonPropertyName("required")]
    public bool? Required { get; set; }
    
    /// <summary>
    /// 占位文本
    /// </summary>
    [JsonPropertyName("placeholder")]
    public PlainText? Placeholder { get; set; }
    
    /// <summary>
    /// 文本标签
    /// </summary>
    [JsonPropertyName("label")]
    public PlainText? Label { get; set; }
    
    /// <summary>
    /// 选择模式：single-单选，multi-多选
    /// </summary>
    [JsonPropertyName("select_mode")]
    public string? SelectMode { get; set; }
    
    /// <summary>
    /// 初始选中图片 key 列表
    /// </summary>
    [JsonPropertyName("initial_options")]
    public List<string>? InitialOptions { get; set; }
    
    /// <summary>
    /// 图片选项列表
    /// </summary>
    [JsonPropertyName("options")]
    public List<SelectImgOption>? Options { get; set; }
    
    /// <summary>
    /// 选择器宽度
    /// </summary>
    [JsonPropertyName("width")]
    public string? Width { get; set; }
    
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
    /// 二次确认配置
    /// </summary>
    [JsonPropertyName("confirm")]
    public ConfirmConfig? Confirm { get; set; }
    
    /// <summary>
    /// 交互行为列表
    /// </summary>
    [JsonPropertyName("behaviors")]
    public List<object>? Behaviors { get; set; }

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
    public PlainText? Text { get; set; }
}
