using Csnp.Credential.Application.Events.Users;
using Csnp.Credential.Domain.Events.Users;
using Csnp.EventBus.Abstractions;
using Csnp.SharedKernel.Application.Abstractions.Events;
using Csnp.SharedKernel.Domain.Events;

namespace Csnp.Credential.Application.Dispatcher;

/// <summary>
/// Converts domain events to integration events and publishes them to the event bus.
/// </summary>
public sealed class DomainToIntegrationDispatcher : IDomainToIntegrationDispatcher
{
    #region -- Implements --

    /// <inheritdoc />
    public async Task DispatchAsync(IEnumerable<IDomainEvent> domainEvents, CancellationToken cancellationToken = default)
    {
        foreach (IDomainEvent domainEvent in domainEvents)
        {
            if (domainEvent is UserSignedUpDomainEvent userSignedUpDomainEvent)
            {
                Domain.Entities.User user = userSignedUpDomainEvent.User;

                var integrationEvent = new UserSignedUpIntegrationEvent(
                    user.Id,
                    user.Email.Value
                );

                await _publisher.PublishAsync(integrationEvent);
            }
        }
    }

    #endregion

    #region -- Methods --

    /// <summary>
    /// Initializes a new instance of the <see cref="DomainToIntegrationDispatcher"/> class.
    /// </summary>
    /// <param name="publisher">The integration event publisher used to publish events.</param>
    public DomainToIntegrationDispatcher(IIntegrationEventPublisher publisher)
    {
        _publisher = publisher;
    }

    #endregion

    #region -- Fields --

    /// <summary>
    /// Publishes integration events to the message bus (e.g., RabbitMQ).
    /// </summary>
    private readonly IIntegrationEventPublisher _publisher;

    #endregion
}
