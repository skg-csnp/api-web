using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Csnp.SharedKernel.Application.Abstractions.Events.Security;
using Csnp.SharedKernel.Configuration.Settings.Security;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Csnp.Security.Infrastructure;

/// <summary>
/// Implementation of <see cref="IJwtService"/> using HMAC SHA256.
/// </summary>
public class JwtService : IJwtService
{
    #region -- Implements --

    /// <inheritdoc/>
    public string GenerateToken(long userId, string userName, IEnumerable<string>? roles = null, TimeSpan? expires = null)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, userId.ToString(CultureInfo.InvariantCulture)),
            new Claim(ClaimTypes.Name, userName)
        };

        if (roles != null)
        {
            claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)));
        }

        var credentials = new SigningCredentials(new SymmetricSecurityKey(_key), SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            issuer: _settings.Issuer,
            audience: _settings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.Add(expires ?? TimeSpan.FromMinutes(_settings.ExpirationMinutes)),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    /// <inheritdoc/>
    public ClaimsPrincipal? ValidateToken(string token)
    {
        try
        {
            var handler = new JwtSecurityTokenHandler();
            ClaimsPrincipal principal = handler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = _settings.Issuer,
                ValidateAudience = true,
                ValidAudience = _settings.Audience,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(_key),
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            }, out _);

            return principal;
        }
        catch
        {
            return null;
        }
    }

    /// <inheritdoc/>
    public ClaimsPrincipal? DecodeToken(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        try
        {
            JwtSecurityToken jwtToken = handler.ReadJwtToken(token);
            var identity = new ClaimsIdentity(jwtToken.Claims, "jwt");
            return new ClaimsPrincipal(identity);
        }
        catch
        {
            return null;
        }
    }

    /// <inheritdoc/>
    public string GenerateRefreshToken()
    {
        byte[] randomBytes = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomBytes);
        return Convert.ToBase64String(randomBytes);
    }

    #endregion

    #region -- Methods --

    /// <summary>
    /// Initializes a new instance of the <see cref="JwtService"/> class.
    /// </summary>
    /// <param name="settings">JWT configuration settings.</param>
    public JwtService(IOptions<JwtSettings> settings)
    {
        _settings = settings.Value;
        _key = Encoding.UTF8.GetBytes(_settings.SecretKey);
    }

    #endregion

    #region -- Fields --

    /// <summary>
    /// JWT settings loaded from configuration.
    /// </summary>
    private readonly JwtSettings _settings;

    /// <summary>
    /// Encoded secret key used for signing and validation.
    /// </summary>
    private readonly byte[] _key;

    #endregion
}
