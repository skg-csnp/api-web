namespace Csnp.SeedWork.Domain.Events;

/// <summary>
/// Represents a base class for all domain events in the system.
/// </summary>
public abstract class DomainEvent : IDomainEvent
{
    #region -- Implements --

    /// <inheritdoc/>
    public DateTime OccurredOn { get; } = DateTime.UtcNow;

    #endregion
}
