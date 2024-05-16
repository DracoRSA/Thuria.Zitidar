using System;
using System.Threading;
using System.Threading.Tasks;
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

        _memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
    }

    /// <inheritdoc />
    public Task<bool> ExistsAsync(string cacheKey)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task<bool> UpsertAsync(string cacheKey, IThuriaCacheData<T> cacheValue, bool setCacheExpiry = true)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task<T> GetAsync(string cacheKey)
    {
        throw new NotImplementedException();
    }
}