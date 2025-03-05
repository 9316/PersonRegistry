using MediatR;
using PersonRegistry.Common.Application.Interfaces;

namespace PersonRegistry.Application.PersonRelation.Command.Create;

/// <summary>
/// Represents a command to create a relationship between two persons.
/// </summary>
/// <param name="PersonId">The unique identifier of the person initiating the relation.</param>
/// <param name="RelatedPersonId">The unique identifier of the related person.</param>
/// <param name="PersonRelationTypeId">The unique identifier of the relationship type between the two persons.</param>
public record CreatePersonRelationCommand(
    int PersonId,
    int RelatedPersonId,
    int PersonRelationTypeId
) : IRequest<int>, ITransactionalRequest;