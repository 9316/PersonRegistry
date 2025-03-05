using FluentAssertions;
using MediatR;
using NSubstitute;
using PersonRegistry.API.Controllers;
using PersonRegistry.API.Tests.TestData;
using PersonRegistry.Application.PersonPhoneNumber.Command.Create;
using PersonRegistry.Application.PersonPhoneNumber.Command.Delete;
using PersonRegistry.Application.PersonPhoneNumber.Command.Update;

/// <summary>
/// Unit tests for the <see cref="PersonPhoneNumberController"/> class.
/// </summary>
public class PersonPhoneNumberControllerTests
{
    private readonly IMediator _mediator;
    private readonly PersonPhoneNumberController _personPhoneNumberController;

    public PersonPhoneNumberControllerTests()
    {
        _mediator = Substitute.For<IMediator>();
        _personPhoneNumberController = new PersonPhoneNumberController(_mediator);
    }

    [Fact]
    public async Task CreatePersonPhoneNumber_ShouldCallMediatR_WhenCalled()
    {
        // Arrange
        var createPersonPhoneNumberRequest = PersonPhoneNumberTestData.BuildCreatePersonPhoneNumberRequest();

        _mediator.Send(Arg.Any<CreatePersonPhoneNumberCommand>(), Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(Unit.Value));

        // Act
        var result = await _personPhoneNumberController.CreatePersonPhoneNumber(createPersonPhoneNumberRequest, CancellationToken.None);

        // Assert
        result.Should().Be(Unit.Value);
        await _mediator.Received(1).Send(Arg.Any<CreatePersonPhoneNumberCommand>(), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task DeletePersonPhoneNumber_ShouldCallMediatR_WhenCalled()
    {
        // Arrange
        var deletePersonPhoneNumberRequest = PersonPhoneNumberTestData.BuildDeletePersonPhoneNumberRequest(PersonPhoneNumberTestData.PERSON_ID);

        // Act
        await _personPhoneNumberController.DeletePersonPhoneNumber(deletePersonPhoneNumberRequest, CancellationToken.None);

        // Assert
        await _mediator.Received(1).Send(Arg.Any<DeletePersonPhoneNumberCommand>(), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task UpdatePersonPhoneNumber_ShouldCallMediatR_WhenCalled()
    {
        // Arrange
        var updatePersonPhoneNumberRequest = PersonPhoneNumberTestData.BuildUpdatePersonPhoneNumberRequest();

        // Act
        await _personPhoneNumberController.UpdatePersonPhoneNumber(updatePersonPhoneNumberRequest, CancellationToken.None);

        // Assert
        await _mediator.Received(1).Send(Arg.Any<UpdatePersonPhoneNumberCommand>(), Arg.Any<CancellationToken>());
    }
}