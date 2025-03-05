using FluentAssertions;
using MediatR;
using NSubstitute;
using PersonRegistry.API.Controllers;
using PersonRegistry.API.Tests.TestData;
using PersonRegistry.Application.City.Commands.Create;
using PersonRegistry.Application.City.Commands.Delete;
using PersonRegistry.Application.City.Commands.Update;
using PersonRegistry.Application.City.Queries.Get;

namespace PersonRegistry.API.Tests.Controllers;

/// <summary>
/// Unit tests for the <see cref="CityController"/> class.
/// </summary>
public class CityControllerTests
{
    private readonly IMediator _mediator;
    private readonly CityController _controller;

    public CityControllerTests()
    {
        _mediator = Substitute.For<IMediator>();
        _controller = new CityController(_mediator);
    }

    [Fact]
    public async Task CreateCity_ShouldReturnCityId_WhenRequestIsValid()
    {
        // Arrange
        var createCityModelRequest = CityTestData.BuildCreateCityModelRequest("City");
        var cityId = CityTestData.CITY_ID;

        _mediator.Send(Arg.Any<CreateCityCommand>(), Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(cityId));

        // Act
        var result = await _controller.CreateCity(createCityModelRequest, CancellationToken.None);

        // Assert
        result.Should().Be(cityId);
        await _mediator.Received(1).Send(Arg.Any<CreateCityCommand>(), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task GetCities_ShouldReturnPagedResult_WhenCalled()
    {
        // Arrange
        var cityModelRequest = CityTestData.BuildCityModelRequest("City");

        var pagedCityResponse = CityTestData.BuildPagedCityResponse();

        _mediator.Send(Arg.Any<GetCitiesQuery>(), Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(pagedCityResponse));

        // Act
        var result = await _controller.GetCities(cityModelRequest, CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(pagedCityResponse);
        await _mediator.Received(1).Send(Arg.Any<GetCitiesQuery>(), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task UpdateCity_ShouldCallMediatR_WhenRequestIsValid()
    {
        // Arrange
        var updateCityModelRequest = CityTestData.BuildUpdateCityModelRequest(CityTestData.CITY_ID, "City");

        // Act
        await _controller.UpdateCity(updateCityModelRequest, CancellationToken.None);

        // Assert
        await _mediator.Received(1).Send(Arg.Any<UpdateCityCommand>(), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task DeleteCity_ShouldCallMediatR_WhenRequestIsValid()
    {
        // Arrange
        var deleteCityModelRequest = CityTestData.BuildDeleteCityModelRequest(CityTestData.CITY_ID);

        // Act
        await _controller.DeleteCity(deleteCityModelRequest, CancellationToken.None);

        // Assert
        await _mediator.Received(1).Send(Arg.Any<DeleteCityCommand>(), Arg.Any<CancellationToken>());
    }
}
