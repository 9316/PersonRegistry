namespace PersonRegistry.Application.City.Queries.Get;

public static class CityFilter
{
    public static IQueryable<Domain.Aggregates.City.City> Filter(IQueryable<Domain.Aggregates.City.City> query, GetCitiesQuery filter)
    {
        if (!string.IsNullOrWhiteSpace(filter.FilterQuery))
            query = query.Where(x => x.Name.ToLower().Contains(filter.FilterQuery.ToLower()));

        return query;
    }
}
