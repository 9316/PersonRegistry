using System.Text;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Moq;
using PersonRegistry.Application.Common.Options;
using PersonRegistry.Common.Exceptions;
using PersonRegistry.Infrastructure.Services;

namespace PersonRegistry.Infastructure.Tests.Services;

/// <summary>
/// Unit tests for the <see cref="FileManagerService"/> class.
/// </summary>
public class FileManagerServiceTests
{
    private readonly Mock<IHostingEnvironment> _mockEnvironment;
    private readonly Mock<IOptions<FileManagerOptions>> _mockOptions;
    private readonly FileManagerService _fileManagerService;

    public FileManagerServiceTests()
    {
        _mockEnvironment = new Mock<IHostingEnvironment>();
        _mockOptions = new Mock<IOptions<FileManagerOptions>>();

        _mockEnvironment.Setup(e => e.ContentRootPath).Returns(Directory.GetCurrentDirectory());
        _mockOptions.Setup(o => o.Value).Returns(new FileManagerOptions { PhotoUrlLocation = "uploads" });

        _fileManagerService = new FileManagerService(_mockEnvironment.Object, _mockOptions.Object);
    }

    [Fact]
    public async Task UploadFileAsync_ShouldSaveFile_AndReturnPath()
    {
        // Arrange
        var fileMock = new Mock<IFormFile>();
        var content = "Test file content";
        var fileName = "testfile.txt";
        var ms = new MemoryStream(Encoding.UTF8.GetBytes(content));

        fileMock.Setup(f => f.OpenReadStream()).Returns(ms);
        fileMock.Setup(f => f.FileName).Returns(fileName);
        fileMock.Setup(f => f.Length).Returns(ms.Length);

        var expectedPath = Path.Combine(Directory.GetCurrentDirectory(), "uploads", fileName);

        // Act
        var result = await _fileManagerService.UploadFileAsync(fileMock.Object);

        // Assert
        result.Should().Be(expectedPath);
        File.Exists(result).Should().BeTrue();

        // Cleanup
        File.Delete(result);
    }

    [Fact]
    public async Task DeleteFileAsync_ShouldDeleteExistingFile()
    {
        // Arrange
        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "testfile.txt");
        await File.WriteAllTextAsync(filePath, "Dummy content");

        // Act
        await _fileManagerService.DeleteFileAsync(filePath);

        // Assert
        File.Exists(filePath).Should().BeFalse();
    }

    [Fact]
    public async Task DeleteFileAsync_ShouldNotThrow_WhenFileDoesNotExist()
    {
        // Arrange
        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "nonexistent.txt");

        // Act & Assert
        await _fileManagerService.DeleteFileAsync(filePath);
    }

    [Fact]
    public async Task ReplaceFileAsync_ShouldReplaceExistingFile()
    {
        // Arrange
        var existingFilePath = Path.Combine(Directory.GetCurrentDirectory(), "oldfile.txt");
        await File.WriteAllTextAsync(existingFilePath, "Old content");

        var fileMock = new Mock<IFormFile>();
        var newContent = "New file content";
        var fileName = "newfile.txt";
        var ms = new MemoryStream(Encoding.UTF8.GetBytes(newContent));
        fileMock.Setup(f => f.OpenReadStream()).Returns(ms);
        fileMock.Setup(f => f.FileName).Returns(fileName);
        fileMock.Setup(f => f.Length).Returns(ms.Length);

        // Act
        var newFilePath = await _fileManagerService.ReplaceFileAsync(fileMock.Object, existingFilePath);

        // Assert
        File.Exists(existingFilePath).Should().BeFalse();
        File.Exists(newFilePath).Should().BeTrue();

        // Cleanup
        File.Delete(newFilePath);
    }

    [Fact]
    public async Task DownloadFileAsync_ShouldReturnFileContent()
    {
        // Arrange
        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "downloadfile.txt");
        var content = "File content for download";
        await File.WriteAllTextAsync(filePath, content);

        // Act
        var result = await _fileManagerService.DownloadFileAsync(filePath);

        // Assert
        Encoding.UTF8.GetString(result).Should().Be(content);

        // Cleanup
        File.Delete(filePath);
    }

    [Fact]
    public async Task DownloadFileAsync_ShouldThrowNotFoundException_WhenFileDoesNotExist()
    {
        // Act
        Func<Task> act = async () => await _fileManagerService.DownloadFileAsync("nonexistent.txt");

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }
}
