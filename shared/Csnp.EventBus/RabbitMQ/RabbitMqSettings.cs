namespace Csnp.EventBus.RabbitMQ;

/// <summary>
/// Represents configuration settings for connecting to a RabbitMQ server.
/// </summary>
public class RabbitMqSettings
{
    #region -- Properties --

    /// <summary>
    /// Gets or sets the hostname or IP address of the RabbitMQ server.
    /// Default is <c>localhost</c>.
    /// </summary>
    public string Host { get; set; } = "localhost";

    /// <summary>
    /// Gets or sets the port used to connect to RabbitMQ.
    /// Default is <c>5672</c>.
    /// </summary>
    public int Port { get; set; } = 5672;

    /// <summary>
    /// Gets or sets the username used for authentication.
    /// Default is <c>guest</c>.
    /// </summary>
    public string Username { get; set; } = "guest";

    /// <summary>
    /// Gets or sets the password used for authentication.
    /// Default is <c>guest</c>.
    /// </summary>
    public string Password { get; set; } = "guest";

    /// <summary>
    /// Gets or sets the virtual host on the RabbitMQ server.
    /// Default is <c>/</c>.
    /// </summary>
    public string VirtualHost { get; set; } = "/";

    #endregion
}
