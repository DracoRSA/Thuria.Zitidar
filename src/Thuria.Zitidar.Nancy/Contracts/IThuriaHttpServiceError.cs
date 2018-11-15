namespace Thuria.Zitidar.Nancy
{
  /// <summary>
  /// Thuria Http Service Error
  /// </summary>
  public interface IThuriaHttpServiceError
  {
    /// <summary>
    /// Http Service Error
    /// </summary>
    ThuriaHttpServiceError HttpServiceError { get; }
  }
}
