using FluentValidation;

namespace PersonRegistry.Application.City.Commands.Delete;

/// <summary>
/// Validator for the <see cref="DeleteCityCommand"/>.
/// </summary>
internal class DeleteCityCommandValidator : AbstractValidator<DeleteCityCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteCityCommandValidator"/> class.
    /// </summary>
    internal DeleteCityCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}
