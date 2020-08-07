using System;
using System.Threading;
using System.Threading.Tasks;
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
    public string ConfigurationFileLocation { get; set; } = "akka.config";

    /// <inheritdoc />
    public ActorSystem ActorSystem { get; set; }

    /// <inheritdoc />
    public virtual void Start()
    {
      var akkaConfiguration = ThuriaHoconLoader.FromFile(ConfigurationFileLocation);
      ActorSystem           = ActorSystem.Create(Name, akkaConfiguration);

      // ReSharper disable once ObjectCreationAsStatement
      new StructureMapDependencyResolver((IContainer)_iocContainer.Container, ActorSystem);
    }

    /// <inheritdoc />
    public Task StartAsync(CancellationToken cancellationToken)
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
    public void Stop()
    {
      if (ActorSystem == null) { return; }

      ActorSystem.Terminate();
      ActorSystem = null;
    }

    /// <inheritdoc />
    public Task StopAsync()
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
