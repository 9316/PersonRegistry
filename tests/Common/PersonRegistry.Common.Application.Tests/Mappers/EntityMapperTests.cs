using FluentAssertions;
using PersonRegistry.API.Tests.TestData;

/// <summary>
/// Unit tests for the <see cref="EntityMapper{TSource, TResponse}"/> class.
/// </summary>
public class EntityMapperTests
{
    [Fact]
    public void Map_ShouldMapProperties_WhenTypesMatch()
    {
        // Arrange
        var source = new EntityMapperTestData.Source { Id = 1, Name = "Test" };

        // Act
        var result = EntityMapper<EntityMapperTestData.Source, EntityMapperTestData.Response>.Map(source);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(source.Id);
        result.Name.Should().Be(source.Name);
    }
}