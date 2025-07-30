namespace Csnp.SeedWork.Domain.Events;

/// <summary>
/// Represents an entity that can produce domain events.
/// </summary>
public interface IEntityWithEvents
{
    /// <summary>
    /// Gets the collection of domain events raised by the entity.
    /// </summary>
    IReadOnlyCollection<IDomainEvent> DomainEvents { get; }

    /// <summary>
    /// Clears all domain events from the entity.
    /// </summary>
    void ClearDomainEvents();
}
