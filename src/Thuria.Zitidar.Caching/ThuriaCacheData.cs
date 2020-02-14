using System;

namespace Thuria.Zitidar.Caching
{
  /// <summary>
  /// Thuria Cache Data
  /// </summary>
  public class ThuriaCacheData<T>
  {
    /// <summary>
    /// Thuria Cache Data Model
    /// </summary>
    /// <param name="expiry">Data Expiry</param>
    /// <param name="value">Data</param>
    public ThuriaCacheData(DateTime expiry, T value)
    {
      Expiry = expiry;
      Value  = value;
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
