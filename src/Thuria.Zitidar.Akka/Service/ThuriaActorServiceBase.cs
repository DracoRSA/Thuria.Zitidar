using System;
using System.Threading;
using System.Threading.Tasks;

namespace Thuria.Zitidar.Akka.Service
{
  /// <summary>
  /// Thuria Actor Service Base implementation
  /// </summary>
  public abstract class ThuriaActorServiceBase : IThuriaActorService
  {
    private bool _isDisposing;

    /// <summary>
    /// Thuria Actor Service Base Constructor
    /// </summary>
    /// <param name="actorSystem">Actor System</param>
    protected ThuriaActorServiceBase(IThuriaActorSystem actorSystem)
    {
      ActorSystem = actorSystem ?? throw new ArgumentNullException(nameof(actorSystem));
    }

    /// <inheritdoc />
    public abstract string Name { get; protected set; }

    /// <inheritdoc />
    public IThuriaActorSystem ActorSystem { get; protected set; }

    /// <inheritdoc />
    public void Dispose()
    {
      if (_isDisposing) { return; }

      _isDisposing = true;
      if (ActorSystem == null) { return; } 

      Stop();
      ActorSystem = null;
    }

    /// <inheritdoc />
    public virtual void Start()
    {
      ActorSystem.Start();
    }

    /// <inheritdoc />
    public virtual Task StartAsync(CancellationToken cancellationToken)
    {
      var taskCompletionSource = new TaskCompletionSource<bool>();

      try
      {
        Start();
        taskCompletionSource.SetResult(true);
      }
      catch (Exception runtimeException)
      {
        taskCompletionSource.SetException(runtimeException);
      }

      return taskCompletionSource.Task;
    }

    /// <inheritdoc />
    public virtual void Stop()
    {
      ActorSystem?.Stop();
    }

    /// <inheritdoc />
    public virtual Task StopAsync(CancellationToken cancellationToken)
    {
      var taskCompletionSource = new TaskCompletionSource<bool>();

      try
      {
        Stop();
        taskCompletionSource.SetResult(true);
      }
      catch (Exception runtimeException)
      {
        taskCompletionSource.SetException(runtimeException);
      }

      return taskCompletionSource.Task;
    }
  }
}
