using Csnp.SeedWork.Application.Abstractions.Events;
using Csnp.SeedWork.Domain;

namespace Csnp.SeedWork.Application.Events;

public static class DomainEventHelper
{
    public static async Task DispatchAndClearAsync(
        Entity<long> entity,
        IDomainEventDispatcher dispatcher,
        CancellationToken cancellationToken = default)
    {
        await dispatcher.DispatchAsync(entity.DomainEvents, cancellationToken);
        entity.ClearDomainEvents();
    }
}
