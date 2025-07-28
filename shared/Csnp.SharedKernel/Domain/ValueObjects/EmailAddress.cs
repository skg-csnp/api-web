using System.Text.RegularExpressions;

namespace Csnp.SharedKernel.Domain.ValueObjects;

public sealed class EmailAddress : ValueObject
{
    public string Value { get; }

    private EmailAddress(string value) => Value = value;

    public static EmailAddress Create(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email is required");

        var isValid = Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        if (!isValid)
            throw new ArgumentException("Email format is invalid");

        return new EmailAddress(email.Trim().ToLowerInvariant());
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString() => Value;
}
