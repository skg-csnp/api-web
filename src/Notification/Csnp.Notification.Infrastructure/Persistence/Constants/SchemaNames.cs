namespace Csnp.Notification.Infrastructure.Persistence.Constants;

/// <summary>
/// Contains schema name constants used in the database.
/// </summary>
public static class SchemaNames
{
    /// <summary>
    /// The default schema used for notification-related tables.
    /// </summary>
    public const string Default = "notification";

    /// <summary>
    /// The schema used for audit log tables.
    /// </summary>
    public const string Audit = "audit";

    /// <summary>
    /// The schema used for external integration-related tables.
    /// </summary>
    public const string Integration = "integration";
}
