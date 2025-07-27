using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Csnp.Migrations.Credential.Configurations;

public class UserClaimConfiguration : IEntityTypeConfiguration<IdentityUserClaim<long>>
{
    #region -- Methods --

    public void Configure(EntityTypeBuilder<IdentityUserClaim<long>> builder)
    {
        builder.ToTable("UserClaims", "credential");
    }

    #endregion
}
