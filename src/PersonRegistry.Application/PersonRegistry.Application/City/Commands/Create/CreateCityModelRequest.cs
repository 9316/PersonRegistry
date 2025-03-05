namespace PersonRegistry.Application.City.Commands.Create;

/// <summary>
/// Represents a request to create a new city.
/// </summary>
/// <param name="Name">The name of the city to be created.</param>
public record CreateCityModelRequest(string Name);
