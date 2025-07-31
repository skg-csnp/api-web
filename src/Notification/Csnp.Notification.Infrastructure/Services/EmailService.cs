using System.Net;
using System.Net.Mail;
using Csnp.Notification.Application.Abstractions.Services;
using Csnp.SharedKernel.Configuration.Settings.Email;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Csnp.Notification.Infrastructure.Services;

/// <summary>
/// Sends emails using SMTP and renders templates from MinIO.
/// </summary>
public class EmailService : IEmailService
{
    private readonly EmailSettings _settings;
    private readonly IEmailTemplateRenderer _renderer;
    private readonly ILogger<EmailService> _logger;

    public EmailService(
        IOptions<EmailSettings> settings,
        IEmailTemplateRenderer renderer,
        ILogger<EmailService> logger)
    {
        _settings = settings.Value;
        _renderer = renderer;
        _logger = logger;
    }

    public async Task SendEmailAsync(string templateName, string toEmail, object model)
    {
        string htmlBody = await _renderer.RenderAsync(templateName, model);

        using var message = new MailMessage(_settings.SenderEmail, toEmail)
        {
            Subject = _settings.SenderName,
            Body = htmlBody,
            IsBodyHtml = true
        };

        using var smtpClient = new SmtpClient(_settings.Host)
        {
            Port = _settings.Port,
            Credentials = new NetworkCredential(_settings.Username, _settings.Password),
            EnableSsl = _settings.EnableSsl
        };

        await smtpClient.SendMailAsync(message);

        _logger.LogInformation("📧 Email sent to {Email} with template {Template}", toEmail, templateName);
    }
}
