using MediatR;
using PersonRegistry.Common.Application.Interfaces;

namespace PersonRegistry.Application.Person.Commands.Delete;

/// <summary>
/// Command to delete a person by their ID.
/// </summary>
/// <param name="Id">The ID of the person to be deleted.</param>
public record DeletePersonCommand(int Id) : IRequest<Unit>, ITransactionalRequest;