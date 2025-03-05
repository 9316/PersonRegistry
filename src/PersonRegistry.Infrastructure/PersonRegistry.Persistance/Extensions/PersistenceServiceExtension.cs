using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PersonRegistry.Domain.Aggregates.City;
using PersonRegistry.Domain.Aggregates.Person;
using PersonRegistry.Domain.Aggregates.Person.PersonRelation;
using PersonRegistry.Domain.Aggregates.PersonRelationType;
using PersonRegistry.Domain.Aggregates.PhoneNumberType;
using PersonRegistry.Domain.Interfaces;
using PersonRegistry.Persistence.Context;
using PersonRegistry.Persistence.Repositories;
using PersonRegistry.Persistence.Repositories.UnitOfWork;

namespace PersonRegistry.Persistance.Extensions;

/// <summary>
/// Provides extension methods for configuring persistence services.
/// </summary>
public static class PersistenceServiceExtension
{
    /// <summary>
    /// Adds database and repository services to the application's dependency injection container.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="configuration">The application configuration containing database connection settings.</param>
    /// <returns>The modified service collection.</returns>
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services,
    IConfiguration configuration)
    {
        services.AddDbContext<PersonRegistryDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("PersonRegistry")));

        // Register database initializer
        services.AddScoped<PersonRegistryDbInitializer>();

        // Register Unit of Work
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        // Register repositories
        services.AddScoped<ICityRepository, CityRepository>();
        services.AddScoped<IPersonRelationRepository, PersonRelationRepository>();
        services.AddScoped<IPersonRepository, PersonRepository>();
        services.AddScoped<IPersonRelationTypeRepository, PersonRelationTypeRepository>();
        services.AddScoped<IPhoneNumberTypeRepository, PhoneNumberTypeRepository>();

        return services;
    }
}