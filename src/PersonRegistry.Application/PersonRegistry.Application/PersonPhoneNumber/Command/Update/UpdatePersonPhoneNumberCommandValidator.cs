using FluentValidation;

namespace PersonRegistry.Application.PersonPhoneNumber.Command.Update;

/// <summary>
/// Validator for the <see cref="UpdatePersonPhoneNumberCommand"/> to ensure correct data input.
/// </summary>
public class UpdatePersonPhoneNumberCommandValidator : AbstractValidator<UpdatePersonPhoneNumberCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UpdatePersonPhoneNumberCommandValidator"/> class.
    /// Defines validation rules for updating a person’s phone number.
    /// </summary>
    public UpdatePersonPhoneNumberCommandValidator()
    {
        RuleFor(x => x.PersonId).NotEmpty();
        RuleFor(x => x.PersonPhoneNumberId).NotEmpty();
        RuleFor(x => x.PhoneNumberTypeId).NotEmpty();
        RuleFor(x => x.PhoneNumber).Length(4, 50).NotEmpty();
    }
}