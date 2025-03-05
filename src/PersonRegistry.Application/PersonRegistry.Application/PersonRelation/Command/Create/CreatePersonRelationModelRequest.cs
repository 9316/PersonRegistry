namespace PersonRegistry.Application.PersonRelation.Command.Create;

/// <summary>
/// Represents a request to create a new relationship between two persons.
/// </summary>
/// <param name="PersonId">The unique identifier of the person for whom the relationship is being defined.</param>
/// <param name="RelatedPersonId">The unique identifier of the person who is related to the first person.</param>
/// <param name="PersonRelationTypeId">The identifier of the type of relationship between the two persons.</param>
public record CreatePersonRelationModelRequest(
    int PersonId,
    int RelatedPersonId,
    int PersonRelationTypeId
);