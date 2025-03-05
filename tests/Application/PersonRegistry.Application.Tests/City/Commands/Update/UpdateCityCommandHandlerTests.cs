using Moq;
using FluentAssertions;
using PersonRegistry.Application.City.Commands.Update;
using PersonRegistry.Common.Exceptions;
using PersonRegistry.Domain.Interfaces;
using PersonRegistry.Domain.Aggregates.City;
using MediatR;
using System.Linq.Expressions;

namespace PersonRegistry.Application.Tests.City.Commands.Update;

/// <summary>
/// Unit tests for custom exceptions in <see cref="UpdateCityCommandHandler"/>.
/// </summary>
public class UpdateCityCommandHandlerTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<ICityRepository> _cityRepositoryMock;
    private readonly UpdateCityCommandHandler _updateCityCommandHandler;

    public UpdateCityCommandHandlerTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _cityRepositoryMock = new Mock<ICityRepository>();

        _unitOfWorkMock.Setup(u => u.CityRepository).Returns(_cityRepositoryMock.Object);

        _updateCityCommandHandler = new UpdateCityCommandHandler(_unitOfWorkMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldThrowAlreadyExistsException_WhenCityWithSameNameExists()
    {
        // Arrange
        var updateCityCommand = new UpdateCityCommand(Id: 1, Name: "Tbilisi");

        _cityRepositoryMock
            .Setup(repo => repo.AnyAsync(It.IsAny<Expression<Func<Domain.Aggregates.City.City, bool>>>()))
            .ReturnsAsync(true);

        // Act
        var act = async () => await _updateCityCommandHandler.Handle(updateCityCommand, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<AlreadyExistsException>()
            .WithMessage("*Tbilisi*");
    }

    [Fact]
    public async Task Handle_ShouldThrowNotFoundException_WhenCityDoesNotExist()
    {
        // Arrange
        var updateCityCommand = new UpdateCityCommand(Id: 1, Name: "Tbilisi");

        _cityRepositoryMock
            .Setup(repo => repo.AnyAsync(It.IsAny<Expression<Func<Domain.Aggregates.City.City, bool>>>()))
            .ReturnsAsync(false);

        _cityRepositoryMock
            .Setup(repo => repo.GetByIdAsync(updateCityCommand.Id))
            .ReturnsAsync((Domain.Aggregates.City.City)null);

        // Act
        var act = async () => await _updateCityCommandHandler.Handle(updateCityCommand, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>()
            .WithMessage("*City*");
    }

    [Fact]
    public async Task Handle_ShouldUpdateCity_WhenCityExistsAndNameIsAvailable()
    {
        // Arrange
        var updateCityCommand = new UpdateCityCommand(Id: 1, Name:"Batumi");
        var city = Domain.Aggregates.City.City.Create(name: "Tbilisi");

        _cityRepositoryMock
            .Setup(repo => repo.AnyAsync(It.IsAny<Expression<Func<Domain.Aggregates.City.City, bool>>>()))
            .ReturnsAsync(false);

        _cityRepositoryMock
            .Setup(repo => repo.GetByIdAsync(updateCityCommand.Id))
            .ReturnsAsync(city);

        _unitOfWorkMock
            .Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        // Act
        var result = await _updateCityCommandHandler.Handle(updateCityCommand, CancellationToken.None);

        // Assert
        result.Should().Be(Unit.Value);

        city.Name.Should().Be("Batumi");

        _cityRepositoryMock.Verify(repo => repo.Update(city), Times.Once);
        _unitOfWorkMock.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}