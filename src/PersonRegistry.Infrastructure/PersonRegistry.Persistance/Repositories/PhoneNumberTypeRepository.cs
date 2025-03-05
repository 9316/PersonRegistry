using PersonRegistry.Domain.Aggregates.PhoneNumberType;
using PersonRegistry.Persistence.Context;
using PersonRegistry.Persistence.Repositories.Base;

namespace PersonRegistry.Persistence.Repositories;

/// <inheritdoc cref="IPhoneNumberTypeRepository"/>
internal class PhoneNumberTypeRepository : Repository<PhoneNumberType>, IPhoneNumberTypeRepository
{
    public PhoneNumberTypeRepository(PersonRegistryDbContext context)
        : base(context)
    {
    }
}
