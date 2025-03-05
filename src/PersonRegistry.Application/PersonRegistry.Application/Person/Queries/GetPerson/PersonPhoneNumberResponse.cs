namespace PersonRegistry.Application.Person.Queries.GetPerson;
using System.ComponentModel;

/// <summary>
/// Represents a response containing phone number details associated with a person.
/// </summary>
/// <param name="Id">The unique identifier of the phone number.</param>
/// <param name="PhoneNumber">The phone number of the person.</param>
/// <param name="PhoneNumberType">The type of the phone number (e.g., Mobile, Home, Work).</param>

[Category("Response")]
public record PersonPhoneNumberResponse(int Id, string PhoneNumber, string PhoneNumberType);