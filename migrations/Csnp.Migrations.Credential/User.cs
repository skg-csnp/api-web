using Microsoft.AspNetCore.Identity;

namespace Csnp.Migrations.Credential;

public class User : IdentityUser<long>
{
    #region -- Properties --

    public string? DisplayName { get; set; }

    #endregion
}
