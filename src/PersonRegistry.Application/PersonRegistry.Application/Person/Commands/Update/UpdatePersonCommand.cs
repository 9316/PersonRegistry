using MediatR;
using PersonRegistry.Common.Application.Interfaces;
using PersonRegistry.Domain.Enums;

namespace PersonRegistry.Application.Person.Commands.Update;

/// <summary>
/// Command to update an existing person.
/// </summary>
/// <param name="Id">The unique identifier of the person.</param>
/// <param name="Name">The first name of the person.</param>
/// <param name="LastName">The last name of the person.</param>
/// <param name="PersonalNumber">The personal identification number of the person.</param>
/// <param name="BirthDate">The birth date of the person.</param>
/// <param name="Gender">The gender of the person.</param>
/// <param name="CityId">The ID of the city where the person resides.</param>
public record UpdatePersonCommand(
    int Id,
    string Name,
    string LastName,
    string PersonalNumber,
    DateTime BirthDate,
    GenderEnum Gender,
    int CityId
) : IRequest<Unit>, ITransactionalRequest;