namespace PersonRegistry.Application.PersonRelation.Command.Delete;

/// <summary>
/// Represents a request to delete an existing relationship between two persons.
/// </summary>
/// <param name="PersonId">The unique identifier of the person from whom the relationship is being removed.</param>
/// <param name="RelatedPersonId">The unique identifier of the other person involved in the relationship.</param>
/// <param name="PersonRelationTypeId">The identifier of the type of relationship that is being deleted.</param>
public record DeletePersonRelationModelRequest(
    int PersonId,
    int RelatedPersonId,
    int PersonRelationTypeId
);