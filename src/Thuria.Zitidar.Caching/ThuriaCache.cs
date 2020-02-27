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
  {
    private readonly ConcurrentDictionary<string, IThuriaCacheData<T>> _cacheData;

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
      _cacheData      = new ConcurrentDictionary<string, IThuriaCacheData<T>>();
    }

    /// <inheritdoc />
    public int ExpiryInSeconds { get; }

    /// <inheritdoc />
    public Task<bool> ExistsAsync(string cacheKey)
    {
      var taskCompletionSource = new TaskCompletionSource<bool>();

      try
      {
        if (string.IsNullOrWhiteSpace(cacheKey)) { throw new ArgumentNullException(nameof(cacheKey)); }

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

    /// <inheritdoc />
    public async Task<bool> UpsertAsync(string cacheKey, IThuriaCacheData<T> cacheValue, bool setCacheExpiry = true)
    {
      if (cacheValue == null) { throw new ArgumentNullException(nameof(cacheValue)); }

      if (setCacheExpiry)
      {
        cacheValue.Expiry = DateTime.UtcNow.AddSeconds(ExpiryInSeconds);
      }

      if (await ExistsAsync(cacheKey))
      {
        _cacheData.AddOrUpdate(cacheKey, cacheValue, (key, oldValue) => cacheValue);
        return true;
      }

      return _cacheData.TryAdd(cacheKey, cacheValue);
    }

    /// <inheritdoc />
    public async Task<T> GetAsync(string cacheKey)
    {
      if (!await ExistsAsync(cacheKey))
      {
        return default(T);
      }

      _cacheData.TryGetValue(cacheKey, out var cacheValue);
      return cacheValue == null ? default(T) : cacheValue.Value;
    }
  }
}
