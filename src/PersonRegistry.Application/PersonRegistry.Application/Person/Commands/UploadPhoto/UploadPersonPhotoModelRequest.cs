using Microsoft.AspNetCore.Http;

namespace PersonRegistry.Application.Person.Commands.UploadPhoto;

/// <summary>
/// Represents a request to upload a photo for a person.
/// </summary>
/// <param name="PersonId">The unique identifier of the person for whom the photo is being uploaded.</param>
/// <param name="Photo">The photo file to be uploaded for the person.</param>
public record UploadPersonPhotoModelRequest(int PersonId, IFormFile Photo);