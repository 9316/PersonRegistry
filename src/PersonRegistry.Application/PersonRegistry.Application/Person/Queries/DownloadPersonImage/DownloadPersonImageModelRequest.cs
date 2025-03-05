using System.ComponentModel.DataAnnotations;

namespace PersonRegistry.Application.Person.Queries.DownloadPersonImage;

/// <summary>
/// Represents a request to download the image of a person using the image's URL.
/// </summary>
/// <param name="PhotoUrl">The URL of the photo to be downloaded.</param>
public record DownloadPersonImageModelRequest(string PhotoUrl);