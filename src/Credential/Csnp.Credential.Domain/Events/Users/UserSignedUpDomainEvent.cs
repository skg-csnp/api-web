using Csnp.Credential.Domain.Entities;
using Csnp.SharedKernel.Domain.Events;

namespace Csnp.Credential.Domain.Events.Users;

/// <summary>
/// Represents a domain event that occurs when a user successfully signs up.
/// </summary>
public sealed class UserSignedUpDomainEvent : IDomainEvent
{
    #region -- Implements --

    /// <summary>
    /// Gets the user who signed up.
    /// </summary>
    public User User { get; }

    /// <summary>
    /// Gets the timestamp when the sign-up occurred.
    /// </summary>
    public DateTime OccurredOn { get; } = DateTime.UtcNow;

    #endregion

    #region -- Methods --

    /// <summary>
    /// Initializes a new instance of the <see cref="UserSignedUpDomainEvent"/> class.
    /// </summary>
    /// <param name="user">The signed-up user entity.</param>
    public UserSignedUpDomainEvent(User user)
    {
        User = user;
    }

    #endregion
}
