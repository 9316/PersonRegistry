using PersonRegistry.Application.PersonRelation.Command.Create;
using PersonRegistry.Application.PersonRelation.Command.Delete;

namespace PersonRegistry.API.Mappers;

/// <summary>
/// Provides static mapping methods for converting request models to commands.
/// </summary>
public static class PersonRelationMapper
{
    /// <summary>
    /// Maps a <see cref="CreatePersonRelationModelRequest"/> to a <see cref="CreatePersonRelationCommand"/>.
    /// </summary>
    public static CreatePersonRelationCommand ToCreatePersonRelationCommand(this CreatePersonRelationModelRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        return new CreatePersonRelationCommand(request.PersonId, request.RelatedPersonId, request.PersonRelationTypeId);
    }

    /// <summary>
    /// Maps a <see cref="DeletePersonRelationModelRequest"/> to a <see cref="DeletePersonRelationCommand"/>.
    /// </summary>
    public static DeletePersonRelationCommand ToDeletePersonRelationCommand(this DeletePersonRelationModelRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        return new DeletePersonRelationCommand(request.PersonId, request.RelatedPersonId, request.PersonRelationTypeId);
    }
}