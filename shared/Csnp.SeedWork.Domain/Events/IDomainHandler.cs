namespace Csnp.SeedWork.Domain.Events;

public interface IDomainHandler<in TEvent> where TEvent : IDomainEvent
{
    Task HandleAsync(TEvent domainEvent, CancellationToken cancellationToken = default);
}
