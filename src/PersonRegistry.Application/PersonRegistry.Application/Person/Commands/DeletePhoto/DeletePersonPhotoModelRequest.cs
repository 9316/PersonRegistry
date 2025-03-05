namespace PersonRegistry.Application.Person.Commands.DeletePhoto;

/// <summary>
/// Represents a request to delete the photo of a person identified by their unique identifier.
/// </summary>
/// <param name="PersonId">The unique identifier of the person whose photo is to be deleted.</param>
public record DeletePersonPhotoModelRequest(int PersonId);