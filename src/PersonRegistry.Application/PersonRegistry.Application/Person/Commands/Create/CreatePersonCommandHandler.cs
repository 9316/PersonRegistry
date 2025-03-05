using MediatR;
using PersonRegistry.Application.Resources;
using PersonRegistry.Common.Exceptions;
using PersonRegistry.Domain.Interfaces;

namespace PersonRegistry.Application.Person.Commands.Create;

/// <summary>
/// Handles the creation of a new person.
/// </summary>
public class CreatePersonCommandHandler(IUnitOfWork _unitOfWork) : IRequestHandler<CreatePersonCommand, int>
{

    /// <summary>
    /// Initializes a new instance of the <see cref="CreatePersonCommandHandler"/> class.
    /// </summary>
    /// <param name="unitOfWork">The unit of work for managing transactions.</param>
    public async Task<int> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
    {
        var city = await _unitOfWork.CityRepository.GetByIdAsync(request.CityId) ??            
            throw new NotFoundException(string.Format(ExceptionMessageResource.NotFound,nameof(Domain.Aggregates.City.City), request.CityId));

        if (await _unitOfWork.PersonRepository.AnyAsync(x => x.PersonalNumber == request.PersonalNumber.Trim()))
            throw new AlreadyExistsException(string.Format(ExceptionMessageResource.RecordAlreadyExists, request.PersonalNumber));

        var person = Domain.Aggregates.Person.Person.Create(request.Name,
                                                            request.LastName,
                                                            request.PersonalNumber,
                                                            request.BirthDate,
                                                            request.Gender,
                                                            request.CityId);

        await _unitOfWork.PersonRepository.AddAsync(person, cancellationToken);
        var personId = await _unitOfWork.SaveChangesAsync(cancellationToken);

        return personId;
    }
}