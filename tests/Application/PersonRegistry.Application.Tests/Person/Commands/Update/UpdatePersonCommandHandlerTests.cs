using Moq;
using FluentAssertions;
using MediatR;
using PersonRegistry.Application.Person.Commands.Update;
using PersonRegistry.Common.Exceptions;
using PersonRegistry.Domain.Interfaces;
using PersonRegistry.Domain.Aggregates.Person;
using PersonRegistry.Domain.Aggregates.City;
using PersonRegistry.Domain.Enums;

namespace PersonRegistry.Application.Tests.Person.Commands.Update;

/// <summary>
/// Unit tests for custom exceptions in <see cref="UpdatePersonCommandHandler"/>.
/// </summary>
public class UpdatePersonCommandHandlerTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<ICityRepository> _cityRepositoryMock;
    private readonly Mock<IPersonRepository> _personRepositoryMock;
    private readonly UpdatePersonCommandHandler _updatePersonCommandHandler;

    public UpdatePersonCommandHandlerTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _cityRepositoryMock = new Mock<ICityRepository>();
        _personRepositoryMock = new Mock<IPersonRepository>();

        _unitOfWorkMock.Setup(u => u.CityRepository).Returns(_cityRepositoryMock.Object);
        _unitOfWorkMock.Setup(u => u.PersonRepository).Returns(_personRepositoryMock.Object);

        _updatePersonCommandHandler = new UpdatePersonCommandHandler(_unitOfWorkMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldThrowNotFoundException_WhenCityDoesNotExist()
    {
        // Arrange
        var command = new UpdatePersonCommand(Id: 1, Name: "John", LastName: "Doe", PersonalNumber: "123456789", BirthDate: new DateTime(1990, 1, 1), Gender: GenderEnum.Male, CityId: 2);

        _cityRepositoryMock
            .Setup(repo => repo.AnyAsync(x => x.Id == command.CityId))
            .ReturnsAsync(false);

        // Act
        var act = async () => await _updatePersonCommandHandler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>()
            .WithMessage("*City*");
    }

    [Fact]
    public async Task Handle_ShouldThrowNotFoundException_WhenPersonDoesNotExist()
    {
        // Arrange
        var command = new UpdatePersonCommand(Id: 1, Name: "John", LastName: "Doe", PersonalNumber: "123456789", BirthDate: new DateTime(1995, 1, 1), Gender: GenderEnum.Male, CityId: 2);

        _cityRepositoryMock
            .Setup(repo => repo.AnyAsync(x => x.Id == command.CityId))
            .ReturnsAsync(true);

        _personRepositoryMock
            .Setup(repo => repo.GetByIdAsync(command.Id))
            .ReturnsAsync((Domain.Aggregates.Person.Person)null);

        // Act
        var act = async () => await _updatePersonCommandHandler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>()
            .WithMessage("*Person*");
    }

    [Fact]
    public async Task Handle_ShouldUpdatePerson_WhenPersonExists()
    {
        // Arrange
        var name = "John";
        var lastName = "Doe";
        var personalNumber = "987654321";
        var birthDate = new DateTime(1995, 1, 1);
        var gender = GenderEnum.Male;
        var cityId = 2; 

        var command = new UpdatePersonCommand(Id: 1, name, lastName, personalNumber, birthDate, gender, cityId);
        var person = Domain.Aggregates.Person.Person.Create(name: "OldName", lastName: "OldLastName", personalNumber: "123456789", birthDate: new DateTime(1990, 1, 1), gender, cityId: 1);

        _cityRepositoryMock
            .Setup(repo => repo.AnyAsync(x => x.Id == command.CityId))
            .ReturnsAsync(true);

        _personRepositoryMock
            .Setup(repo => repo.GetByIdAsync(command.Id))
            .ReturnsAsync(person);

        _unitOfWorkMock
            .Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        // Act
        var result = await _updatePersonCommandHandler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().Be(Unit.Value);
        person.Name.Should().Be(name);
        person.LastName.Should().Be(lastName);
        person.PersonalNumber.Should().Be(personalNumber);
        person.BirthDate.Should().Be(birthDate);
        person.Gender.Should().Be(gender);
        person.CityId.Should().Be(cityId);

        _personRepositoryMock.Verify(repo => repo.Update(person), Times.Once);
        _unitOfWorkMock.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}