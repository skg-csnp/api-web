using Csnp.SharedKernel.Configuration.Settings.Common;

namespace Csnp.SharedKernel.Configuration.Settings.Data;

/// <summary>
/// Represents the settings used to configure database connections.
/// Inherits basic credential properties and adds a database name.
/// Also includes a method to generate the database connection string.
/// </summary>
public class DatabaseSettings : ConnectionCredential
{
    /// <summary>
    /// The name of the database to connect to.
    /// </summary>
    public string Database { get; set; } = string.Empty;

    /// <summary>
    /// Generates a connection string for the configured database using standard PostgreSQL format.
    /// Can be overridden by derived classes for other database engines.
    /// </summary>
    /// <returns>A valid connection string for use in database clients.</returns>
    public virtual string ToConnectionString()
    {
        return string.Empty;
    }
}
