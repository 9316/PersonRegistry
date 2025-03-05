namespace PersonRegistry.Application.City.Commands.Update;

/// <summary>
/// Represents a request to update an existing city.
/// </summary>
/// <param name="Id">The unique identifier of the city to be updated.</param>
/// <param name="Name">The new name for the city.</param>
public record UpdateCityModelRequest(int Id, string Name);