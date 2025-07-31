namespace Csnp.Notification.Application.Abstractions.Services;

/// <summary>
/// Provides functionality to render email templates using a data model.
/// </summary>
public interface IEmailTemplateRenderer
{
    /// <summary>
    /// Renders an email template with the provided model.
    /// </summary>
    /// <param name="templateName">The name of the email template.</param>
    /// <param name="model">The dynamic data model.</param>
    /// <returns>The rendered HTML string.</returns>
    Task<string> RenderAsync(string templateName, object model);
}
