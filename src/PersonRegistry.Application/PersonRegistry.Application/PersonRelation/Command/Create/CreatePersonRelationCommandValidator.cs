using FluentValidation;

namespace PersonRegistry.Application.PersonRelation.Command.Create;

/// <summary>
/// Validator for the <see cref="CreatePersonRelationCommand"/> to ensure correct data input.
/// </summary>
public class CreatePersonRelationCommandValidator : AbstractValidator<CreatePersonRelationCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreatePersonRelationCommandValidator"/> class.
    /// Defines validation rules for creating a person relationship.
    /// </summary>
    public CreatePersonRelationCommandValidator()
    {
        RuleFor(x => x.PersonId).NotEmpty();
        RuleFor(x => x.RelatedPersonId).NotEqual(x => x.PersonId).NotEmpty();
        RuleFor(x => x.PersonRelationTypeId).NotEmpty();
    }
}