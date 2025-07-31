using System.Text.RegularExpressions;

namespace Csnp.SeedWork.Domain.Validators;

/// <summary>
/// Provides validation logic for email address format.
/// </summary>
public static class EmailAddressValidator
{
    #region -- Methods --

    /// <summary>
    /// Determines whether the specified email is in a valid format.
    /// </summary>
    /// <param name="email">The email string to validate.</param>
    /// <returns>
    /// <c>true</c> if the format is valid according to the defined email pattern; otherwise, <c>false</c>.
    /// </returns>
    /// <remarks>
    /// This uses a simplified regular expression and may not cover all edge cases of valid email formats defined by RFC 5322.
    /// </remarks>
    public static bool IsValid(string email)
    {
        return Regex.IsMatch(email, EmailPattern);
    }

    #endregion

    #region -- Constants --

    /// <summary>
    /// The regular expression pattern used to validate basic email address format.
    /// </summary>
    private const string EmailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

    #endregion
}
