using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Csnp.Migrations.Credential.Configurations;

public class UserLoginConfiguration : IEntityTypeConfiguration<IdentityUserLogin<long>>
{
    #region -- Methods --

    public void Configure(EntityTypeBuilder<IdentityUserLogin<long>> builder)
    {
        builder.ToTable("UserLogins", "credential");
    }

    #endregion
}
