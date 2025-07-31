namespace Csnp.SharedKernel.Domain.Events;

/// <summary>
/// Marker interface for domain events.
/// Used to represent a business event that has occurred within the domain.
/// </summary>
public interface IDomainEvent
{
    /// <summary>
    /// Gets the date and time when the event occurred, in UTC.
    /// </summary>
    DateTime OccurredOn { get; }
}
