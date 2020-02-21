using System;

namespace Thuria.Zitidar.Core.Cache
{
  /// <summary>
  /// Thuria Cache Data
  /// </summary>
  /// <typeparam name="T">Cache Value Data Type</typeparam>
  public interface IThuriaCacheData<T>
  {
    /// <summary>
    /// Data Expiry Date
    /// </summary>
    DateTime Expiry { get; }

    /// <summary>
    /// Data Value
    /// </summary>
    T Value { get; }
  }
}