namespace PersonRegistry.Application.City.Queries.Get;

/// <summary>
/// Represents a response model for a city query.
/// </summary>
/// <param name="Id">The unique identifier of the city.</param>
/// <param name="Name">The name of the city.</param>
public record CityModelResponse(int Id, string Name);