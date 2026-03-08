namespace LarkKit.Core.Http;

/// <summary>
/// 飞书 HTTP 客户端接口
/// </summary>
public interface ILarkHttpClient
{
    /// <summary>
    /// 发送 GET 请求
    /// </summary>
    /// <typeparam name="T">响应数据类型</typeparam>
    /// <param name="url">请求 URL</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>响应数据</returns>
    Task<T?> GetAsync<T>(string url, CancellationToken cancellationToken = default);

    /// <summary>
    /// 发送 POST 请求
    /// </summary>
    /// <typeparam name="TRequest">请求数据类型</typeparam>
    /// <typeparam name="TResponse">响应数据类型</typeparam>
    /// <param name="url">请求 URL</param>
    /// <param name="data">请求数据</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>响应数据</returns>
    Task<TResponse?> PostAsync<TRequest, TResponse>(string url, TRequest data, CancellationToken cancellationToken = default);

    /// <summary>
    /// 发送 POST 请求（无响应数据）
    /// </summary>
    /// <typeparam name="T">请求数据类型</typeparam>
    /// <param name="url">请求 URL</param>
    /// <param name="data">请求数据</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>任务</returns>
    Task PostAsync<T>(string url, T data, CancellationToken cancellationToken = default);

    /// <summary>
    /// 发送 PUT 请求
    /// </summary>
    /// <typeparam name="TRequest">请求数据类型</typeparam>
    /// <typeparam name="TResponse">响应数据类型</typeparam>
    /// <param name="url">请求 URL</param>
    /// <param name="data">请求数据</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>响应数据</returns>
    Task<TResponse?> PutAsync<TRequest, TResponse>(string url, TRequest data, CancellationToken cancellationToken = default);

    /// <summary>
    /// 发送 DELETE 请求
    /// </summary>
    /// <typeparam name="T">响应数据类型</typeparam>
    /// <param name="url">请求 URL</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>响应数据</returns>
    Task<T?> DeleteAsync<T>(string url, CancellationToken cancellationToken = default);
}
