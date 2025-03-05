using MediatR;
using PersonRegistry.Common.Application.Interfaces;

namespace PersonRegistry.Application.PersonPhoneNumber.Command.Create;

/// <summary>
/// Represents a command to create a phone number for a person.
/// </summary>
/// <param name="PersonId">The unique identifier of the person.</param>
/// <param name="PhoneNumberTypeId">The type of phone number (e.g., Mobile, Home, Work).</param>
/// <param name="PhoneNumber">The actual phone number in string format.</param>
public record CreatePersonPhoneNumberCommand(
    int PersonId,
    int PhoneNumberTypeId,
    string PhoneNumber
) : IRequest<Unit>, ITransactionalRequest;