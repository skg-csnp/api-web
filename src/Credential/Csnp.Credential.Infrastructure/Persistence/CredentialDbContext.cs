using Csnp.Credential.Infrastructure.Persistence.Constants;
using Csnp.SharedKernel.Infrastructure.Extensions;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Csnp.Credential.Infrastructure.Persistence;

/// <summary>
/// Represents the EF Core DbContext for the Credential module, including Identity.
/// </summary>
public class CredentialDbContext : IdentityDbContext<UserEntity, RoleEntity, long>
{
    #region -- Overrides --

    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.UseSnakeCaseNames(); // apply snake_case to all table/column/index/etc.
        builder.HasDefaultSchema(SchemaNames.Default);
        builder.ApplyConfigurationsFromAssembly(typeof(UserEntity).Assembly);
    }

    #endregion

    #region -- Methods --

    /// <summary>
    /// Initializes a new instance of the <see cref="CredentialDbContext"/> class.
    /// </summary>
    /// <param name="options">The DB context options.</param>
    public CredentialDbContext(DbContextOptions<CredentialDbContext> options) : base(options)
    {
    }

    #endregion
}
