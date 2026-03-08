using System.Text.Json.Serialization;

namespace LarkKit.Models;

/// <summary>
/// 飞书 API 通用响应模型
/// </summary>
/// <typeparam name="T">响应数据类型</typeparam>
public class LarkResponse<T>
{
    /// <summary>
    /// 错误码，0 表示成功
    /// </summary>
    [JsonPropertyName("code")]
    public int Code { get; set; }

    /// <summary>
    /// 错误描述
    /// </summary>
    [JsonPropertyName("msg")]
    public string? Message { get; set; }

    /// <summary>
    /// 响应数据
    /// </summary>
    [JsonPropertyName("data")]
    public T? Data { get; set; }

    /// <summary>
    /// 详细错误信息
    /// </summary>
    [JsonPropertyName("error")]
    public string? Error { get; set; }

    /// <summary>
    /// 是否为成功响应
    /// </summary>
    [JsonIgnore]
    public bool IsSuccess => Code == 0;
}

/// <summary>
/// 无数据类型的通用响应
/// </summary>
public class LarkResponse : LarkResponse<object>
{
}

/// <summary>
/// 飞书 API 直接数据响应模型（用于 Token 等直接在根级别的响应）
/// </summary>
public class LarkDirectResponse<T>
{
    /// <summary>
    /// 错误码，0 表示成功
    /// </summary>
    [JsonPropertyName("code")]
    public int Code { get; set; }

    /// <summary>
    /// 错误描述
    /// </summary>
    [JsonPropertyName("msg")]
    public string? Message { get; set; }

    /// <summary>
    /// Token 访问令牌
    /// </summary>
    [JsonPropertyName("tenant_access_token")]
    public string? TenantAccessToken { get; set; }

    /// <summary>
    /// 过期时间（秒）
    /// </summary>
    [JsonPropertyName("expire")]
    public int Expire { get; set; }

    /// <summary>
    /// 详细错误信息
    /// </summary>
    [JsonPropertyName("error")]
    public string? Error { get; set; }

    /// <summary>
    /// 是否为成功响应
    /// </summary>
    [JsonIgnore]
    public bool IsSuccess => Code == 0;
}
