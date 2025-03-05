using FluentValidation;
using PersonRegistry.Application.Person.Helper;
using PersonRegistry.Application.Resources;

namespace PersonRegistry.Application.Person.Commands.Update;

/// <summary>
/// Validator for the <see cref="UpdatePersonCommand"/>.
/// </summary>
public class UpdatePersonCommandValidator : AbstractValidator<UpdatePersonCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UpdatePersonCommandValidator"/> class.
    /// </summary>
    public UpdatePersonCommandValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).NotEmpty();

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