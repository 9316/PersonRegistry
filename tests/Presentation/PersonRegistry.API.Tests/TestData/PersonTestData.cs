using Microsoft.AspNetCore.Http;
using PersonRegistry.Application.City.Commands.Create;
using PersonRegistry.Application.City.Queries.Get;
using PersonRegistry.Application.Person.Commands.Create;
using PersonRegistry.Application.Person.Commands.Delete;
using PersonRegistry.Application.Person.Commands.Update;
using PersonRegistry.Application.Person.Commands.UploadPhoto;
using PersonRegistry.Application.Person.Queries.DownloadPersonImage;
using PersonRegistry.Application.Person.Queries.GetPerson;
using PersonRegistry.Application.Person.Queries.GetPersons;
using PersonRegistry.Common.Application.Paging;
using PersonRegistry.Domain.Enums;
using Moq;
using PersonRegistry.Application.Person.Commands.DeletePhoto;

namespace PersonRegistry.API.Tests.TestData;

internal static class PersonTestData
{
    internal const int PERSON_ID = 1;
    internal const string PHOTO_URL = "https://example.com/photo.jpg";
    internal const string DEFAULT_NAME = "Updated Name";
    internal const string DEFAULT_LASTNAME = "Doe";
    internal const string DEFAULT_PERSONAL_NUMBER = "1234567890";
    internal static readonly DateTime DEFAULT_BIRTHDATE = new DateTime(1990, 1, 1);
    internal const GenderEnum DEFAULT_GENDER = GenderEnum.Male;
    internal const int DEFAULT_CITY_ID = 100;
    internal const string DEFAULT_PHOTO_NAME = "test-photo.jpg";
    internal const string DEFAULT_FILTER_QUERY = "John";
    internal const int PAGE_SIZE = 10;
    internal const int PAGE_NUMBER = 1;

    internal static GetPersonModelRequest BuildGetPersonModelRequest(int id) => new GetPersonModelRequest(id);

    internal static GetPersonModelResponse BuildGetPersonModelResponse(int id, string name)
    {
        var birthDate = new DateTime(1995, 5, 5);

        return new GetPersonModelResponse(
            id,
            name,
            LastName: "Doe",
            PersonalNumber: "0987654321",
            BirthDate: birthDate,
            Gender: GenderEnum.Female,
            CityId: 5,
            Photo: "photo2.jpg",
            new PersonCityResponse(10, "City"),
            [
                new PersonPhoneNumberResponse(1, "123-456-7890", "Mobile")
            ],
            [
                new PersonRelationResponse(2, "Jane", "Doe", GenderEnum.Male, "21212121212", birthDate)
            ]
        );
    }

    internal static CityModelRequest BuildCityModelRequest(string filterQuery, int pageSize = 10, int pageNumber = 1) => new CityModelRequest(filterQuery, pageSize, pageNumber);

    internal static PagedResult<GetPersonsModelResponse> BuildPagedPersons()
    {
        return new PagedResult<GetPersonsModelResponse>
        {
            Items =
            [
                new GetPersonsModelResponse(
                    1, "John", "Doe", "1234567890", "photo1.jpg",
                    new DateTime(1990, 1, 1), GenderEnum.Male),

                new GetPersonsModelResponse(
                    2, "Jane", "Doe", "0987654321", "photo2.jpg",
                    new DateTime(1995, 5, 5), GenderEnum.Female)
            ],
            TotalItemCount = 2,
            PageSize = 10,
            PageNumber = 1
        };
    }

    internal static DownloadPersonImageModelRequest BuildDownloadPersonImageRequest() => new DownloadPersonImageModelRequest(PHOTO_URL);

    internal static DeletePersonModelRequest BuildDeletePersonRequest(int id) => new DeletePersonModelRequest(id);

    internal static UploadPersonPhotoModelRequest BuildUploadPersonPhotoRequest(int personId, string photo)
    {
        var mockedPhoto = CreateMockFormFile(photo ?? DEFAULT_PHOTO_NAME);
        return new UploadPersonPhotoModelRequest(personId, mockedPhoto);
    }

    public static DeletePersonPhotoModelRequest BuildDeletePersonPhotoRequest(int personId) => new DeletePersonPhotoModelRequest(personId);

    internal static GetPersonsModelRequest BuildGetPersonsModelRequest(
       string filterQuery = DEFAULT_FILTER_QUERY,
       string name = DEFAULT_NAME,
       string lastName = DEFAULT_LASTNAME,
       string personalNumber = DEFAULT_PERSONAL_NUMBER,
       GenderEnum? gender = DEFAULT_GENDER,
       DateTime? birthDate = null,
       int pageSize = PAGE_SIZE,
       int pageNumber = PAGE_NUMBER)
    {
        return new GetPersonsModelRequest
        {
            FilterQuery = filterQuery,
            Name = name,
            LastName = lastName,
            PersonalNumber = personalNumber,
            Gender = gender,
            BirthDate = birthDate ?? DEFAULT_BIRTHDATE,
            PageSize = pageSize,
            PageNumber = pageNumber
        };
    }

    internal static CreatePersonModelRequest BuildCreatePersonRequest(
        string name = DEFAULT_NAME,
        string lastName = DEFAULT_LASTNAME,
        string personalNumber = DEFAULT_PERSONAL_NUMBER,
        DateTime? birthDate = null,
        GenderEnum gender = DEFAULT_GENDER,
        int cityId = DEFAULT_CITY_ID)
    {
        return new CreatePersonModelRequest(
            name,
            lastName,
            personalNumber,
            birthDate ?? DEFAULT_BIRTHDATE,
            gender,
            cityId
        );
    }

    internal static UpdatePersonModelRequest BuildUpdatePersonRequest(
        int id = PERSON_ID,
        string name = DEFAULT_NAME,
        string lastName = DEFAULT_LASTNAME,
        string personalNumber = DEFAULT_PERSONAL_NUMBER,
        DateTime? birthDate = null,
        GenderEnum gender = DEFAULT_GENDER,
        int cityId = DEFAULT_CITY_ID)
    {
        return new UpdatePersonModelRequest(
            id,
            name,
            lastName,
            personalNumber,
            birthDate ?? DEFAULT_BIRTHDATE,
            gender,
            cityId
        );
    }

    private static IFormFile CreateMockFormFile(string fileName)
    {
        var fileMock = new Mock<IFormFile>();
        var ms = new MemoryStream();
        var writer = new StreamWriter(ms);
        writer.Write("Fake Image Data");
        writer.Flush();
        ms.Position = 0;

        fileMock.Setup(f => f.OpenReadStream()).Returns(ms);
        fileMock.Setup(f => f.FileName).Returns(fileName);
        fileMock.Setup(f => f.Length).Returns(ms.Length);

        return fileMock.Object;
    }
}
