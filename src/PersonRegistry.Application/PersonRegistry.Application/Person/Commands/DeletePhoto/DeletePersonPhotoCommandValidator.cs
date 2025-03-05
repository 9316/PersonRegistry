using FluentValidation;

namespace PersonRegistry.Application.Person.Commands.DeletePhoto;

/// <summary>
/// Validator for the <see cref="DeletePersonPhotoCommand"/>.
/// </summary>
public class DeletePersonPhotoCommandValidator : AbstractValidator<DeletePersonPhotoCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DeletePersonPhotoCommandValidator"/> class.
    /// </summary>
    public DeletePersonPhotoCommandValidator()
    {
        RuleFor(x => x.PersonId).NotEmpty();
    }
}