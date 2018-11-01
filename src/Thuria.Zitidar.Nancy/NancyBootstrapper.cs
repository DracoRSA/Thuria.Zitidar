using Nancy;
using StructureMap;
using Nancy.Bootstrapper;
using Nancy.Configuration;

namespace Thuria.Zitidar.Nancy
{
  /// <summary>
  /// Nancy Bootstrapper
  /// </summary>
  public class NancyBootstrapper : StructureMapNancyBootstrapper
  {
    private readonly bool _enableTracing;

    /// <summary>
    /// Nancy Bootstrapper Constructor
    /// </summary>
    /// <param name="structuremapContainer"></param>
    /// <param name="enableTracing"></param>
    public NancyBootstrapper(IContainer structuremapContainer, bool enableTracing = false)
      : base(structuremapContainer)
    {
      _enableTracing = enableTracing;
    }

    /// <summary>
    /// Configure the Nancy Bootstrapper
    /// </summary>
    /// <param name="environment"></param>
    public override void Configure(INancyEnvironment environment)
    {
      if (_enableTracing)
      {
        environment.Tracing(enabled: true, displayErrorTraces: true);
      }
    }

    /// <summary>
    /// Request Startup
    /// </summary>
    /// <param name="container">Structuremap Container</param>
    /// <param name="pipelines">Nancy Pipelines</param>
    /// <param name="context">Nancy Context</param>
    protected override void RequestStartup(IContainer container, IPipelines pipelines, NancyContext context)
    {
      pipelines.OnError += (ctx, exception) =>
        {
          ctx.Items.Add("OnErrorContextHook", exception);
          return null;
        };
    }
  }
}
