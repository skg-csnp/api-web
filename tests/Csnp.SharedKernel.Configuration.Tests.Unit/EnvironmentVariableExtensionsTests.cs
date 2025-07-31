using Csnp.SharedKernel.Configuration.Settings.Email;
using Csnp.SharedKernel.Configuration.Settings.Messaging;
using Csnp.SharedKernel.Configuration.Settings.Persistence;
using Csnp.SharedKernel.Configuration.Settings.Storage;

namespace Csnp.SharedKernel.Configuration.Tests.Unit;

public class EnvironmentVariableExtensionsTests
{
    #region -- Email --

    [Fact]
    public void ConvertEnvironmentVariable_EmailSettings_ShouldMapCorrectly()
    {
        // Arrange
        Environment.SetEnvironmentVariable("LOC_NOTIFICATION_EMAIL__USERNAME", "admin@csnp.vn");
        Environment.SetEnvironmentVariable("LOC_NOTIFICATION_EMAIL__PASSWORD", "abc@123");
        Environment.SetEnvironmentVariable("LOC_NOTIFICATION_EMAIL__PORT", "587");
        Environment.SetEnvironmentVariable("LOC_NOTIFICATION_EMAIL__ENABLESSL", "true");

        // Act
        EmailSettings result = "Email".ConvertEnvironmentVariable<EmailSettings>("LOC_NOTIFICATION");

        // Assert
        Assert.Equal("admin@csnp.vn", result.Username);
        Assert.Equal("abc@123", result.Password);
        Assert.Equal(587, result.Port);
        Assert.True(result.EnableSsl);
    }

    #endregion

    #region -- Database --

    [Fact]
    public void ConvertEnvironmentVariable_DatabaseSettings_ShouldMapCorrectly()
    {
        // Arrange
        Environment.SetEnvironmentVariable("LOC_NOTIFICATION_DATABASE__HOST", "localhost");
        Environment.SetEnvironmentVariable("LOC_NOTIFICATION_DATABASE__DATABASE", "local_csnp_notification");
        Environment.SetEnvironmentVariable("LOC_NOTIFICATION_DATABASE__USERNAME", "local");
        Environment.SetEnvironmentVariable("LOC_NOTIFICATION_DATABASE__PASSWORD", "");
        Environment.SetEnvironmentVariable("LOC_NOTIFICATION_DATABASE__TRUSTSERVERCERTIFICATE", "true");

        // Act
        SqlServerSettings result = "Database".ConvertEnvironmentVariable<SqlServerSettings>("LOC_NOTIFICATION");

        // Assert
        Assert.Equal("localhost", result.Host);
        Assert.Equal("local_csnp_notification", result.Database);
        Assert.Equal("local", result.Username);
        Assert.Equal(string.Empty, result.Password);
        Assert.True(result.TrustServerCertificate);
    }

    #endregion

    #region -- RabbitMQ --

    [Fact]
    public void ConvertEnvironmentVariable_RabbitMqSettings_ShouldMapCorrectly()
    {
        // Arrange
        Environment.SetEnvironmentVariable("LOC_NOTIFICATION_RABBITMQ__HOST", "localhost");
        Environment.SetEnvironmentVariable("LOC_NOTIFICATION_RABBITMQ__PORT", "5672");
        Environment.SetEnvironmentVariable("LOC_NOTIFICATION_RABBITMQ__USERNAME", "guest");
        Environment.SetEnvironmentVariable("LOC_NOTIFICATION_RABBITMQ__PASSWORD", "guest");
        Environment.SetEnvironmentVariable("LOC_NOTIFICATION_RABBITMQ__VIRTUALHOST", "/");

        // Act
        RabbitMqSettings result = "RabbitMQ".ConvertEnvironmentVariable<RabbitMqSettings>("LOC_NOTIFICATION");

        // Assert
        Assert.Equal("localhost", result.Host);
        Assert.Equal(5672, result.Port);
        Assert.Equal("guest", result.Username);
        Assert.Equal("guest", result.Password);
        Assert.Equal("/", result.VirtualHost);
    }

    #endregion

    #region -- MinIO --

    [Fact]
    public void ConvertEnvironmentVariable_MinioSettings_ShouldMapCorrectly()
    {
        // Arrange
        Environment.SetEnvironmentVariable("LOC_NOTIFICATION_MINIO__ENDPOINT", "localhost:9000");
        Environment.SetEnvironmentVariable("LOC_NOTIFICATION_MINIO__ACCESSKEY", "minioadmin");
        Environment.SetEnvironmentVariable("LOC_NOTIFICATION_MINIO__SECRETKEY", "minioadmin");
        Environment.SetEnvironmentVariable("LOC_NOTIFICATION_MINIO__SECURE", "false");
        Environment.SetEnvironmentVariable("LOC_NOTIFICATION_MINIO__BUCKET", "email-templates");

        // Act
        MinioSettings result = "MinIO".ConvertEnvironmentVariable<MinioSettings>("LOC_NOTIFICATION");

        // Assert
        Assert.Equal("localhost:9000", result.Endpoint);
        Assert.Equal("minioadmin", result.AccessKey);
        Assert.Equal("minioadmin", result.SecretKey);
        Assert.False(result.Secure);
        Assert.Equal("email-templates", result.Bucket);
    }

    #endregion

    #region -- Missing Config --

    [Fact]
    public void ConvertEnvironmentVariable_UnknownPrefix_ShouldReturnDefaultObject()
    {
        // Act
        EmailSettings result = "INVALID".ConvertEnvironmentVariable<EmailSettings>("LOC");

        // Assert
        Assert.Equal(string.Empty, result.Host);
        Assert.Equal(0, result.Port);
        Assert.Null(result.SenderEmail);
        Assert.Null(result.SenderName);
        Assert.Equal(string.Empty, result.Username);
        Assert.Equal(string.Empty, result.Password);
        Assert.True(result.EnableSsl);
    }

    #endregion
}
