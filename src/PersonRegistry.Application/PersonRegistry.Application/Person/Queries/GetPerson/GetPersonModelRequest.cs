namespace PersonRegistry.Application.Person.Queries.GetPerson;

/// <summary>
/// Represents a request to retrieve detailed information about a specific person identified by their unique identifier.
/// </summary>
/// <param name="Id">The unique identifier of the person whose details are being requested.</param>
public record GetPersonModelRequest(int Id);