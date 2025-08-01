using System.Net;
using System.Net.Mail;
using Csnp.Notification.Application.Abstractions.Services;
using Csnp.Notification.Application.Commands.EmailLogs.CreateEmailLog;
using Csnp.SharedKernel.Configuration.Settings.Email;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Csnp.Notification.Infrastructure.Services;

/// <summary>
/// Provides functionality to send emails using SMTP and renders templates from a storage backend.
/// </summary>
public class EmailService : IEmailService
{
    #region -- Implements --

    /// <inheritdoc />
    public async Task SendEmailAsync(string templateName, string toEmail, object model)
    {
        if (string.IsNullOrWhiteSpace(_settings.SenderEmail))
        {
            throw new InvalidOperationException("Email sender address (SenderEmail) is not configured.");
        }

        if (string.IsNullOrWhiteSpace(_settings.SenderName))
        {
            throw new InvalidOperationException("Email sender name (SenderName) is not configured.");
        }

        string htmlBody = await _renderer.RenderAsync(templateName, model);

        bool isSuccess = false;
        string? errorMessage = null;

        try
        {
            MailMessage message = new MailMessage(_settings.SenderEmail, toEmail)
            {
                Subject = _settings.SenderName,
                Body = htmlBody,
                IsBodyHtml = true
            };

            SmtpClient smtpClient = new SmtpClient(_settings.Host)
            {
                Port = _settings.Port,
                Credentials = new NetworkCredential(_settings.Username, _settings.Password),
                EnableSsl = _settings.EnableSsl
            };

            using (message)
            using (smtpClient)
            {
                await smtpClient.SendMailAsync(message);
            }

            isSuccess = true;
            _logger.LogInformation("Email sent to {Email} with template {Template}", toEmail, templateName);
        }
        catch (Exception ex)
        {
            errorMessage = ex.Message;
            _logger.LogError(ex, "Failed to send email to {Email} with template {Template}", toEmail, templateName);
        }

        await _mediator.Send(new CreateEmailLogCommand
        {
            To = toEmail,
            Subject = _settings.SenderName,
            Body = htmlBody,
            IsSuccess = isSuccess,
            ErrorMessage = errorMessage
        });
    }

    #endregion

    #region -- Methods --

    /// <summary>
    /// Initializes a new instance of the <see cref="EmailService"/> class.
    /// </summary>
    /// <param name="settings">The email configuration settings.</param>
    /// <param name="renderer">The email template renderer.</param>
    /// <param name="logger">The logger used to log information.</param>
    /// <param name="mediator">The mediator used to dispatch application commands.</param>
    public EmailService(
        IOptions<EmailSettings> settings,
        IEmailTemplateRenderer renderer,
        ILogger<EmailService> logger,
        IMediator mediator)
    {
        _settings = settings.Value;
        _renderer = renderer;
        _logger = logger;
        _mediator = mediator;
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

    /// <summary>
    /// The mediator for sending commands to the application layer.
    /// </summary>
    private readonly IMediator _mediator;

    #endregion
}
