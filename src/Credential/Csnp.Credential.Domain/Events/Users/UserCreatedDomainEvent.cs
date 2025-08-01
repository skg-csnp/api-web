using Csnp.SharedKernel.Domain.Events;

namespace Csnp.Credential.Domain.Events.Users;

/// <summary>
/// Represents a domain event that occurs when a user is created.
/// </summary>
/// <param name="UserId">The ID of the created user.</param>
/// <param name="Email">The email of the created user.</param>
public sealed record UserCreatedDomainEvent(long UserId, string Email) : IDomainEvent
{
    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime OccurredOn { get; } = DateTime.UtcNow;
}
