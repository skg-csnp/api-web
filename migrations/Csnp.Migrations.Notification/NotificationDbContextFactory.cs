using Csnp.Notification.Infrastructure.Persistence;
using Csnp.Notification.Infrastructure.Persistence.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Csnp.Migrations.Notification;

/// <summary>
/// Provides a design-time factory for creating <see cref="NotificationDbContext"/> instances,
/// used by EF Core tools to generate and apply migrations.
/// </summary>
public class NotificationDbContextFactory : IDesignTimeDbContextFactory<NotificationDbContext>
{
    /// <inheritdoc />
    public NotificationDbContext CreateDbContext(string[] args)
    {
        string connectionString = args.Length > 0
            ? args[0]
            : "Host=localhost;Port=5432;Database=local_csnp;Username=local;Password=Local+54321z@";

        DbContextOptionsBuilder<NotificationDbContext> optionsBuilder = new DbContextOptionsBuilder<NotificationDbContext>();

        optionsBuilder.UseNpgsql(connectionString, builder =>
        {
            builder.MigrationsAssembly("Csnp.Migrations.Notification");
            builder.MigrationsHistoryTable("__ef_migrations_history", SchemaNames.Default);
        });

        return new NotificationDbContext(optionsBuilder.Options);
    }
}
