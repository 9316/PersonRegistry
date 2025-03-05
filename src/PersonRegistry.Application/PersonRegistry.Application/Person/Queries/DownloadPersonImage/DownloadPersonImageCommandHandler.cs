using MediatR;
using PersonRegistry.Application.Services;

namespace PersonRegistry.Application.Person.Queries.DownloadPersonImage;

/// <summary>
/// Handles the request to download a person's image.
/// </summary>
public class DownloadPersonImageCommandHandler(IFileManagerService _fileManagerService) : IRequestHandler<DownloadPersonImageCommand, byte[]>
{
    /// <summary>
    /// Handles the request to download an image for a person.
    /// </summary>
    /// <param name="request">The command containing the photo URL.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The image as a byte array.</returns>
    public async Task<byte[]> Handle(DownloadPersonImageCommand request, CancellationToken cancellationToken)
    {
        var photo = await _fileManagerService.DownloadFileAsync(request.PhotoUrl);

        return photo;
    }
}