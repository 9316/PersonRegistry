namespace PersonRegistry.Application.PersonPhoneNumber.Command.Create;

/// <summary>
/// Represents a request to add a phone number to a specific person.
/// </summary>
/// <param name="PersonId">The unique identifier of the person to whom the phone number will be added.</param>
/// <param name="PhoneNumberTypeId">The identifier of the phone number type (e.g., mobile, home, work).</param>
/// <param name="PhoneNumber">The phone number to be added to the person.</param>
public record CreatePersonPhoneNumberModelRequest(
    int PersonId,
    int PhoneNumberTypeId,
    string PhoneNumber
);