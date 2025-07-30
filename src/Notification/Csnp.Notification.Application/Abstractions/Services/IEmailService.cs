namespace Csnp.Notification.Application.Abstractions.Services;

public interface IEmailService
{
    Task SendWelcomeEmail(string email);
}
