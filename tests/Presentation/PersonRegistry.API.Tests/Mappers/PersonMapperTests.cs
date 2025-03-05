using FluentAssertions;
using PersonRegistry.API.Mappers;
using PersonRegistry.API.Tests.TestData;

/// <summary>
/// Unit tests for the <see cref="PersonMapper"/> class.
/// </summary>
public class PersonMapperTests
{
    [Fact]
    public void ToPersonQuery_WhenRequestIsValid_ShouldMapCorrectly()
    {
        // Arrange
        var request = PersonTestData.BuildGetPersonModelRequest(1);

        // Act
        var query = request.ToPersonQuery();

        // Assert
        query.Should().NotBeNull();
        query.Id.Should().Be(request.Id);
    }

    [Fact]
    public void ToPersonQuery_WhenRequestIsNull_ShouldThrowArgumentNullException()
    {
        // Act
        Action act = () => PersonMapper.ToPersonQuery(null);

        // Assert
        act.Should().Throw<ArgumentNullException>();
    }


    [Fact]
    public void ToGetPersonQuery_WhenRequestIsValid_ShouldMapCorrectly()
    {
        // Arrange
        var getPersonsModelRequest = PersonTestData.BuildGetPersonsModelRequest();

        // Act
        var result = getPersonsModelRequest.ToGetPersonQuery();

        // Assert
        result.Should().NotBeNull();
        result.FilterQuery.Should().Be(getPersonsModelRequest.FilterQuery);
        result.Name.Should().Be(getPersonsModelRequest.Name);
        result.LastName.Should().Be(getPersonsModelRequest.LastName);
        result.PersonalNumber.Should().Be(getPersonsModelRequest.PersonalNumber);
        result.Gender.Should().Be(getPersonsModelRequest.Gender);
        result.BirthDate.Should().Be(getPersonsModelRequest.BirthDate);
        result.PageSize.Should().Be(getPersonsModelRequest.PageSize);
        result.PageNumber.Should().Be(getPersonsModelRequest.PageNumber);
    }

    [Fact]
    public void ToGetPersonQuery_WhenRequestIsNull_ShouldThrowArgumentNullException()
    {
        // Act
        Action act = () => PersonMapper.ToGetPersonQuery(null);

        // Assert
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void ToDownloadPersonImageCommand_WhenRequestIsValid_ShouldMapCorrectly()
    {
        // Arrange
        var downloadPersonImageRequest = PersonTestData.BuildDownloadPersonImageRequest();

        // Act
        var result = downloadPersonImageRequest.ToDownloadPersonImageCommand();

        // Assert
        result.Should().NotBeNull();
        result.PhotoUrl.Should().Be(downloadPersonImageRequest.PhotoUrl);
    }

    [Fact]
    public void ToCreatePersonCommand_WhenRequestIsValid_ShouldMapCorrectly()
    {
        // Arrange
        var createPersonRequest = PersonTestData.BuildCreatePersonRequest();

        // Act
        var result = createPersonRequest.ToCreatePersonCommand();

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(createPersonRequest.Name);
        result.LastName.Should().Be(createPersonRequest.LastName);
        result.PersonalNumber.Should().Be(createPersonRequest.PersonalNumber);
        result.BirthDate.Should().Be(createPersonRequest.BirthDate);
        result.Gender.Should().Be(createPersonRequest.Gender);
        result.CityId.Should().Be(createPersonRequest.CityId);
    }

    [Fact]
    public void ToUpdatePersonCommand_WhenRequestIsValid_ShouldMapCorrectly()
    {
        // Arrange
        var updatePersonRequest = PersonTestData.BuildUpdatePersonRequest();

        // Act
        var result = updatePersonRequest.ToUpdatePersonCommand();

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(updatePersonRequest.Id);
        result.Name.Should().Be(updatePersonRequest.Name);
        result.LastName.Should().Be(updatePersonRequest.LastName);
        result.PersonalNumber.Should().Be(updatePersonRequest.PersonalNumber);
        result.BirthDate.Should().Be(updatePersonRequest.BirthDate);
        result.Gender.Should().Be(updatePersonRequest.Gender);
        result.CityId.Should().Be(updatePersonRequest.CityId);
    }

    [Fact]
    public void ToDeletePersonCommand_WhenRequestIsValid_ShouldMapCorrectly()
    {
        // Arrange
        var deletePersonRequest = PersonTestData.BuildDeletePersonRequest(PersonTestData.PERSON_ID);

        // Act
        var result = deletePersonRequest.ToDeletePersonCommand();

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(deletePersonRequest.Id);
    }

    [Fact]
    public void ToDeletePersonPhotoCommand_WhenRequestIsValid_ShouldMapCorrectly()
    {
        // Arrange
        var deletePersonPhotoRequest = PersonTestData.BuildDeletePersonPhotoRequest(PersonTestData.PERSON_ID);

        // Act
        var result = deletePersonPhotoRequest.ToDeletePersonPhotoCommand();

        // Assert
        result.Should().NotBeNull();
        result.PersonId.Should().Be(deletePersonPhotoRequest.PersonId);
    }
}
