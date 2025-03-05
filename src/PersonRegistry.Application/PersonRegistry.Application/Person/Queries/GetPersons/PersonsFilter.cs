using PersonRegistry.Domain.Enums;

namespace PersonRegistry.Application.Person.Queries.GetPersons;

/// <summary>
/// Provides filtering logic for querying persons based on various criteria.
/// </summary>
public static class PersonsFilter
{
    /// <summary>
    /// Applies filtering conditions to an <see cref="IQueryable{T}"/> of persons based on the provided filter criteria.
    /// </summary>
    /// <param name="query">The queryable collection of persons.</param>
    /// <param name="filter">The filtering criteria.</param>
    /// <returns>The filtered <see cref="IQueryable{T}"/>.</returns>
    public static IQueryable<Domain.Aggregates.Person.Person> Filter(IQueryable<Domain.Aggregates.Person.Person> query, GetPersonsQuery filter)
    {
        ArgumentNullException.ThrowIfNull(query);
        ArgumentNullException.ThrowIfNull(filter);

        if (!string.IsNullOrWhiteSpace(filter.FilterQuery))
            query = query.Where(x => x.Name.ToLower().Contains(filter.FilterQuery.ToLower()) ||
                                     x.LastName.ToLower().Contains(filter.FilterQuery.ToLower()) ||
                                     x.PersonalNumber.Contains(filter.FilterQuery));


        if (!string.IsNullOrWhiteSpace(filter.Name))
            query = query.Where(x => x.Name.ToLower().Contains(filter.Name.ToLower()));

        if (!string.IsNullOrWhiteSpace(filter.LastName))
            query = query.Where(x => x.LastName.ToLower().Contains(filter.LastName.ToLower()));

        if (!string.IsNullOrWhiteSpace(filter.PersonalNumber))
            query = query.Where(x => x.PersonalNumber.Contains(filter.PersonalNumber));

        if (filter.CityId.HasValue && filter.CityId.Value > 0)
            query = query.Where(x => x.CityId == filter.CityId);

        if (filter.Gender.HasValue && Enum.IsDefined(typeof(GenderEnum), filter.Gender))
            query = query.Where(x => x.Gender == filter.Gender);

        if (filter.BirthDate != default(DateTime))
            query = query.Where(x => x.BirthDate.Date == filter.BirthDate.Date);

        return query;
    }
}