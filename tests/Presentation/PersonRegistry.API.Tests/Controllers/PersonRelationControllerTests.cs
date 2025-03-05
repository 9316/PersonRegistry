using FluentAssertions;
using MediatR;
using NSubstitute;
using PersonRegistry.API.Controllers;
using PersonRegistry.API.Tests.TestData;
using PersonRegistry.Application.PersonRelation.Command.Create;
using PersonRegistry.Application.PersonRelation.Command.Delete;

/// <summary>
/// Unit tests for the <see cref="PersonRelationController"/> class.
/// </summary>
public class PersonRelationControllerTests
{
    private readonly IMediator _mediator;
    private readonly PersonRelationController _personRelationController;

    public PersonRelationControllerTests()
    {
        _mediator = Substitute.For<IMediator>();
        _personRelationController = new PersonRelationController(_mediator);
    }

    [Fact]
    public async Task CreatePersonRelation_ShouldCallMediatR_WhenCalled()
    {
        // Arrange
        var relationId = PersonRelationTestData.RELATION_ID;
        var buildCreatePersonRelationRequest = PersonRelationTestData.BuildCreatePersonRelationRequest();

        _mediator.Send(Arg.Any<CreatePersonRelationCommand>(), Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(relationId));

        // Act
        var result = await _personRelationController.CreatePersonRelation(buildCreatePersonRelationRequest, CancellationToken.None);

        // Assert
        result.Should().Be(relationId);
        await _mediator.Received(1).Send(Arg.Any<CreatePersonRelationCommand>(), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task DeletePerson_ShouldCallMediatR_WhenCalled()
    {
        // Arrange
        var deletePersonRelationRequest = PersonRelationTestData.BuildDeletePersonRelationRequest(personId: PersonRelationTestData.PERSON_ID);

        // Act
        await _personRelationController.DeletePerson(deletePersonRelationRequest, CancellationToken.None);

        // Assert
        await _mediator.Received(1).Send(Arg.Any<DeletePersonRelationCommand>(), Arg.Any<CancellationToken>());
    }
}
