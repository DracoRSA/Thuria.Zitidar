namespace Thuria.Zitidar.Core
{
  /// <summary>
  /// Thuria Nancy Settings
  /// </summary>
  public interface IThuriaNancySettings : IThuriaSettings
  {
    /// <summary>
    /// Host Base URI
    /// </summary>
    string HostBaseUri { get; }
  }
}
