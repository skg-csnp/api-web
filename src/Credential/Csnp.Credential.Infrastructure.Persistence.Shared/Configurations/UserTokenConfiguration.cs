using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Csnp.Credential.Infrastructure.Persistence.Shared.Configurations;

public class UserTokenConfiguration : IEntityTypeConfiguration<IdentityUserToken<long>>
{
    #region -- Methods --

    public void Configure(EntityTypeBuilder<IdentityUserToken<long>> builder)
    {
        builder.ToTable("UserTokens", "credential");
    }

    #endregion
}
