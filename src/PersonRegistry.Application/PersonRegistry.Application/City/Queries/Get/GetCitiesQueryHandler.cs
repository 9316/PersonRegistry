using MediatR;
using PersonRegistry.Common.Application.Paging;
using PersonRegistry.Domain.Interfaces;

namespace PersonRegistry.Application.City.Queries.Get;

/// <summary>
/// Handles the retrieval of a paginated list of cities based on filtering criteria.
/// </summary>
public class GetCitiesQueryHandler(IUnitOfWork _unitOfWork) : IRequestHandler<GetCitiesQuery, PagedResult<CityModelResponse>>
{
    /// <summary>
    /// Handles the request to retrieve a paginated list of cities.
    /// </summary>
    /// <param name="request">The query containing pagination parameters and optional filtering criteria.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A paginated result of cities matching the filter criteria.</returns>
    public async Task<PagedResult<CityModelResponse>> Handle(GetCitiesQuery request, CancellationToken cancellationToken)
    {
        var queryFiltered = CityFilter.Filter(_unitOfWork.CityRepository.Query(), request);

        return await BasePaging.CreatePagedItemsAsync<Domain.Aggregates.City.City, CityModelResponse>
                           (_unitOfWork.CityRepository, queryFiltered, request.PageNumber, request.PageSize);
    }
}
