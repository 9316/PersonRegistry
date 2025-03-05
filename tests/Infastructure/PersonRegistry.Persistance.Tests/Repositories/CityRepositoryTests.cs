using Microsoft.EntityFrameworkCore;
using PersonRegistry.Domain.Aggregates.City;
using PersonRegistry.Persistence.Context;
using PersonRegistry.Persistence.Repositories;
using FluentAssertions;
using Moq;
using Microsoft.Extensions.Configuration;

namespace PersonRegistry.Persistance.Tests.Repositories;

/// <summary>
/// Unit tests for custom exceptions in <see cref="CityRepository"/>.
/// </summary>
public class CityRepositoryTests : IDisposable
{
    private readonly PersonRegistryDbContext _dbContext;
    private readonly CityRepository _cityRepository;

    public CityRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<PersonRegistryDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

        var configurationMock = new Mock<IConfiguration>();

        _dbContext = new PersonRegistryDbContext(options, configurationMock.Object);
        _cityRepository = new CityRepository(_dbContext);
    }

    [Fact]
    public async Task AddAsync_ShouldAddCity()
    {
        // Arrange
        var city = City.Create("Tbilisi");

        // Act
        await _cityRepository.AddAsync(city);
        await _dbContext.SaveChangesAsync();

        var retrievedCity = await _cityRepository.GetByIdAsync(city.Id);

        // Assert
        retrievedCity.Should().NotBeNull();
        retrievedCity.Name.Should().Be("Tbilisi");
    }

    [Fact]
    public async Task DeleteAsync_ShouldRemoveCity()
    {
        // Arrange
        var city = City.Create("Kutaisi");

        await _cityRepository.AddAsync(city);
        await _dbContext.SaveChangesAsync();

        // Act
        _cityRepository.Delete(city);
        await _dbContext.SaveChangesAsync();

        var retrievedCity = await _cityRepository.GetByIdAsync(city.Id);

        // Assert
        retrievedCity.Should().BeNull();
    }

    [Fact]
    public async Task Exists_ShouldReturnTrue_WhenCityExists()
    {
        // Arrange
        var city = City.Create("Batumi");

        await _cityRepository.AddAsync(city);
        await _dbContext.SaveChangesAsync();

        // Act
        var exists = await _cityRepository.AnyAsync(c => c.Id == city.Id);

        // Assert
        exists.Should().BeTrue();
    }

    [Fact]
    public async Task Exists_ShouldReturnFalse_WhenCityDoesNotExist()
    {
        // Act
        var exists = await _cityRepository.AnyAsync(c => c.Id == 999);

        // Assert
        exists.Should().BeFalse();
    }

    [Fact]
    public async Task Update_ShouldModifyCity()
    {
        // Arrange
        var city = City.Create("Rustavi");

        await _cityRepository.AddAsync(city);
        await _dbContext.SaveChangesAsync();

        // Act
        city.Update("New Rustavi");
        _cityRepository.Update(city);
        await _dbContext.SaveChangesAsync();

        var updatedCity = await _cityRepository.GetByIdAsync(city.Id);

        // Assert
        updatedCity.Name.Should().Be("New Rustavi");
    }

    public void Dispose()
    {
        _dbContext.Dispose();
    }
}
