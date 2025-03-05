using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PersonRegistry.Application.Common.Options;
using PersonRegistry.Application.Services;
using PersonRegistry.Infrastructure.Services;

namespace PersonRegistry.Infrastructure.Extensions;

/// <summary>
/// Provides extension methods to register infrastructure services.
/// </summary>
public static class InfrastructureServiceExtensions
{
    /// <summary>
    /// Registers infrastructure services in the dependency injection container.
    /// </summary>
    /// <param name="services">The service collection to configure.</param>
    /// <param name="configuration">The application configuration.</param>
    /// <returns>The modified service collection.</returns>
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<FileManagerOptions>(configuration.GetSection("FileManagerOptions"));
        services.AddScoped<IFileManagerService, FileManagerService>();

        return services;
    }
}
