namespace PersonRegistry.Application.Common.Options;

/// <summary>
/// Configuration options for the file manager.
/// </summary>
public class FileManagerOptions
{
    /// <summary>
    /// Gets or sets the base URL location for storing photos.
    /// </summary>
    public required string PhotoUrlLocation { get; init; }
}