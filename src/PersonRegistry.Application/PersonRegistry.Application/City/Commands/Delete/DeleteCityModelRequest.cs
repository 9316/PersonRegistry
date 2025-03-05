namespace PersonRegistry.Application.City.Commands.Delete;

/// <summary>
/// Represents a request to delete an existing city.
/// </summary>
/// <param name="Id">The identifier of the city to be deleted.</param>
public record DeleteCityModelRequest(int Id);