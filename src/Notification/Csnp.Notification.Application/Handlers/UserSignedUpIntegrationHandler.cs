using Csnp.Notification.Application.Abstractions.Services;
using Csnp.Notification.Application.Events;
using Csnp.SeedWork.Domain.Events;
using Microsoft.Extensions.Logging;

namespace Csnp.Notification.Application.Handlers;

public class UserSignedUpIntegrationHandler : IIntegrationHandler<UserSignedUpIntegrationEvent>
{
    private readonly ILogger<UserSignedUpIntegrationHandler> _logger;
    private readonly IEmailService _emailService;

    public UserSignedUpIntegrationHandler(ILogger<UserSignedUpIntegrationHandler> logger, IEmailService emailService)
    {
        _logger = logger;
        _emailService = emailService;
    }

    public Task HandleAsync(UserSignedUpIntegrationEvent integrationEvent, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("[Notification] Send welcome email to: {Email} (UserId: {UserId})", integrationEvent.Email, integrationEvent.UserId);
        return _emailService.SendWelcomeEmail(integrationEvent.Email);
    }
}
