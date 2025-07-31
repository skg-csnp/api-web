using System.Text;
using Csnp.Notification.Application.Abstractions.Services;
using Csnp.SharedKernel.Configuration.Settings.Storage;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Minio;
using Minio.DataModel.Args;

namespace Csnp.Notification.Infrastructure.Services;

/// <summary>
/// Loads email template files from a MinIO bucket.
/// </summary>
public class MinioTemplateLoader : IMinioTemplateLoader
{
    #region -- Implements --

    /// <summary>
    /// Loads the content of a template file from the configured MinIO bucket.
    /// </summary>
    /// <param name="templateName">The object key or path of the template inside the bucket.</param>
    /// <returns>The raw content of the template as a string.</returns>
    /// <exception cref="Exception">Throws any underlying MinIO or IO exceptions.</exception>
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
            using var reader = new StreamReader(ms, Encoding.UTF8);
            return await reader.ReadToEndAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to load email template from MinIO: {TemplateName}", templateName);
            throw;
        }
    }

    #endregion

    #region -- Constructor --

    /// <summary>
    /// Initializes a new instance of the <see cref="MinioTemplateLoader"/> class.
    /// </summary>
    /// <param name="minioClient">The MinIO client used to access the bucket.</param>
    /// <param name="options">The options that contain MinIO settings.</param>
    /// <param name="logger">The logger used for diagnostics.</param>
    public MinioTemplateLoader(
        IMinioClient minioClient,
        IOptions<MinioSettings> options,
        ILogger<MinioTemplateLoader> logger)
    {
        _minioClient = minioClient;
        _settings = options.Value;
        _logger = logger;
    }

    #endregion

    #region -- Fields --

    /// <summary>
    /// The MinIO client used to retrieve objects from the bucket.
    /// </summary>
    private readonly IMinioClient _minioClient;

    /// <summary>
    /// The logger used for logging load operations and failures.
    /// </summary>
    private readonly ILogger<MinioTemplateLoader> _logger;

    /// <summary>
    /// The settings that define MinIO connection parameters and bucket name.
    /// </summary>
    private readonly MinioSettings _settings;

    #endregion
}
