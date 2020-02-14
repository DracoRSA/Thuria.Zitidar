using System;
using System.Threading.Tasks;
using System.Collections.Concurrent;

using Thuria.Zitidar.Core;

namespace Thuria.Zitidar.Caching
{
  /// <summary>
  /// Thuria Type Cache
  /// </summary>
  public class ThuriaCache<T> : IThuriaCache
    where T : ThuriaCacheData<T>
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
    /// Insert / Update the Cahe Data Item
    /// </summary>
    /// <typeparam name="T">Data Type</typeparam>
    /// <param name="cacheKey">Cache Key</param>
    /// <param name="cacheValue">Cache Value</param>
    /// <returns></returns>
    public Task<bool> Upsert<T>(string cacheKey, T cacheValue)
    {
      throw new NotImplementedException();
    }
  }
}
