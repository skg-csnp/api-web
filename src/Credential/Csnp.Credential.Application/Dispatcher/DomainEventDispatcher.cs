using Csnp.Credential.Application.Events.Users;
using Csnp.Credential.Domain.Events.Users;
using Csnp.EventBus.Abstractions;
using Csnp.SharedKernel.Application.Abstractions.Events;
using Csnp.SharedKernel.Domain.Events;

namespace Csnp.Credential.Application.Dispatcher;

/// <summary>
/// Dispatches domain events and publishes corresponding integration events.
/// </summary>
public sealed class DomainEventDispatcher : IDomainEventDispatcher
{
    #region -- Implements --

    /// <summary>
    /// Dispatches domain events asynchronously and maps them to integration events when applicable.
    /// </summary>
    /// <param name="domainEvents">The collection of domain events to dispatch.</param>
    /// <param name="cancellationToken">A cancellation token for the async operation.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
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
    /// Initializes a new instance of the <see cref="DomainEventDispatcher"/> class.
    /// </summary>
    /// <param name="publisher">The integration event publisher used to publish events.</param>
    public DomainEventDispatcher(IIntegrationEventPublisher publisher)
    {
        _publisher = publisher;
    }

    #endregion

    #region -- Fields --

    private readonly IIntegrationEventPublisher _publisher;

    #endregion
}
