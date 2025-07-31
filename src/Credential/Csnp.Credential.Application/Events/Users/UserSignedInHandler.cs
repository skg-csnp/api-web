using Csnp.Credential.Domain.Events.Users;
using Csnp.SharedKernel.Domain.Events;
using Microsoft.Extensions.Logging;

namespace Csnp.Credential.Application.Events.Users;

public class UserSignedInHandler : IDomainHandler<UserSignedInDomainEvent>
{
    private readonly ILogger<UserSignedInHandler> _logger;

    public UserSignedInHandler(ILogger<UserSignedInHandler> logger)
    {
        _logger = logger;
    }

    public Task HandleAsync(UserSignedInDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"User {domainEvent.UserId} signed in at {domainEvent.OccurredOn}.");
        return Task.CompletedTask;
    }
}
