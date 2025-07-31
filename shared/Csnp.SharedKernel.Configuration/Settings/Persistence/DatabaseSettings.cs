using Csnp.SharedKernel.Configuration.Settings.Common;

namespace Csnp.SharedKernel.Configuration.Settings.Persistence;

/// <summary>
/// Represents the settings used to configure database connections.
/// Inherits basic credential properties and adds a database name.
/// Also includes a method to generate the database connection string.
/// </summary>
public abstract class DatabaseSettings : ConnectionCredential
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
    public abstract string ToConnectionString();
}
