using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using PersonRegistry.Application.Common.Options;
using PersonRegistry.Application.Services;
using PersonRegistry.Common.Exceptions;

namespace PersonRegistry.Infrastructure.Services;

/// <summary>
/// File service is for managing files in local directory
/// </summary>
/// <param name="_environment"></param>
/// <param name="_fileManagerOptions"></param>
public class FileManagerService(IHostingEnvironment _environment, IOptions<FileManagerOptions> _fileManagerOptions) : IFileManagerService
{
    /// <inheritdoc cref="IFileManagerService.UploadFileAsync" />
    public async Task<string> UploadFileAsync(IFormFile file)
    {
        if (file == null || file.Length == 0)
            throw new Common.Exceptions.ArgumentException(nameof(file));

        var fileName = Path.GetFileName(file.FileName);

        if (!Directory.Exists(Path.Combine(_environment.ContentRootPath, _fileManagerOptions.Value.PhotoUrlLocation)))
        {
            Directory.CreateDirectory(Path.Combine(_environment.ContentRootPath,
                _fileManagerOptions.Value.PhotoUrlLocation));
        }

        var fullPath = Path.Combine(_environment.ContentRootPath, _fileManagerOptions.Value.PhotoUrlLocation, fileName);

        using (var stream = new FileStream(fullPath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        return fullPath;
    }

    /// <inheritdoc cref="IFileManagerService.DeleteFileAsync" />
    public async Task DeleteFileAsync(string filePath)
    {
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
    }

    /// <inheritdoc cref="IFileManagerService.ReplaceFileAsync" />
    public async Task<string> ReplaceFileAsync(IFormFile file, string existingFilePath)
    {
        if (file == null || file.Length == 0)
            throw new Common.Exceptions.ArgumentException(nameof(file));

        await DeleteFileAsync(existingFilePath);

        return await UploadFileAsync(file);
    }

    /// <inheritdoc cref="IFileManagerService.DownloadFileAsync" />
    public async Task<byte[]> DownloadFileAsync(string filePath)
    {
        if (File.Exists(filePath))
        {
            return await File.ReadAllBytesAsync(filePath);
        }

        throw new NotFoundException(filePath);
    }
}
