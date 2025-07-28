using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Csnp.Credential.Infrastructure.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
{
    #region -- Methods --

    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.ToTable("Users", "credential");
        builder.Property(p => p.DisplayName).HasMaxLength(256);
        builder.HasIndex(p => p.Email).IsUnique();
        builder.Property(p => p.Id).ValueGeneratedNever();
    }

    #endregion
}
