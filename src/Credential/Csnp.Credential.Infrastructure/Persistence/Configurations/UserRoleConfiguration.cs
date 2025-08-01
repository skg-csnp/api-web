using Csnp.Credential.Infrastructure.Persistence.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Csnp.Credential.Infrastructure.Persistence.Configurations;

/// <summary>
/// Configures the database schema for the <see cref="IdentityUserRole{TKey}"/> entity.
/// </summary>
public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<long>>
{
    #region -- Implements --

    /// <summary>
    /// Configures the <see cref="IdentityUserRole{TKey}"/> entity.
    /// </summary>
    /// <param name="builder">The entity type builder.</param>
    public void Configure(EntityTypeBuilder<IdentityUserRole<long>> builder)
    {
        builder.ToTable(TableNames.UserRoles);
    }

    #endregion
}
