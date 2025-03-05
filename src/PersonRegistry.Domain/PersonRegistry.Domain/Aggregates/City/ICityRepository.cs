using PersonRegistry.Domain.Interfaces;

namespace PersonRegistry.Domain.Aggregates.City;

/// <summary>
/// Repository interface for managing <see cref="City"/> entities.
/// </summary>
public interface ICityRepository : IRepository<City>
{
}