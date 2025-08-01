using Csnp.Notification.Application.Abstractions.Services;
using Microsoft.Extensions.Logging;
using Scriban;

namespace Csnp.Notification.Infrastructure.Services;

/// <summary>
/// Renders email templates using content loaded from a storage provider (e.g., MinIO).
/// Uses Scriban as the rendering engine.
/// </summary>
public class EmailTemplateRenderer : IEmailTemplateRenderer
{
    #region -- Implements --

    /// <summary>
    /// Loads and renders a template with the specified model.
    /// </summary>
    /// <param name="templateName">The name or path of the template to render.</param>
    /// <param name="model">The data model used to populate the template.</param>
    /// <returns>The rendered template as an HTML string.</returns>
    public async Task<string> RenderAsync(string templateName, object model)
    {
        try
        {
            string templateContent = await _loader.LoadTemplateAsync(templateName);

            var template = Template.Parse(templateContent);
            if (template.HasErrors)
            {
                _logger.LogWarning("Template parse errors for '{Template}': {Errors}", templateName, string.Join(", ", template.Messages));
                return templateContent;
            }

            return template.Render(model, memberRenamer: member => member.Name);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to render template '{TemplateName}'", templateName);
            return $"[Template '{templateName}' could not be rendered]";
        }
    }

    #endregion

    #region -- Methods --

    /// <summary>
    /// Initializes a new instance of the <see cref="EmailTemplateRenderer"/> class.
    /// </summary>
    /// <param name="loader">The template loader used to retrieve templates from storage.</param>
    /// <param name="logger">The logger used to log template parsing and rendering information.</param>
    public EmailTemplateRenderer(
        IMinioTemplateLoader loader,
        ILogger<EmailTemplateRenderer> logger)
    {
        _loader = loader;
        _logger = logger;
    }

    #endregion

    #region -- Fields --

    /// <summary>
    /// The service responsible for loading templates from storage (e.g., MinIO).
    /// </summary>
    private readonly IMinioTemplateLoader _loader;

    /// <summary>
    /// The logger used for logging render status and errors.
    /// </summary>
    private readonly ILogger<EmailTemplateRenderer> _logger;

    #endregion
}
