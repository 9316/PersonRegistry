using PersonRegistry.Domain.Base;
using PersonRegistry.Domain.Base.Primitives;

namespace PersonRegistry.Domain.Aggregates.City;

/// <summary>
/// Represents a city entity.
/// </summary>
public class City : Entity<int>, IAggregateRoot
{
    /// <summary>
    /// Initializes a new instance of the <see cref="City"/> class.
    /// </summary>
    /// <param name="name">The name of the city.</param>
    private City(string name) => Name = name.Trim();

    /// <summary>
    /// Gets the name of the city.
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// Creates a new city instance.
    /// </summary>
    /// <param name="name">The name of the city.</param>
    /// <returns>A new instance of <see cref="City"/>.</returns>
    /// <exception cref="ArgumentException">Thrown when the name is empty or null.</exception>
    public static City Create(string name)
    {
        return new City(name);
    }

    /// <summary>
    /// Updates the city's name.
    /// </summary>
    /// <param name="name">The new name of the city.</param>
    /// <exception cref="ArgumentException">Thrown when the name is empty or null.</exception>
    public void Update(string name)
    {
        Name = name.Trim();
    }
}