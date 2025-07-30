using System.Text.RegularExpressions;
using Csnp.SharedKernel.Domain.Exceptions;

namespace Csnp.SharedKernel.Domain.ValueObjects;

public sealed class EmailAddress : ValueObject
{
    public string Value { get; }

    private EmailAddress(string value)
    {
        Value = value;
    }

    public static EmailAddress Create(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            throw new ArgumentException("Email is required");
        }

        bool isValid = Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        if (!isValid)
        {
            throw new InvalidEmailException("Email format is invalid");
        }

        return new EmailAddress(email.Trim().ToUpperInvariant());
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString() => Value;
}
