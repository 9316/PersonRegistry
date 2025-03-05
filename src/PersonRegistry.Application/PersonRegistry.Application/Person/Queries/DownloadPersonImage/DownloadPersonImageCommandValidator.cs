using FluentValidation;

namespace PersonRegistry.Application.Person.Queries.DownloadPersonImage;

/// <summary>
/// Validator for the <see cref="DownloadPersonImageCommand"/>.
/// </summary>
public class DownloadPersonImageCommandValidator : AbstractValidator<DownloadPersonImageCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DownloadPersonImageCommandValidator"/> class.
    /// </summary>
    public DownloadPersonImageCommandValidator()
    {
        RuleFor(x => x.PhotoUrl).NotEmpty();
    }
}