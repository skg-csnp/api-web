using Csnp.SharedKernel.Configuration.Settings.Data;
using Csnp.SharedKernel.Configuration.Settings.Email;
using Csnp.SharedKernel.Configuration.Settings.Messaging;
using Csnp.SharedKernel.Configuration.Settings.Security;

namespace Csnp.SharedKernel.Configuration.Settings;

/// <summary>
/// Represents the full application configuration model used to load structured settings
/// from environment variables or configuration files.
/// Includes settings such as RabbitMQ, database connections, JWT, and email.
/// </summary>
public class CsnpConfiguration
{
    /// <summary>
    /// The RabbitMQ message broker configuration.
    /// </summary>
    public RabbitMqSettings RabbitMq { get; set; } = new();

    /// <summary>
    /// The database connection settings.
    /// </summary>
    public DatabaseSettings Database { get; set; } = new();

    /// <summary>
    /// JWT-related authentication settings such as issuer, audience, key, and expiration.
    /// </summary>
    public JwtSettings Jwt { get; set; } = new();

    /// <summary>
    /// Email SMTP configuration.
    /// </summary>
    public EmailSettings Email { get; set; } = new();
}
