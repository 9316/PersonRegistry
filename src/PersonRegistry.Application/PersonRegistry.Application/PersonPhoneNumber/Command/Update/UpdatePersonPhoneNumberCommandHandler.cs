using MediatR;
using PersonRegistry.Application.Resources;
using PersonRegistry.Common.Exceptions;
using PersonRegistry.Domain.Interfaces;

namespace PersonRegistry.Application.PersonPhoneNumber.Command.Update;

/// <summary>
/// Handles updating an existing phone number associated with a person.
/// </summary>
public class UpdatePersonPhoneNumberCommandHandler(IUnitOfWork _unitOfWork) : IRequestHandler<UpdatePersonPhoneNumberCommand, Unit>
{
    /// <summary>
    /// Handles updating a person's phone number details.
    /// </summary>
    /// <param name="request">The request containing phone number update details.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>A unit result indicating successful execution.</returns>
    /// <exception cref="NotFoundException">Thrown when the person or phone number type is not found.</exception>
    public async Task<Unit> Handle(UpdatePersonPhoneNumberCommand request, CancellationToken cancellationToken)
    {
        var person = await _unitOfWork.PersonRepository.GetDetailsByIdAsync(request.PersonId);

        if (person is null)
            throw new NotFoundException(string.Format(ExceptionMessageResource.NotFound,
                        nameof(Domain.Aggregates.Person.Person), request.PersonId));

        if (!await _unitOfWork.PhoneNumberTypeRepository.AnyAsync(x => x.Id == request.PhoneNumberTypeId))
            throw new NotFoundException(string.Format(ExceptionMessageResource.NotFound,
                        nameof(Domain.Aggregates.PhoneNumberType.PhoneNumberType), request.PhoneNumberTypeId));

        person.UpdateNumber(request.PersonPhoneNumberId, request.PhoneNumberTypeId, request.PhoneNumber);

        _unitOfWork.PersonRepository.Update(person);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}