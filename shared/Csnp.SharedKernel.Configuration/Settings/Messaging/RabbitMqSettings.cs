using Csnp.SharedKernel.Configuration.Settings.Common;

namespace Csnp.SharedKernel.Configuration.Settings.Messaging;

/// <summary>
/// Represents the settings required to connect to a RabbitMQ message broker.
/// Inherits basic network and credential settings.
/// </summary>
public class RabbitMqSettings : ConnectionCredential
{
    /// <summary>
    /// The name of the virtual host on the RabbitMQ server.
    /// </summary>
    public string VirtualHost { get; set; } = "/";
}
