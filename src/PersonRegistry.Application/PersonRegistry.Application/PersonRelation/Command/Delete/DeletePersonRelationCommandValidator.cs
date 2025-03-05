using FluentValidation;

namespace PersonRegistry.Application.PersonRelation.Command.Delete;

/// <summary>
/// Validator for the <see cref="DeletePersonRelationCommand"/> to ensure valid data input.
/// </summary>
public class DeletePersonRelationCommandValidator : AbstractValidator<DeletePersonRelationCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DeletePersonRelationCommandValidator"/> class.
    /// Defines validation rules for deleting a person relationship.
    /// </summary>
    public DeletePersonRelationCommandValidator()
    {
        RuleFor(x => x.PersonId).NotEmpty();
        RuleFor(x => x.RelatedPersonId).NotEqual(x => x.PersonId).NotEmpty();
        RuleFor(x => x.PersonRelationTypeId).NotEmpty();
    }
}