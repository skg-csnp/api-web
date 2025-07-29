using Csnp.SeedWork.Domain.Events;

namespace Csnp.Credential.Domain.Events.Users;

public record UserCreatedDomainEvent(long UserId, string Email) : IDomainEvent
{
    public DateTime OccurredOn { get; } = DateTime.UtcNow;
}
