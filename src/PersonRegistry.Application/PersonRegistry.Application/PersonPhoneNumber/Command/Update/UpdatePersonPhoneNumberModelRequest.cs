namespace PersonRegistry.Application.PersonPhoneNumber.Command.Update;

/// <summary>
/// Represents a request to update the details of a phone number associated with a person.
/// </summary>
/// <param name="PersonId">The unique identifier of the person whose phone number is to be updated.</param>
/// <param name="PersonPhoneNumberId">The unique identifier of the phone number to be updated.</param>
/// <param name="PhoneNumberTypeId">The identifier of the new phone number type (e.g., mobile, home).</param>
/// <param name="Number">The new phone number to be associated with the person.</param>
public record UpdatePersonPhoneNumberModelRequest(
    int PersonId,
    int PersonPhoneNumberId,
    int PhoneNumberTypeId,
    string PhoneNumber
);