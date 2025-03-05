using PersonRegistry.Domain.Aggregates.City;
using PersonRegistry.Persistence.Context;
using PersonRegistry.Persistence.Repositories.Base;

namespace PersonRegistry.Persistence.Repositories;

/// <inheritdoc cref="ICityRepository"/>
internal class CityRepository : Repository<City>, ICityRepository
{
    public CityRepository(PersonRegistryDbContext context)
        : base(context)
    {
    }
}
