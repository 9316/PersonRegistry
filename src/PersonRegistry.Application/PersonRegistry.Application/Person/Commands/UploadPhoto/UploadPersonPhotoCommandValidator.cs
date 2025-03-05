using FluentValidation;

namespace PersonRegistry.Application.Person.Commands.UploadPhoto;

/// <summary>
/// Validator for the <see cref="UploadPersonPhotoCommand"/>.
/// </summary>
public class UploadPersonPhotoCommandValidator : AbstractValidator<UploadPersonPhotoCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UploadPersonPhotoCommandValidator"/> class.
    /// </summary>
    public UploadPersonPhotoCommandValidator()
    {
        RuleFor(x => x.PersonId).NotEmpty();
        RuleFor(x => x.Photo).NotEmpty();
    }
}