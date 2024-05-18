using System.Reflection;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

using Thuria.Zitidar.WebApi.Abstractions;

namespace Thuria.Zitidar.WebApi;

/// <summary>
/// Web API Extensions
/// </summary>
public static class WebApiExtensions
{
    /// <summary>
    /// Add Endpoints from the specified assembly to the Service Collection
    /// </summary>
    /// <param name="serviceCollection">Service Collection</param>
    /// <param name="assemblyToScan">Assembly to be scanned for Endpoint definitions</param>
    /// <returns>
    /// Configured Service Collection with Endpoints from the given Assembly
    /// </returns>
    public static IServiceCollection AddEndpoints(this IServiceCollection serviceCollection, Assembly assemblyToScan)
    {
        // Scan for all types that implements IDxcEndpoint in the given Assembly
        var serviceDescriptors = assemblyToScan.DefinedTypes
                                               .Where(type => type is { IsAbstract: false, IsInterface: false } &&
                                                              type.IsAssignableTo(typeof(IWebApiEndpoint)))
                                               .Select(type => ServiceDescriptor.Transient(typeof(IWebApiEndpoint), type))
                                               .ToArray();

        // Add the service descriptors to the service collection
        serviceCollection.TryAddEnumerable(serviceDescriptors);

        return serviceCollection;
    }

#if NET6_0
    /// <summary>
    /// Map Endpoints to the Web Application
    /// </summary>
    /// <param name="webApplication">Web Application</param>
    /// <returns>
    /// Configured Application Builder
    /// </returns>
    public static IApplicationBuilder MapEndpoints(this WebApplication webApplication)
    {
        // Search for all the registered DXC Endpoints and map them to the Web Application / Route Group
        webApplication.Services
                      .GetServices<IWebApiEndpoint>()
                      .ToList()
                      .ForEach(endpoint => endpoint.MapEndpoint(webApplication));
        return webApplication;
    }
#else
        /// <summary>
        /// Map Endpoints to the Web Application
        /// </summary>
        /// <param name="webApplication">Web Application</param>
        /// <param name="routeGroupBuilder">
        /// Route Group Builder
        /// Use this when a Versioned Group is created
        /// </param>
        /// <returns>
        /// Configured Application Builder
        /// </returns>
        public static IApplicationBuilder MapEndpoints(this WebApplication webApplication, 
                                                       RouteGroupBuilder? routeGroupBuilder = null)
        {
            // Search for all the registered DXC Endpoints and map them to the Web Application / Route Group
            webApplication.Services
                          .GetServices<IWebApiEndpoint>()
                          .ToList()
                          .ForEach(endpoint => endpoint.MapEndpoint(routeGroupBuilder is null ? webApplication : routeGroupBuilder));
            return webApplication;
        }
#endif
}