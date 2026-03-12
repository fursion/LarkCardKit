using System.Text.Json;
using System.Text.Json.Serialization;
using LarkCardKit.Models.Elements;

namespace LarkCardKit.Converters;

/// <summary>
/// Element 多态 JSON 转换器
/// 根据 tag 字段反序列化为具体的元素类型
/// </summary>
public class ElementConverter : JsonConverter<Element>
{
    /// <summary>
    /// 读取并反序列化 Element
    /// </summary>
    public override Element? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using var doc = JsonDocument.ParseValue(ref reader);
        var root = doc.RootElement;
        
        if (root.TryGetProperty("tag", out var tagProperty))
        {
            var tag = tagProperty.GetString();
            var json = root.GetRawText();
            
            return tag switch
            {
                "plain_text" => JsonSerializer.Deserialize<PlainText>(json, options),
                "markdown" => JsonSerializer.Deserialize<Markdown>(json, options),
                "lark_md" => JsonSerializer.Deserialize<MarkdownText>(json, options),
                "button" => JsonSerializer.Deserialize<Button>(json, options),
                "input" => JsonSerializer.Deserialize<Input>(json, options),
                "select" => JsonSerializer.Deserialize<Select>(json, options),
                "date_picker" => JsonSerializer.Deserialize<DatePicker>(json, options),
                "checkbox" => JsonSerializer.Deserialize<Checkbox>(json, options),
                "img" => JsonSerializer.Deserialize<Image>(json, options),
                "div" => JsonSerializer.Deserialize<TextDiv>(json, options),
                "form" => JsonSerializer.Deserialize<Form>(json, options),
                "column_set" => JsonSerializer.Deserialize<ColumnSet>(json, options),
                "column" => JsonSerializer.Deserialize<Column>(json, options),
                _ => throw new JsonException($"未知的元素类型：{tag}")
            };
        }
        
        throw new JsonException("Element 缺少 tag 属性");
    }

    /// <summary>
    /// 写入并序列化 Element
    /// </summary>
    public override void Write(Utf8JsonWriter writer, Element value, JsonSerializerOptions options)
    {
        // 直接序列化具体类型
        JsonSerializer.Serialize(writer, value, value.GetType(), options);
    }
}
