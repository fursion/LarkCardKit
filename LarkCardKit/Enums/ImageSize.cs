namespace LarkCardKit.Enums;

/// <summary>
/// 图片尺寸枚举
/// 定义飞书卡片中图片的裁剪和显示方式
/// </summary>
public enum ImageSize
{
    /// <summary>
    /// 居中裁剪
    /// 从图片中心区域裁剪，保持宽高比
    /// </summary>
    CropCenter,
    
    /// <summary>
    /// 顶部裁剪
    /// 从图片顶部区域裁剪，保持宽高比
    /// </summary>
    CropTop,
    
    /// <summary>
    /// 底部裁剪
    /// 从图片底部区域裁剪，保持宽高比
    /// </summary>
    CropBottom,
    
    /// <summary>
    /// 原始尺寸
    /// 显示图片原始尺寸，不裁剪
    /// </summary>
    Origin
}
