using MediatR;
using PersonRegistry.Application.Resources;
using PersonRegistry.Application.Services;
using PersonRegistry.Common.Exceptions;
using PersonRegistry.Domain.Interfaces;

namespace PersonRegistry.Application.Person.Commands.UploadPhoto;

/// <summary>
/// Handles the uploading of a person's photo.
/// </summary>
public class UploadPersonPhotoCommandHandler(IUnitOfWork _unitOfWork, IFileManagerService _fileManager) : IRequestHandler<UploadPersonPhotoCommand, Unit>
{

    /// <summary>
    /// Handles the request to upload a photo for a person.
    /// </summary>
    /// <param name="request">The upload person photo command.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A MediatR unit value.</returns>
    /// <exception cref="NotFoundException">Thrown when the person does not exist.</exception>
    public async Task<Unit> Handle(UploadPersonPhotoCommand request, CancellationToken cancellationToken)
    {
        var newPhotoUrl = string.Empty;

        var person = await _unitOfWork.PersonRepository.GetByIdAsync(request.PersonId) ??
            throw new NotFoundException(string.Format(ExceptionMessageResource.NotFound, nameof(Domain.Aggregates.Person.Person), request.PersonId));

        newPhotoUrl = !string.IsNullOrEmpty(person.Photo)
                      ? (newPhotoUrl = await _fileManager.ReplaceFileAsync(request.Photo, person.Photo))
                      : newPhotoUrl = await _fileManager.UploadFileAsync(request.Photo);

        person.UpdatePhotoUrl(newPhotoUrl);

        _unitOfWork.PersonRepository.Update(person);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
