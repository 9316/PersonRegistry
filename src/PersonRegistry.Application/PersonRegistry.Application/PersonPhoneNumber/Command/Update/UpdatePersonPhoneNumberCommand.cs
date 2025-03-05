using MediatR;
using PersonRegistry.Common.Application.Interfaces;

namespace PersonRegistry.Application.PersonPhoneNumber.Command.Update;

/// <summary>
/// Represents a command to update an existing phone number associated with a person.
/// </summary>
/// <param name="PersonId">The unique identifier of the person whose phone number will be updated.</param>
/// <param name="PersonPhoneNumberId">The unique identifier of the phone number to be updated.</param>
/// <param name="PhoneNumberTypeId">The new phone number type (e.g., mobile, home, work).</param>
/// <param name="Number">The updated phone number.</param>
public record UpdatePersonPhoneNumberCommand(
    int PersonId,
    int PersonPhoneNumberId,
    int PhoneNumberTypeId,
    string PhoneNumber
) : IRequest<Unit>, ITransactionalRequest;