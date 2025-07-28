using Csnp.Notification.Infrastructure.Persistence.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Csnp.Migrations.Notification;

public class NotificationDbContextFactory : IDesignTimeDbContextFactory<NotificationDbContext>
{
    public NotificationDbContext CreateDbContext(string[] args)
    {
        var connectionString = args.Length > 0
            ? args[0]
            : "Server=localhost;Database=local_csnp_notification;User Id=local;Password=Local+54321z@;TrustServerCertificate=True;";

        var optionsBuilder = new DbContextOptionsBuilder<NotificationDbContext>();
        optionsBuilder.UseSqlServer(connectionString, b => b.MigrationsAssembly("Csnp.Migrations.Notification"));

        return new NotificationDbContext(optionsBuilder.Options);
    }
}
