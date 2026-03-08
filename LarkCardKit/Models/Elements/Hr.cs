using System.Text.Json.Serialization;

namespace LarkCardKit.Models.Elements;

/// <summary>
/// 分割线组件
/// </summary>
/// <remarks>
/// 分割线组件是一条长横线，用于分割卡片的内容，使呈现内容更清晰。
/// </remarks>
public class Hr : Element
{
    /// <inheritdoc/>
    public override string Tag => "hr";
}
