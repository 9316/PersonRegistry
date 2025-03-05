using MediatR;
using PersonRegistry.Common.Application.Paging;
using PersonRegistry.Domain.Interfaces;

namespace PersonRegistry.Application.Person.Queries.GetPersons;

/// <summary>
/// Handles the retrieval of a paginated list of persons based on filter criteria.
/// </summary>
public class GetPersonsQueryHandler(IUnitOfWork _unitOfWork) : IRequestHandler<GetPersonsQuery, PagedResult<GetPersonsModelResponse>>
{
    /// <summary>
    /// Handles the query request to retrieve paginated persons based on filters.
    /// </summary>
    /// <param name="request">The query request containing filter parameters.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A paginated result set of persons matching the filter criteria.</returns>
    public async Task<PagedResult<GetPersonsModelResponse>> Handle(GetPersonsQuery request, CancellationToken cancellationToken)
    {
        var queryFiltered = PersonsFilter.Filter(_unitOfWork.PersonRepository.Query(), request);

        return await BasePaging.CreatePagedItemsAsync<Domain.Aggregates.Person.Person, GetPersonsModelResponse>
                           (_unitOfWork.PersonRepository, queryFiltered, request.PageNumber, request.PageSize);
    }
}