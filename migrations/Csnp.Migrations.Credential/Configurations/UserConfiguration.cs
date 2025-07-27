using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Csnp.Migrations.Credential.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    #region -- Methods --

    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users", "credential");
        builder.Property(p => p.DisplayName).HasMaxLength(256);
        builder.HasIndex(p => p.Email).IsUnique();
    }

    #endregion
}
