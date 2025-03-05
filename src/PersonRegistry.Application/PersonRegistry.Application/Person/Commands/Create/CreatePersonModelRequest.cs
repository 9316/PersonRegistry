using PersonRegistry.Domain.Enums;

namespace PersonRegistry.Application.Person.Commands.Create;

/// <summary>
/// Represents the request model for creating a new person.
/// </summary>
/// <param name="Name">The first name of the person.</param>
/// <param name="LastName">The last name of the person.</param>
/// <param name="PersonalNumber">The personal identification number of the person.</param>
/// <param name="BirthDate">The birth date of the person.</param>
/// <param name="Gender">The gender of the person.</param>
/// <param name="CityId">The identifier for the city where the person lives.</param>
public record CreatePersonModelRequest(
    string Name,
    string LastName,
    string PersonalNumber,
    DateTime BirthDate,
    GenderEnum Gender,
    int CityId
);