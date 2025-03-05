namespace PersonRegistry.Domain.Schema;

/// <summary>
/// Defines the database table names as constants to ensure consistency across the application.
/// </summary>
public static class TableSchema
{
    /// <summary>
    /// The database table name for persons.
    /// </summary>
    public const string PERSON = "Person";

    /// <summary>
    /// The database table name for cities.
    /// </summary>
    public const string CITY = "City";
}