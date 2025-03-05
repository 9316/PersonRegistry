using PersonRegistry.Domain.Base;

namespace PersonRegistry.Domain.Aggregates.Person;

/// <summary>
/// Represents a phone number associated with a person.
/// </summary>
public class PersonPhoneNumber(
    string phoneNumber,
    int phoneNumberTypeId) : Entity<int>
{
    /// <summary>
    /// Gets the phone number.
    /// </summary>
    public string PhoneNumber { get; private set; } = phoneNumber;

    /// <summary>
    /// Gets the unique identifier of the person associated with this phone number.
    /// </summary>
    public int PersonId { get; private set; }

    /// <summary>
    /// Gets the unique identifier of the phone number type (e.g., Mobile, Home, Work).
    /// </summary>
    public int PhoneNumberTypeId { get; private set; } = phoneNumberTypeId;

    /// <summary>
    /// Navigation property for the associated person.
    /// </summary>
    public virtual Person Person { get; private set; }

    /// <summary>
    /// Navigation property for the type of phone number.
    /// </summary>
    public virtual PhoneNumberType.PhoneNumberType PhoneNumberType { get; private set; }

    /// <summary>
    /// Creates a new instance of <see cref="PersonPhoneNumber"/>.
    /// </summary>
    /// <param name="phoneNumber">The phone number.</param>
    /// <param name="phoneNumberTypeId">The type of phone number.</param>
    /// <returns>A new instance of <see cref="PersonPhoneNumber"/>.</returns>
    public static PersonPhoneNumber Create(string phoneNumber, int phoneNumberTypeId)
    {
        return new PersonPhoneNumber(phoneNumber, phoneNumberTypeId);
    }

    /// <summary>
    /// Updates the phone number and its type.
    /// </summary>
    /// <param name="phoneNumberTypeId">The new phone number type.</param>
    /// <param name="phoneNumber">The new phone number.</param>
    public void Update(int phoneNumberTypeId, string phoneNumber)
    {
        PhoneNumberTypeId = phoneNumberTypeId;
        PhoneNumber = phoneNumber;
    }
}