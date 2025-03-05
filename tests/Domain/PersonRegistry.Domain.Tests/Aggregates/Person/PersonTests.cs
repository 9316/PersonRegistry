using FluentAssertions;
using PersonRegistry.Domain.Aggregates.Person;
using PersonRegistry.Domain.Enums;
using PersonRegistry.Common.Exceptions;

namespace PersonRegistry.Domain.Tests;

/// <summary>
/// Unit tests for the <see cref="Person"/> entity.
/// </summary>
public class PersonTests
{
    [Fact]
    public void Create_ShouldCreatePerson_WhenValidDataProvided()
    {
        // Act
        var person = Person.Create("John", "Doe", "123456789", new DateTime(1990, 5, 5), GenderEnum.Male, 1);

        // Assert
        person.Should().NotBeNull();
        person.Name.Should().Be("John");
        person.LastName.Should().Be("Doe");
        person.PersonalNumber.Should().Be("123456789");
        person.BirthDate.Should().Be(new DateTime(1990, 5, 5));
        person.Gender.Should().Be(GenderEnum.Male);
        person.CityId.Should().Be(1);
    }

    [Fact]
    public void Update_ShouldModifyPersonDetails()
    {
        // Arrange
        var person = Person.Create("John", "Doe", "123456789", new DateTime(1990, 5, 5), GenderEnum.Male, 1);

        // Act
        person.Update("Jane", "Smith", "987654321", new DateTime(1995, 3, 10), GenderEnum.Female, 2);

        // Assert
        person.Name.Should().Be("Jane");
        person.LastName.Should().Be("Smith");
        person.PersonalNumber.Should().Be("987654321");
        person.BirthDate.Should().Be(new DateTime(1995, 3, 10));
        person.Gender.Should().Be(GenderEnum.Female);
        person.CityId.Should().Be(2);
    }

    [Fact]
    public void UpdatePhotoUrl_ShouldUpdatePhoto()
    {
        // Arrange
        var photoUrl = "https://example.com/photo.jpg";
        var person = Person.Create("John", "Doe", "123456789", DateTime.Now, GenderEnum.Male, 1);

        // Act
        person.UpdatePhotoUrl(photoUrl);

        // Assert
        person.Photo.Should().Be(photoUrl);
    }

    [Fact]
    public void DeletePhoto_ShouldResetPhotoUrl()
    {
        // Arrange
        var person = Person.Create("John", "Doe", "123456789", DateTime.Now, GenderEnum.Male, 1);
        person.UpdatePhotoUrl("https://example.com/photo.jpg");

        // Act
        person.DeletePhoto();

        // Assert
        person.Photo.Should().BeEmpty();
    }

    [Fact]
    public void AddPhoneNumber_ShouldAddPhoneNumberToList()
    {
        // Arrange
        var person = Person.Create("John", "Doe", "123456789", DateTime.Now, GenderEnum.Male, 1);
        var phoneNumber = new PersonPhoneNumber("123-456-7890", 1);

        // Act
        person.AddPhoneNumber(phoneNumber);

        // Assert
        person.PhoneNumbers.Should().Contain(phoneNumber);
    }

    [Fact]
    public void DeleteNumber_ShouldThrowException_WhenPhoneNumberNotFound()
    {
        // Arrange
        var person = Person.Create("John", "Doe", "123456789", DateTime.Now, GenderEnum.Male, 1);

        // Act
        Action act = () => person.DeleteNumber(99);

        // Assert
        act.Should().Throw<NotFoundException>().WithMessage("*PersonPhoneNumber*");
    }

    [Fact]
    public void UpdateNumber_ShouldThrowException_WhenPhoneNumberNotFound()
    {
        // Arrange
        var person = Person.Create("John", "Doe", "123456789", DateTime.Now, GenderEnum.Male, 1);

        // Act
        Action act = () => person.UpdateNumber(99, 2, "987-654-3210");

        // Assert
        act.Should().Throw<NotFoundException>().WithMessage("*PersonPhoneNumber*");
    }
}