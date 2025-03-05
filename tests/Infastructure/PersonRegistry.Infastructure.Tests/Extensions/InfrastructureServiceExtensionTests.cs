using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Moq;
using PersonRegistry.Application.Common.Options;
using PersonRegistry.Application.Services;
using PersonRegistry.Infrastructure.Extensions;
using Microsoft.AspNetCore.Hosting;
using PersonRegistry.Infrastructure.Services;

namespace PersonRegistry.Infrastructure.Tests.Extensions;

/// <summary>
/// Unit tests for the <see cref="InfrastructureServiceExtensions"/> class.
/// </summary>
public class InfrastructureServiceExtensionsTests
{
    [Fact]
    public void AddInfrastructureServices_ShouldRegisterDependencies()
    {
        // Arrange
        var services = new ServiceCollection();

        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string>
            {
               { "FileManagerOptions:PhotoUrlLocation", "/uploads" }
            })
            .Build();

        var mockEnvironment = new Mock<IHostingEnvironment>();
        mockEnvironment.Setup(env => env.ContentRootPath).Returns("/app-root");

        services.AddSingleton(mockEnvironment.Object);

        // Act
        services.AddInfrastructureServices(configuration);
        var serviceProvider = services.BuildServiceProvider();

        // Assert
        var fileManagerService = serviceProvider.GetService<IFileManagerService>();
        var fileManagerOptions = serviceProvider.GetService<IOptions<FileManagerOptions>>();

        fileManagerService.Should().NotBeNull().And.BeOfType<FileManagerService>();
        fileManagerOptions.Should().NotBeNull();
        fileManagerOptions.Value.PhotoUrlLocation.Should().Be("/uploads");
    }


    [Fact]
    public void AddInfrastructureServices_ShouldThrowException_WhenConfigurationIsNull()
    {
        // Arrange
        var serviceCollection = new ServiceCollection();

        // Act
        Action act = () => serviceCollection.AddInfrastructureServices(null);

        // Assert
        act.Should().Throw<NullReferenceException>();
    }
}
