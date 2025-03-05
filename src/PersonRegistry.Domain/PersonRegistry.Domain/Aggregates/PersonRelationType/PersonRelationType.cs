using PersonRegistry.Domain.Base;
using PersonRegistry.Domain.Base.Primitives;

namespace PersonRegistry.Domain.Aggregates.PersonRelationType;

/// <summary>
/// Represents a type of relationship between persons.
/// </summary>
public class PersonRelationType(string name) : Entity<int>, IAggregateRoot
{
    /// <summary>
    /// Gets the name of the person relation type.
    /// </summary>
    public string Name { get; private set; } = name.Trim();

    /// <summary>
    /// Creates a new instance of <see cref="PersonRelationType"/>.
    /// </summary>
    /// <param name="name">The name of the relation type.</param>
    /// <returns>A new instance of <see cref="PersonRelationType"/>.</returns>
    public static PersonRelationType Create(string name)
    {
        return new PersonRelationType(name);
    }

    /// <summary>
    /// Updates the relation type's name.
    /// </summary>
    /// <param name="name">The new name of the relation type.</param>
    public void Update(string name)
    {
        Name = name.Trim();
    }
}