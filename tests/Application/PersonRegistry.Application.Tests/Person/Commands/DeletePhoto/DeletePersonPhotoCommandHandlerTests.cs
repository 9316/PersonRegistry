using Moq;
using FluentAssertions;
using MediatR;
using PersonRegistry.Application.Person.Commands.DeletePhoto;
using PersonRegistry.Application.Services;
using PersonRegistry.Common.Exceptions;
using PersonRegistry.Domain.Interfaces;
using PersonRegistry.Domain.Aggregates.Person;
using PersonRegistry.Domain.Enums;

namespace PersonRegistry.Application.Tests.Person.Commands.DeletePhoto;

/// <summary>
/// Unit tests for custom exceptions in <see cref="DeletePersonPhotoCommandHandler"/>.
/// </summary>
public class DeletePersonPhotoCommandHandlerTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IPersonRepository> _personRepositoryMock;
    private readonly Mock<IFileManagerService> _fileManagerMock;
    private readonly DeletePersonPhotoCommandHandler _deletePersonPhotoCommandHandler;

    public DeletePersonPhotoCommandHandlerTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _personRepositoryMock = new Mock<IPersonRepository>();
        _fileManagerMock = new Mock<IFileManagerService>();

        _unitOfWorkMock.Setup(u => u.PersonRepository).Returns(_personRepositoryMock.Object);

        _deletePersonPhotoCommandHandler = new DeletePersonPhotoCommandHandler(_unitOfWorkMock.Object, _fileManagerMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldThrowNotFoundException_WhenPersonDoesNotExist()
    {
        // Arrange
        var deletePersonPhotoCommand = new DeletePersonPhotoCommand(1);

        _personRepositoryMock
            .Setup(repo => repo.GetByIdAsync(deletePersonPhotoCommand.PersonId))
            .ReturnsAsync((Domain.Aggregates.Person.Person)null);

        // Act
        var act = async () => await _deletePersonPhotoCommandHandler.Handle(deletePersonPhotoCommand, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>()
            .WithMessage("*Person*");
    }

    [Fact]
    public async Task Handle_ShouldReturnUnitValue_WhenPersonHasNoPhoto()
    {
        // Arrange
        var deletePersonPhotoCommand = new DeletePersonPhotoCommand(1);
        var person = Domain.Aggregates.Person.Person.Create(name: "John", lastName: "Doe", personalNumber: "123456789", birthDate: new DateTime(1990, 1, 1), gender: GenderEnum.Male, cityId: 1);

        _personRepositoryMock
            .Setup(repo => repo.GetByIdAsync(deletePersonPhotoCommand.PersonId))
            .ReturnsAsync(person);

        // Act
        var result = await _deletePersonPhotoCommandHandler.Handle(deletePersonPhotoCommand, CancellationToken.None);

        // Assert
        result.Should().Be(Unit.Value);

        _fileManagerMock.Verify(fm => fm.DeleteFileAsync(It.IsAny<string>()), Times.Never);
        _personRepositoryMock.Verify(repo => repo.Update(It.IsAny<Domain.Aggregates.Person.Person>()), Times.Never);
        _unitOfWorkMock.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }
}