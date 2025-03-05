using PersonRegistry.Common.Exceptions;
using PersonRegistry.Domain.Base;
using PersonRegistry.Domain.Base.Primitives;
using PersonRegistry.Domain.Enums;
using PersonRegistry.Domain.Resources;

namespace PersonRegistry.Domain.Aggregates.Person;

/// <summary>
/// Represents a person entity with personal details, city association, gender, and contact information.
/// </summary>
public class Person : Entity<int>, IAggregateRoot
{
    private readonly List<PersonPhoneNumber> phoneNumbers;

    /// <summary>
    /// Initializes a new instance of the <see cref="Person"/> class.
    /// </summary>
    /// <param name="name">The person's first name.</param>
    /// <param name="lastName">The person's last name.</param>
    /// <param name="personalNumber">A unique personal identifier (e.g., national ID number).</param>
    /// <param name="birthDate">The person's date of birth.</param>
    /// <param name="gender">The person's gender.</param>
    /// <param name="cityId">The ID of the city where the person resides.</param>
    public Person(
                string name,
                string lastName,
                string personalNumber,
                DateTime birthDate,
                GenderEnum gender,
                int cityId)
    {
        Name = name;
        LastName = lastName;
        PersonalNumber = personalNumber;
        BirthDate = birthDate;
        Gender = gender;
        Photo = string.Empty;
        CityId = cityId;
        phoneNumbers = new List<PersonPhoneNumber>();
    }

    /// <summary>
    /// Parameterless constructor required for ORM frameworks.
    /// </summary>
    public Person() => phoneNumbers = new List<PersonPhoneNumber>();

    /// <summary>
    /// Gets the person's first name.
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// Gets the person's last name.
    /// </summary>
    public string LastName { get; private set; }

    /// <summary>
    /// Gets the unique personal identifier (e.g., national ID number).
    /// </summary>
    public string PersonalNumber { get; private set; }

    /// <summary>
    /// Gets the person's date of birth.
    /// </summary>
    public DateTime BirthDate { get; private set; }

    /// <summary>
    /// Gets the URL of the person's photo.
    /// </summary>
    public string Photo { get; private set; }

    /// <summary>
    /// Gets the person's gender.
    /// </summary>
    public GenderEnum Gender { get; private set; }

    /// <summary>
    /// Gets the ID of the city where the person resides.
    /// </summary>
    public int CityId { get; private set; }

    /// <summary>
    /// Gets the navigation property for the person's city.
    /// </summary>
    public City.City City { get; private set; }

    /// <summary>
    /// Gets the collection of the person's phone numbers.
    /// </summary>
    public IReadOnlyCollection<PersonPhoneNumber> PhoneNumbers => phoneNumbers;

    /// <summary>
    /// Gets the collection of the person's related persons.
    /// </summary>
    public IReadOnlyCollection<PersonRelation.PersonRelation> RelatedPersons;

    /// <summary>
    /// Creates a new instance of <see cref="Person"/>.
    /// </summary>
    /// <param name="name">The person's first name.</param>
    /// <param name="lastName">The person's last name.</param>
    /// <param name="personalNumber">A unique personal identifier.</param>
    /// <param name="birthDate">The person's date of birth.</param>
    /// <param name="gender">The person's gender.</param>
    /// <param name="cityId">The ID of the city where the person resides.</param>
    /// <returns>A new instance of <see cref="Person"/>.</returns>
    public static Person Create(string name, string lastName, string personalNumber, DateTime birthDate, GenderEnum gender, int cityId)
        => new(name, lastName, personalNumber, birthDate, gender, cityId);

    /// <summary>
    /// Updates the person's details.
    /// </summary>
    /// <param name="name">The updated first name.</param>
    /// <param name="lastName">The updated last name.</param>
    /// <param name="personalNumber">The updated personal number.</param>
    /// <param name="birthDate">The updated date of birth.</param>
    /// <param name="gender">The updated gender.</param>
    /// <param name="cityId">The updated city ID.</param>
    public void Update(string name, string lastName, string personalNumber, DateTime birthDate, GenderEnum gender, int cityId)
    {
        Name = name;
        LastName = lastName;
        PersonalNumber = personalNumber;
        BirthDate = birthDate;
        Gender = gender;
        CityId = cityId;
    }

    /// <summary>
    /// Updates the person's profile photo URL.
    /// </summary>
    /// <param name="photoUrl">The new photo URL.</param>
    public void UpdatePhotoUrl(string photoUrl) => Photo = photoUrl;

    /// <summary>
    /// Removes the person's profile photo by resetting the URL to an empty string.
    /// </summary>
    public void DeletePhoto() => Photo = string.Empty;

    /// <summary>
    /// Adds a new phone number to the person.
    /// </summary>
    /// <param name="phoneNumber">The phone number entity.</param>
    public void AddPhoneNumber(PersonPhoneNumber phoneNumber)
    {
        phoneNumbers.Add(phoneNumber);
    }

    /// <summary>
    /// Deletes a phone number from the person's phone number collection.
    /// </summary>
    /// <param name="id">The ID of the phone number to remove.</param>
    /// <exception cref="NotFoundException">Thrown when the phone number is not found.</exception>
    public void DeleteNumber(int id)
    {
        var phoneNumber = phoneNumbers.FirstOrDefault(x => x.Id == id) ?? 
            throw new NotFoundException(string.Format(ExceptionMessageResources.NotFound, nameof(PersonPhoneNumber), id));

        phoneNumber.Delete();
    }

    /// <summary>
    /// Updates an existing phone number in the person's collection.
    /// </summary>
    /// <param name="phoneNumberId">The ID of the phone number to update.</param>
    /// <param name="phoneNumberTypeId">The updated phone number type ID.</param>
    /// <param name="number">The updated phone number.</param>
    /// <exception cref="NotFoundException">Thrown when the phone number is not found.</exception>
    public void UpdateNumber(int phoneNumberId, int phoneNumberTypeId, string number)
    {
        var phoneNumber = phoneNumbers.FirstOrDefault(x => x.Id == phoneNumberId) ?? 
            throw new NotFoundException(string.Format(ExceptionMessageResources.NotFound,nameof(PersonPhoneNumber), phoneNumberId));

        phoneNumber.Update(phoneNumberTypeId, number);
    }
}