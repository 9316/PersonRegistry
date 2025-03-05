using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using PersonRegistry.API.Controllers;
using PersonRegistry.API.Tests.TestData;
using PersonRegistry.Application.Person.Commands.Create;
using PersonRegistry.Application.Person.Commands.Delete;
using PersonRegistry.Application.Person.Commands.Update;
using PersonRegistry.Application.Person.Commands.UploadPhoto;
using PersonRegistry.Application.Person.Queries.DownloadPersonImage;
using PersonRegistry.Application.Person.Queries.GetPerson;
using PersonRegistry.Application.Person.Queries.GetPersons;

/// <summary>
/// Unit tests for the <see cref="PersonController"/> class.
/// </summary>
public class PersonControllerTests
{
    private readonly IMediator _mediator;
    private readonly PersonController _personController;

    public PersonControllerTests()
    {
        _mediator = Substitute.For<IMediator>();
        _personController = new PersonController(_mediator);
    }

    [Fact]
    public async Task GetPersonById_ShouldReturnPerson_WhenCalled()
    {
        // Arrange
        var personId = PersonTestData.PERSON_ID;
        var getPersonModelRequest = PersonTestData.BuildGetPersonModelRequest(personId);
        var getPersonModelResponse = PersonTestData.BuildGetPersonModelResponse(personId, "Name");

        _mediator.Send(Arg.Any<GetPersonQuery>(), Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(getPersonModelResponse));

        // Act
        var result = await _personController.GetPersonById(getPersonModelRequest, CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(getPersonModelResponse);
        await _mediator.Received(1).Send(Arg.Any<GetPersonQuery>(), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task GetPersons_ShouldReturnPagedResult_WhenCalled()
    {
        // Arrange
        var request = new GetPersonsModelRequest();

        var pagedPersonsResponse = PersonTestData.BuildPagedPersons();

        _mediator.Send(Arg.Any<GetPersonsQuery>(), Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(pagedPersonsResponse));

        // Act
        var result = await _personController.GetPersons(request, CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(pagedPersonsResponse);
        await _mediator.Received(1).Send(Arg.Any<GetPersonsQuery>(), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task PostPerson_ShouldReturnPersonId_WhenCalled()
    {
        // Arrange
        var createPersonRequest = PersonTestData.BuildCreatePersonRequest();
        var personId = PersonTestData.PERSON_ID;

        _mediator.Send(Arg.Any<CreatePersonCommand>(), Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(personId));

        // Act
        var result = await _personController.PostPerson(createPersonRequest, CancellationToken.None);

        // Assert
        result.Should().Be(personId);
        await _mediator.Received(1).Send(Arg.Any<CreatePersonCommand>(), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task UpdatePerson_ShouldCallMediatR_WhenCalled()
    {
        // Arrange
        var updatePersonRequest = PersonTestData.BuildUpdatePersonRequest();

        // Act
        await _personController.UpdatePerson(updatePersonRequest, CancellationToken.None);

        // Assert
        await _mediator.Received(1).Send(Arg.Any<UpdatePersonCommand>(), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task DeletePerson_ShouldCallMediatR_WhenCalled()
    {
        // Arrange
        var deletePersonRequest = PersonTestData.BuildDeletePersonRequest(PersonTestData.PERSON_ID);

        // Act
        await _personController.DeletePerson(deletePersonRequest, CancellationToken.None);

        // Assert
        await _mediator.Received(1).Send(Arg.Any<DeletePersonCommand>(), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task DownloadPersonPhoto_ShouldReturnFile_WhenPhotoUrlIsValid()
    {
        // Arrange
        var downloadPersonImageRequest = PersonTestData.BuildDownloadPersonImageRequest();
        var fakeImageBytes = new byte[] { 1, 2, 3, 4, 5 };

        _mediator.Send(Arg.Any<DownloadPersonImageCommand>(), Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(fakeImageBytes));

        // Act
        var result = await _personController.DownloadPersonPhoto(downloadPersonImageRequest, CancellationToken.None);

        // Assert
        result.Should().BeOfType<FileContentResult>();
        var fileResult = (FileContentResult)result;
        fileResult.ContentType.Should().Be("image/jpeg");
        fileResult.FileContents.Should().BeEquivalentTo(fakeImageBytes);
    }

    [Fact]
    public async Task UploadPersonPhoto_ShouldCallMediatR_WhenCalled()
    {
        // Arrange
        var uploadPersonPhotoRequest = PersonTestData.BuildUploadPersonPhotoRequest(PersonTestData.PERSON_ID, "Photo");

        _mediator.Send(Arg.Any<UploadPersonPhotoCommand>(), Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(Unit.Value));

        // Act
        var result = await _personController.UploadPersonPhoto(uploadPersonPhotoRequest, CancellationToken.None);

        // Assert
        result.Should().Be(Unit.Value);
        await _mediator.Received(1).Send(Arg.Any<UploadPersonPhotoCommand>(), Arg.Any<CancellationToken>());
    }
}