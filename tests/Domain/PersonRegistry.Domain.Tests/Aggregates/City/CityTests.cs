using FluentAssertions;
using PersonRegistry.Domain.Aggregates.City;

namespace PersonRegistry.Domain.Tests.Aggregates;

/// <summary>
/// Unit tests for the <see cref="City"/> entity.
/// </summary>
public class CityTests
{
    [Fact]
    public void Create_ShouldTrimCityName()
    {
        // Arrange
        var cityName = "  Tbilisi  ";

        // Act
        var city = City.Create(cityName);

        // Assert
        city.Name.Should().Be("Tbilisi");
    }

    [Fact]
    public void Update_ShouldChangeCityName()
    {
        // Arrange
        var city = City.Create("Tbilisi");

        // Act
        city.Update("Batumi");

        // Assert
        city.Name.Should().Be("Batumi");
    }
}