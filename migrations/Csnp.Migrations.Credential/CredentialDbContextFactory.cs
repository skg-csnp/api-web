using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Csnp.Migrations.Credential;

public class CredentialDbContextFactory : IDesignTimeDbContextFactory<CredentialDbContext>
{
    public CredentialDbContext CreateDbContext(string[] args)
    {
        var connectionString = args.Length > 0
            ? args[0]
            : "Server=localhost;Database=local_csnp_credential;User Id=local;Password=Local+54321z@;TrustServerCertificate=True;";

        var optionsBuilder = new DbContextOptionsBuilder<CredentialDbContext>();
        optionsBuilder.UseSqlServer(connectionString);

        return new CredentialDbContext(optionsBuilder.Options);
    }
}
