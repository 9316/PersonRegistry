using FluentAssertions;
using MediatR;
using Moq;
using PersonRegistry.Application.PersonPhoneNumber.Command.Delete;
using PersonRegistry.Common.Exceptions;
using PersonRegistry.Domain.Aggregates.Person;
using PersonRegistry.Domain.Enums;
using PersonRegistry.Domain.Interfaces;

namespace PersonRegistry.Application.Tests.PersonPhoneNumber.Command.Delete;

/// <summary>
/// Unit tests for custom exceptions in <see cref="DeletePersonPhoneNumberCommandHandler"/>.
/// </summary>
public class DeletePersonPhoneNumberCommandHandlerTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IPersonRepository> _personRepositoryMock;
    private readonly DeletePersonPhoneNumberCommandHandler _deletePersonPhoneNumberCommandHandler;

    public DeletePersonPhoneNumberCommandHandlerTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _personRepositoryMock = new Mock<IPersonRepository>();

        _unitOfWorkMock.Setup(u => u.PersonRepository).Returns(_personRepositoryMock.Object);

        _deletePersonPhoneNumberCommandHandler = new DeletePersonPhoneNumberCommandHandler(_unitOfWorkMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldThrowNotFoundException_WhenPersonDoesNotExist()
    {
        // Arrange
        var personId = 1;
        var phoneNumberId = 100;
        var deletePersonPhoneNumberCommand = new DeletePersonPhoneNumberCommand(personId, phoneNumberId);

        _personRepositoryMock.Setup(repo => repo.GetDetailsByIdAsync(personId))
            .ReturnsAsync((Domain.Aggregates.Person.Person)null);

        // Act
        var act = async () => await _deletePersonPhoneNumberCommandHandler.Handle(deletePersonPhoneNumberCommand, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>()
            .WithMessage($"*{personId}*");

        _unitOfWorkMock.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task Handle_ShouldThrowNotFoundException_WhenPhoneNumberDoesNotExist()
    {
        // Arrange
        var personId = 1;
        var phoneNumberId = 100;

        var person = Domain.Aggregates.Person.Person.Create(name: "John", lastName: "Doe", personalNumber: "123456789", birthDate: new DateTime(1990, 1, 1), gender: GenderEnum.Male, cityId: 1);

        var deletePersonPhoneNumberCommand = new DeletePersonPhoneNumberCommand(personId, phoneNumberId);

        _personRepositoryMock.Setup(repo => repo.GetDetailsByIdAsync(personId))
            .ReturnsAsync(person);

        // Act
        var act = async () => await _deletePersonPhoneNumberCommandHandler.Handle(deletePersonPhoneNumberCommand, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>()
            .WithMessage($"*{phoneNumberId}*");

        _unitOfWorkMock.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }
}