using Thuria.Zitidar.Caching.Abstractions.Caching;

namespace Thuria.Zitidar.Caching.Abstractions.Models;

/// <summary>
/// Thuria Cache Data item
/// </summary>
public record ThuriaCacheData<T>(DateTime Expiry, T Value) : IThuriaCacheData<T>
    where T : class;