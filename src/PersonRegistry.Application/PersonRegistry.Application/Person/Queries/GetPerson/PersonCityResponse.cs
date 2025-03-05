namespace PersonRegistry.Application.Person.Queries.GetPerson;

/// <summary>
/// Represents a response containing city details associated with a person.
/// </summary>
/// <param name="Id">The unique identifier of the city.</param>
/// <param name="Name">The name of the city.</param>

public record PersonCityResponse(int Id, string Name);