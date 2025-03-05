using Moq;
using FluentAssertions;
using MediatR;
using PersonRegistry.Application.Person.Commands.Delete;
using PersonRegistry.Common.Exceptions;
using PersonRegistry.Domain.Interfaces;
using PersonRegistry.Domain.Aggregates.Person;
using PersonRegistry.Domain.Enums;

namespace PersonRegistry.Application.Tests.Person.Commands.Delete;

/// <summary>
/// Unit tests for custom exceptions in <see cref="DeletePersonCommandHandler"/>.
/// </summary>
public class DeletePersonCommandHandlerTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IPersonRepository> _personRepositoryMock;
    private readonly DeletePersonCommandHandler _deletePersonCommandHandler;

    public DeletePersonCommandHandlerTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _personRepositoryMock = new Mock<IPersonRepository>();

        _unitOfWorkMock.Setup(u => u.PersonRepository).Returns(_personRepositoryMock.Object);

        _deletePersonCommandHandler = new DeletePersonCommandHandler(_unitOfWorkMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldThrowNotFoundException_WhenPersonDoesNotExist()
    {
        // Arrange
        var deletePersonCommand = new DeletePersonCommand(1);

        _personRepositoryMock
            .Setup(repo => repo.GetByIdAsync(deletePersonCommand.Id))
            .ReturnsAsync((Domain.Aggregates.Person.Person)null);

        // Act
        var act = async () => await _deletePersonCommandHandler.Handle(deletePersonCommand, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>()
            .WithMessage("*Person*");
    }

    [Fact]
    public async Task Handle_ShouldDeletePerson_WhenPersonExists()
    {
        // Arrange
        var deletePersonCommand = new DeletePersonCommand(1);
        var person = Domain.Aggregates.Person.Person.Create(name:"John", lastName: "Doe", personalNumber: "123456789", birthDate: new DateTime(1990, 1, 1), gender: GenderEnum.Male, cityId: 1);

        _personRepositoryMock
            .Setup(repo => repo.GetByIdAsync(deletePersonCommand.Id))
            .ReturnsAsync(person);

        _unitOfWorkMock
            .Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        // Act
        var result = await _deletePersonCommandHandler.Handle(deletePersonCommand, CancellationToken.None);

        // Assert
        result.Should().Be(Unit.Value);

        _personRepositoryMock.Verify(repo => repo.Update(person), Times.Once);
        _unitOfWorkMock.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}