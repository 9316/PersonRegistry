using PersonRegistry.Domain.Enums;

namespace PersonRegistry.Application.Person.Queries.GetPerson;

/// <summary>
/// Represents a response containing information about a related person.
/// </summary>
/// <param name="Id">The unique identifier of the related person.</param>
/// <param name="Name">The first name of the related person.</param>
/// <param name="LastName">The last name of the related person.</param>
/// <param name="Gender">The gender of the related person.</param>
/// <param name="PersonalNumber">The personal identification number of the related person.</param>
/// <param name="BirthDate">The birth date of the related person.</param>
public record PersonRelationResponse(
    int Id,
    string Name,
    string LastName,
    GenderEnum Gender,
    string PersonalNumber,
    DateTime BirthDate
);