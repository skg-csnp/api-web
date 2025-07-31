namespace Csnp.SharedKernel.Configuration.Settings.Data;

/// <summary>
/// Represents the configuration settings required to connect to a PostgreSQL database.
/// Inherits common database settings from <see cref="DatabaseSettings"/>.
/// </summary>
public class PostgreSqlSettings : DatabaseSettings
{
    /// <summary>
    /// Builds a PostgreSQL connection string using the current configuration properties.
    /// </summary>
    /// <returns>A formatted connection string for connecting to a PostgreSQL database.</returns>
    public override string ToConnectionString()
    {
        return $"Host={Host};Port={Port};Database={Database};Username={Username};Password={Password}";
    }
}
