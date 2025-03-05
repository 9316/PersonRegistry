namespace PersonRegistry.Domain.Base;

/// <summary>
/// Represents a base entity with an identifier and soft delete functionality.
/// </summary>
/// <typeparam name="T">The type of the identifier.</typeparam>
public abstract class Entity<T>
{
    /// <summary>
    /// Gets the unique identifier of the entity.
    /// </summary>
    public virtual T Id { get; protected set; }

    /// <summary>
    /// Gets a value indicating whether the entity is marked as deleted.
    /// </summary>
    public bool IsDeleted { get; private set; }

    /// <summary>
    /// Marks the entity as deleted.
    /// </summary>
    public void Delete()
    {
        IsDeleted = true;
    }
}
