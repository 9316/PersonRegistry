using MediatR;
using PersonRegistry.Application.Resources;
using PersonRegistry.Common.Exceptions;
using PersonRegistry.Domain.Interfaces;

namespace PersonRegistry.Application.PersonRelation.Command.Delete;


/// <summary>
/// Handles the deletion of a specific relationship between two persons.
/// </summary>
public class DeletePersonRelationCommandHandler(IUnitOfWork _unitOfWork) : IRequestHandler<DeletePersonRelationCommand, Unit>
{

    /// <summary>
    /// Handles the deletion of a person relation.
    /// </summary>
    /// <param name="request">The request containing person relation details.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>An acknowledgment of the deletion operation.</returns>
    /// <exception cref="NotFoundException">Thrown when the relationship is not found.</exception>
    public async Task<Unit> Handle(DeletePersonRelationCommand request, CancellationToken cancellationToken)
    {
        var personRelation = await _unitOfWork.PersonRelationRepository.GetAsync(x => x.PersonId == request.PersonId
                                              && x.RelatedPersonId == request.RelatedPersonId
                                              && x.PersonRelationTypeId == request.PersonRelationTypeId);

        ValidateOnExceptions(personRelation, request);

        personRelation.Delete();

        _unitOfWork.PersonRelationRepository.Update(personRelation);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }

    /// <summary>
    /// Validates the request for deleting a person relation.
    /// </summary>
    /// <param name="personRelation">The person relation entity.</param>
    /// <param name="request">The request containing person relation details.</param>
    /// <exception cref="NotFoundException">Thrown when the specified person relation does not exist.</exception>
    private void ValidateOnExceptions(Domain.Aggregates.Person.PersonRelation.PersonRelation personRelation, DeletePersonRelationCommand request)
    {
        if (personRelation is null)
            throw new NotFoundException(string.Format(ExceptionMessageResource.NotFoundPersonRelation,
                        nameof(Domain.Aggregates.Person.PersonRelation.PersonRelation),
                        request.PersonId, request.RelatedPersonId, request.PersonRelationTypeId));
    }
}