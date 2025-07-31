using Csnp.Credential.Domain.Entities;
using Csnp.SharedKernel.Domain.Events;

namespace Csnp.Credential.Domain.Events.Users;

public class UserSignedUpDomainEvent : IDomainEvent
{
    public User User { get; }

    public DateTime OccurredOn { get; } = DateTime.UtcNow;

    public UserSignedUpDomainEvent(User user)
    {
        User = user;
    }
}
