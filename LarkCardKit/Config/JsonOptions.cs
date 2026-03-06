using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using LarkCardKit.Converters;
using LarkCardKit.Enums;

namespace LarkCardKit.Config;

/// <summary>
/// JSON 序列化配置
/// 提供符合飞书卡片 2.0 规范的 JSON 序列化选项
/// </summary>
public static class JsonOptions
{
    /// <summary>
    /// 默认序列化选项（紧凑格式）
    /// </summary>
    /// <remarks>
    /// <para>配置说明：</para>
    /// <list type="bullet">
    ///   <item><description>属性命名策略：camelCase（驼峰命名）</description></item>
    ///   <item><description>忽略 null 值：不输出值为 null 的属性</description></item>
    ///   <item><description>缩进：无（紧凑格式）</description></item>
    ///   <item><description>编码器：允许非 ASCII 字符直出（中文不转义）</description></item>
    /// </list>
    /// </remarks>
    public static JsonSerializerOptions DefaultOptions => new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        WriteIndented = false,
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
        Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase), new ElementConverter() }
    };
    
    /// <summary>
    /// 格式化序列化选项（带缩进）
    /// </summary>
    /// <remarks>
    /// <para>配置说明：</para>
    /// <list type="bullet">
    ///   <item><description>属性命名策略：camelCase（驼峰命名）</description></item>
    ///   <item><description>忽略 null 值：不输出值为 null 的属性</description></item>
    ///   <item><description>缩进：有（格式化格式，便于阅读）</description></item>
    ///   <item><description>编码器：允许非 ASCII 字符直出（中文不转义）</description></item>
    /// </list>
    /// </remarks>
    public static JsonSerializerOptions IndentedOptions => new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        WriteIndented = true,
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
        Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase), new ElementConverter() }
    };
}

/// <summary>
/// JSON 序列化扩展方法
/// 为所有类型提供便捷的 JSON 序列化功能
/// </summary>
public static class JsonExtensions
{
    /// <summary>
    /// 将对象序列化为符合飞书卡片 2.0 规范的 JSON 字符串（紧凑格式）
    /// </summary>
    /// <typeparam name="T">要序列化的对象类型</typeparam>
    /// <param name="obj">要序列化的对象</param>
    /// <returns>紧凑格式的 JSON 字符串</returns>
    /// <example>
    /// 以下示例演示如何使用此方法：
    /// <code>
    /// var card = new Card { /* ... */ };
    /// var json = card.ToJson();
    /// Console.WriteLine(json);
    /// // 输出：{"schema":"2.0","body":{"elements":[...]}}
    /// </code>
    /// </example>
    public static string ToJson<T>(this T obj)
    {
        return JsonSerializer.Serialize(obj, JsonOptions.DefaultOptions);
    }
    
    /// <summary>
    /// 将对象序列化为 JSON 字符串（可指定格式）
    /// </summary>
    /// <typeparam name="T">要序列化的对象类型</typeparam>
    /// <param name="obj">要序列化的对象</param>
    /// <param name="indented">
    /// 是否格式化输出
    /// <list type="bullet">
    ///   <item><description><c>true</c> - 输出带缩进和换行的格式化 JSON</description></item>
    ///   <item><description><c>false</c> - 输出紧凑格式的 JSON（默认）</description></item>
    /// </list>
    /// </param>
    /// <returns>JSON 字符串</returns>
    /// <example>
    /// 以下示例演示如何使用此方法：
    /// <code>
    /// var card = new Card { /* ... */ };
    /// 
    /// // 紧凑格式
    /// var json = card.ToJson();
    /// 
    /// // 格式化格式
    /// var jsonIndented = card.ToJson(indented: true);
    /// </code>
    /// </example>
    /// <remarks>
    /// <para>此方法使用 <see cref="System.Text.Json.JsonSerializer"/> 进行序列化。</para>
    /// <para>输出的 JSON 符合飞书卡片 2.0 规范，可直接用于飞书 API。</para>
    /// </remarks>
    public static string ToJson<T>(this T obj, bool indented)
    {
        var options = indented ? JsonOptions.IndentedOptions : JsonOptions.DefaultOptions;
        return JsonSerializer.Serialize(obj, options);
    }
}
