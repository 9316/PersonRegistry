namespace PersonRegistry.Application.Person.Queries.GetPersonRelationReport;

/// <summary>
/// Represents a response model for a person's relation report.
/// </summary>
/// <param name="Name">The first name of the person.</param>
/// <param name="LastName">The last name of the person.</param>
/// <param name="PersonalNumber">The personal identification number of the person.</param>
/// <param name="PersonRelationType">The type of relationship the person has.</param>
/// <param name="Quantity">The quantity of related persons of this type.</param>
public record PersonRelationReportModelResponse(
    string Name,
    string LastName,
    string PersonalNumber,
    string PersonRelationType,
    int Quantity
);