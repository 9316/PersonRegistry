using Moq;
using FluentAssertions;
using PersonRegistry.Application.Person.Queries.DownloadPersonImage;
using PersonRegistry.Application.Services;

namespace PersonRegistry.Application.Tests.Person.Queries.DownloadPersonImage;

/// <summary>
/// Unit tests for custom exceptions in <see cref="DownloadPersonImageCommandHandler"/>.
/// </summary>
public class DownloadPersonImageCommandHandlerTests
{
    private readonly Mock<IFileManagerService> _fileManagerServiceMock;
    private readonly DownloadPersonImageCommandHandler _downloadPersonImageCommandHandler;

    public DownloadPersonImageCommandHandlerTests()
    {
        _fileManagerServiceMock = new Mock<IFileManagerService>();
        _downloadPersonImageCommandHandler = new DownloadPersonImageCommandHandler(_fileManagerServiceMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnByteArray_WhenFileExists()
    {
        // Arrange
        var photoUrl = "https://example.com/sample-image.jpg";
        var expectedBytes = new byte[] { 1, 2, 3, 4, 5 };

        _fileManagerServiceMock
            .Setup(service => service.DownloadFileAsync(photoUrl))
            .ReturnsAsync(expectedBytes);

        var downloadPersonImageCommand = new DownloadPersonImageCommand(photoUrl);

        // Act
        var result = await _downloadPersonImageCommandHandler.Handle(downloadPersonImageCommand, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(expectedBytes);
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenFileDownloadFails()
    {
        // Arrange
        var photoUrl = "https://example.com/missing-image.jpg";

        _fileManagerServiceMock
            .Setup(service => service.DownloadFileAsync(photoUrl))
            .ThrowsAsync(new Exception("File not found"));

        var downloadPersonImageCommand = new DownloadPersonImageCommand(photoUrl);

        // Act
        var act = async () => await _downloadPersonImageCommandHandler.Handle(downloadPersonImageCommand, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<Exception>()
            .WithMessage("File not found");
    }
}
