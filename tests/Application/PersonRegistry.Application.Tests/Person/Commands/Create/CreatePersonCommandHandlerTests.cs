using Moq;
using FluentAssertions;
using PersonRegistry.Application.Person.Commands.Create;
using PersonRegistry.Common.Exceptions;
using PersonRegistry.Domain.Interfaces;
using PersonRegistry.Domain.Aggregates.City;
using PersonRegistry.Domain.Aggregates.Person;
using PersonRegistry.Domain.Enums;

namespace PersonRegistry.Application.Tests.Person.Commands.Create;

/// <summary>
/// Unit tests for custom exceptions in <see cref="CreatePersonCommandHandler"/>.
/// </summary>
public class CreatePersonCommandHandlerTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<ICityRepository> _cityRepositoryMock;
    private readonly Mock<IPersonRepository> _personRepositoryMock;
    private readonly CreatePersonCommandHandler _createPersonCommandHandler;

    public CreatePersonCommandHandlerTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _cityRepositoryMock = new Mock<ICityRepository>();
        _personRepositoryMock = new Mock<IPersonRepository>();

        _unitOfWorkMock.Setup(u => u.CityRepository).Returns(_cityRepositoryMock.Object);
        _unitOfWorkMock.Setup(u => u.PersonRepository).Returns(_personRepositoryMock.Object);

        _createPersonCommandHandler = new CreatePersonCommandHandler(_unitOfWorkMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldThrowNotFoundException_WhenCityDoesNotExist()
    {
        // Arrange
        var createPersonCommand = new CreatePersonCommand(Name:"John", LastName:"Doe", PersonalNumber: "123456789", BirthDate: new DateTime(1990, 1, 1), Gender: GenderEnum.Male, CityId:1);

        _cityRepositoryMock
            .Setup(repo => repo.GetByIdAsync(createPersonCommand.CityId))
            .ReturnsAsync((Domain.Aggregates.City.City)null);

        // Act
        var act = async () => await _createPersonCommandHandler.Handle(createPersonCommand, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>()
            .WithMessage("*City*");
    }

    [Fact]
    public async Task Handle_ShouldThrowAlreadyExistsException_WhenPersonWithSamePersonalNumberExists()
    {
        // Arrange
        var createPersonCommand = new CreatePersonCommand(Name: "John", LastName: "Doe", PersonalNumber: "123456789", BirthDate: new DateTime(1990, 1, 1), Gender: GenderEnum.Male, CityId: 1);
        var existingCity = Domain.Aggregates.City.City.Create("Tbilisi");

        _cityRepositoryMock
            .Setup(repo => repo.GetByIdAsync(createPersonCommand.CityId))
            .ReturnsAsync(existingCity);

        _personRepositoryMock
            .Setup(repo => repo.AnyAsync(p => p.PersonalNumber == createPersonCommand.PersonalNumber.Trim()))
            .ReturnsAsync(true);

        // Act
        var act = async () => await _createPersonCommandHandler.Handle(createPersonCommand, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<AlreadyExistsException>()
            .WithMessage("*123456789*");
    }

    [Fact]
    public async Task Handle_ShouldCreatePerson_WhenPersonDoesNotExist()
    {
        // Arrange
        var createPersonCommand = new CreatePersonCommand(Name: "John", LastName: "Doe", PersonalNumber: "123456789", BirthDate: new DateTime(1990, 1, 1), Gender: GenderEnum.Male, CityId: 1);
        var existingCity = Domain.Aggregates.City.City.Create("Tbilisi");

        _cityRepositoryMock
            .Setup(repo => repo.GetByIdAsync(createPersonCommand.CityId))
            .ReturnsAsync(existingCity);

        _personRepositoryMock
            .Setup(repo => repo.AnyAsync(p => p.PersonalNumber == createPersonCommand.PersonalNumber.Trim()))
            .ReturnsAsync(false);

        _personRepositoryMock
            .Setup(repo => repo.AddAsync(It.IsAny<Domain.Aggregates.Person.Person>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        _unitOfWorkMock
            .Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        // Act
        var result = await _createPersonCommandHandler.Handle(createPersonCommand, CancellationToken.None);

        // Assert
        result.Should().Be(1);

        _personRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Domain.Aggregates.Person.Person>(), It.IsAny<CancellationToken>()), Times.Once);
        _unitOfWorkMock.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}
