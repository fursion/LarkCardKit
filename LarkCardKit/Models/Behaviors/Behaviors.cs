using System.Text.Json.Serialization;
using LarkCardKit.Enums;

namespace LarkCardKit.Models.Behaviors;

/// <summary>
/// 打开 URL 行为
/// </summary>
public class OpenUrlBehavior
{
    /// <summary>
    /// 交互类型
    /// </summary>
    [JsonPropertyName("type")]
    public string Type => "open_url";
    
    /// <summary>
    /// 兜底跳转地址
    /// </summary>
    [JsonPropertyName("default_url")]
    public string DefaultUrl { get; set; } = string.Empty;
    
    /// <summary>
    /// 安卓端跳转地址
    /// </summary>
    [JsonPropertyName("android_url")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? AndroidUrl { get; set; }
    
    /// <summary>
    /// iOS 端跳转地址
    /// </summary>
    [JsonPropertyName("ios_url")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? IosUrl { get; set; }
    
    /// <summary>
    /// PC 端跳转地址
    /// </summary>
    [JsonPropertyName("pc_url")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? PcUrl { get; set; }
}

/// <summary>
/// 回调行为
/// </summary>
public class CallbackBehavior
{
    /// <summary>
    /// 交互类型
    /// </summary>
    [JsonPropertyName("type")]
    public string Type => "callback";
    
    /// <summary>
    /// 回传数据，支持 object 类型
    /// </summary>
    [JsonPropertyName("value")]
    public object? Value { get; set; }
}

/// <summary>
/// 表单提交行为
/// </summary>
public class FormSubmitBehavior
{
    /// <summary>
    /// 交互类型
    /// </summary>
    [JsonPropertyName("type")]
    public string Type => "form_submit";
}

/// <summary>
/// 表单重置行为
/// </summary>
public class FormResetBehavior
{
    /// <summary>
    /// 交互类型
    /// </summary>
    [JsonPropertyName("type")]
    public string Type => "form_reset";
}
