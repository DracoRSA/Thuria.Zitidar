using System.Threading;
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
    /// <param name="cancellationToken">Cancellation Token</param>
    /// <returns>An awaitable task</returns>
    Task StopAsync(CancellationToken cancellationToken);
  }
}
