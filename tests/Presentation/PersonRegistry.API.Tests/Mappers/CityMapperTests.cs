using FluentAssertions;
using PersonRegistry.API.Mappers;
using PersonRegistry.API.Tests.TestData;

public class CityMapperTests
{
    [Fact]
    public void ToCreateCommand_WhenCreateCityModelRequest_ShouldMapCorrectly()
    {
        // Arrange
        var createCityModelRequest = CityTestData.BuildCreateCityModelRequest(name: "Batumi");

        // Act
        var result = createCityModelRequest.ToCreateCommand();

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(createCityModelRequest.Name);
    }

    [Fact]
    public void ToCreateCommand_ShouldThrowException_WhenRequestIsNull()
    {
        // Act
        Action act = () => CityMapper.ToCreateCommand(null);

        // Assert
        act.Should().Throw<ArgumentNullException>();
    }

    public void ToCitiesQuery_WhenCityModelRequestIsValid_ShouldMapCorrectly()
    {
        // Arrange
        var cityModelRequest = CityTestData.BuildCityModelRequest(filterQuery: "Tbilisi");

        // Act
        var result = cityModelRequest.ToCitiesQuery();

        // Assert
        result.Should().NotBeNull();
        result.FilterQuery.Should().Be(cityModelRequest.FilterQuery);
        result.PageSize.Should().Be(cityModelRequest.PageSize);
        result.PageNumber.Should().Be(cityModelRequest.PageNumber);
    }

    [Fact]
    public void ToCitiesQuery_ShouldThrowException_WhenRequestIsNull()
    {
        // Act
        Action act = () => CityMapper.ToCitiesQuery(null);

        // Assert
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void ToUpdateCityCommand_WhenUpdateCityModelRequestIsValid_ShouldMapCorrectly()
    {
        // Arrange
        var updateCityModelRequest = CityTestData.BuildUpdateCityModelRequest(id: CityTestData.CITY_ID, name: "Updated City");

        // Act
        var result = updateCityModelRequest.ToUpdateCityCommand();

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(updateCityModelRequest.Id);
        result.Name.Should().Be(updateCityModelRequest.Name);
    }

    [Fact]
    public void ToUpdateCityCommand_ShouldThrowException_WhenRequestIsNull()
    {
        // Act
        Action act = () => CityMapper.ToUpdateCityCommand(null);

        // Assert
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void ToDeleteCityCommand_WhenDeleteCityModelRequest_ShouldMapCorrectly()
    {
        // Arrange
        var deleteCityModelRequest = CityTestData.BuildDeleteCityModelRequest(id: CityTestData.CITY_ID);

        // Act
        var result = deleteCityModelRequest.ToDeleteCityCommand();

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(deleteCityModelRequest.Id);
    }

    [Fact]
    public void ToDeleteCityCommand_ShouldThrowException_WhenRequestIsNull()
    {
        // Act
        Action act = () => CityMapper.ToDeleteCityCommand(null);

        // Assert
        act.Should().Throw<ArgumentNullException>();
    }
}