namespace Csnp.SharedKernel.Domain.Events;

/// <summary>
/// Defines a contract for handling domain events.
/// </summary>
/// <typeparam name="TEvent">The type of domain event to handle.</typeparam>
public interface IDomainHandler<in TEvent> where TEvent : IDomainEvent
{
    /// <summary>
    /// Handles the specified domain event.
    /// </summary>
    /// <param name="domainEvent">The domain event to handle.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    Task HandleAsync(TEvent domainEvent, CancellationToken cancellationToken = default);
}
