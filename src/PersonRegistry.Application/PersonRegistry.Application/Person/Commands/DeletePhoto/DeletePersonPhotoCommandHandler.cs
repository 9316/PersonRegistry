using MediatR;
using PersonRegistry.Application.Resources;
using PersonRegistry.Application.Services;
using PersonRegistry.Common.Exceptions;
using PersonRegistry.Domain.Interfaces;

namespace PersonRegistry.Application.Person.Commands.DeletePhoto;

/// <summary>
/// Handles the deletion of a person's profile photo.
/// </summary>
/// <param name="_unitOfWork">Unit of work for data access.</param>
/// <param name="_fileManager">File manager service for handling file operations.</param>
public class DeletePersonPhotoCommandHandler(IUnitOfWork _unitOfWork, IFileManagerService _fileManager) : IRequestHandler<DeletePersonPhotoCommand, Unit>
{
    /// <summary>
    /// Handles the request to delete a person's profile photo.
    /// </summary>
    /// <param name="request">The delete command containing the person's ID.</param>
    /// <param name="cancellationToken">A token to observe while waiting for the task to complete.</param>
    /// <returns>A <see cref="Unit"/> indicating the operation was successful.</returns>
    /// <exception cref="NotFoundException">
    /// Thrown if a person with the specified ID is not found.
    /// </exception>
    public async Task<Unit> Handle(DeletePersonPhotoCommand request, CancellationToken cancellationToken)
    {
        var person = await _unitOfWork.PersonRepository.GetByIdAsync(request.PersonId) ??
            throw new NotFoundException(string.Format(ExceptionMessageResource.NotFound, nameof(Domain.Aggregates.Person.Person), request.PersonId));

        if (!string.IsNullOrEmpty(person.Photo))
        {
            await _fileManager.DeleteFileAsync(person.Photo);

            person.DeletePhoto();

            _unitOfWork.PersonRepository.Update(person);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        return Unit.Value;
    }
}