using FluentValidation;

namespace PersonRegistry.Application.City.Commands.Update;

/// <summary>
/// Validator for the <see cref="UpdateCityCommand"/>.
/// </summary>
internal class UpdateCityCommandValidator : AbstractValidator<UpdateCityCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateCityCommandValidator"/> class.
    /// </summary>
    internal UpdateCityCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Name).NotEmpty();
    }
}