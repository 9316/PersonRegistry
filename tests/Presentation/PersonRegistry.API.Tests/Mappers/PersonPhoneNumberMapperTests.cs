using FluentAssertions;
using PersonRegistry.API.Mappers;
using PersonRegistry.API.Tests.TestData;

/// <summary>
/// Unit tests for the <see cref="PersonPhoneNumberMapper"/> class.
/// </summary>
public class PersonPhoneNumberMapperTests
{
    [Fact]
    public void ToCreatePersonPhoneNumberCommand_WhenRequestIsValid_ShouldMapCorrectly()
    {
        // Arrange
        var createPersonPhoneNumberRequest = PersonPhoneNumberTestData.BuildCreatePersonPhoneNumberRequest();

        // Act
        var result = createPersonPhoneNumberRequest.ToCreatePersonPhoneNumberCommand();

        // Assert
        result.Should().NotBeNull();
        result.PersonId.Should().Be(createPersonPhoneNumberRequest.PersonId);
        result.PhoneNumberTypeId.Should().Be(createPersonPhoneNumberRequest.PhoneNumberTypeId);
        result.PhoneNumber.Should().Be(createPersonPhoneNumberRequest.PhoneNumber);
    }

    [Fact]
    public void ToCreatePersonPhoneNumberCommand_WhenRequestIsNull_ShouldThrowArgumentNullException()
    {
        // Act
        Action act = () => PersonPhoneNumberMapper.ToCreatePersonPhoneNumberCommand(null);

        // Assert
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void ToDeletePersonPhoneNumberCommand_WhenRequestIsValid_ShouldMapCorrectly()
    {
        // Arrange
        var deletePersonPhoneNumberRequest = PersonPhoneNumberTestData.BuildDeletePersonPhoneNumberRequest();

        // Act
        var result = deletePersonPhoneNumberRequest.ToDeletePersonPhoneNumberCommand();

        // Assert
        result.Should().NotBeNull();
        result.PersonId.Should().Be(deletePersonPhoneNumberRequest.PersonId);
        result.PersonPhoneNumberId.Should().Be(deletePersonPhoneNumberRequest.PersonPhoneNumberId);
    }

    [Fact]
    public void ToDeletePersonPhoneNumberCommand_WhenRequestIsNull_ShouldThrowArgumentNullException()
    {
        // Act
        Action act = () => PersonPhoneNumberMapper.ToDeletePersonPhoneNumberCommand(null);

        // Assert
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void ToUpdatePersonPhoneNumberCommand_WhenRequestIsValid_ShouldMapCorrectly()
    {
        // Arrange
        var updatePersonPhoneNumberRequest = PersonPhoneNumberTestData.BuildUpdatePersonPhoneNumberRequest();

        // Act
        var result = updatePersonPhoneNumberRequest.ToUpdatePersonPhoneNumberCommand();

        // Assert
        result.Should().NotBeNull();
        result.PersonId.Should().Be(updatePersonPhoneNumberRequest.PersonId);
        result.PhoneNumberTypeId.Should().Be(updatePersonPhoneNumberRequest.PhoneNumberTypeId);
        result.PersonPhoneNumberId.Should().Be(updatePersonPhoneNumberRequest.PersonPhoneNumberId);
        result.PhoneNumber.Should().Be(updatePersonPhoneNumberRequest.PhoneNumber);
    }

    [Fact]
    public void ToUpdatePersonPhoneNumberCommand_WhenRequestIsNull_ShouldThrowArgumentNullException()
    {
        // Act
        Action act = () => PersonPhoneNumberMapper.ToUpdatePersonPhoneNumberCommand(null);

        // Assert
        act.Should().Throw<ArgumentNullException>();
    }
}