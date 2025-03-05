using MediatR;
using PersonRegistry.Application.Resources;
using PersonRegistry.Common.Exceptions;
using PersonRegistry.Domain.Aggregates.PersonRelationType;
using PersonRegistry.Domain.Interfaces;

namespace PersonRegistry.Application.PersonRelation.Command.Create;


/// <summary>
/// Handles the creation of a relationship between two persons.
/// </summary>
public class CreatePersonRelationCommandHandler(IUnitOfWork _unitOfWork) : IRequestHandler<CreatePersonRelationCommand, int>
{

    /// <summary>
    /// Handles the creation of a person relation.
    /// </summary>
    /// <param name="request">The request containing person relation details.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>The ID of the created person relation.</returns>
    /// <exception cref="NotFoundException">Thrown when the person, related person, or relation type is not found.</exception>
    /// <exception cref="AlreadyExistsException">Thrown if the relationship already exists.</exception>
    public async Task<int> Handle(CreatePersonRelationCommand request, CancellationToken cancellationToken)
    {
        var person = await _unitOfWork.PersonRepository.GetByIdAsync(request.PersonId);

        await ValidateOnExceptions(person, request);

        var personRelation = Domain.Aggregates.Person.PersonRelation.PersonRelation
                 .Create(request.PersonId, request.RelatedPersonId, request.PersonRelationTypeId);

        _unitOfWork.PersonRelationRepository.Update(personRelation);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return personRelation.Id;
    }

    /// <summary>
    /// Validates the request for creating a person relation.
    /// </summary>
    /// <param name="person">The person entity.</param>
    /// <param name="request">The request containing person relation details.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    /// <exception cref="NotFoundException">Thrown when required entities are not found.</exception>
    /// <exception cref="AlreadyExistsException">Thrown if the relationship already exists.</exception>
    private async Task ValidateOnExceptions(Domain.Aggregates.Person.Person person, CreatePersonRelationCommand request)
    {
        if (person is null)
            throw new NotFoundException(string.Format(ExceptionMessageResource.NotFound,
                        nameof(Domain.Aggregates.Person.Person), request.PersonId));

        if (!await _unitOfWork.PersonRepository.AnyAsync(x => x.Id == request.RelatedPersonId))
            throw new NotFoundException(string.Format(ExceptionMessageResource.NotFound,
                        nameof(Domain.Aggregates.Person.Person), request.RelatedPersonId));

        if (!await _unitOfWork.PersonRelationTypeRepository.AnyAsync(x => x.Id == request.PersonRelationTypeId))
            throw new NotFoundException(string.Format(ExceptionMessageResource.NotFound,
                        nameof(PersonRelationType), request.PersonRelationTypeId));

        if (await _unitOfWork.PersonRelationRepository.AnyAsync(x => x.PersonId == request.PersonId
                                              && x.RelatedPersonId == request.RelatedPersonId
                                              && x.PersonRelationTypeId == request.PersonRelationTypeId))
            throw new AlreadyExistsException(string.Format(ExceptionMessageResource.RecordAlreadyExists,
                nameof(Domain.Aggregates.Person.PersonRelation.PersonRelation)));
    }
}
