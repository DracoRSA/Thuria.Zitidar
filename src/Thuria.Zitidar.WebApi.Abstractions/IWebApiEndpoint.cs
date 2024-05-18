using Microsoft.AspNetCore.Routing;

namespace Thuria.Zitidar.WebApi.Abstractions;

/// <summary>
/// Web API Endpoint
/// </summary>
public interface IWebApiEndpoint
{
    /// <summary>
    /// Map Endpoint
    /// </summary>
    /// <param name="endpointRouteBuilder">Endpoint Route Builder</param>
    void MapEndpoint(IEndpointRouteBuilder endpointRouteBuilder);
}