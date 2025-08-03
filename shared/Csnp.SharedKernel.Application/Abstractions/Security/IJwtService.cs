using System.Security.Claims;

namespace Csnp.SharedKernel.Application.Abstractions.Security;

/// <summary>
/// Provides methods for generating and validating JSON Web Tokens (JWT).
/// </summary>
public interface IJwtService
{
    /// <summary>
    /// Generate JWT token for the specified user
    /// </summary>
    /// <param name="userId">User ID</param>
    /// <param name="userName">User name or email</param>
    /// <param name="roles">Optional list of roles</param>
    /// <param name="expires">Optional expiration time (default in config)</param>
    /// <returns>Generated JWT token</returns>
    string GenerateToken(long userId, string userName, IEnumerable<string>? roles = null, TimeSpan? expires = null);

    /// <summary>
    /// Validate token and return principal
    /// </summary>
    /// <param name="token">JWT token</param>
    /// <returns>ClaimsPrincipal if valid; otherwise null</returns>
    ClaimsPrincipal? ValidateToken(string token);

    /// <summary>
    /// Get claims from token without validating signature
    /// </summary>
    /// <param name="token">JWT token</param>
    /// <returns>ClaimsPrincipal with extracted claims</returns>
    ClaimsPrincipal? DecodeToken(string token);

    /// <summary>
    /// Generates a new refresh token.
    /// </summary>
    /// <returns>A randomly generated refresh token string.</returns>
    string GenerateRefreshToken();
}
