using PersonRegistry.Domain.Interfaces;

namespace PersonRegistry.Domain.Aggregates.PersonRelationType;

/// <summary>
/// Repository interface for managing <see cref="PersonRelationType"/> entities.
/// </summary>
public interface IPersonRelationTypeRepository : IRepository<PersonRelationType>
{
}