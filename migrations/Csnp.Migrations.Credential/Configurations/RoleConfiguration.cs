using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Csnp.Migrations.Credential.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    #region -- Methods --

    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("Roles", "credential");
    }

    #endregion
}
