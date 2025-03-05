using FluentValidation;

namespace PersonRegistry.Application.City.Commands.Create;

/// <summary>
/// Validator for the <see cref="CreateCityCommand"/>.
/// </summary>
internal class CreateCityCommandValidator : AbstractValidator<CreateCityCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateCityCommandValidator"/> class.
    /// </summary>
    internal CreateCityCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
    }
}
