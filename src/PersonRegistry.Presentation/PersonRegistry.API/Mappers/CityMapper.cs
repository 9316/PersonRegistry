using PersonRegistry.Application.City.Commands.Create;
using PersonRegistry.Application.City.Commands.Delete;
using PersonRegistry.Application.City.Commands.Update;
using PersonRegistry.Application.City.Queries.Get;

namespace PersonRegistry.API.Mappers;

/// <summary>
/// Provides mapping extensions for converting request models to commands and queries.
/// </summary>
public static class CityMapper
{
    /// <summary>
    /// Maps a <see cref="CreateCityModelRequest"/> to a <see cref="CreateCityCommand"/>.
    /// </summary>
    /// <param name="request">The request containing city creation details.</param>
    /// <returns>A <see cref="CreateCityCommand"/> with the mapped properties.</returns>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="request"/> is null.</exception>
    public static CreateCityCommand ToCreateCommand(this CreateCityModelRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        return new CreateCityCommand(request.Name);
    }

    /// <summary>
    /// Maps a <see cref="CityModelRequest"/> to a <see cref="GetCitiesQuery"/>.
    /// </summary>
    /// <param name="request">The request containing filtering and pagination details.</param>
    /// <returns>A <see cref="GetCitiesQuery"/> with the mapped properties.</returns>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="request"/> is null.</exception>
    public static GetCitiesQuery ToCitiesQuery(this CityModelRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        return new GetCitiesQuery(request.FilterQuery, request.PageSize, request.PageNumber);
    }

    /// <summary>
    /// Maps a <see cref="UpdateCityModelRequest"/> to an <see cref="UpdateCityCommand"/>.
    /// </summary>
    /// <param name="request">The request containing updated city details.</param>
    /// <returns>An <see cref="UpdateCityCommand"/> with the mapped properties.</returns>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="request"/> is null.</exception>
    public static UpdateCityCommand ToUpdateCityCommand(this UpdateCityModelRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        return new UpdateCityCommand(request.Id, request.Name);
    }

    /// <summary>
    /// Maps a <see cref="DeleteCityModelRequest"/> to a <see cref="DeleteCityCommand"/>.
    /// </summary>
    /// <param name="request">The request containing the ID of the city to be deleted.</param>
    /// <returns>A <see cref="DeleteCityCommand"/> with the mapped properties.</returns>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="request"/> is null.</exception>
    public static DeleteCityCommand ToDeleteCityCommand(this DeleteCityModelRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        return new DeleteCityCommand(request.Id);
    }
}
