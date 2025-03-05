using FluentValidation;
using PersonRegistry.Application.Person.Helper;
using PersonRegistry.Application.Resources;

namespace PersonRegistry.Application.Person.Commands.Create;

/// <summary>
/// Validator for the <see cref="CreatePersonCommand"/>.
/// </summary>
public class CreatePersonCommandValidator : AbstractValidator<CreatePersonCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreatePersonCommandValidator"/> class.
    /// </summary>
    public CreatePersonCommandValidator()
    {
        RuleFor(x => x.Gender).NotEmpty().IsInEnum();

        RuleFor(x => x.Name)
            .NotEmpty()
            .Length(2, 50)
            .Must(ValidationHelper.ValidateForMixedLanguages)
            .WithMessage(x => string.Format(ValidationResource.MixedLanguage, nameof(x.Name)));

        RuleFor(x => x.LastName)
            .NotEmpty()
            .Length(2, 50)
            .Must(ValidationHelper.ValidateForMixedLanguages)
            .WithMessage(x => string.Format(ValidationResource.MixedLanguage, nameof(x.LastName)));

        RuleFor(x => x.PersonalNumber)
            .NotEmpty()
            .Length(11)
            .Matches("^[0-9]*$");

        RuleFor(x => x.BirthDate)
            .NotEmpty()
            .Must(ValidationHelper.PersonAgeValidation)
            .WithMessage(ValidationResource.PersonAge);
    }
}