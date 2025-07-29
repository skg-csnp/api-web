using Csnp.Credential.Domain.Events.Users;
using Csnp.SeedWork.Domain.Events;

namespace Csnp.Credential.Application.Events.Users;

public class UserCreatedHandler : IDomainHandler<UserCreatedDomainEvent>
{
    public Task HandleAsync(UserCreatedDomainEvent domainEvent, CancellationToken cancellationToken = default)
    {
        Console.WriteLine($"Sending welcome email to {domainEvent.Email}");
        return Task.CompletedTask;
    }
}
