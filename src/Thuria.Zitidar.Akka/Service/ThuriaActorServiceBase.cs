using System;

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
    public virtual void Stop()
    {
      ActorSystem?.Stop();
    }
  }
}
