using Csnp.SeedWork.Domain.Events;

namespace Csnp.Credential.Domain.Events.Users;

public sealed class UserSignedInDomainEvent : IDomainEvent
{
    public long UserId { get; }
    public DateTime OccurredOn { get; }

    public UserSignedInDomainEvent(long userId, DateTime occurredOn)
    {
        UserId = userId;
        OccurredOn = occurredOn;
    }
}
