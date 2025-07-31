namespace Csnp.SharedKernel.Configuration.Settings.Security;

/// <summary>
/// Represents the configuration for JSON Web Token (JWT) authentication.
/// Contains metadata used to issue and validate tokens.
/// </summary>
public class JwtSettings
{
    /// <summary>
    /// The token issuer identifier.
    /// </summary>
    public string Issuer { get; set; } = string.Empty;

    /// <summary>
    /// The intended audience of the token.
    /// </summary>
    public string Audience { get; set; } = string.Empty;

    /// <summary>
    /// The symmetric key used to sign the JWT.
    /// </summary>
    public string SecretKey { get; set; } = string.Empty;

    /// <summary>
    /// The expiration time of the token in minutes.
    /// </summary>
    public int ExpirationMinutes { get; set; } = 60;
}
