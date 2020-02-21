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
    /// <param name="expiryDate">Data Expiry</param>
    /// <param name="cacheValue">Data</param>
    public ThuriaCacheData(DateTime expiryDate, T cacheValue)
    {
      Expiry = expiryDate;
      Value  = cacheValue ?? throw new ArgumentNullException(nameof(cacheValue));
    }

    /// <summary>
    /// Data Expiry Date
    /// </summary>
    public DateTime Expiry { get; private set; }

    /// <summary>
    /// Data Value
    /// </summary>
    public T Value { get; private set; }
  }
}
