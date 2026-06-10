using System.Text.Json.Serialization;
using LarkCardKit.Enums;
using LarkCardKit.Models.Behaviors;

namespace LarkCardKit.Models.Elements;

/// <summary>
/// 输入框组件
/// </summary>
public class Input : Element
{
    /// <inheritdoc/>
    public override string Tag => "input";

    /// <summary>
    /// 输入框唯一标识，表单容器中必填
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }
    
    /// <summary>
    /// 是否必填（仅在表单容器中生效）
    /// </summary>
    [JsonPropertyName("required")]
    public bool? Required { get; set; }
    
    /// <summary>
    /// 占位文本
    /// </summary>
    [JsonPropertyName("placeholder")]
    public PlainText? Placeholder { get; set; }
    
    /// <summary>
    /// 默认值
    /// </summary>
    [JsonPropertyName("default_value")]
    public string? DefaultValue { get; set; }
    
    /// <summary>
    /// 输入类型
    /// </summary>
    [JsonPropertyName("input_type")]
    public string? InputType { get; set; }
    
    /// <summary>
    /// 文本标签
    /// </summary>
    [JsonPropertyName("label")]
    public PlainText? Label { get; set; }
    
    /// <summary>
    /// 标签位置
    /// </summary>
    [JsonPropertyName("label_position")]
    public string? LabelPosition { get; set; }
    
    /// <summary>
    /// 最大长度
    /// </summary>
    [JsonPropertyName("max_length")]
    public int? MaxLength { get; set; }
    
    /// <summary>
    /// 默认行数（多行文本时）
    /// </summary>
    [JsonPropertyName("rows")]
    public int? Rows { get; set; }
    
    /// <summary>
    /// 是否自适应高度（多行文本时）
    /// </summary>
    [JsonPropertyName("auto_resize")]
    public bool? AutoResize { get; set; }
    
    /// <summary>
    /// 最大行数（多行文本时）
    /// </summary>
    [JsonPropertyName("max_rows")]
    public int? MaxRows { get; set; }
    
    /// <summary>
    /// 输入框宽度
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
    /// 当输入类型为密码类型时，是否展示前缀图标
    /// </summary>
    [JsonPropertyName("show_icon")]
    public bool? ShowIcon { get; set; }
    
    /// <summary>
    /// 交互行为列表
    /// </summary>
    [JsonPropertyName("behaviors")]
    public List<object>? Behaviors { get; set; }

    }
