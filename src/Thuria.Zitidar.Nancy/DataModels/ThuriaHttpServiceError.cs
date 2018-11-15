using Nancy;

namespace Thuria.Zitidar.Nancy
{
  /// <summary>
  /// Thuria Http Service Error
  /// </summary>
  public class ThuriaHttpServiceError
  {
    /// <summary>
    /// Http Status Code
    /// </summary>
    public HttpStatusCode HttpStatusCode { get; set; }

    /// <summary>
    /// Thuria Service Error Model
    /// </summary>
    public ThuriaServiceErrorModel ServiceErrorModel { get; set; }
  }
}
