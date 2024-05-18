using Microsoft.Extensions.Caching.Memory;

using Thuria.Zitidar.Caching.Abstractions.Caching;

namespace Thuria.Zitidar.Caching;

/// <summary>
/// Thuria Type Cache
/// </summary>
public class ThuriaCache<T> : IThuriaCache<T>
    where T : class
{
    private readonly IMemoryCache _memoryCache;
    private readonly SemaphoreSlim _cacheLock = new(1, 1);
    private readonly DateTimeOffset _expiryTimeSpan;

    /// <summary>
    /// Thuria Type Cache constructor
    /// </summary>
    /// <param name="memoryCache">Memory Cache</param>
    /// <param name="expiryInSeconds">Expiry in Seconds (Default 15 minutes)</param>
    public ThuriaCache(IMemoryCache memoryCache, int expiryInSeconds = 54000)
    {
        if (expiryInSeconds <= 0)
        {
            throw new ArgumentException($"Expiry in Seconds cannot be Zero or Negative [{expiryInSeconds}]", nameof(expiryInSeconds));
        }

        _memoryCache    = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
        _expiryTimeSpan = DateTimeOffset.Now.AddSeconds(expiryInSeconds);
    }

    /// <inheritdoc />
    public Task<bool> ExistsAsync(string cacheKey)
    {
        return Task.FromResult(_memoryCache.TryGetValue(cacheKey, out _));
    }

    /// <inheritdoc />
    public async Task<bool> UpsertAsync(string cacheKey, IThuriaCacheData<T> cacheValue, bool setCacheExpiry = true)
    {
        if (cacheValue == null)
        {
            throw new ArgumentNullException(nameof(cacheValue));
        }

        await _cacheLock.WaitAsync();

        try
        {
            if (_memoryCache.TryGetValue(cacheKey, out _))
            {
                _memoryCache.Remove(cacheKey);
            }

            var cacheEntryOptions = new MemoryCacheEntryOptions
                                    {
                                        AbsoluteExpiration = setCacheExpiry ? _expiryTimeSpan : null
                                    };

            _memoryCache.Set(cacheKey, cacheValue, cacheEntryOptions);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
        finally
        {
            _cacheLock.Release();
        }
    }

    /// <inheritdoc />
    public async Task<T?> GetAsync(string cacheKey)
    {
        await _cacheLock.WaitAsync();
        try
        {
            return _memoryCache.TryGetValue(cacheKey, out IThuriaCacheData<T>? cacheValue) 
                       ? cacheValue?.Value ?? default
                       : null;
        }
        finally
        {
            _cacheLock.Release();
        }
    }
}
