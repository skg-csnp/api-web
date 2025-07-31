using Csnp.SharedKernel.Domain.Events;

namespace Csnp.SharedKernel.Application.Abstractions.Events;

/// <summary>
/// Defines a contract for dispatching domain events to their corresponding handlers.
/// </summary>
public interface IDomainEventDispatcher
{
    /// <summary>
    /// Dispatches a collection of domain events asynchronously.
    /// </summary>
    /// <param name="domainEvents">The domain events to dispatch.</param>
    /// <param name="cancellationToken">A token for cancelling the operation.</param>
    /// <returns>A task that represents the asynchronous dispatch operation.</returns>
    Task DispatchAsync(IEnumerable<IDomainEvent> domainEvents, CancellationToken cancellationToken = default);
}
