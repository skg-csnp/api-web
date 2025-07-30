using Csnp.SeedWork.Application.Messaging;

namespace Csnp.Notification.Application.Events;

public class UserSignedUpIntegrationEvent : IntegrationEvent
{
    public long UserId { get; set; }
    public string Email { get; set; } = default!;
}
