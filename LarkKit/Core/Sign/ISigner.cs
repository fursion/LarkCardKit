namespace LarkKit.Core.Sign;

/// <summary>
/// 签名验证接口
/// </summary>
public interface ISigner
{
    /// <summary>
    /// 验证签名
    /// </summary>
    /// <param name="timestamp">时间戳</param>
    /// <param name="nonce">随机数</param>
    /// <param name="signature">签名值</param>
    /// <param name="appSecret">应用密钥</param>
    /// <returns>签名是否有效</returns>
    bool VerifySignature(string timestamp, string nonce, string signature, string appSecret);

    /// <summary>
    /// 生成签名
    /// </summary>
    /// <param name="timestamp">时间戳</param>
    /// <param name="nonce">随机数</param>
    /// <param name="appSecret">应用密钥</param>
    /// <returns>签名值</returns>
    string GenerateSignature(string timestamp, string nonce, string appSecret);
}
