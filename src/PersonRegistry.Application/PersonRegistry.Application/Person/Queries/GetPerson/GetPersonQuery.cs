using MediatR;

namespace PersonRegistry.Application.Person.Queries.GetPerson;

/// <summary>
/// Represents a query to retrieve detailed information about a specific person.
/// </summary>
/// <param name="Id">The unique identifier of the person.</param>
public record GetPersonQuery(int Id) : IRequest<GetPersonModelResponse>;