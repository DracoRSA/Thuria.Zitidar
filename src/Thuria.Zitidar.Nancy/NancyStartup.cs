using Nancy.Owin;
using Nancy.Bootstrapper;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

using Thuria.Zitidar.Core;

namespace Thuria.Zitidar.Nancy
{
  /// <summary>
  /// Basic Nancy Startup class
  /// </summary>
  public class NancyStartup
  {
    /// <summary>
    /// Thuria Ioc Container
    /// </summary>
    public static IThuriaIocContainer IocContainer { get; set; }

    /// <summary>
    /// Configure the relevant Service(s)
    /// </summary>
    /// <param name="services"></param>
    public virtual void ConfigureServices(IServiceCollection services)
    {
    }

    /// <summary>
    /// Configure the Container
    /// </summary>
    /// <param name="applicationBuilder">Application Builder</param>
    /// <param name="hostingEnvironment">Hosting Environment</param>
    /// <param name="loggerFactory">Logger Factory</param>
    public virtual void Configure(IApplicationBuilder applicationBuilder, IHostingEnvironment hostingEnvironment, ILoggerFactory loggerFactory)
    {
      var nancyBootstrapper = NancyStartup.IocContainer.GetInstance<INancyBootstrapper>();
      applicationBuilder.UseOwin(action => action.UseNancy(options => options.Bootstrapper = nancyBootstrapper));
    }
  }
}
