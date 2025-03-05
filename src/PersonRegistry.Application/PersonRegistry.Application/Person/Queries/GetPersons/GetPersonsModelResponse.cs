using PersonRegistry.Domain.Enums;

namespace PersonRegistry.Application.Person.Queries.GetPersons;

/// <summary>
/// Represents a response model containing basic person details.
/// </summary>
/// <param name="PersonId">The unique identifier of the person.</param>
/// <param name="Name">The first name of the person.</param>
/// <param name="LastName">The last name of the person.</param>
/// <param name="PersonalNumber">The personal identification number of the person.</param>
/// <param name="Photo">The URL or file path of the person's photo.</param>
/// <param name="BirthDate">The birth date of the person.</param>
/// <param name="Gender">The gender of the person.</param>
public record GetPersonsModelResponse(
    int PersonId,
    string Name,
    string LastName,
    string PersonalNumber,
    string Photo,
    DateTime BirthDate,
    GenderEnum Gender
);