using System.Threading.Tasks;

namespace Thuria.Zitidar.Core.Cache
{
  /// <summary>
  /// Thuria Cache
  /// </summary>
  public interface IThuriaCache<T>
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
    /// <param name="cacheKey">Cache Key</param>
    /// <param name="cacheValue">Cache Value</param>
    /// <param name="setCacheExpiry">Set the Cache Expiry Value (Optional - Default true)</param>
    /// <returns>
    /// A boolean indicating if the Upsert was successful
    /// </returns>
    Task<bool> UpsertAsync(string cacheKey, IThuriaCacheData<T> cacheValue, bool setCacheExpiry = true);

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
