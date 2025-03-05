using FluentValidation;

namespace PersonRegistry.Application.Person.Commands.Delete;

/// <summary>
/// Validator for <see cref="DeletePersonCommand"/>.
/// Ensures the ID is provided and valid.
/// </summary>
public class DeletePersonCommandValidator : AbstractValidator<DeletePersonCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DeletePersonCommandValidator"/> class.
    /// </summary>
    public DeletePersonCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}