using Csnp.SharedKernel.Application.Abstractions.Events;
using Csnp.SharedKernel.Domain;

namespace Csnp.SharedKernel.Application.Events;

/// <summary>
/// Provides helper methods for dispatching and clearing domain events from entities.
/// </summary>
public static class DomainEventHelper
{
    #region -- Methods --

    /// <summary>
    /// Dispatches all domain events from the specified entity and then clears them.
    /// </summary>
    /// <param name="entity">The entity containing domain events.</param>
    /// <param name="dispatcher">The dispatcher responsible for handling the domain events.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public static async Task DispatchAndClearAsync(
        Entity<long> entity,
        ICompositeDomainEventDispatcher dispatcher,
        CancellationToken cancellationToken = default)
    {
        await dispatcher.DispatchAsync(entity.DomainEvents, cancellationToken);
        entity.ClearDomainEvents();
    }

    #endregion
}
