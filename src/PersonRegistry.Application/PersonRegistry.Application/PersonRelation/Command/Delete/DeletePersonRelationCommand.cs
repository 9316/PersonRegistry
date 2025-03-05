using MediatR;
using PersonRegistry.Common.Application.Interfaces;

namespace PersonRegistry.Application.PersonRelation.Command.Delete;

/// <summary>
/// Represents a command to delete a specific relationship between two persons.
/// </summary>
/// <param name="PersonId">The unique identifier of the person for whom the relationship is being deleted.</param>
/// <param name="RelatedPersonId">The unique identifier of the related person.</param>
/// <param name="PersonRelationTypeId">The identifier of the type of relationship to be deleted.</param>
public record DeletePersonRelationCommand(
    int PersonId,
    int RelatedPersonId,
    int PersonRelationTypeId
) : IRequest<Unit>, ITransactionalRequest;