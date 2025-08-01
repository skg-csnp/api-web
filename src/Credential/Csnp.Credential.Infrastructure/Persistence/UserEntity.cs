using Microsoft.AspNetCore.Identity;

namespace Csnp.Credential.Infrastructure.Persistence;

/// <summary>
/// Represents an application user in the Credential module using long as the key type.
/// </summary>
public class UserEntity : IdentityUser<long>
{
    #region -- Properties --

    /// <summary>
    /// Gets or sets the display name of the user.
    /// </summary>
    public string? DisplayName { get; set; }

    #endregion
}
