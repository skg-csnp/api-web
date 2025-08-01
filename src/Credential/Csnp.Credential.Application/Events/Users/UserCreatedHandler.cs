using Csnp.Credential.Domain.Events.Users;
using Csnp.SharedKernel.Domain.Events;

namespace Csnp.Credential.Application.Events.Users;

/// <summary>
/// Handles the <see cref="UserCreatedDomainEvent"/> by sending a welcome email.
/// </summary>
public sealed class UserCreatedHandler : IDomainHandler<UserCreatedDomainEvent>
{
    #region -- Implements --

    /// <summary>
    /// Handles the user created domain event asynchronously.
    /// </summary>
    /// <param name="domainEvent">The domain event that occurred.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public Task HandleAsync(UserCreatedDomainEvent domainEvent, CancellationToken cancellationToken = default)
    {
        Console.WriteLine($"Sending welcome email to {domainEvent.Email}");
        return Task.CompletedTask;
    }

    #endregion
}
