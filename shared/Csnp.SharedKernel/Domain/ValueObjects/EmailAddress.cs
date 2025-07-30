using Csnp.SharedKernel.Domain.Exceptions;
using Csnp.SharedKernel.Domain.Validators;

namespace Csnp.SharedKernel.Domain.ValueObjects;

/// <summary>
/// Represents an email address as a value object with validation logic.
/// </summary>
public sealed class EmailAddress : ValueObject
{
    #region -- Overrides --

    /// <inheritdoc/>
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    /// <inheritdoc/>
    public override string ToString() => Value;

    #endregion

    #region -- Methods --

    /// <summary>
    /// Initializes a new instance of the <see cref="EmailAddress"/> class.
    /// Use <see cref="Create"/> to instantiate.
    /// </summary>
    /// <param name="value">The validated email address.</param>
    private EmailAddress(string value)
    {
        Value = value;
    }

    /// <summary>
    /// Creates a new <see cref="EmailAddress"/> instance after validating the input.
    /// </summary>
    /// <param name="email">The raw email string to validate and create.</param>
    /// <returns>A new instance of <see cref="EmailAddress"/>.</returns>
    /// <exception cref="ArgumentException">Thrown when the email is null or whitespace.</exception>
    /// <exception cref="InvalidEmailException">Thrown when the email format is invalid.</exception>
    public static EmailAddress Create(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            throw new ArgumentException("Email is required", nameof(email));
        }

        if (!EmailAddressValidator.IsValid(email))
        {
            throw new InvalidEmailException("Email format is invalid");
        }

        return new EmailAddress(email.Trim().ToUpperInvariant());
    }

    #endregion

    #region -- Properties --

    /// <summary>
    /// Gets the normalized email address string.
    /// </summary>
    public string Value { get; }

    #endregion
}
