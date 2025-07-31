using System.Net;
using System.Net.Mail;
using Csnp.Notification.Application.Abstractions.Services;
using Csnp.SharedKernel.Configuration.Settings.Email;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Csnp.Notification.Infrastructure.Services;

/// <summary>
/// Provides functionality to send emails using SMTP and renders templates from a storage backend.
/// </summary>
public class EmailService : IEmailService
{
    #region -- Implements --

    /// <summary>
    /// Sends an HTML email using the specified template and data model.
    /// </summary>
    /// <param name="templateName">The name of the email template to render.</param>
    /// <param name="toEmail">The recipient's email address.</param>
    /// <param name="model">The data model used to render the email template.</param>
    /// <returns>A task representing the asynchronous send operation.</returns>
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

        _logger.LogInformation("Email sent to {Email} with template {Template}", toEmail, templateName);
    }

    #endregion

    #region -- Methods --

    /// <summary>
    /// Initializes a new instance of the <see cref="EmailService"/> class.
    /// </summary>
    /// <param name="settings">The email configuration settings.</param>
    /// <param name="renderer">The email template renderer.</param>
    /// <param name="logger">The logger used to log information.</param>
    public EmailService(
        IOptions<EmailSettings> settings,
        IEmailTemplateRenderer renderer,
        ILogger<EmailService> logger)
    {
        _settings = settings.Value;
        _renderer = renderer;
        _logger = logger;
    }

    #endregion

    #region -- Fields --

    /// <summary>
    /// The email configuration settings.
    /// </summary>
    private readonly EmailSettings _settings;

    /// <summary>
    /// The service used to render email templates.
    /// </summary>
    private readonly IEmailTemplateRenderer _renderer;

    /// <summary>
    /// The logger used for logging email operations.
    /// </summary>
    private readonly ILogger<EmailService> _logger;

    #endregion
}
