using Csnp.SharedKernel.Domain.Events;

namespace Csnp.Credential.Domain.Events.Users;

/// <summary>
/// Represents a domain event that occurs when a user signs in.
/// </summary>
public sealed class UserSignedInDomainEvent : IDomainEvent
{
    #region -- Implements --

    /// <summary>
    /// Gets the ID of the user who signed in.
    /// </summary>
    public long UserId { get; }

    /// <summary>
    /// Gets the timestamp when the sign-in occurred.
    /// </summary>
    public DateTime OccurredOn { get; }

    #endregion

    #region -- Methods --

    /// <summary>
    /// Initializes a new instance of the <see cref="UserSignedInDomainEvent"/> class.
    /// </summary>
    /// <param name="userId">The ID of the signed-in user.</param>
    /// <param name="occurredOn">The timestamp of the sign-in event.</param>
    public UserSignedInDomainEvent(long userId, DateTime occurredOn)
    {
        UserId = userId;
        OccurredOn = occurredOn;
    }

    #endregion
}
