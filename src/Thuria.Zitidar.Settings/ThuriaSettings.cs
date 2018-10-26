using System;
using System.IO;
using System.Linq;

using Microsoft.Extensions.Configuration;

using Thuria.Zitidar.Core;

namespace Thuria.Zitidar.Settings
{
  public class ThuriaSettings : IThuriaSettings
  {
    private bool _isInitialised;
    private IConfigurationRoot _configurationRoot;

    public string ApplicationName => GetConfigurationValue<string>("ApplicationName", true);

    public bool StartDebugMode => GetConfigurationValue<bool>("StartDebugMode", true);

    public ThuriaSettingsType SettingsType { get; set; } = ThuriaSettingsType.JsonFile;
    public string SettingFilename { get; set; } = "appsettings.json";

    protected IConfigurationRoot Configuration
    {
      get
      {
        if (!_isInitialised) { Initialise(); }
        return _configurationRoot;
      }
    }

    protected void Initialise()
    {
      if (_isInitialised) { return; }

      switch (SettingsType)
      {
        case ThuriaSettingsType.JsonFile:
          _configurationRoot = new ConfigurationBuilder()
                                              .SetBasePath(Directory.GetCurrentDirectory())
                                              .AddJsonFile(SettingFilename, optional: false, reloadOnChange: true)
                                              .AddEnvironmentVariables()
                                              .Build();
          break;

        default:
          throw new Exception($"{SettingsType} not currently supported by the Thuria Settings provider");
      }

      _isInitialised = true;
      _configurationRoot.Bind(this);
    }

    protected T GetConfigurationValue<T>(string configurationKey, bool canBeNull = false)
    {
      var configurationValue = Configuration.GetValue<T>(configurationKey);
      if (!canBeNull && configurationValue == null)
      {
        throw new Exception($"{configurationKey} not found in Application Configuration");
      }

      return configurationValue;
    }

    protected T[] GetConfigurationValues<T>(string configurationKey, bool canBeNull = false)
    {
      var configurationSection = Configuration.GetSection(configurationKey);
      if (!canBeNull && configurationSection == null)
      {
        throw new Exception($"{configurationKey} not found in Application Configuration");
      }

      var configurationValues = configurationSection.Get<T[]>();
      if (!canBeNull && !configurationValues.Any())
      {
        throw new Exception($"No values specified for {configurationKey} in Application Configuration");
      }

      return configurationValues;
    }
  }
}
