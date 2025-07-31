using Csnp.EventBus.Abstractions;
using Csnp.Notification.Application.Abstractions.Services;
using Csnp.Notification.Application.Events;
using Microsoft.Extensions.Logging;

namespace Csnp.Notification.Application.Handlers;

/// <summary>
/// Handles the <see cref="UserSignedUpIntegrationEvent"/> by sending a welcome email to the newly registered user.
/// </summary>
public class UserSignedUpIntegrationHandler : IIntegrationHandler<UserSignedUpIntegrationEvent>
{
    #region -- Implements --

    /// <summary>
    /// Handles the integration event by sending a welcome email to the user.
    /// </summary>
    /// <param name="integrationEvent">The integration event containing user signup details.</param>
    /// <param name="cancellationToken">A cancellation token for the async operation.</param>
    public async Task HandleAsync(UserSignedUpIntegrationEvent integrationEvent, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("[Notification] Send welcome email to: {Email} (UserId: {UserId})", integrationEvent.Email, integrationEvent.UserId);
        await _emailService.SendEmailAsync("signup-welcome.html", integrationEvent.Email, integrationEvent);
    }

    #endregion

    #region -- Methods --

    /// <summary>
    /// Initializes a new instance of the <see cref="UserSignedUpIntegrationHandler"/> class.
    /// </summary>
    /// <param name="logger">The logger instance for diagnostic messages.</param>
    /// <param name="emailService">The email service responsible for sending emails.</param>
    public UserSignedUpIntegrationHandler(ILogger<UserSignedUpIntegrationHandler> logger, IEmailService emailService)
    {
        _logger = logger;
        _emailService = emailService;
    }

    #endregion

    #region -- Fields --

    /// <summary>
    /// Provides logging capabilities for this handler.
    /// </summary>
    private readonly ILogger<UserSignedUpIntegrationHandler> _logger;

    /// <summary>
    /// Service responsible for sending emails.
    /// </summary>
    private readonly IEmailService _emailService;

    #endregion
}
