using PersonRegistry.Domain.Aggregates.PersonRelationType;
using PersonRegistry.Persistence.Context;
using PersonRegistry.Persistence.Repositories.Base;

namespace PersonRegistry.Persistence.Repositories;

/// <inheritdoc cref="IPersonRelationTypeRepository"/>
internal class PersonRelationTypeRepository : Repository<PersonRelationType>, IPersonRelationTypeRepository
{
    public PersonRelationTypeRepository(PersonRegistryDbContext context)
        : base(context)
    {
    }
}
