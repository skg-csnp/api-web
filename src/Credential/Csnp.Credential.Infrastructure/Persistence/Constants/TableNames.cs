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
    public const string Users = "users";

    /// <summary>
    /// The table name for application roles.
    /// </summary>
    public const string Roles = "roles";

    /// <summary>
    /// The table name for user claims.
    /// </summary>
    public const string UserClaims = "user_claims";

    /// <summary>
    /// The table name for role claims.
    /// </summary>
    public const string RoleClaims = "role_claims";

    /// <summary>
    /// The table name for user-role relationships.
    /// </summary>
    public const string UserRoles = "user_roles";

    /// <summary>
    /// The table name for user tokens.
    /// </summary>
    public const string UserTokens = "user_tokens";

    /// <summary>
    /// The table name for user logins (external providers).
    /// </summary>
    public const string UserLogins = "user_logins";

    #endregion
}
