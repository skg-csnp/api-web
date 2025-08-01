using Csnp.Notification.Domain.Entities;

namespace Csnp.Notification.Application.Abstractions.Persistence;

/// <summary>
/// Defines the write operations for persisting <see cref="EmailLog"/> entities.
/// </summary>
public interface IEmailLogWriteRepository
{
    /// <summary>
    /// Inserts a new <see cref="EmailLog"/> into the data store.
    /// </summary>
    /// <param name="emailLog">The email log to persist.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task InsertAsync(EmailLog emailLog, CancellationToken cancellationToken);
}
