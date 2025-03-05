using FluentAssertions;
using PersonRegistry.API.Mappers;
using PersonRegistry.API.Tests.TestData;

/// <summary>
/// Unit tests for the <see cref="PersonRelationMapper"/> class.
/// </summary>
public class PersonRelationMapperTests
{
    [Fact]
    public void ToCreatePersonRelationCommand_WhenRequestIsValid_ShouldMapCorrectly()
    {
        // Arrange
        var createPersonRelationRequest = PersonRelationTestData.BuildCreatePersonRelationRequest();

        // Act
        var result = createPersonRelationRequest.ToCreatePersonRelationCommand();

        // Assert
        result.Should().NotBeNull();
        result.PersonId.Should().Be(createPersonRelationRequest.PersonId);
        result.RelatedPersonId.Should().Be(createPersonRelationRequest.RelatedPersonId);
        result.PersonRelationTypeId.Should().Be(createPersonRelationRequest.PersonRelationTypeId);
    }

    [Fact]
    public void ToCreatePersonRelationCommand_WhenRequestIsNull_ShouldThrowArgumentNullException()
    {
        // Act
        Action act = () => PersonRelationMapper.ToCreatePersonRelationCommand(null);

        // Assert
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void ToDeletePersonRelationCommand_WhenRequestIsValid_ShouldMapCorrectly()
    {
        // Arrange
        var deletePersonRelationRequest = PersonRelationTestData.BuildDeletePersonRelationRequest();

        // Act
        var result = deletePersonRelationRequest.ToDeletePersonRelationCommand();

        // Assert
        result.Should().NotBeNull();
        result.PersonId.Should().Be(deletePersonRelationRequest.PersonId);
        result.RelatedPersonId.Should().Be(deletePersonRelationRequest.RelatedPersonId);
        result.PersonRelationTypeId.Should().Be(deletePersonRelationRequest.PersonRelationTypeId);
    }

    [Fact]
    public void ToDeletePersonRelationCommand_WhenRequestIsNull_ShouldThrowArgumentNullException()
    {
        // Act
        Action act = () => PersonRelationMapper.ToDeletePersonRelationCommand(null);

        // Assert
        act.Should().Throw<ArgumentNullException>();
    }
}