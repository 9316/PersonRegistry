using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using PersonRegistry.Domain.Aggregates.City;
using PersonRegistry.Domain.Aggregates.Person;
using PersonRegistry.Domain.Aggregates.PersonRelationType;
using PersonRegistry.Domain.Aggregates.PhoneNumberType;
using PersonRegistry.Domain.Enums;
using PersonRegistry.Persistence.Context;

namespace PersonRegistry.Persistance.Tests.Context;

/// <summary>
/// Unit tests for the <see cref="PersonRegistryDbContext"/> class.
/// </summary>
public class PersonRegistryDbContextTests : IDisposable
{
    private readonly PersonRegistryDbContext _dbContext;
    private readonly Mock<IConfiguration> _mockConfiguration;

    public PersonRegistryDbContextTests()
    {
        _mockConfiguration = new Mock<IConfiguration>();

        var options = new DbContextOptionsBuilder<PersonRegistryDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        _dbContext = new PersonRegistryDbContext(options, _mockConfiguration.Object);
        _dbContext.Database.EnsureCreated();
    }

    [Fact]
    public async Task AddPerson_ShouldStorePersonInDatabase_WhenValidPersonProvided()
    {
        // Arrange
        var person = new Person("John", "Doe", "123456789", DateTime.Now, GenderEnum.Male, cityId: 1);

        // Act
        _dbContext.Persons.Add(person);
        await _dbContext.SaveChangesAsync();

        // Assert
        var dbPerson = await _dbContext.Persons.FindAsync(person.Id);
        dbPerson.Should().NotBeNull();
        dbPerson.Name.Should().Be("John");
    }

    [Fact]
    public async Task AddCity_ShouldStoreCityInDatabase_WhenValidCityProvided()
    {
        // Arrange
        var city = City.Create("Tbilisi");

        // Act
        _dbContext.Cities.Add(city);
        await _dbContext.SaveChangesAsync();

        // Assert
        var dbCity = await _dbContext.Cities.FindAsync(city.Id);
        dbCity.Should().NotBeNull();
        dbCity.Name.Should().Be("Tbilisi");
    }

    [Fact]
    public async Task AddPhoneNumberType_ShouldStorePhoneNumberTypeInDatabase_WhenValidPhoneNumberTypeProvided()
    {
        // Arrange
        var phoneNumberType = PhoneNumberType.Create("Mobile");

        // Act
        _dbContext.PhoneNumberTypes.Add(phoneNumberType);
        await _dbContext.SaveChangesAsync();

        // Assert
        var dbPhoneNumberType = await _dbContext.PhoneNumberTypes.FindAsync(phoneNumberType.Id);
        dbPhoneNumberType.Should().NotBeNull();
        dbPhoneNumberType.Name.Should().Be("Mobile");
    }

    [Fact]
    public async Task AddPersonRelationType_ShouldStoreRelationTypeInDatabase_WhenValidRelationTypeProvided()
    {
        // Arrange
        var relationType = PersonRelationType.Create("Sibling");

        // Act
        _dbContext.PersonRelationTypes.Add(relationType);
        await _dbContext.SaveChangesAsync();

        // Assert
        var dbRelationType = await _dbContext.PersonRelationTypes.FindAsync(relationType.Id);
        dbRelationType.Should().NotBeNull();
        dbRelationType.Name.Should().Be("Sibling");
    }

    public void Dispose()
    {
        _dbContext.Database.EnsureDeleted();
        _dbContext.Dispose();
    }
}