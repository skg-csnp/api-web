namespace Csnp.Notification.Application.Abstractions.Services;

/// <summary>
/// Defines a service for sending templated emails.
/// </summary>
public interface IEmailService
{
    /// <summary>
    /// Sends an email using a template and dynamic content model.
    /// </summary>
    /// <param name="templateName">Template key or file name without extension.</param>
    /// <param name="toEmail">Recipient email address.</param>
    /// <param name="model">Data model used to render the template.</param>
    Task SendEmailAsync(string templateName, string toEmail, object model);
}
