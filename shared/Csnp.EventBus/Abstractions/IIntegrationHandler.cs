using Csnp.EventBus.Events;

namespace Csnp.EventBus.Abstractions;

/// <summary>
/// Defines a contract for handling integration events from other systems or bounded contexts.
/// </summary>
/// <typeparam name="TEvent">The type of integration event to handle.</typeparam>
public interface IIntegrationHandler<in TEvent> where TEvent : IntegrationEvent
{
    /// <summary>
    /// Handles the specified integration event.
    /// </summary>
    /// <param name="integrationEvent">The integration event instance.</param>
    /// <param name="cancellationToken">A token to observe while waiting for the task to complete.</param>
    Task HandleAsync(TEvent integrationEvent, CancellationToken cancellationToken = default);
}
