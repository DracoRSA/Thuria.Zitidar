namespace Thuria.Zitidar.Core
{
  /// <summary>
  /// Thuria Database Settings
  /// </summary>
  public interface IThuriaDatabaseSettings : IThuriaSettings
  {
    /// <summary>
    /// Retrieve the Connection String for the given Database Context Name
    /// </summary>
    /// <param name="dbContextName">Database Context Name</param>
    /// <returns></returns>
    string GetConnectionString(string dbContextName = null);
  }
}
