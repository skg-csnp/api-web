using Csnp.EventBus.Events;

namespace Csnp.EventBus.Abstractions;

/// <summary>
/// Provides metadata for routing integration events in RabbitMQ.
/// </summary>
/// <typeparam name="TEvent">The type of the integration event.</typeparam>
public interface IIntegrationEventMetadata<TEvent>
    where TEvent : IntegrationEvent
{
    /// <summary>
    /// Gets the name of the queue that will receive the event.
    /// </summary>
    string QueueName { get; }

    /// <summary>
    /// Gets the name of the exchange that the event will be published to.
    /// </summary>
    string ExchangeName { get; }
}
