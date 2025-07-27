using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Csnp.Migrations.Credential;

public class CredentialDbContext : IdentityDbContext<User, Role, long>
{
    #region -- Overrides --

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(typeof(CredentialDbContext).Assembly);
    }

    #endregion

    #region -- Methods --

    public CredentialDbContext(DbContextOptions<CredentialDbContext> options) : base(options) { }

    #endregion
}
