namespace Csnp.SharedKernel.Configuration.Settings.Common;

/// <summary>
/// Represents the basic credentials and network configuration used to connect to a remote service or database.
/// Includes common properties such as host, port, username, and password.
/// This class can be extended by other settings classes that require authentication.
/// </summary>
public class ConnectionCredential
{
    /// <summary>
    /// The host address of the remote service or server.
    /// </summary>
    public string Host { get; set; } = string.Empty;

    /// <summary>
    /// The network port used to connect to the host.
    /// </summary>
    public int Port { get; set; }

    /// <summary>
    /// The username used for authentication.
    /// </summary>
    public string Username { get; set; } = string.Empty;

    /// <summary>
    /// The password used for authentication.
    /// </summary>
    public string Password { get; set; } = string.Empty;
}
