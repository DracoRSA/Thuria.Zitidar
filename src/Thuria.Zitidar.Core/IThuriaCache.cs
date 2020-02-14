using System.Threading.Tasks;

namespace Thuria.Zitidar.Core
{
  /// <summary>
  /// Thuria Cache
  /// </summary>
  public interface IThuriaCache
  {
    /// <summary>
    /// Cache Expiry in Seconds
    /// </summary>
    int ExpiryInSeconds { get; }

    /// <summary>
    /// Determine if the Key exists in the Cache
    /// </summary>
    /// <param name="cacheKey">Cache Data Key</param>
    /// <returns>
    /// A boolean indicating whether the data item exists and not expired
    /// </returns>
    Task<bool> ExistsAsync(string cacheKey);
  }
}
