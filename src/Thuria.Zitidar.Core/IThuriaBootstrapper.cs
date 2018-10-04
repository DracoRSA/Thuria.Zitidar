namespace Thuria.Zitidar.Core
{
  /// <summary>
  /// Thuria Bootstrapper
  /// </summary>
  public interface IThuriaBootstrapper
  {
    /// <summary>
    /// Thuria IOC Container
    /// </summary>
    IThuriaIocContainer IocContainer { get; }

    /// <summary>
    /// Build the bootstrapper
    /// </summary>
    /// <returns></returns>
    IThuriaBootstrapper Build();
  }
}
