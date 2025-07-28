using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Csnp.Common.Security;

public class JwtService : IJwtService
{
    private readonly JwtSettings _settings;
    private readonly byte[] _key;

    public JwtService(IOptions<JwtSettings> settings)
    {
        _settings = settings.Value;
        _key = Encoding.UTF8.GetBytes(_settings.SecretKey);
    }

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

    public string GenerateRefreshToken()
    {
        byte[] randomBytes = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomBytes);
        return Convert.ToBase64String(randomBytes);
    }
}
