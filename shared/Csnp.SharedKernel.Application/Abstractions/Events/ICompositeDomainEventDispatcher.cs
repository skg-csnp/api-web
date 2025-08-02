using Csnp.SharedKernel.Domain.Events;

namespace Csnp.SharedKernel.Application.Abstractions.Events;

/// <summary>
/// Coordinates dispatching of domain events to both handlers and integration publishers.
/// </summary>
public interface ICompositeDomainEventDispatcher
{
    #region -- Methods --

    /// <summary>
    /// Dispatches the domain events to both in-process handlers and integration publishers.
    /// </summary>
    /// <param name="domainEvents">The domain events to dispatch.</param>
    /// <param name="cancellationToken">A token for cancelling the asynchronous operation.</param>
    /// <returns>A task that represents the asynchronous dispatch operation.</returns>
    Task DispatchAsync(IEnumerable<IDomainEvent> domainEvents, CancellationToken cancellationToken = default);

    #endregion
}
