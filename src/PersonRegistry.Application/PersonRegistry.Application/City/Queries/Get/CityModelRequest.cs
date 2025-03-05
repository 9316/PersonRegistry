namespace PersonRegistry.Application.City.Queries.Get;

/// <summary>
/// Represents a request for querying city data, supporting optional filtering and pagination.
/// </summary>
/// <param name="FilterQuery">An optional query string used for filtering cities based on matching criteria.</param>
/// <param name="PageSize">The size of the page for pagination. Specifies the number of cities to include in a single page.</param>
/// <param name="PageNumber">The page number for pagination, indicating which page of the results to retrieve.</param>
public record CityModelRequest(string? FilterQuery, int PageSize, int PageNumber);
