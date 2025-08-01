using MediatR;

namespace Csnp.Credential.Application.Commands.Authorizes.SignUp;

/// <summary>
/// Represents a command to sign up a new user.
/// </summary>
/// <param name="Email">User's email address.</param>
/// <param name="Password">User's password.</param>
/// <param name="DisplayName">User's display name.</param>
public record SignUpCommand(string Email, string Password, string DisplayName) : IRequest<long>;
