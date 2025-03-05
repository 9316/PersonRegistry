using PersonRegistry.Application.Person.Queries.GetPerson;
using PersonRegistry.Application.Person.Queries.GetPersons;

namespace PersonRegistry.Application.Common.Mappers;

/// <summary>
/// Provides manual mapping between domain models and DTOs.
/// </summary>
public static class PersonMapper
{
    /// <summary>
    /// Maps a Person entity to GetPersonModelResponse DTO.
    /// </summary>
    /// <param name="person">The person entity.</param>
    /// <returns>The mapped DTO.</returns>
    public static GetPersonModelResponse ToPersonResponse(this Domain.Aggregates.Person.Person person)
    {
        ArgumentNullException.ThrowIfNull(person);

        var result = new GetPersonModelResponse(
            person.Id,
            person.Name,
            person.LastName,
            person.PersonalNumber,
            person.BirthDate,
            person.Gender,
            person.CityId,
            person.Photo,
            person.City.ToPersonCityResponse(),
            person.PhoneNumbers.Select(ToPersonPhoneNumberResponse).ToList(),
            person.RelatedPersons?.Select(ToPersonRelationResponse).ToList()
        );
        return result;
    }

    /// <summary>
    /// Maps a PersonRelation entity to PersonRelationResponse DTO.
    /// </summary>
    /// <param name="relation">The person relation entity.</param>
    /// <returns>The mapped DTO.</returns>
    public static PersonRelationResponse ToPersonRelationResponse(Domain.Aggregates.Person.PersonRelation.PersonRelation relation)
    {
        ArgumentNullException.ThrowIfNull(relation);

        return new PersonRelationResponse(
            relation.RelatedPerson.Id,
            relation.RelatedPerson.Name,
            relation.RelatedPerson.LastName,
            relation.RelatedPerson.Gender,
            relation.RelatedPerson.PersonalNumber,
            relation.RelatedPerson.BirthDate);
    }

    /// <summary>
    /// Maps a PersonPhoneNumber entity to PersonPhoneNumberResponse DTO.
    /// </summary>
    /// <param name="phoneNumber">The phone number entity.</param>
    /// <returns>The mapped DTO.</returns>
    public static PersonPhoneNumberResponse ToPersonPhoneNumberResponse(Domain.Aggregates.Person.PersonPhoneNumber phoneNumber)
    {
        ArgumentNullException.ThrowIfNull(phoneNumber);

        return new PersonPhoneNumberResponse(
            phoneNumber.Id,
            phoneNumber.PhoneNumber,
            phoneNumber.PhoneNumberType?.Name);
    }

    /// <summary>
    /// Maps a collection of Person entities to a list of GetPersonsModelResponse DTOs.
    /// </summary>
    /// <param name="people">The collection of persons.</param>
    /// <returns>A list of mapped DTOs.</returns>
    public static List<GetPersonsModelResponse> MapToPersonsResponse(IEnumerable<Domain.Aggregates.Person.Person> people)
    {
        ArgumentNullException.ThrowIfNull(people);

        return people.Select(person => new GetPersonsModelResponse
        (
            person.Id,
            person.Name,
            person.LastName,
            person.PersonalNumber,
            person.Photo,
            person.BirthDate,
            person.Gender
        )).ToList();
    }

    /// <summary>
    /// Maps a PersonCity entity to PersonCityResponse DTO.
    /// </summary>
    /// <param name="city">The person city entity.</param>
    /// <returns>The mapped DTO.</returns>
    private static PersonCityResponse ToPersonCityResponse(this Domain.Aggregates.City.City? city)
    {
        ArgumentNullException.ThrowIfNull(city);

        return new PersonCityResponse(city.Id, city.Name);
    }
}