using Csnp.Notification.Infrastructure.Persistence.Constants;
using Microsoft.EntityFrameworkCore;

namespace Csnp.Notification.Infrastructure.Persistence;

public class NotificationDbContext : DbContext
{
    #region -- Overrides --

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.HasDefaultSchema(SchemaNames.Default);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(NotificationDbContext).Assembly);
    }

    #endregion

    #region -- Methods --

    public NotificationDbContext(DbContextOptions<NotificationDbContext> options) : base(options) { }

    #endregion

    #region -- Properties --

    public DbSet<EmailLogEntity> EmailLogs { get; set; }

    #endregion
}
