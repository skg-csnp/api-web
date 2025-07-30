using Csnp.SeedWork.Application.Messaging;

namespace Csnp.Credential.Application.Events.Users;

public class UserSignedUpIntegrationEvent : IntegrationEvent
{
    public long UserId { get; set; }
    public string Email { get; set; }

    public UserSignedUpIntegrationEvent(long userId, string email)
    {
        UserId = userId;
        Email = email;
    }
}
