using Moq;
using FluentAssertions;
using PersonRegistry.Application.Person.Queries.GetPerson;
using PersonRegistry.Common.Exceptions;
using PersonRegistry.Domain.Interfaces;
using PersonRegistry.Domain.Aggregates.Person;
using PersonRegistry.Domain.Enums;

namespace PersonRegistry.Application.Tests.Person.Queries.GetPerson;

/// <summary>
/// Unit tests for custom exceptions in <see cref="UploadPersonPhotoCommandHandler"/>.
/// </summary>
public class GetPersonQueryHandlerTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IPersonRepository> _personRepositoryMock;
    private readonly GetPersonQueryHandler _getPersonQueryHandler;

    public GetPersonQueryHandlerTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _personRepositoryMock = new Mock<IPersonRepository>();

        _unitOfWorkMock.Setup(u => u.PersonRepository).Returns(_personRepositoryMock.Object);

        _getPersonQueryHandler = new GetPersonQueryHandler(_unitOfWorkMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldThrowNotFoundException_WhenPersonDoesNotExist()
    {
        // Arrange
        var getPersonQuery = new GetPersonQuery(Id: 1);

        _personRepositoryMock
            .Setup(repo => repo.GetDetailsByIdAsync(getPersonQuery.Id))
            .ReturnsAsync((Domain.Aggregates.Person.Person)null);

        // Act
        var act = async () => await _getPersonQueryHandler.Handle(getPersonQuery, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>()
            .WithMessage("*Person*");
    }

    [Fact]
    public async Task Handle_ShouldReturnPersonDetails_WhenPersonExists()
    {
        // Arrange
        var name = "John";
        var lastName = "Doe";
        var personalNumber = "987654321";
        var birthDate = new DateTime(1995, 1, 1);
        var gender = GenderEnum.Male;
        var cityId = 1;

        var city = Domain.Aggregates.City.City.Create("Rustavi");

        var getPersonQuery = new GetPersonQuery(Id: 1);
        var person = Domain.Aggregates.Person.Person.Create(name, lastName, personalNumber, birthDate, gender, cityId);


        typeof(Domain.Aggregates.Person.Person).GetProperty("City")?.SetValue(person, city);

        _personRepositoryMock
            .Setup(repo => repo.GetDetailsByIdAsync(getPersonQuery.Id))
        .ReturnsAsync(person);

        // Act
        var result = await _getPersonQueryHandler.Handle(getPersonQuery, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(name);
        result.LastName.Should().Be(lastName);
        result.PersonalNumber.Should().Be(personalNumber);
        result.BirthDate.Should().Be(birthDate);
        result.Gender.Should().Be(gender);
        result.CityId.Should().Be(cityId);
    }
}