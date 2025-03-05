using Moq;
using FluentAssertions;
using MediatR;
using PersonRegistry.Application.Person.Commands.UploadPhoto;
using PersonRegistry.Application.Services;
using PersonRegistry.Common.Exceptions;
using PersonRegistry.Domain.Interfaces;
using PersonRegistry.Domain.Aggregates.Person;
using Microsoft.AspNetCore.Http;
using PersonRegistry.Domain.Enums;

namespace PersonRegistry.Application.Tests.Person.Commands.UploadPhoto;

/// <summary>
/// Unit tests for custom exceptions in <see cref="UploadPersonPhotoCommandHandler"/>.
/// </summary>
public class UploadPersonPhotoCommandHandlerTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IPersonRepository> _personRepositoryMock;
    private readonly Mock<IFileManagerService> _fileManagerMock;
    private readonly UploadPersonPhotoCommandHandler _uploadPersonPhotoCommandHandler;

    public UploadPersonPhotoCommandHandlerTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _personRepositoryMock = new Mock<IPersonRepository>();
        _fileManagerMock = new Mock<IFileManagerService>();

        _unitOfWorkMock.Setup(u => u.PersonRepository).Returns(_personRepositoryMock.Object);

        _uploadPersonPhotoCommandHandler = new UploadPersonPhotoCommandHandler(_unitOfWorkMock.Object, _fileManagerMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldThrowNotFoundException_WhenPersonDoesNotExist()
    {
        // Arrange
        var command = new UploadPersonPhotoCommand(PersonId: 1, Photo: CreateMockFile("new-photo.jpg"));

        _personRepositoryMock
            .Setup(repo => repo.GetByIdAsync(command.PersonId))
            .ReturnsAsync((Domain.Aggregates.Person.Person)null);

        // Act
        var act = async () => await _uploadPersonPhotoCommandHandler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>()
            .WithMessage("*Person*");
    }

    [Fact]
    public async Task Handle_ShouldUploadNewPhoto_WhenPersonHasNoExistingPhoto()
    {
        // Arrange
        var photo = "uploaded-photo-url.jpg";
        var uploadPersonPhotoCommand = new UploadPersonPhotoCommand(PersonId: 1, Photo: CreateMockFile("new-photo.jpg"));
        var person = Domain.Aggregates.Person.Person.Create(name: "John", lastName: "Doe", personalNumber:"123456789", birthDate: new DateTime(1990, 1, 1), gender: GenderEnum.Male, cityId: 1);

        _personRepositoryMock
            .Setup(repo => repo.GetByIdAsync(uploadPersonPhotoCommand.PersonId))
            .ReturnsAsync(person);
        _fileManagerMock
            .Setup(fm => fm.UploadFileAsync(uploadPersonPhotoCommand.Photo))
            .ReturnsAsync(photo);

        _unitOfWorkMock
            .Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        // Act
        var result = await _uploadPersonPhotoCommandHandler.Handle(uploadPersonPhotoCommand, CancellationToken.None);

        // Assert
        result.Should().Be(Unit.Value);

        person.Photo.Should().Be(photo);

        _fileManagerMock.Verify(fm => fm.UploadFileAsync(uploadPersonPhotoCommand.Photo), Times.Once);
        _fileManagerMock.Verify(fm => fm.ReplaceFileAsync(It.IsAny<IFormFile>(), It.IsAny<string>()), Times.Never);
        _personRepositoryMock.Verify(repo => repo.Update(person), Times.Once);
        _unitOfWorkMock.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    private IFormFile CreateMockFile(string fileName)
    {
        var fileMock = new Mock<IFormFile>();
        var stream = new MemoryStream();
        var writer = new StreamWriter(stream);

        writer.Write("dummy file content");
        writer.Flush();
        stream.Position = 0;

        fileMock.Setup(f => f.OpenReadStream()).Returns(stream);
        fileMock.Setup(f => f.FileName).Returns(fileName);
        fileMock.Setup(f => f.Length).Returns(stream.Length);

        return fileMock.Object;
    }
}
