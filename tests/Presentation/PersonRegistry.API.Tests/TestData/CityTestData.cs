using PersonRegistry.Application.City.Commands.Create;
using PersonRegistry.Application.City.Commands.Delete;
using PersonRegistry.Application.City.Commands.Update;
using PersonRegistry.Application.City.Queries.Get;
using PersonRegistry.Common.Application.Paging;

namespace PersonRegistry.API.Tests.TestData;

internal static class CityTestData
{
    internal const int CITY_ID = 1;
    internal const int PAGE_SIZE = 5;
    internal const int PAGE_NUMBER = 5;

    internal static CreateCityModelRequest BuildCreateCityModelRequest(string name) =>  new CreateCityModelRequest(name);

    internal static UpdateCityModelRequest BuildUpdateCityModelRequest(int id, string name) => new UpdateCityModelRequest(id, name);

    internal static CityModelRequest BuildCityModelRequest(string filterQuery) => new CityModelRequest(filterQuery, PAGE_SIZE, PAGE_NUMBER);

    internal static PagedResult<CityModelResponse> BuildPagedCityResponse()
    {
        return new PagedResult<CityModelResponse>
        {
            Items =
            [
                new CityModelResponse(1, "Tbilisi"),
                new CityModelResponse(2, "Batumi")
            ],
            TotalItemCount = 2,
            PageSize = PAGE_SIZE,
            PageNumber = PAGE_NUMBER
        };
    }

    internal static DeleteCityModelRequest BuildDeleteCityModelRequest(int id) => new DeleteCityModelRequest(id);
}
