using Microsoft.AspNetCore.Http;

namespace PersonRegistry.Application.Services;

/// <summary>
/// Provides file management operations such as upload, delete, replace, and download.
/// </summary>
public interface IFileManagerService
{
    /// <summary>
    /// Uploads a new file and returns its storage path.
    /// </summary>
    /// <param name="file">The file to be uploaded.</param>
    /// <returns>The file path of the uploaded file.</returns>
    Task<string> UploadFileAsync(IFormFile file);

    /// <summary>
    /// Deletes a file from storage.
    /// </summary>
    /// <param name="filePath">The path of the file to be deleted.</param>
    Task DeleteFileAsync(string filePath);

    /// <summary>
    /// Replaces an existing file with a new one and returns the new file path.
    /// </summary>
    /// <param name="file">The new file to replace the existing file.</param>
    /// <param name="existingFilePath">The path of the file being replaced.</param>
    /// <returns>The new file path after replacement.</returns>
    Task<string> ReplaceFileAsync(IFormFile file, string existingFilePath);

    /// <summary>
    /// Downloads a file from storage and returns its byte content.
    /// </summary>
    /// <param name="filePath">The path of the file to be downloaded.</param>
    /// <returns>The byte array representing the file's content.</returns>
    Task<byte[]> DownloadFileAsync(string filePath);
}
