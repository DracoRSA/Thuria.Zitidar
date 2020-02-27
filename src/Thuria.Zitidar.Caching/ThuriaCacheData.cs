using System;

using Thuria.Zitidar.Core.Cache;

namespace Thuria.Zitidar.Caching
{
  /// <summary>
  /// Thuria Cache Data
  /// </summary>
  public class ThuriaCacheData<T> : IThuriaCacheData<T>
  {
    /// <summary>
    /// Thuria Cache Data Model
    /// </summary>
    /// <param name="cacheValue">Data</param>
    /// <param name="expiryDate">Data Expiry</param>
    public ThuriaCacheData(T cacheValue, DateTime? expiryDate = null)
    {
      Expiry = expiryDate;
      Value  = cacheValue ?? throw new ArgumentNullException(nameof(cacheValue));
    }

    /// <summary>
    /// Data Expiry Date
    /// </summary>
    public DateTime? Expiry { get; set; }

    /// <summary>
    /// Data Value
    /// </summary>
    public T Value { get; private set; }
  }
}
