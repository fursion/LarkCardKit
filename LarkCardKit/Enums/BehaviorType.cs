namespace LarkCardKit.Enums;

/// <summary>
/// 交互行为类型枚举
/// 定义飞书卡片中交互组件的行为类型
/// </summary>
public enum BehaviorType
{
    /// <summary>
    /// 打开 URL 跳转交互
    /// 点击后跳转到指定的链接地址
    /// </summary>
    OpenUrl,
    
    /// <summary>
    /// 回传交互数据
    /// 点击后将自定义数据回传到服务端
    /// </summary>
    Callback,
    
    /// <summary>
    /// 表单提交
    /// 提交表单容器中的数据到服务端（仅在表单容器中使用）
    /// </summary>
    FormSubmit,
    
    /// <summary>
    /// 表单重置
    /// 清空表单容器中的所有数据（仅在表单容器中使用）
    /// </summary>
    FormReset
}
