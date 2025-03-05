using MediatR;
using PersonRegistry.Common.Application.Interfaces;

namespace PersonRegistry.Application.PersonPhoneNumber.Command.Delete;

/// <summary>
/// Represents a command to delete a specific phone number from a person.
/// </summary>
/// <param name="PersonId">The unique identifier of the person who owns the phone number.</param>
/// <param name="PersonPhoneNumberId">The unique identifier of the phone number to be deleted.</param>
public record DeletePersonPhoneNumberCommand(
    int PersonId,
    int PersonPhoneNumberId
) : IRequest<Unit>, ITransactionalRequest;