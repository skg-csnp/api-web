using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Csnp.Credential.Infrastructure.Persistence;

public class CredentialDbContext : IdentityDbContext<UserEntity, RoleEntity, long>
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
