using MediatR;
using PersonRegistry.Application.Resources;
using PersonRegistry.Common.Exceptions;
using PersonRegistry.Domain.Interfaces;

namespace PersonRegistry.Application.Person.Commands.Update;

/// <summary>
/// Handles the update of an existing person's information.
/// </summary>
/// <param name="_unitOfWork">Unit of work for managing database transactions.</param>
public class UpdatePersonCommandHandler(IUnitOfWork _unitOfWork) : IRequestHandler<UpdatePersonCommand, Unit>
{
    /// <summary>
    /// Handles the request to update a person's details.
    /// </summary>
    /// <param name="request">The update command containing the person's new details.</param>
    /// <param name="cancellationToken">A token to observe while waiting for the task to complete.</param>
    /// <returns>A <see cref="Unit"/> indicating that the operation was successful.</returns>
    /// <exception cref="NotFoundException">
    /// Thrown if the specified city or person does not exist in the system.
    /// </exception>
    public async Task<Unit> Handle(UpdatePersonCommand request, CancellationToken cancellationToken)
    {
        if (!await _unitOfWork.CityRepository.AnyAsync(x => x.Id == request.CityId))
            throw new NotFoundException(string.Format(ExceptionMessageResource.NotFound,
                                        nameof(Domain.Aggregates.City.City), request.CityId));

        var person = await _unitOfWork.PersonRepository.GetByIdAsync(request.Id) ?? 
            throw new NotFoundException(string.Format(ExceptionMessageResource.NotFound,nameof(Domain.Aggregates.Person.Person), request.Id));

        person.Update(request.Name,
                      request.LastName,
                      request.PersonalNumber,
                      request.BirthDate,
                      request.Gender,
                      request.CityId);

        _unitOfWork.PersonRepository.Update(person);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
