namespace LarkCardKit.Enums;

/// <summary>
/// 输入框类型枚举
/// 定义飞书卡片中输入框的不同输入模式
/// </summary>
public enum InputType
{
    /// <summary>
    /// 普通文本
    /// 单行文本输入
    /// </summary>
    Text,
    
    /// <summary>
    /// 多行文本
    /// 支持换行符的多行文本输入
    /// </summary>
    MultilineText,
    
    /// <summary>
    /// 密码类型
    /// 输入内容以圆点显示，保护隐私
    /// </summary>
    Password
}
