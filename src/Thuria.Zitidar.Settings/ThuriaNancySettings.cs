using Thuria.Zitidar.Core;

namespace Thuria.Zitidar.Settings
{
  /// <summary>
  /// Thuria Nancy Settings
  /// </summary>
  public class ThuriaNancySettings : ThuriaSettings, IThuriaNancySettings
  {
    /// <inheritdoc />
    public string HostBaseUri => GetConfigurationValue<string>("NancyHostBaseUri");
  }
}
