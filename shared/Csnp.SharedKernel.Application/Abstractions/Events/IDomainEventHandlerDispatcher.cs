using Csnp.SharedKernel.Domain.Events;

namespace Csnp.SharedKernel.Application.Abstractions.Events;

/// <summary>
/// Dispatches domain events to their corresponding in-process event handlers.
/// </summary>
public interface IDomainEventHandlerDispatcher
{
    #region -- Methods --

    /// <summary>
    /// Dispatches the specified domain events to their handlers.
    /// </summary>
    /// <param name="domainEvents">The domain events to dispatch.</param>
    /// <param name="cancellationToken">A token for cancelling the asynchronous operation.</param>
    /// <returns>A task that represents the asynchronous dispatch operation.</returns>
    Task DispatchAsync(IEnumerable<IDomainEvent> domainEvents, CancellationToken cancellationToken = default);

    #endregion
}
