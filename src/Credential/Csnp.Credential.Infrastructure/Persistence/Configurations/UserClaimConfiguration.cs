using Csnp.Credential.Infrastructure.Persistence.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Csnp.Credential.Infrastructure.Persistence.Configurations;

/// <summary>
/// Configures the database schema for <see cref="IdentityUserClaim{TKey}"/> entity.
/// </summary>
public class UserClaimConfiguration : IEntityTypeConfiguration<IdentityUserClaim<long>>
{
    #region -- Implements --

    /// <summary>
    /// Configures the <see cref="IdentityUserClaim{TKey}"/> entity.
    /// </summary>
    /// <param name="builder">The entity type builder.</param>
    public void Configure(EntityTypeBuilder<IdentityUserClaim<long>> builder)
    {
        builder.ToTable(TableNames.UserClaims);
    }

    #endregion
}
