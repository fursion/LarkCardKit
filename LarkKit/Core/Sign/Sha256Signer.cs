using System.Security.Cryptography;
using System.Text;

namespace LarkKit.Core.Sign;

/// <summary>
/// SHA256 签名实现
/// 使用 HMAC-SHA256 算法进行签名验证
/// </summary>
public class Sha256Signer : ISigner
{
    /// <summary>
    /// 验证签名
    /// </summary>
    /// <param name="timestamp">时间戳</param>
    /// <param name="nonce">随机数</param>
    /// <param name="signature">签名值</param>
    /// <param name="appSecret">应用密钥</param>
    /// <returns>签名是否有效</returns>
    public bool VerifySignature(string timestamp, string nonce, string signature, string appSecret)
    {
        var expectedSignature = GenerateSignature(timestamp, nonce, appSecret);
        return string.Equals(expectedSignature, signature, StringComparison.OrdinalIgnoreCase);
    }

    /// <summary>
    /// 生成签名
    /// 签名算法：HMAC-SHA256(timestamp + nonce + appSecret)
    /// </summary>
    /// <param name="timestamp">时间戳</param>
    /// <param name="nonce">随机数</param>
    /// <param name="appSecret">应用密钥</param>
    /// <returns>签名值（16 进制字符串）</returns>
    public string GenerateSignature(string timestamp, string nonce, string appSecret)
    {
        var data = timestamp + nonce + appSecret;
        var keyBytes = Encoding.UTF8.GetBytes(appSecret);
        var dataBytes = Encoding.UTF8.GetBytes(data);

        using var hmac = new HMACSHA256(keyBytes);
        var hashBytes = hmac.ComputeHash(dataBytes);

        return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
    }
}
