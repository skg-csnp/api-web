using Csnp.Credential.Domain.Events.Users;
using Csnp.SharedKernel.Domain.Events;
using Microsoft.Extensions.Logging;

namespace Csnp.Credential.Application.Events.Users;

/// <summary>
/// Handles the <see cref="UserSignedInDomainEvent"/> when a user successfully signs in.
/// </summary>
public class UserSignedInHandler : IDomainHandler<UserSignedInDomainEvent>
{
    #region -- Fields --

    private readonly ILogger<UserSignedInHandler> _logger;

    #endregion

    #region -- Implements --

    /// <summary>
    /// Handles the user signed-in domain event asynchronously.
    /// </summary>
    /// <param name="domainEvent">The domain event that occurred.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>A completed task.</returns>
    public Task HandleAsync(UserSignedInDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"User {domainEvent.UserId} signed in at {domainEvent.OccurredOn}.");
        return Task.CompletedTask;
    }

    #endregion

    #region -- Methods --

    /// <summary>
    /// Initializes a new instance of the <see cref="UserSignedInHandler"/> class.
    /// </summary>
    /// <param name="logger">The logger instance for this handler.</param>
    public UserSignedInHandler(ILogger<UserSignedInHandler> logger)
    {
        _logger = logger;
    }

    #endregion
}
