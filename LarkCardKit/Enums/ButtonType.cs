namespace LarkCardKit.Enums;

/// <summary>
/// 按钮类型枚举
/// 定义飞书卡片中按钮的不同样式类型
/// </summary>
public enum ButtonType
{
    /// <summary>
    /// 默认类型
    /// 黑色字体，有边框
    /// </summary>
    Default,
    
    /// <summary>
    /// 主要类型
    /// 蓝色字体，有边框
    /// </summary>
    Primary,
    
    /// <summary>
    /// 危险类型
    /// 红色字体，有边框
    /// </summary>
    Danger,
    
    /// <summary>
    /// 文本类型
    /// 黑色字体，无边框
    /// </summary>
    Text,
    
    /// <summary>
    /// 主要文本类型
    /// 蓝色字体，无边框
    /// </summary>
    PrimaryText,
    
    /// <summary>
    /// 危险文本类型
    /// 红色字体，无边框
    /// </summary>
    DangerText,
    
    /// <summary>
    /// 主要填充类型
    /// 蓝底白字
    /// </summary>
    PrimaryFilled,
    
    /// <summary>
    /// 危险填充类型
    /// 红底白字
    /// </summary>
    DangerFilled,
    
    /// <summary>
    /// 镭射按钮
    /// 特殊效果的按钮样式
    /// </summary>
    Laser
}
