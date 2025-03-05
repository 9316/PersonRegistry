using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PersonRegistry.Domain.Aggregates.City;
using PersonRegistry.Domain.Aggregates.PersonRelationType;
using PersonRegistry.Domain.Aggregates.PhoneNumberType;

namespace PersonRegistry.Persistence.Context;

/// <summary>
/// Initializes the Person Registry database with default seed data.
/// </summary>
/// <param name="_dbContext">The database context.</param>
/// <param name="_logger">The logger for error handling.</param>
public class PersonRegistryDbInitializer(PersonRegistryDbContext _dbContext, ILogger<PersonRegistryDbInitializer> _logger)
{
    /// <summary>
    /// Seeds initial data into the database if it is empty.
    /// </summary>
    public async Task InitialDataAsync()
    {
        try
        {
            await _dbContext.Database.MigrateAsync();

            if (!await _dbContext.Cities.AnyAsync())
            {
                var cities = new List<City>()
                {
                    City.Create("Tbilisi"),
                    City.Create("Batumi"),
                    City.Create("Kutaisi"),
                    City.Create("Sachkhere"),
                    City.Create("Chiatura"),
                    City.Create("Telavi")
                };

                await _dbContext.Cities.AddRangeAsync(cities);
            }

            if (!await _dbContext.PersonRelationTypes.AnyAsync())
            {
                var cities = new List<PersonRelationType>()
                {
                    PersonRelationType.Create("Colleague"),
                    PersonRelationType.Create("Familliar"),
                    PersonRelationType.Create("Relative"),
                    PersonRelationType.Create("Other"),
                };

                await _dbContext.PersonRelationTypes.AddRangeAsync(cities);
            }

            if (!await _dbContext.PhoneNumberTypes.AnyAsync())
            {
                var cities = new List<PhoneNumberType>()
                {
                    PhoneNumberType.Create("Personal"),
                    PhoneNumberType.Create("Office"),
                    PhoneNumberType.Create("Home"),
                };

                await _dbContext.PhoneNumberTypes.AddRangeAsync(cities);
            }

            await _dbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"An Error occured while inserting initial values in database. message:{ex.Message}");
            throw;
        }
    }

}