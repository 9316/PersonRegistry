using PersonRegistry.Domain.Base;

namespace PersonRegistry.Domain.Aggregates.Person.PersonRelation;

/// <summary>
/// Represents a relationship between two persons in the registry.
/// </summary>
public class PersonRelation : Entity<int>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PersonRelation"/> class.
    /// </summary>
    /// <param name="personId">The ID of the primary person.</param>
    /// <param name="relatedPersonId">The ID of the related person.</param>
    /// <param name="personRelationTypeId">The ID of the relationship type.</param>
    private PersonRelation(int personId, int relatedPersonId, int personRelationTypeId)
    {
        PersonId = personId;
        RelatedPersonId = relatedPersonId;
        PersonRelationTypeId = personRelationTypeId;
    }

    /// <summary>
    /// Gets the ID of the primary person in the relationship.
    /// </summary>
    public int PersonId { get; private set; }

    /// <summary>
    /// Gets the ID of the related person in the relationship.
    /// </summary>
    public int RelatedPersonId { get; private set; }

    /// <summary>
    /// Gets the ID of the relationship type.
    /// </summary>
    public int PersonRelationTypeId { get; private set; }

    /// <summary>
    /// Creates a new person relationship instance.
    /// </summary>
    /// <param name="personId">The ID of the primary person.</param>
    /// <param name="relatedPersonId">The ID of the related person.</param>
    /// <param name="personRelationTypeId">The ID of the relationship type.</param>
    /// <returns>A new instance of <see cref="PersonRelation"/>.</returns>
    public static PersonRelation Create(int personId, int relatedPersonId, int personRelationTypeId) =>
        new PersonRelation(personId, relatedPersonId, personRelationTypeId);

    /// <summary>
    /// Navigation property to the primary person.
    /// </summary>
    public virtual Person Person { get; private set; }

    /// <summary>
    /// Navigation property to the related person.
    /// </summary>
    public virtual Person RelatedPerson { get; private set; }

    /// <summary>
    /// Navigation property to the relationship type.
    /// </summary>
    public virtual PersonRelationType.PersonRelationType PersonRelationType { get; private set; }
}