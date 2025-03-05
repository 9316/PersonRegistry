using MediatR;
using PersonRegistry.Common.Application.Interfaces;

namespace PersonRegistry.Application.City.Commands.Delete;

/// <summary>
/// Command to delete a city by its ID.
/// </summary>
/// <param name="Id">The ID of the city to be deleted.</param>
public record DeleteCityCommand(int Id) : IRequest<Unit>, ITransactionalRequest;