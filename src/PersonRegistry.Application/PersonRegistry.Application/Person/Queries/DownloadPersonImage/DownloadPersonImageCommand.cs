using MediatR;

namespace PersonRegistry.Application.Person.Queries.DownloadPersonImage;

/// <summary>
/// Command to download a person's image using a photo URL.
/// </summary>
/// <param name="PhotoUrl">The URL of the photo to be downloaded.</param>
public record DownloadPersonImageCommand(string PhotoUrl) : IRequest<byte[]>;
