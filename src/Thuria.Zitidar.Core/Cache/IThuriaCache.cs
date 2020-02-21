using System.Threading.Tasks;

namespace Thuria.Zitidar.Core.Cache
{
  /// <summary>
  /// Thuria Cache
  /// </summary>
  public interface IThuriaCache<T>
    where T : IThuriaCacheData<T>
  {
    /// <summary>
    /// Cache Expiry in Seconds
    /// </summary>
    int ExpiryInSeconds { get; }

    /// <summary>
    /// Determine if the Key exists in the Cache (Async)
    /// </summary>
    /// <param name="cacheKey">Cache Data Key</param>
    /// <returns>
    /// A boolean indicating whether the data item exists and not expired
    /// </returns>
    Task<bool> ExistsAsync(string cacheKey);

    /// <summary>
    /// Insert / Update the Cache Data Item (Async)
    /// </summary>
    /// <typeparam name="T">Data Type</typeparam>
    /// <param name="cacheKey">Cache Key</param>
    /// <param name="cacheValue">Cache Value</param>
    /// <returns>
    /// A boolean indicating if the Upsert was successful
    /// </returns>
    Task<bool> Upsert(string cacheKey, T cacheValue);

    /// <summary>
    /// Get Cache Value (Async)
    /// </summary>
    /// <param name="cacheKey">Cache Key</param>
    /// <returns>
    /// Null if item not found
    /// Cache Value if Cache Value found
    /// </returns>
    Task<T> GetAsync(string cacheKey);
  }
}
