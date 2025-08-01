using Csnp.Credential.Infrastructure.Persistence.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Csnp.Credential.Infrastructure.Persistence.Configurations;

/// <summary>
/// Configures the database schema for the <see cref="IdentityRoleClaim{TKey}"/> entity.
/// </summary>
public class RoleClaimConfiguration : IEntityTypeConfiguration<IdentityRoleClaim<long>>
{
    #region -- Implements --

    /// <summary>
    /// Configures the <see cref="IdentityRoleClaim{TKey}"/> entity.
    /// </summary>
    /// <param name="builder">The builder to configure the entity.</param>
    public void Configure(EntityTypeBuilder<IdentityRoleClaim<long>> builder)
    {
        builder.ToTable(TableNames.RoleClaims);
    }

    #endregion
}
