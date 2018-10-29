using Thuria.Zitidar.Core;

namespace Thuria.Zitidar.Settings
{
  public class ThuriaDatabaseSettings : ThuriaSettings, IThuriaDatabaseSettings
  {
    public string GetConnectionString(string dbContextName = null)
    {
      var settingName = $"ConnectionStrings:{dbContextName}";
      return GetConfigurationValue<string>(settingName);
    }
  }
}
