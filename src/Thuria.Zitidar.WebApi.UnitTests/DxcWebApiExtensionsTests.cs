using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

using NUnit.Framework;
using FluentAssertions;
using Thuria.Zitidar.WebApi.Abstractions;

namespace Thuria.Zitidar.WebApi.UnitTests;

[TestFixture]
public class DxcWebApiExtensionsTests
{
    [Test]
    public void AddEndpoints_GivenValidAssembly_ShouldAddEndpointsToServiceCollection()
    {
        // Arrange
        var serviceCollection = new ServiceCollection();

        // Act
        serviceCollection.AddEndpoints(typeof(DxcWebApiExtensionsTests).Assembly);

        // Assert
        var serviceProvider = serviceCollection.BuildServiceProvider();
        var dxcEndpoints    = serviceProvider.GetServices<IWebApiEndpoint>();

        var foundEndpoints = dxcEndpoints as IWebApiEndpoint[] ?? dxcEndpoints.ToArray();
        foundEndpoints.Should().NotBeNullOrEmpty();
        foundEndpoints.Length.Should().Be(2);
    }

    [Test]
    public void MapEndpoints_GivenEndpoints_ShouldMapEndpointsToWebApplication()
    {
        // Arrange
        var builder = WebApplication.CreateBuilder();
        builder.Services.AddEndpoints(typeof(DxcWebApiExtensionsTests).Assembly);
        var webApplication = builder.Build();

        // Act
        webApplication.MapEndpoints();

        // Assert
        webApplication.Services.GetServices<IWebApiEndpoint>().Should().NotBeNullOrEmpty();
        webApplication.Services.GetServices<IWebApiEndpoint>().Count().Should().Be(2);
    }
}