using Microsoft.AspNetCore.Identity;

namespace Csnp.Credential.Infrastructure.Persistence;

public class UserEntity : IdentityUser<long>
{
    #region -- Properties --

    public string? DisplayName { get; set; }

    #endregion
}
