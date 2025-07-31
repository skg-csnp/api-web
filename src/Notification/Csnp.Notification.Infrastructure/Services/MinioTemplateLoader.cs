using Csnp.Notification.Application.Abstractions.Services;
using Csnp.SharedKernel.Configuration.Settings.Storage;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Minio;
using Minio.DataModel.Args;

namespace Csnp.Notification.Infrastructure.Services;

/// <summary>
/// Loads email templates stored in MinIO.
/// </summary>
public class MinioTemplateLoader : IMinioTemplateLoader
{
    private readonly IMinioClient _minioClient;
    private readonly ILogger<MinioTemplateLoader> _logger;
    private readonly MinioSettings _settings;

    public MinioTemplateLoader(
        IMinioClient minioClient,
        IOptions<MinioSettings> options,
        ILogger<MinioTemplateLoader> logger)
    {
        _minioClient = minioClient;
        _settings = options.Value;
        _logger = logger;
    }

    /// <inheritdoc />
    public async Task<string> LoadTemplateAsync(string templateName)
    {
        try
        {
            using var ms = new MemoryStream();
            await _minioClient.GetObjectAsync(new GetObjectArgs()
                .WithBucket(_settings.Bucket)
                .WithObject(templateName)
                .WithCallbackStream(stream => stream.CopyTo(ms)));

            ms.Seek(0, SeekOrigin.Begin);
            using var reader = new StreamReader(ms);
            return await reader.ReadToEndAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to load email template: {TemplateName}", templateName);
            throw;
        }
    }
}
