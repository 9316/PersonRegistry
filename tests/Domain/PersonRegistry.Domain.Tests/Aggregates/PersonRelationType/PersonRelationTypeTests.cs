using FluentAssertions;
using PersonRegistry.Domain.Aggregates.PersonRelationType;

namespace PersonRegistry.Domain.Tests.Aggregates;

/// <summary>
/// Unit tests for <see cref="PersonRelationType"/> entity.
/// </summary>
public class PersonRelationTypeTests
{
    [Fact]
    public void Create_ShouldReturnValidInstance_WhenValidNameIsProvided()
    {
        // Arrange
        var name = "Sibling";

        // Act
        var relationType = PersonRelationType.Create(name);

        // Assert
        relationType.Should().NotBeNull();
        relationType.Name.Should().Be(name);
    }

    [Fact]
    public void Create_ShouldTrimName_WhenNameHasWhitespace()
    {
        // Arrange
        var name = "   Parent   ";

        // Act
        var relationType = PersonRelationType.Create(name);

        // Assert
        relationType.Name.Should().Be("Parent"); // Ensures trimming works
    }

    [Fact]
    public void Update_ShouldModifyRelationTypeName()
    {
        // Arrange
        var relationType = PersonRelationType.Create("Friend");

        // Act
        relationType.Update("Colleague");

        // Assert
        relationType.Name.Should().Be("Colleague");
    }

    [Fact]
    public void Update_ShouldTrimName_WhenNameHasWhitespace()
    {
        // Arrange
        var relationType = PersonRelationType.Create("Spouse");

        // Act
        relationType.Update("   Partner   ");

        // Assert
        relationType.Name.Should().Be("Partner");
    }
}