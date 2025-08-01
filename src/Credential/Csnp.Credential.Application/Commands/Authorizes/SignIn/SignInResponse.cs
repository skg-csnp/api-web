namespace Csnp.Credential.Application.Commands.Authorizes.SignIn;

/// <summary>
/// Represents the response returned after a successful sign-in attempt.
/// </summary>
public class SignInResponse
{
    #region -- Properties --

    /// <summary>
    /// Gets or sets the JWT access token.
    /// </summary>
    public string AccessToken { get; set; } = default!;

    /// <summary>
    /// Gets or sets the refresh token used to obtain a new access token.
    /// </summary>
    public string RefreshToken { get; set; } = default!;

    /// <summary>
    /// Gets or sets the expiration date and time of the access token.
    /// </summary>
    public DateTime ExpiresAt { get; set; }

    #endregion
}
