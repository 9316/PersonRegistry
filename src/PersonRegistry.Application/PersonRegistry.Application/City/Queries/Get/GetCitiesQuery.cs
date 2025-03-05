using MediatR;
using PersonRegistry.Common.Application.Paging;

namespace PersonRegistry.Application.City.Queries.Get;

/// <summary>
/// Represents a query for retrieving a paginated list of cities.
/// </summary>
/// <param name="FilterQuery">The search filter for city names.</param>
/// <param name="PageSize">The number of records per page.</param>
/// <param name="PageNumber">The page number to retrieve.</param>
public record GetCitiesQuery(string FilterQuery, int PageSize, int PageNumber) : IRequest<PagedResult<CityModelResponse>>;
