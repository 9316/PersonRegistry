using MediatR;
using PersonRegistry.Common.Application.Paging;
using PersonRegistry.Domain.Enums;

namespace PersonRegistry.Application.Person.Queries.GetPersons;

/// <summary>
/// Represents a query to retrieve a paginated list of persons with optional filtering.
/// </summary>
/// <remarks>
/// This query allows filtering by various fields such as name, personal number, city, gender, and birth date. 
/// The results are paginated using the specified <paramref name="PageSize"/> and <paramref name="PageNumber"/>.
/// </remarks>
/// <param name="FilterQuery">A general query filter applied across multiple fields.</param>
/// <param name="Name">Filters by a person's first name.</param>
/// <param name="LastName">Filters by a person's last name.</param>
/// <param name="PersonalNumber">Filters by a person's personal identification number.</param>
/// <param name="CityId">Filters by a specific city ID.</param>
/// <param name="Gender">Filters by gender.</param>
/// <param name="BirthDate">Filters by birth date.</param>
/// <param name="PageSize">The number of records to return per page.</param>
/// <param name="PageNumber">The page number to retrieve.</param>
/// <returns>A paginated list of persons that match the filter criteria.</returns>
public record GetPersonsQuery(
    string FilterQuery,
    string Name,
    string LastName,
    string PersonalNumber,
    GenderEnum? Gender,
    DateTime BirthDate,
    int? CityId,
    int PageSize,
    int PageNumber
) : IRequest<PagedResult<GetPersonsModelResponse>>;
