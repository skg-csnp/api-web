using Csnp.SharedKernel.Domain.Events;

namespace Csnp.SharedKernel.Application.Abstractions.Events;

/// <summary>
/// Converts domain events to integration events and publishes them to external systems.
/// </summary>
public interface IDomainToIntegrationDispatcher
{
    #region -- Methods --

    /// <summary>
    /// Dispatches domain events as integration events when applicable.
    /// </summary>
    /// <param name="domainEvents">The domain events to convert and publish.</param>
    /// <param name="cancellationToken">A token for cancelling the asynchronous operation.</param>
    /// <returns>A task that represents the asynchronous dispatch operation.</returns>
    Task DispatchAsync(IEnumerable<IDomainEvent> domainEvents, CancellationToken cancellationToken = default);

    #endregion
}
