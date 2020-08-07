using System.Threading.Tasks;

namespace Thuria.Zitidar.Core
{
  /// <summary>
  /// Thuria Stoppable object
  /// </summary>
  public interface IThuriaStoppable
  {
    /// <summary>
    /// Stop
    /// </summary>
    void Stop();

    /// <summary>
    /// Start the Service (Async)
    /// </summary>
    /// <returns>An awaitable task</returns>
    Task StopAsync();
  }
}
