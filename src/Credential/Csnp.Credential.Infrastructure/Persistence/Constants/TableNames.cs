namespace Csnp.Credential.Infrastructure.Persistence.Constants;

/// <summary>
/// Contains table names used in the Credential module.
/// </summary>
public static class TableNames
{
    #region -- Constants --

    /// <summary>
    /// The table name for application users.
    /// </summary>
    public const string Users = "Users";

    /// <summary>
    /// The table name for application roles.
    /// </summary>
    public const string Roles = "Roles";

    /// <summary>
    /// The table name for user claims.
    /// </summary>
    public const string UserClaims = "UserClaims";

    /// <summary>
    /// The table name for role claims.
    /// </summary>
    public const string RoleClaims = "RoleClaims";

    /// <summary>
    /// The table name for user-role relationships.
    /// </summary>
    public const string UserRoles = "UserRoles";

    /// <summary>
    /// The table name for user tokens.
    /// </summary>
    public const string UserTokens = "UserTokens";

    /// <summary>
    /// The table name for user logins (external providers).
    /// </summary>
    public const string UserLogins = "UserLogins";

    #endregion
}
