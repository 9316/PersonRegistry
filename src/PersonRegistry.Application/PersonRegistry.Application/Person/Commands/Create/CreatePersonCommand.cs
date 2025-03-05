using MediatR;
using PersonRegistry.Common.Application.Interfaces;
using PersonRegistry.Domain.Enums;

namespace PersonRegistry.Application.Person.Commands.Create;

/// <summary>
/// Command for creating a new person.
/// </summary>
/// <param name="Name">The first name of the person.</param>
/// <param name="LastName">The last name of the person.</param>
/// <param name="PersonalNumber">The unique personal identification number.</param>
/// <param name="BirthDate">The birth date of the person.</param>
/// <param name="Gender">The gender of the person.</param>
/// <param name="CityId">The ID of the city where the person is registered.</param>
public record CreatePersonCommand(
    string Name,
    string LastName,
    string PersonalNumber,
    DateTime BirthDate,
    GenderEnum Gender,
    int CityId
) : IRequest<int>, ITransactionalRequest;

