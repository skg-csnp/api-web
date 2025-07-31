using Csnp.SharedKernel.Domain.Events;

namespace Csnp.SharedKernel.Domain;

/// <summary>
/// Represents the base class for all entities in the domain model.
/// </summary>
/// <typeparam name="TId">The type of the unique identifier for the entity.</typeparam>
public abstract class Entity<TId> : IEntityWithEvents
{
    #region -- Overrides --

    /// <summary>
    /// Determines whether the specified object is equal to the current entity.
    /// </summary>
    /// <param name="obj">The object to compare with the current entity.</param>
    /// <returns><c>true</c> if the specified object is equal to the current entity; otherwise, <c>false</c>.</returns>
    public override bool Equals(object? obj)
    {
        if (obj is not Entity<TId> other)
        {
            return false;
        }

        return EqualityComparer<TId>.Default.Equals(Id, other.Id);
    }

    /// <summary>
    /// Returns a hash code for the current entity.
    /// </summary>
    /// <returns>A hash code for the current entity.</returns>
    public override int GetHashCode() => Id?.GetHashCode() ?? 0;

    #endregion

    #region -- Properties --

    /// <summary>
    /// Holds the list of domain events raised by the entity.
    /// </summary>
    private readonly List<IDomainEvent> _domainEvents = new();

    /// <summary>
    /// Gets the unique identifier of the entity.
    /// </summary>
    public TId Id { get; protected set; } = default!;

    /// <summary>
    /// Gets the domain events that have occurred for this entity.
    /// </summary>
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    #endregion

    #region -- Methods --

    /// <summary>
    /// Adds a domain event to the entity's list of domain events.
    /// </summary>
    /// <param name="event">The domain event to add.</param>
    protected void AddDomainEvent(IDomainEvent @event) => _domainEvents.Add(@event);

    /// <summary>
    /// Clears all domain events from the entity.
    /// </summary>
    public void ClearDomainEvents() => _domainEvents.Clear();

    #endregion
}
