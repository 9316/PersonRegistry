using PersonRegistry.Application.Person.Commands.Create;
using PersonRegistry.Application.Person.Commands.Delete;
using PersonRegistry.Application.Person.Commands.DeletePhoto;
using PersonRegistry.Application.Person.Commands.Update;
using PersonRegistry.Application.Person.Queries.DownloadPersonImage;
using PersonRegistry.Application.Person.Queries.GetPerson;
using PersonRegistry.Application.Person.Queries.GetPersons;

namespace PersonRegistry.API.Mappers;

/// <summary>
/// Provides static mapping methods for converting request models to commands and queries.
/// </summary>
public static class PersonMapper
{
    /// <summary>
    /// Maps a <see cref="GetPersonModelRequest"/> to a <see cref="GetPersonQuery"/>.
    /// </summary>
    public static GetPersonQuery ToPersonQuery(this GetPersonModelRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        return new GetPersonQuery(request.Id);
    }

    /// <summary>
    /// Maps a <see cref="GetPersonsModelRequest"/> to a <see cref="GetPersonsQuery"/>.
    /// </summary>
    public static GetPersonsQuery ToGetPersonQuery(this GetPersonsModelRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        return new GetPersonsQuery(
            request.FilterQuery, 
            request.Name, 
            request.LastName,
            request.PersonalNumber,
            request.Gender,
            request.BirthDate,
            null,
            request.PageSize,
            request.PageNumber);
    }

    /// <summary>
    /// Maps a <see cref="DownloadPersonImageModelRequest"/> to a <see cref="DownloadPersonImageCommand"/>.
    /// </summary>
    public static DownloadPersonImageCommand ToDownloadPersonImageCommand(this DownloadPersonImageModelRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        return new DownloadPersonImageCommand(request.PhotoUrl);
    }

    /// <summary>
    /// Maps a <see cref="CreatePersonModelRequest"/> to a <see cref="CreatePersonCommand"/>.
    /// </summary>
    public static CreatePersonCommand ToCreatePersonCommand(this CreatePersonModelRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        return new CreatePersonCommand(
            request.Name,
            request.LastName,
            request.PersonalNumber,
            request.BirthDate,
            request.Gender,
            request.CityId);
    }

    /// <summary>
    /// Maps a <see cref="UpdatePersonModelRequest"/> to an <see cref="UpdatePersonCommand"/>.
    /// </summary>
    public static UpdatePersonCommand ToUpdatePersonCommand(this UpdatePersonModelRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        return new UpdatePersonCommand(
            request.Id,
            request.Name,
            request.LastName,
            request.PersonalNumber,
            request.BirthDate,
            request.Gender,
            request.CityId);
    }

    /// <summary>
    /// Maps a <see cref="DeletePersonModelRequest"/> to a <see cref="DeletePersonCommand"/>.
    /// </summary>
    public static DeletePersonCommand ToDeletePersonCommand(this DeletePersonModelRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        return new DeletePersonCommand(request.Id);
    }

    /// <summary>
    /// Maps a <see cref="DeletePersonPhotoModelRequest"/> to a <see cref="DeletePersonPhotoCommand"/>.
    /// </summary>
    public static DeletePersonPhotoCommand ToDeletePersonPhotoCommand(this DeletePersonPhotoModelRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        return new DeletePersonPhotoCommand(request.PersonId);
    }
}
