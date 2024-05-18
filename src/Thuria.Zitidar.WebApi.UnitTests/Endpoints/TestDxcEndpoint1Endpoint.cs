using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

using Thuria.Zitidar.WebApi.Abstractions;

namespace Thuria.Zitidar.WebApi.UnitTests.Endpoints;

public class TestDxcEndpoint1Endpoint : IWebApiEndpoint
{
    /// <inheritdoc />
    public void MapEndpoint(IEndpointRouteBuilder endpointRouteBuilder)
    {
        endpointRouteBuilder.MapGet("/api/test1",
                                    async context =>
                                    {
                                        await context.Response.WriteAsJsonAsync(new { Message = "Zitidar Test Endpoint 1" });
                                    });
    }
}