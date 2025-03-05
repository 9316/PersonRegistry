using Microsoft.EntityFrameworkCore;
using PersonRegistry.Domain.Aggregates.Person.PersonRelation;
using PersonRegistry.Persistence.Context;
using PersonRegistry.Persistence.Repositories.Base;

namespace PersonRegistry.Persistence.Repositories;

/// <inheritdoc cref="IPersonRelationRepository"/>
internal class PersonRelationRepository(PersonRegistryDbContext context) : Repository<PersonRelation>(context), IPersonRelationRepository
{
    /// <inheritdoc cref="IPersonRelationRepository.GetAllRelations"/>
    public IQueryable<PersonRelation> GetAllRelations() =>
        context.PersonRelations
        .Include(x => x.Person)
        .Include(x => x.PersonRelationType)
        .Include(x => x.RelatedPerson);
}
