using Csnp.Credential.Infrastructure.Persistence.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Csnp.Credential.Infrastructure.Persistence.Configurations;

/// <summary>
/// Configures the database schema for the <see cref="UserEntity"/>.
/// </summary>
public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
{
    #region -- Implements --

    /// <summary>
    /// Configures the <see cref="UserEntity"/> entity.
    /// </summary>
    /// <param name="builder">The entity type builder.</param>
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.ToTable(TableNames.Users);

        builder.Property(p => p.DisplayName)
               .HasMaxLength(256);

        builder.HasIndex(p => p.Email)
               .IsUnique();

        builder.Property(p => p.Id)
               .ValueGeneratedNever();
    }

    #endregion
}
