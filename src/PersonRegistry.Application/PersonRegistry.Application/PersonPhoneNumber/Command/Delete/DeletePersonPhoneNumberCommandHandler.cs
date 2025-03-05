using MediatR;
using PersonRegistry.Application.Resources;
using PersonRegistry.Common.Exceptions;
using PersonRegistry.Domain.Interfaces;

namespace PersonRegistry.Application.PersonPhoneNumber.Command.Delete;

/// <summary>
/// Handles the deletion of a phone number from a person's record.
/// </summary>
public class DeletePersonPhoneNumberCommandHandler(IUnitOfWork _unitOfWork) : IRequestHandler<DeletePersonPhoneNumberCommand, Unit>
{

    /// <summary>
    /// Handles the command to delete a phone number from a person's record.
    /// </summary>
    /// <param name="request">The request containing the person ID and phone number ID.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A unit result indicating successful execution.</returns>
    /// <exception cref="NotFoundException">Thrown when the person or phone number is not found.</exception>
    public async Task<Unit> Handle(DeletePersonPhoneNumberCommand request, CancellationToken cancellationToken)
    {
        var person = await _unitOfWork.PersonRepository.GetDetailsByIdAsync(request.PersonId) ??
            throw new NotFoundException(string.Format(ExceptionMessageResource.NotFound, nameof(Domain.Aggregates.Person.Person), request.PersonId));

        person.DeleteNumber(request.PersonPhoneNumberId);

        _unitOfWork.PersonRepository.Update(person);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}