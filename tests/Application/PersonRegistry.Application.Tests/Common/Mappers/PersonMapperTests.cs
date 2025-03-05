using FluentAssertions;
using PersonRegistry.Application.Common.Mappers;
using PersonRegistry.Domain.Enums;

namespace PersonRegistry.Application.Tests.Common.Mappers;

/// <summary>
/// Unit tests for custom exceptions in <see cref="PersonMapper"/>.
/// </summary>
public class PersonMapperTests
{
    [Fact]
    public void ToPersonResponse_ShouldMapCorrectly()
    {
        // Arrange
        var city = Domain.Aggregates.City.City.Create(name: "Tbilisi");
        var person = Domain.Aggregates.Person.Person.Create(name:"John", lastName: "Doe", personalNumber: "123456789", birthDate:new DateTime(1990, 1, 1), gender: GenderEnum.Male, city.Id);

        typeof(Domain.Aggregates.Person.Person)
            .GetProperty("City")?
            .SetValue(person, city);

        // Act
        var result = person.ToPersonResponse();

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(person.Name);
        result.LastName.Should().Be(person.LastName);
        result.PersonalNumber.Should().Be(person.PersonalNumber);
        result.BirthDate.Should().Be(person.BirthDate);
        result.Gender.Should().Be(person.Gender);
        result.CityId.Should().Be(person.CityId);
        result.City.Should().NotBeNull();
        result.City.Name.Should().Be(city.Name);
    }

    [Fact]
    public void MapToPersonsResponse_ShouldMapListCorrectly()
    {
        // Arrange
        var persons = new List<Domain.Aggregates.Person.Person>
        {
            Domain.Aggregates.Person.Person.Create(name:"John", lastName: "Doe", personalNumber: "123456789", birthDate: new DateTime(1990, 1, 1), gender: GenderEnum.Male, cityId: 1),
            Domain.Aggregates.Person.Person.Create(name: "Jane", lastName: "Smith", personalNumber: "987654321", birthDate: new DateTime(1995, 5, 10), gender: GenderEnum.Female, cityId: 1)
        };

        // Act
        var result = PersonMapper.MapToPersonsResponse(persons);

        // Assert
        result.Should().HaveCount(2);
        result.First().Name.Should().Be("John");
        result.Last().Name.Should().Be("Jane");
    }

    [Theory]
    [InlineData(null)]
    public void MappingMethods_ShouldThrowArgumentNullException_WhenNullEntityPassed(Domain.Aggregates.Person.Person person)
    {
        // Act
        Action act = () => person.ToPersonResponse();

        // Assert
        act.Should().Throw<ArgumentNullException>();
    }
}