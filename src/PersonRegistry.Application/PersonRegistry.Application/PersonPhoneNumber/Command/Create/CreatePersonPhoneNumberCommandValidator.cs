using FluentValidation;

namespace PersonRegistry.Application.PersonPhoneNumber.Command.Create;

/// <summary>
/// Validator for the <see cref="CreatePersonPhoneNumberCommand"/> to ensure correct data input.
/// </summary>
public class CreatePersonPhoneNumberCommandValidator : AbstractValidator<CreatePersonPhoneNumberCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreatePersonPhoneNumberCommandValidator"/> class.
    /// Defines validation rules for creating a person’s phone number.
    /// </summary>
    public CreatePersonPhoneNumberCommandValidator()
    {
        RuleFor(x => x.PhoneNumber).Length(4, 50).NotEmpty();
        RuleFor(x => x.PersonId).NotEmpty();
        RuleFor(x => x.PhoneNumberTypeId).NotEmpty();
    }
}