using PersonRegistry.Domain.Base;
using PersonRegistry.Domain.Base.Primitives;

namespace PersonRegistry.Domain.Aggregates.PhoneNumberType;

/// <summary>
/// Represents a type of phone number (e.g., Mobile, Home, Work).
/// </summary>
public class PhoneNumberType : Entity<int>, IAggregateRoot
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PhoneNumberType"/> class.
    /// </summary>
    /// <param name="name">The name of the phone number type.</param>
    public PhoneNumberType(string name) => Name = name.Trim();

    /// <summary>
    /// Gets the name of the phone number type.
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// Creates a new instance of <see cref="PhoneNumberType"/>.
    /// </summary>
    /// <param name="name">The name of the phone number type.</param>
    /// <returns>A new instance of <see cref="PhoneNumberType"/>.</returns>
    public static PhoneNumberType Create(string name)
    {
        return new PhoneNumberType(name);
    }

    /// <summary>
    /// Updates the name of the phone number type.
    /// </summary>
    /// <param name="name">The new name of the phone number type.</param>
    public void Update(string name)
    {
        Name = name.Trim();
    }
}