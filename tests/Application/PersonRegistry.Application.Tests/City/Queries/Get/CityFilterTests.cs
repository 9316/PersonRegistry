using FluentAssertions;
using PersonRegistry.Application.City.Queries.Get;

namespace PersonRegistry.Application.Tests.City.Queries.Get;

/// <summary>
/// Unit tests for custom exceptions in <see cref="CityFilter"/>.
/// </summary>
public class CityFilterTests
{
    private const int PAGE_SIZE = 1;
    private const int PAGE_NUMBER = 5;

    [Fact]
    public void Filter_ShouldReturnAllCities_WhenFilterQueryIsNullOrEmpty()
    {
        // Arrange
        var cities = new List<Domain.Aggregates.City.City>
        {
            Domain.Aggregates.City.City.Create("Tbilisi"),
            Domain.Aggregates.City.City.Create("Batumi"),
            Domain.Aggregates.City.City.Create("Kutaisi")
        }.AsQueryable();

        var query = new GetCitiesQuery(null, PAGE_SIZE, PAGE_NUMBER); // FilterQuery = null

        // Act
        var result = CityFilter.Filter(cities, query);

        // Assert
        result.Should().HaveCount(3); // ყველა ქალაქი უნდა დარჩეს
    }

    [Fact]
    public void Filter_ShouldReturnMatchingCity_WhenFilterQueryMatches()
    {
        // Arrange
        var cities = new List<Domain.Aggregates.City.City>
        {
            Domain.Aggregates.City.City.Create("Tbilisi"),
            Domain.Aggregates.City.City.Create("Batumi"),
            Domain.Aggregates.City.City.Create("Kutaisi")
        }.AsQueryable();

        var query = new GetCitiesQuery("Batumi", PAGE_SIZE, PAGE_NUMBER);

        // Act
        var result = CityFilter.Filter(cities, query);

        // Assert
        result.Should().HaveCount(1);
        result.First().Name.Should().Be("Batumi");
    }

    [Fact]
    public void Filter_ShouldReturnEmpty_WhenNoCityMatchesQuery()
    {
        // Arrange
        var cities = new List<Domain.Aggregates.City.City>
        {
            Domain.Aggregates.City.City.Create("Tbilisi"),
            Domain.Aggregates.City.City.Create("Batumi"),
            Domain.Aggregates.City.City.Create("Kutaisi")
        }.AsQueryable();

        var query = new GetCitiesQuery("Rustavi", PAGE_SIZE, PAGE_NUMBER);

        // Act
        var result = CityFilter.Filter(cities, query);

        // Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public void Filter_ShouldBeCaseInsensitive_WhenFiltering()
    {
        // Arrange
        var cities = new List<Domain.Aggregates.City.City>
        {
            Domain.Aggregates.City.City.Create("Tbilisi"),
            Domain.Aggregates.City.City.Create("Batumi"),
            Domain.Aggregates.City.City.Create("Kutaisi")
        }.AsQueryable();

        var query = new GetCitiesQuery(FilterQuery: "batumi", PAGE_SIZE, PAGE_NUMBER);

        // Act
        var result = CityFilter.Filter(cities, query);

        // Assert
        result.Should().HaveCount(1);
        result.First().Name.Should().Be("Batumi");
    }
}