using Csnp.SharedKernel.Domain.Exceptions;
using Csnp.SharedKernel.Domain.ValueObjects;

namespace Csnp.SharedKernel.Tests.Unit.Domain.ValueObjects;

public class EmailAddressTests
{
    [Fact]
    public void Create_Should_Return_EmailAddress_When_Valid()
    {
        // Arrange
        string input = "test@example.com";

        // Act
        var result = EmailAddress.Create(input);

        // Assert
        Assert.Equal("TEST@EXAMPLE.COM", result.Value); // ToUpperInvariant()
        Assert.Equal("TEST@EXAMPLE.COM", result.ToString());
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("    ")]
    public void Create_Should_Throw_When_Empty_Or_Whitespace(string? input)
    {
        // Act & Assert
        ArgumentException ex = Assert.Throws<ArgumentException>(() => EmailAddress.Create(input!));
        Assert.Equal("Email is required", ex.Message);
    }

    [Theory]
    [InlineData("abc")]
    [InlineData("noatsymbol.com")]
    [InlineData("@missinguser.com")]
    [InlineData("user@")]
    [InlineData("user@.com")]
    [InlineData("user@domain")]
    public void Create_Should_Throw_When_Format_Is_Invalid(string input)
    {
        // Act & Assert
        InvalidEmailException ex = Assert.Throws<InvalidEmailException>(() => EmailAddress.Create(input));
        Assert.Equal("Email format is invalid", ex.Message);
    }

    [Fact]
    public void Equals_Should_Return_True_For_Same_Email()
    {
        var email1 = EmailAddress.Create("Test@Example.com");
        var email2 = EmailAddress.Create("test@example.com");

        Assert.Equal(email1, email2);
        Assert.True(email1 == email2);
    }

    [Fact]
    public void Equals_Should_Return_False_For_Different_Emails()
    {
        var email1 = EmailAddress.Create("one@example.com");
        var email2 = EmailAddress.Create("two@example.com");

        Assert.NotEqual(email1, email2);
        Assert.True(email1 != email2);
    }

    [Fact]
    public void GetHashCode_Should_Match_For_Same_Email()
    {
        var email1 = EmailAddress.Create("test@example.com");
        var email2 = EmailAddress.Create("TEST@EXAMPLE.COM");

        Assert.Equal(email1.GetHashCode(), email2.GetHashCode());
    }
}
