namespace Csnp.SeedWork.Domain.Events;

public interface IEntityWithEvents
{
    IReadOnlyCollection<IDomainEvent> DomainEvents { get; }
    void ClearDomainEvents();
}
