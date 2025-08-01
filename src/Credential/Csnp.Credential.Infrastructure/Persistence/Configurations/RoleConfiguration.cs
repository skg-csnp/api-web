using Csnp.Credential.Infrastructure.Persistence.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Csnp.Credential.Infrastructure.Persistence.Configurations;

/// <summary>
/// Configures the database schema for the <c>RoleEntity</c>.
/// </summary>
public class RoleConfiguration : IEntityTypeConfiguration<RoleEntity>
{
    #region -- Implements --

    /// <summary>
    /// Configures the <see cref="RoleEntity"/> entity.
    /// </summary>
    /// <param name="builder">The builder to configure the entity.</param>
    public void Configure(EntityTypeBuilder<RoleEntity> builder)
    {
        builder.ToTable(TableNames.Roles);
    }

    #endregion
}
