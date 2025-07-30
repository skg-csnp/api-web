using Csnp.EventBus.Events;

namespace Csnp.EventBus.Abstractions;

/// <summary>
/// Defines a contract for publishing integration events to external systems or services.
/// </summary>
public interface IIntegrationEventPublisher
{
    /// <summary>
    /// Publishes the specified integration event asynchronously.
    /// </summary>
    /// <param name="integrationEvent">The integration event to publish.</param>
    /// <returns>A task that represents the asynchronous publish operation.</returns>
    Task PublishAsync(IntegrationEvent integrationEvent);
}
