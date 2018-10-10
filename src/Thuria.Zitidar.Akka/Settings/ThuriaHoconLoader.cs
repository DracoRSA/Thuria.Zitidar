using System.IO;
using Akka.Configuration;

namespace Thuria.Zitidar.Akka.Settings
{
  /// <summary>
  /// Thuria Hocon Loader
  /// </summary>
  public static class ThuriaHoconLoader
  {
    /// <summary>
    /// Load the Hocon Configuration from the specified file
    /// </summary>
    /// <param name="hoconFile">Hocon File</param>
    /// <returns>Loaded Hocon Configuration</returns>
    public static Config FromFile(string hoconFile)
    {
      return ConfigurationFactory.ParseString(File.ReadAllText(hoconFile));
    }
  }
}
