namespace Csnp.EventBus.Events;

/// <summary>
/// Represents a base class for integration events shared across bounded contexts.
/// </summary>
public abstract class IntegrationEvent
{
    #region -- Properties --

    /// <summary>
    /// Gets the unique identifier for the event.
    /// </summary>
    public Guid Id { get; init; } = Guid.NewGuid();

    /// <summary>
    /// Gets the timestamp when the event was created (UTC).
    /// </summary>
    public DateTime OccurredOn { get; init; } = DateTime.UtcNow;

    #endregion
}
