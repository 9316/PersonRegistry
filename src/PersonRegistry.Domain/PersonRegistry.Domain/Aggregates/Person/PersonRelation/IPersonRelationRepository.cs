using PersonRegistry.Domain.Interfaces;

namespace PersonRegistry.Domain.Aggregates.Person.PersonRelation;

/// <summary>
/// Repository interface for managing person relations.
/// </summary>
public interface IPersonRelationRepository : IRepository<PersonRelation>
{
    /// <summary>
    /// Retrieves all person relations as an asynchronous operation.
    /// </summary>
    /// <returns>An <see cref="IQueryable{T}"/> representing all person relations.</returns>
    IQueryable<PersonRelation> GetAllRelations();
}
