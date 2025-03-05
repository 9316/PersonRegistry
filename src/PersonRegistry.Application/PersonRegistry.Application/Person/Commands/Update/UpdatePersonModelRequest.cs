using PersonRegistry.Domain.Enums;

namespace PersonRegistry.Application.Person.Commands.Update;

/// <summary>
/// Represents a request to update the information of an existing person identified by their unique identifier.
/// </summary>
/// <param name="Id">The unique identifier of the person to be updated.</param>
/// <param name="Name">The updated name of the person.</param>
/// <param name="LastName">The updated last name of the person.</param>
/// <param name="PersonalNumber">The updated personal number of the person.</param>
/// <param name="BirthDate">The updated birth date of the person.</param>
/// <param name="Gender">The updated gender of the person.</param>
/// <param name="CityId">The updated city identifier where the person resides.</param>
public record UpdatePersonModelRequest(
    int Id,
    string Name,
    string LastName,
    string PersonalNumber,
    DateTime BirthDate,
    GenderEnum Gender,
    int CityId
);