namespace PersonRegistry.Application.PersonPhoneNumber.Command.Delete;

/// <summary>
/// Represents a request to delete a specific phone number associated with a person.
/// </summary>
/// <param name="PersonId">The unique identifier of the person from whom the phone number will be deleted.</param>
/// <param name="PersonPhoneNumberId">The unique identifier of the phone number to be deleted.</param>
public record DeletePersonPhoneNumberModelRequest(
    int PersonId,
    int PersonPhoneNumberId
);