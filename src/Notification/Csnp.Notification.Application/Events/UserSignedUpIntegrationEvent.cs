using Csnp.EventBus.Events;

namespace Csnp.Notification.Application.Events;

/// <summary>
/// Represents an integration event that occurs when a user has signed up.
/// </summary>
public class UserSignedUpIntegrationEvent : IntegrationEvent
{
    #region -- Properties --

    /// <summary>
    /// Gets or sets the unique identifier of the signed-up user.
    /// </summary>
    public long UserId { get; set; }

    /// <summary>
    /// Gets or sets the email address of the signed-up user.
    /// </summary>
    public string Email { get; set; } = default!;

    #endregion
}
