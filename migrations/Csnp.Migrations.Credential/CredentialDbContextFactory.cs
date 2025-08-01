using Csnp.Credential.Infrastructure.Persistence;
using Csnp.Credential.Infrastructure.Persistence.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Csnp.Migrations.Credential;

/// <summary>
/// Provides a design-time factory for creating <see cref="CredentialDbContext"/> instances,
/// used by EF Core tools to generate and apply migrations.
/// </summary>
public class CredentialDbContextFactory : IDesignTimeDbContextFactory<CredentialDbContext>
{
    /// <inheritdoc />
    public CredentialDbContext CreateDbContext(string[] args)
    {
        string connectionString = args.Length > 0
            ? args[0]
            : "Server=localhost;Database=local_csnp;User Id=local;Password=Local+54321z@;TrustServerCertificate=True;";

        DbContextOptionsBuilder<CredentialDbContext> optionsBuilder = new DbContextOptionsBuilder<CredentialDbContext>();

        optionsBuilder.UseSqlServer(connectionString, builder =>
        {
            builder.MigrationsAssembly("Csnp.Migrations.Credential");
            builder.MigrationsHistoryTable("__EFMigrationsHistory", SchemaNames.Default);
        });

        return new CredentialDbContext(optionsBuilder.Options);
    }
}
