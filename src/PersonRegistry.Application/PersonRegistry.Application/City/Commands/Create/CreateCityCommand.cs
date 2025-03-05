using MediatR;
using PersonRegistry.Common.Application.Interfaces;

namespace PersonRegistry.Application.City.Commands.Create;

/// <summary>
/// Command to create a new city.
/// </summary>
/// <param name="Name">The name of the city.</param>
public record CreateCityCommand(string Name) : IRequest<int>, ITransactionalRequest;