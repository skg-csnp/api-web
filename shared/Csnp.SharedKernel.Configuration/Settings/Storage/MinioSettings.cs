namespace Csnp.SharedKernel.Configuration.Settings.Storage;

/// <summary>
/// Represents the configuration settings required to connect and interact with a MinIO object storage service.
/// </summary>
public class MinioSettings
{
    /// <summary>
    /// The endpoint URL or IP address of the MinIO server.
    /// </summary>
    public string Endpoint { get; set; } = string.Empty;

    /// <summary>
    /// The access key used for authentication with the MinIO server.
    /// </summary>
    public string AccessKey { get; set; } = string.Empty;

    /// <summary>
    /// The secret key used for authentication with the MinIO server.
    /// </summary>
    public string SecretKey { get; set; } = string.Empty;

    /// <summary>
    /// Indicates whether to use HTTPS (secure connection) when connecting to MinIO.
    /// </summary>
    public bool Secure { get; set; }

    /// <summary>
    /// The name of the default bucket to use in MinIO.
    /// </summary>
    public string Bucket { get; set; } = string.Empty;
}
