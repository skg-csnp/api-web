using Csnp.Notification.Application.Abstractions.Persistence;
using Csnp.Notification.Domain.Entities;
using Csnp.Notification.Infrastructure.Mappers;

namespace Csnp.Notification.Infrastructure.Persistence.Repositories;

/// <summary>
/// Implements write operations for persisting <see cref="EmailLog"/> entities.
/// </summary>
internal sealed class EmailLogWriteRepository : IEmailLogWriteRepository
{
    #region -- Implements --

    /// <inheritdoc />
    public async Task InsertAsync(EmailLog emailLog, CancellationToken cancellationToken)
    {
        EmailLogEntity entity = emailLog.ToEntity();
        await _dbContext.EmailLogs.AddAsync(entity, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    #endregion

    #region -- Methods --

    /// <summary>
    /// Initializes a new instance of the <see cref="EmailLogWriteRepository"/> class.
    /// </summary>
    /// <param name="dbContext">The database context for the notification module.</param>
    public EmailLogWriteRepository(NotificationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    #endregion

    #region -- Fields --

    /// <summary>
    /// The database context used to persist <see cref="EmailLog"/> entities.
    /// </summary>
    private readonly NotificationDbContext _dbContext;

    #endregion
}
