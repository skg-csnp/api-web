using MediatR;

namespace Csnp.Credential.Application.Commands.Authorizes.SignIn;

/// <summary>
/// Represents the command for signing in a user.
/// </summary>
public class SignInCommand : IRequest<SignInResponse>
{
    #region -- Properties --

    /// <summary>
    /// Gets the email of the user.
    /// </summary>
    public string Email { get; init; } = default!;

    /// <summary>
    /// Gets the password of the user.
    /// </summary>
    public string Password { get; init; } = default!;

    #endregion
}
