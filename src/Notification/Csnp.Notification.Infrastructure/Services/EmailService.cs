using Csnp.Notification.Application.Abstractions.Services;
using Microsoft.Extensions.Logging;

namespace Csnp.Notification.Infrastructure.Services;

public class EmailService : IEmailService
{
    private readonly ILogger<EmailService> _logger;

    public EmailService(ILogger<EmailService> logger)
    {
        _logger = logger;
    }

    public Task SendWelcomeEmail(string email)
    {
        _logger.LogInformation("📧 Sending welcome email to {Email}", email);
        // TODO: integrate with actual mail service
        return Task.CompletedTask;
    }
}
