using Csnp.Credential.Application.Events;
using Csnp.SeedWork.Domain.Events;

namespace Csnp.Credential.Infrastructure.Events;

public class NoOpDomainEventDispatcher : IDomainEventDispatcher
{
    public Task DispatchAsync(IEnumerable<IDomainEvent> domainEvents, CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }
}
