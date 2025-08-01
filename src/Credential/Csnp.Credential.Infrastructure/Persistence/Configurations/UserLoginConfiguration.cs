using Csnp.Credential.Infrastructure.Persistence.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Csnp.Credential.Infrastructure.Persistence.Configurations;

/// <summary>
/// Configures the database schema for the <see cref="IdentityUserLogin{TKey}"/> entity.
/// </summary>
public class UserLoginConfiguration : IEntityTypeConfiguration<IdentityUserLogin<long>>
{
    #region -- Implements --

    /// <summary>
    /// Configures the <see cref="IdentityUserLogin{TKey}"/> entity.
    /// </summary>
    /// <param name="builder">The entity type builder.</param>
    public void Configure(EntityTypeBuilder<IdentityUserLogin<long>> builder)
    {
        builder.ToTable(TableNames.UserLogins);
    }

    #endregion
}
