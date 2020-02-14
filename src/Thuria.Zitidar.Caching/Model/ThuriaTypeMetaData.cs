using System;

namespace Thuria.Zitidar.Caching.Model
{
  /// <summary>
  /// Thuria Type Metadata data model
  /// </summary>
  public class ThuriaTypeMetaData
  {
    /// <summary>
    /// Thuria Type Metadata data model constructor
    /// </summary>
    /// <param name="expiryDate">Object Metadata Expiry Date</param>
    /// <param name="objectType">Object Type</param>
    public ThuriaTypeMetaData(DateTime expiryDate, Type objectType)
    {
      ExpiryDate = expiryDate;
      Type       = objectType ?? throw new ArgumentNullException(nameof(objectType));
    }

    /// <summary>
    /// Metadata Expiry Date
    /// </summary>
    public DateTime ExpiryDate { get; }

    /// <summary>
    /// Object Type
    /// </summary>
    public Type Type { get; private set; }
  }
}
