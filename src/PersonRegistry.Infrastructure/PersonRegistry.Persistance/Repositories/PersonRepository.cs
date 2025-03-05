using Microsoft.EntityFrameworkCore;
using PersonRegistry.Domain.Aggregates.Person;
using PersonRegistry.Persistence.Context;
using PersonRegistry.Persistence.Repositories.Base;

namespace PersonRegistry.Persistence.Repositories;

/// <inheritdoc cref="IPersonRepository"/>
public class PersonRepository(PersonRegistryDbContext context) : Repository<Person>(context), IPersonRepository
{
    /// <inheritdoc cref="IPersonRepository.GetDetailsByIdAsync"/>
    public async Task<Person?> GetDetailsByIdAsync(int Id)
    {
        return await context.Persons.Where(x => x.Id == Id)
           .Include(x => x.City)
           .Include(x => x.RelatedPersons).ThenInclude(x => x.RelatedPerson)
           .Include(x => x.PhoneNumbers).ThenInclude(x => x.PhoneNumberType)
           .FirstOrDefaultAsync();
    }
}
