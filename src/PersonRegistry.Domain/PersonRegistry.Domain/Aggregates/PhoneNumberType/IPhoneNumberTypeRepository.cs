using PersonRegistry.Domain.Interfaces;

namespace PersonRegistry.Domain.Aggregates.PhoneNumberType;

/// <summary>
/// Repository interface for managing <see cref="PhoneNumberType"/> entities.
/// </summary>
public interface IPhoneNumberTypeRepository : IRepository<PhoneNumberType>
{
}