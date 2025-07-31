using Csnp.Notification.Application.Abstractions.Services;

namespace Csnp.Notification.Infrastructure.Services;

/// <summary>
/// Renders email templates using content loaded from storage.
/// </summary>
public class EmailTemplateRenderer : IEmailTemplateRenderer
{
    private readonly IMinioTemplateLoader _loader;

    public EmailTemplateRenderer(IMinioTemplateLoader loader)
    {
        _loader = loader;
    }

    public async Task<string> RenderAsync(string templateName, object model)
    {
        string templateContent = await _loader.LoadTemplateAsync(templateName);

        // TODO: Replace with a real template engine (e.g., RazorLight, Fluid, Scriban)
        string email = model.GetType().GetProperty("Email")?.GetValue(model)?.ToString() ?? "";
        string rendered = templateContent.Replace("{{Email}}", email, StringComparison.OrdinalIgnoreCase);

        return rendered;
    }
}
