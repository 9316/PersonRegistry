using System.Text.RegularExpressions;

namespace PersonRegistry.Application.Person.Helper;

public static class ValidationHelper
{
    /// <summary>
    /// Validates the first name, contains one language characters.
    /// </summary>
    /// <param name="firstName">The first name to validate.</param>
    /// <returns>true if the first name is valid; otherwise, false.</returns>
    public static bool ValidateForMixedLanguages(string firstName)
    {
        if (string.IsNullOrWhiteSpace(firstName)) return false;

        var isValidEnglishName = Regex.IsMatch(firstName, "^[a-zA-Z ]*$");
        var isValidGeorgianName = Regex.IsMatch(firstName, "^[ა-ჰ ]*$");

        return isValidEnglishName || isValidGeorgianName;
    }
    /// <summary>
    /// Validates person's age, under 18
    /// </summary>
    /// <param name="birthDate"></param>
    /// <returns></returns>
    public static bool PersonAgeValidation(DateTime birthDate)
    {
        var today = DateTime.Today;
        var age = today.Year - birthDate.Year;

        if (birthDate.Date > today.AddYears(-age)) age--;

        return age >= 18;
    }
}
