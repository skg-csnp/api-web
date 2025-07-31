namespace Csnp.SharedKernel.Configuration.Settings.Persistence;

/// <summary>
/// Represents the configuration settings required to connect to a SQL Server database.
/// Inherits common database settings from <see cref="DatabaseSettings"/>.
/// </summary>
public class SqlServerSettings : DatabaseSettings
{
    /// <summary>
    /// Gets or sets a value indicating whether the SQL Server client should trust the server's SSL certificate.
    /// This is useful when connecting to a SQL Server instance with a self-signed certificate.
    /// </summary>
    public bool TrustServerCertificate { get; set; } = true;

    /// <summary>
    /// Builds a SQL Server connection string using the current configuration properties.
    /// </summary>
    /// <returns>A formatted connection string for connecting to a SQL Server database.</returns>
    public override string ToConnectionString()
    {
        return $"Server={Host},{Port};Database={Database};User Id={Username};Password={Password};TrustServerCertificate={TrustServerCertificate};";
    }
}
