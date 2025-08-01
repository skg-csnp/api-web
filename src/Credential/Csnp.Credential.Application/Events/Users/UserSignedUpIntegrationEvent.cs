using Csnp.EventBus.Events;

namespace Csnp.Credential.Application.Events.Users;

/// <summary>
/// Represents an integration event that is published when a user has signed up.
/// </summary>
public class UserSignedUpIntegrationEvent : IntegrationEvent
{
    #region -- Methods --

    /// <summary>
    /// Initializes a new instance of the <see cref="UserSignedUpIntegrationEvent"/> class.
    /// </summary>
    /// <param name="userId">The ID of the user.</param>
    /// <param name="email">The email address of the user.</param>
    public UserSignedUpIntegrationEvent(long userId, string email)
    {
        UserId = userId;
        Email = email;
    }

    #endregion

    #region -- Properties --

    /// <summary>
    /// Gets the ID of the user who signed up.
    /// </summary>
    public long UserId { get; }

    /// <summary>
    /// Gets the email of the user who signed up.
    /// </summary>
    public string Email { get; }

    #endregion
}
