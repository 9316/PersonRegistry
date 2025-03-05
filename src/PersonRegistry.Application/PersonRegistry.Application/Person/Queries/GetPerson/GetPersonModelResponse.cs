using PersonRegistry.Domain.Enums;

namespace PersonRegistry.Application.Person.Queries.GetPerson;

/// <summary>
/// Represents the response model for retrieving detailed information about a person.
/// </summary>
/// <param name="PersonId">The unique identifier of the person.</param>
/// <param name="Name">The first name of the person.</param>
/// <param name="LastName">The last name of the person.</param>
/// <param name="PersonalNumber">The personal identification number of the person.</param>
/// <param name="BirthDate">The birth date of the person.</param>
/// <param name="Gender">The gender of the person.</param>
/// <param name="CityId">The unique identifier of the city where the person resides.</param>
/// <param name="Photo">The URL or path to the person's photo.</param>
/// <param name="City">The details of the city where the person resides.</param>
/// <param name="PhoneNumbers">The list of phone numbers associated with the person.</param>
/// <param name="RelatedPersons">The list of related persons.</param>
public record GetPersonModelResponse(
    int PersonId,
    string Name,
    string LastName,
    string PersonalNumber,
    DateTime BirthDate,
    GenderEnum Gender,
    int CityId,
    string Photo,
    PersonCityResponse? City,
    IList<PersonPhoneNumberResponse> PhoneNumbers,
    IList<PersonRelationResponse> RelatedPersons
);