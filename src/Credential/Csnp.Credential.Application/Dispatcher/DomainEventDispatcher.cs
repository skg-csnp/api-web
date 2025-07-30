using Csnp.Credential.Application.Events.Users;
using Csnp.Credential.Domain.Events.Users;
using Csnp.EventBus.Abstractions;
using Csnp.SeedWork.Application.Abstractions.Events;
using Csnp.SeedWork.Domain.Events;

namespace Csnp.Credential.Application.Dispatcher;

public class DomainEventDispatcher : IDomainEventDispatcher
{
    private readonly IIntegrationEventPublisher _publisher;

    public DomainEventDispatcher(IIntegrationEventPublisher publisher)
    {
        _publisher = publisher;
    }

    public async Task DispatchAsync(IEnumerable<IDomainEvent> domainEvents, CancellationToken cancellationToken = default)
    {
        foreach (IDomainEvent domainEvent in domainEvents)
        {
            if (domainEvent is UserSignedUpDomainEvent userSignedUpDomainEvent)
            {
                Domain.Entities.User user = userSignedUpDomainEvent.User;
                var integrationEvent = new UserSignedUpIntegrationEvent(user.Id, user.Email.Value);
                await _publisher.PublishAsync(integrationEvent);
            }
        }
    }
}
