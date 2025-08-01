using Csnp.Notification.Infrastructure.Persistence.Constants;
using Microsoft.EntityFrameworkCore;

namespace Csnp.Notification.Infrastructure.Persistence;

/// <summary>
/// The Entity Framework database context for the Notification module.
/// </summary>
public class NotificationDbContext : DbContext
{
    #region -- Overrides --

    /// <summary>
    /// Configures the entity mappings and schema during model creation.
    /// </summary>
    /// <param name="modelBuilder">The builder used to construct the model for the context.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.HasDefaultSchema(SchemaNames.Default);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(NotificationDbContext).Assembly);
    }

    #endregion

    #region -- Methods --

    /// <summary>
    /// Initializes a new instance of the <see cref="NotificationDbContext"/> class.
    /// </summary>
    /// <param name="options">The options to be used by the <see cref="DbContext"/>.</param>
    public NotificationDbContext(DbContextOptions<NotificationDbContext> options)
        : base(options)
    {
    }

    #endregion

    #region -- Properties --

    /// <summary>
    /// Gets or sets the set of email log records.
    /// </summary>
    public DbSet<EmailLogEntity> EmailLogs { get; set; }

    #endregion
}
