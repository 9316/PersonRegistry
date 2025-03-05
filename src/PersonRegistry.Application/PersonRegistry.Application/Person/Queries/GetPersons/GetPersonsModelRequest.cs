using PersonRegistry.Domain.Enums;

namespace PersonRegistry.Application.Person.Queries.GetPersons;

/// <summary>
/// Represents a request to retrieve a list of persons, optionally filtered by various criteria, 
/// and supports pagination.
/// </summary>
public class GetPersonsModelRequest
{
    /// <summary>
    /// A general query filter to apply across multiple fields.
    /// </summary>
    public string FilterQuery { get; set; }

    /// <summary>
    /// Filter for persons with a matching name.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Filter for persons with a matching last name.
    /// </summary>
    public string LastName { get; set; }

    /// <summary>
    /// Filter for persons with a matching personal number.
    /// </summary>
    public string PersonalNumber { get; set; }

    /// <summary>
    /// Filter for persons with a specific gender.
    /// </summary>
    public GenderEnum? Gender { get; set; }

    /// <summary>
    /// Filter for persons born on a specific date.
    /// </summary>
    public DateTime BirthDate { get; set; }

    /// <summary>
    /// The number of items to return per page (for pagination).
    /// </summary>
    public int PageSize { get; set; }

    /// <summary>
    /// The page number of the result set to return (for pagination).
    /// </summary>
    public int PageNumber { get; set; }
}
