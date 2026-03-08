using System.Text.Json.Serialization;

namespace LarkKit.Message.Content;

/// <summary>
/// 图片消息内容
/// </summary>
public class ImageContent
{
    /// <summary>
    /// 图片的 image_key（通过图片上传接口获取）
    /// </summary>
    [JsonPropertyName("image_key")]
    public string ImageKey { get; set; } = string.Empty;

    /// <summary>
    /// 初始化 ImageContent 的新实例
    /// </summary>
    public ImageContent() { }

    /// <summary>
    /// 初始化 ImageContent 的新实例
    /// </summary>
    /// <param name="imageKey">图片的 image_key</param>
    public ImageContent(string imageKey)
    {
        ImageKey = imageKey;
    }
}
