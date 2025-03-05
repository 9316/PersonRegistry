using PersonRegistry.Domain.Interfaces;

namespace PersonRegistry.Domain.Aggregates.Person;

/// <summary>
/// Repository interface for managing person-related data.
/// </summary>
public interface IPersonRepository : IRepository<Person>
{
    /// <summary>
    /// Retrieves detailed information about a person by their ID.
    /// </summary>
    /// <param name="id">The unique identifier of the person.</param>
    /// <returns>A task representing the asynchronous operation. The task result contains the person details if found, otherwise null.</returns>
    Task<Person?> GetDetailsByIdAsync(int id);
}