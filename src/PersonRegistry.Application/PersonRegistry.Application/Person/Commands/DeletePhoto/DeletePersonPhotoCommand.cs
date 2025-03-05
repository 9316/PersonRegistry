using MediatR;
using PersonRegistry.Common.Application.Interfaces;

namespace PersonRegistry.Application.Person.Commands.DeletePhoto;

/// <summary>
/// Command to delete a person's photo.
/// </summary>
/// <param name="PersonId">The ID of the person whose photo is to be deleted.</param>
public record DeletePersonPhotoCommand(int PersonId) : IRequest<Unit>, ITransactionalRequest;