using PersonRegistry.Application.PersonPhoneNumber.Command.Create;
using PersonRegistry.Application.PersonPhoneNumber.Command.Delete;
using PersonRegistry.Application.PersonPhoneNumber.Command.Update;

namespace PersonRegistry.API.Mappers;

/// <summary>
/// Provides static mapping methods for converting request models to commands.
/// </summary>
public static class PersonPhoneNumberMapper
{
    /// <summary>
    /// Maps a <see cref="CreatePersonPhoneNumberModelRequest"/> to a <see cref="CreatePersonPhoneNumberCommand"/>.
    /// </summary>
    public static CreatePersonPhoneNumberCommand ToCreatePersonPhoneNumberCommand(this CreatePersonPhoneNumberModelRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        return new CreatePersonPhoneNumberCommand(
            request.PersonId, 
            request.PhoneNumberTypeId,
            request.PhoneNumber);
    }

    /// <summary>
    /// Maps a <see cref="DeletePersonPhoneNumberModelRequest"/> to a <see cref="DeletePersonPhoneNumberCommand"/>.
    /// </summary>
    public static DeletePersonPhoneNumberCommand ToDeletePersonPhoneNumberCommand(this DeletePersonPhoneNumberModelRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        return new DeletePersonPhoneNumberCommand(request.PersonId, request.PersonPhoneNumberId);
    }

    /// <summary>
    /// Maps a <see cref="UpdatePersonPhoneNumberModelRequest"/> to an <see cref="UpdatePersonPhoneNumberCommand"/>.
    /// </summary>
    public static UpdatePersonPhoneNumberCommand ToUpdatePersonPhoneNumberCommand(this UpdatePersonPhoneNumberModelRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        return new UpdatePersonPhoneNumberCommand(request.PersonId, request.PersonPhoneNumberId, request.PhoneNumberTypeId, request.PhoneNumber);
    }
}