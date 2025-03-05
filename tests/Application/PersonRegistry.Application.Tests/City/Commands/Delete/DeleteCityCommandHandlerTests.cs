using Moq;
using FluentAssertions;
using PersonRegistry.Application.City.Commands.Delete;
using PersonRegistry.Common.Exceptions;
using PersonRegistry.Domain.Interfaces;
using PersonRegistry.Domain.Aggregates.City;
using MediatR;

namespace PersonRegistry.Application.Tests.City.Commands.Delete;

/// <summary>
/// Unit tests for custom exceptions in <see cref="DeleteCityCommandHandler"/>.
/// </summary>
public class DeleteCityCommandHandlerTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<ICityRepository> _cityRepositoryMock;
    private readonly DeleteCityCommandHandler _deleteCityCommandHandler;

    public DeleteCityCommandHandlerTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _cityRepositoryMock = new Mock<ICityRepository>();

        _unitOfWorkMock.Setup(u => u.CityRepository).Returns(_cityRepositoryMock.Object);

        _deleteCityCommandHandler = new DeleteCityCommandHandler(_unitOfWorkMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldThrowNotFoundException_WhenCityDoesNotExist()
    {
        // Arrange
        var deleteCityCommand = new DeleteCityCommand(Id: 1);

        _cityRepositoryMock
            .Setup(repo => repo.GetByIdAsync(deleteCityCommand.Id))
            .ReturnsAsync((Domain.Aggregates.City.City)null);

        // Act
        var act = async () => await _deleteCityCommandHandler.Handle(deleteCityCommand, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>()
            .WithMessage("*City*");
    }

    [Fact]
    public async Task Handle_ShouldDeleteCity_WhenCityExists()
    {
        // Arrange
        var deleteCityCommand = new DeleteCityCommand(Id: 1);
        var city = Domain.Aggregates.City.City.Create("Tbilisi");

        _cityRepositoryMock
            .Setup(repo => repo.GetByIdAsync(deleteCityCommand.Id))
            .ReturnsAsync(city);

        _unitOfWorkMock
            .Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        // Act
        var result = await _deleteCityCommandHandler.Handle(deleteCityCommand, CancellationToken.None);

        // Assert
        result.Should().Be(Unit.Value);

        _cityRepositoryMock.Verify(repo => repo.Update(city), Times.Once);
        _unitOfWorkMock.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}