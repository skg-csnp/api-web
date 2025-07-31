namespace Csnp.Notification.Application.Abstractions.Services;

/// <summary>
/// Provides functionality to load email templates from MinIO or similar storage.
/// </summary>
public interface IMinioTemplateLoader
{
    /// <summary>
    /// Loads a template by its name.
    /// </summary>
    /// <param name="templateName">The name of the template (e.g., "forgot-password.html").</param>
    /// <returns>The template content as string.</returns>
    Task<string> LoadTemplateAsync(string templateName);
}
