using FluentValidation;

namespace PersonRegistry.Application.PersonPhoneNumber.Command.Delete;

/// <summary>
/// Validator for the <see cref="DeletePersonPhoneNumberCommand"/> to ensure correct data input.
/// </summary>
public class DeletePersonPhoneNumberCommandValidator : AbstractValidator<DeletePersonPhoneNumberCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DeletePersonPhoneNumberCommandValidator"/> class.
    /// Defines validation rules for deleting a person’s phone number.
    /// </summary>
    public DeletePersonPhoneNumberCommandValidator()
    {
        RuleFor(x => x.PersonId).NotEmpty();
        RuleFor(x => x.PersonPhoneNumberId).NotEmpty();
    }
}