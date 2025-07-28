using Microsoft.EntityFrameworkCore;

namespace Csnp.Notification.Infrastructure.Persistence.Shared;

public class NotificationDbContext : DbContext
{
    #region -- Overrides --

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.HasDefaultSchema("notification");
        builder.ApplyConfigurationsFromAssembly(typeof(NotificationDbContext).Assembly);
    }

    #endregion

    #region -- Methods --

    public NotificationDbContext(DbContextOptions<NotificationDbContext> options) : base(options) { }

    #endregion

    #region -- Properties --

    public DbSet<EmailLogEntity> EmailLogs { get; set; }

    #endregion
}
