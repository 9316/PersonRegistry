using MediatR;
using Microsoft.AspNetCore.Http;
using PersonRegistry.Common.Application.Interfaces;

namespace PersonRegistry.Application.Person.Commands.UploadPhoto;

/// <summary>
/// Command to upload a photo for a person.
/// </summary>
/// <param name="PersonId">The unique identifier of the person.</param>
/// <param name="Photo">The uploaded photo file.</param>
public record UploadPersonPhotoCommand(int PersonId, IFormFile Photo) : IRequest<Unit>, ITransactionalRequest;