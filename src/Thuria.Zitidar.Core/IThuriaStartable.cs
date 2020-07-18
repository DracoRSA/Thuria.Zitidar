using System.Threading;
using System.Threading.Tasks;

namespace Thuria.Zitidar.Core
{
  /// <summary>
  /// Thuria Startable object
  /// </summary>
  public interface IThuriaStartable
  {
    /// <summary>
    /// Start
    /// </summary>
    void Start();

    /// <summary>
    /// Start the Service (Async)
    /// </summary>
    /// <param name="cancellationToken">Cancellation Token</param>
    /// <returns>An awaitable task</returns>
    Task StartAsync(CancellationToken cancellationToken);
  }
}
