using Moq;
using FluentAssertions;
using PersonRegistry.Application.City.Commands.Create;
using PersonRegistry.Common.Exceptions;
using PersonRegistry.Domain.Interfaces;
using PersonRegistry.Domain.Aggregates.City;
using System.Linq.Expressions;

namespace PersonRegistry.Application.Tests.City.Commands.Create;

/// <summary>
/// Unit tests for custom exceptions in <see cref="CreateCityCommandHandler"/>.
/// </summary>
public class CreateCityCommandHandlerTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<ICityRepository> _cityRepositoryMock;
    private readonly CreateCityCommandHandler _createCityCommandHandler;

    public CreateCityCommandHandlerTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _cityRepositoryMock = new Mock<ICityRepository>();

        _unitOfWorkMock.Setup(u => u.CityRepository).Returns(_cityRepositoryMock.Object);

        _createCityCommandHandler = new CreateCityCommandHandler(_unitOfWorkMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldThrowAlreadyExistsException_WhenCityAlreadyExists()
    {
        // Arrange
        var createCityCommand = new CreateCityCommand(Name: "Tbilisi");

        _cityRepositoryMock
            .Setup(repo => repo.AnyAsync(It.IsAny<Expression<Func<Domain.Aggregates.City.City, bool>>>()))
            .ReturnsAsync(true);

        // Act
        var act = async () => await _createCityCommandHandler.Handle(createCityCommand, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<AlreadyExistsException>()
            .WithMessage("*Tbilisi*");
    }

    [Fact]
    public async Task Handle_ShouldCreateCity_WhenCityDoesNotExist()
    {
        // Arrange
        var createCityCommand = new CreateCityCommand(Name: "Tbilisi");

        _cityRepositoryMock
            .Setup(repo => repo.AnyAsync(It.IsAny<Expression<Func<Domain.Aggregates.City.City, bool>>>()))
            .ReturnsAsync(false);

        _cityRepositoryMock
            .Setup(repo => repo.AddAsync(It.IsAny<Domain.Aggregates.City.City>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        _unitOfWorkMock
            .Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        // Act
        var result = await _createCityCommandHandler.Handle(createCityCommand, CancellationToken.None);

        // Assert
        _cityRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Domain.Aggregates.City.City>(), It.IsAny<CancellationToken>()), Times.Once);
        _unitOfWorkMock.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}