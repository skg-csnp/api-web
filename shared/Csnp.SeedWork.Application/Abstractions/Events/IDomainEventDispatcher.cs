using Csnp.SeedWork.Domain.Events;

namespace Csnp.SeedWork.Application.Abstractions.Events;

public interface IDomainEventDispatcher
{
    Task DispatchAsync(IEnumerable<IDomainEvent> domainEvents, CancellationToken cancellationToken = default);
}
