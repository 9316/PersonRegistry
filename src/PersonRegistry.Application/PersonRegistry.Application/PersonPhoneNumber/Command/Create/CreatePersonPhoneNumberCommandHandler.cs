using MediatR;
using PersonRegistry.Application.Resources;
using PersonRegistry.Common.Exceptions;
using PersonRegistry.Domain.Interfaces;

namespace PersonRegistry.Application.PersonPhoneNumber.Command.Create;

/// <summary>
/// Handles the creation of a new phone number for a person.
/// </summary>
public class CreatePersonPhoneNumberCommandHandler(IUnitOfWork _unitOfWork) : IRequestHandler<CreatePersonPhoneNumberCommand, Unit>
{

    /// <summary>
    /// Handles the command to create a new phone number for a person.
    /// </summary>
    /// <param name="request">The request containing phone number details.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>A unit result indicating successful execution.</returns>
    public async Task<Unit> Handle(CreatePersonPhoneNumberCommand request, CancellationToken cancellationToken)
    {
        var person = await _unitOfWork.PersonRepository.GetByIdAsync(request.PersonId);

        await ValidateOnException(person, request);

        person.AddPhoneNumber(Domain.Aggregates.Person.PersonPhoneNumber.Create(request.PhoneNumber, request.PhoneNumberTypeId));

        _unitOfWork.PersonRepository.Update(person);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }

    /// <summary>
    /// Validates whether the person and phone number type exist in the database.
    /// </summary>
    /// <param name="person">The person entity.</param>
    /// <param name="request">The request containing phone number details.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    /// <exception cref="NotFoundException">Thrown if the person or phone number type is not found.</exception>
    private async Task ValidateOnException(Domain.Aggregates.Person.Person person, CreatePersonPhoneNumberCommand request)
    {
        if (person is null)
            throw new NotFoundException(string.Format(ExceptionMessageResource.NotFound,
                        nameof(Domain.Aggregates.Person.Person), request.PersonId));

        if (!await _unitOfWork.PhoneNumberTypeRepository.AnyAsync(x => x.Id == request.PhoneNumberTypeId))
            throw new NotFoundException(string.Format(ExceptionMessageResource.NotFound,
                                        nameof(Domain.Aggregates.PhoneNumberType.PhoneNumberType), request.PhoneNumberTypeId));
    }
}