using Csnp.EventBus.Abstractions;
using Csnp.Notification.Application.Events;

namespace Csnp.Notification.Infrastructure.Messaging;

/// <summary>
/// Metadata for the UserSignedUpIntegrationEvent.
/// </summary>
public sealed class UserSignedUpMetadata : IIntegrationEventMetadata<UserSignedUpIntegrationEvent>
{
    /// <inheritdoc />
    public string QueueName => "user-signed-up";

    /// <inheritdoc />
    public string ExchangeName => "user.signup";
}
