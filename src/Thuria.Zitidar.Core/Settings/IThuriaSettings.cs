namespace Thuria.Zitidar.Core
{
  /// <summary>
  /// Thuria Settings
  /// </summary>
  public interface IThuriaSettings
  {
    /// <summary>
    /// Application Name
    /// </summary>
    string ApplicationName { get; }

    /// <summary>
    /// Start the Application in Debug Mode indicator
    /// </summary>
    bool StartDebugMode { get; }

    /// <summary>
    /// Settings Type
    /// </summary>
    ThuriaSettingsType SettingsType { get; set; }

    /// <summary>
    /// Settings Filename
    /// </summary>
    string SettingFilename { get; set; }
  }
}
