namespace Thuria.Zitidar.Caching.Abstractions.Caching;

/// <summary>
/// Thuria Cache Data item
/// </summary>
public interface IThuriaCacheData<T> where T : class
{
    /// <summary>
    /// Data Expiry Date
    /// </summary>
    DateTime Expiry { get; init; }

    /// <summary>
    /// Data Value
    /// </summary>
    T Value { get; init; }
}