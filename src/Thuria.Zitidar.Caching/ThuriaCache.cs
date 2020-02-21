using System;
using System.Threading.Tasks;
using System.Collections.Concurrent;

using Thuria.Zitidar.Core.Cache;

namespace Thuria.Zitidar.Caching
{
  /// <summary>
  /// Thuria Type Cache
  /// </summary>
  public class ThuriaCache<T> : IThuriaCache<T>
    where T : IThuriaCacheData<T>
  {
    private readonly ConcurrentDictionary<string, T> _cacheData;

    /// <summary>
    /// Thuria Type Cache constructor
    /// </summary>
    /// <param name="expiryInSeconds">Expiry in Seconds (Default 15 minutes)</param>
    public ThuriaCache(int expiryInSeconds = 54000)
    {
      if (expiryInSeconds <= 0)
      {
        throw new ArgumentException($"Expiry in Seconds cannot be Zero or Negative [{expiryInSeconds}]", nameof(expiryInSeconds));
      }

      ExpiryInSeconds = expiryInSeconds;
      _cacheData      = new ConcurrentDictionary<string, T>();
    }

    /// <inheritdoc />
    public int ExpiryInSeconds { get; }

    /// <inheritdoc />
    public Task<bool> ExistsAsync(string cacheKey)
    {
      var taskCompletionSource = new TaskCompletionSource<bool>();

      try
      {
        var doesCacheDataExist = false;
        if (_cacheData.ContainsKey(cacheKey))
        {
          doesCacheDataExist = _cacheData[cacheKey].Expiry > DateTime.UtcNow;
        }

        taskCompletionSource.SetResult(doesCacheDataExist);
      }
      catch (Exception runtimeException)
      {
        taskCompletionSource.SetException(runtimeException);
      }

      return taskCompletionSource.Task;
    }

    /// <summary>
    /// Insert / Update the Cache Data Item
    /// </summary>
    /// <typeparam name="T">Data Type</typeparam>
    /// <param name="cacheKey">Cache Key</param>
    /// <param name="cacheValue">Cache Value</param>
    /// <returns>
    /// A boolean indicating if the Upsert was successful
    /// </returns>
    public async Task<bool> Upsert(string cacheKey, T cacheValue)
    {
      if (await ExistsAsync(cacheKey))
      {
        _cacheData.AddOrUpdate(cacheKey, cacheValue, (key, oldValue) => cacheValue);
        return true;
      }

      return _cacheData.TryAdd(cacheKey, cacheValue);
    }

    /// <summary>
    /// Get Cache Value (Async)
    /// </summary>
    /// <param name="cacheKey">Cache Key</param>
    /// <returns>
    /// Null if item not found
    /// Cache Value if Cache Value found
    /// </returns>
    public async Task<T> GetAsync(string cacheKey)
    {
      if (!await ExistsAsync(cacheKey))
      {
        return default(T);
      }

      _cacheData.TryGetValue(cacheKey, out var cacheValue);
      return cacheValue;
    }
  }
}
