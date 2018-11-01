using System;
using System.IO;

using Microsoft.AspNetCore.Hosting;

using Thuria.Zitidar.Core;

namespace Thuria.Zitidar.Nancy
{
  /// <summary>
  /// Nancy Service Host
  /// </summary>
  public class NancyServiceHost : IThuriaNamedInstance, IThuriaStartable, IThuriaStoppable
  {
    private bool _isDisposing;
    private IWebHost _webHost;

    private readonly IThuriaNancySettings _thuriaNancySettings;
    private readonly IThuriaLogger _thuriaLogger;

    /// <summary>
    /// Nancy Service Host constructor
    /// </summary>
    /// <param name="iocContainer"></param>
    public NancyServiceHost(IThuriaIocContainer iocContainer)
    {
      if (NancyStartup.IocContainer == null || iocContainer != null)
      {
        NancyStartup.IocContainer = iocContainer ?? throw new ArgumentNullException(nameof(iocContainer));
      }

      _thuriaNancySettings = NancyStartup.IocContainer.GetInstance<IThuriaNancySettings>() ?? throw new ArgumentNullException(nameof(_thuriaNancySettings));
      _thuriaLogger        = NancyStartup.IocContainer.GetInstance<IThuriaLogger>() ?? throw new ArgumentNullException(nameof(_thuriaLogger));
    }

    /// <summary>
    /// Clean and Dispose the current object
    /// </summary>
    public void Dispose()
    {
      if (_isDisposing) { return; }
      _isDisposing = true;

      Stop();
    }

    /// <inheritdoc />
    public string Name { get; } = "Nancy Service";

    /// <inheritdoc />
    public void Start()
    {
      var startableServices = NancyStartup.IocContainer.GetAllInstances<IThuriaStartable>();
      foreach (var currentStartableService in startableServices)
      {
        _thuriaLogger.LogMessage(LogSeverity.Info, $"Starting {currentStartableService.GetType().Name} Service");
        currentStartableService.Start();
      }

      var hostBaseUri = _thuriaNancySettings.HostBaseUri;
      _thuriaLogger.LogMessage(LogSeverity.Info, $"Starting Nancy Service on {hostBaseUri}");

      _webHost = new WebHostBuilder()
                          .UseContentRoot(Directory.GetCurrentDirectory())
                          .UseKestrel()
                          .UseStartup<NancyStartup>()
                          .UseUrls(hostBaseUri)
                          .Build();
      _webHost.Run();
    }

    /// <inheritdoc />
    public void Stop()
    {
      var stoppableServices = NancyStartup.IocContainer.GetAllInstances<IThuriaStoppable>();
      foreach (var currentStoppableService in stoppableServices)
      {
        _thuriaLogger.LogMessage(LogSeverity.Info, $"Stopping {currentStoppableService.GetType().Name} Service");
        currentStoppableService.Stop();
      }

      if (_webHost == null) { return; }

      _webHost.StopAsync().Wait();
      _webHost.Dispose();
      _webHost = null;
    }
  }
}
