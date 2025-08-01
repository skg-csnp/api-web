using Csnp.Credential.Infrastructure.Persistence.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Csnp.Credential.Infrastructure.Persistence.Configurations;

/// <summary>
/// Configures the database schema for the <see cref="IdentityUserToken{TKey}"/> entity.
/// </summary>
public class UserTokenConfiguration : IEntityTypeConfiguration<IdentityUserToken<long>>
{
    #region -- Implements --

    /// <summary>
    /// Configures the <see cref="IdentityUserToken{TKey}"/> entity.
    /// </summary>
    /// <param name="builder">The entity type builder.</param>
    public void Configure(EntityTypeBuilder<IdentityUserToken<long>> builder)
    {
        builder.ToTable(TableNames.UserTokens);
    }

    #endregion
}
