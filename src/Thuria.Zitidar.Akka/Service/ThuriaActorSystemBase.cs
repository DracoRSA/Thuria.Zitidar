using System;

using Akka.Actor;
using StructureMap;
using Akka.DI.StructureMap;

using Thuria.Zitidar.Core;
using Thuria.Zitidar.Akka.Settings;

namespace Thuria.Zitidar.Akka.Service
{
  /// <inheritdoc />
  public abstract class ThuriaActorSystemBase : IThuriaActorSystem
  {
    private readonly IThuriaIocContainer _iocContainer;

    /// <summary>
    /// Thuria Actor System Base Constructor
    /// </summary>
    /// <param name="iocContainer">IOC Container</param>
    protected ThuriaActorSystemBase(IThuriaIocContainer iocContainer)
    {
      _iocContainer = iocContainer ?? throw new ArgumentNullException(nameof(iocContainer));
    }

    /// <summary>
    /// Dispose allocated resources
    /// </summary>
    public void Dispose()
    {
      if (IsDisposing) { return; }
      IsDisposing = true;

      Stop();
    }

    /// <summary>
    /// Is Disposing indicator
    /// </summary>
    protected bool IsDisposing { get; set; }

    /// <inheritdoc />
    public abstract string Name { get; }

    /// <inheritdoc />
    public ActorSystem ActorSystem { get; set; }

    /// <inheritdoc />
    public virtual void Start()
    {
      var akkaConfiguration = ThuriaHoconLoader.FromFile("akka.config");
      ActorSystem           = ActorSystem.Create(Name, akkaConfiguration);

      // ReSharper disable once ObjectCreationAsStatement
      new StructureMapDependencyResolver((IContainer)_iocContainer.Container, ActorSystem);
    }

    /// <inheritdoc />
    public void Stop()
    {
      if (ActorSystem == null) { return; }

      ActorSystem.Terminate();
      ActorSystem = null;
    }
  }
}
