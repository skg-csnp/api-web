using Csnp.SharedKernel.Configuration.Settings.Common;

namespace Csnp.SharedKernel.Configuration.Settings.Email;

/// <summary>
/// Represents the settings required to connect to an SMTP email server.
/// Inherits basic credential and network configuration.
/// </summary>
public class EmailSettings : ConnectionCredential
{
    /// <summary>
    /// Indicates whether SSL should be used when connecting to the SMTP server.
    /// </summary>
    public bool EnableSsl { get; set; } = true;

    /// <summary>
    /// The display name of the sender when sending emails.
    /// </summary>
    public string SenderName { get; set; } = string.Empty;

    /// <summary>
    /// The email address of the sender.
    /// </summary>
    public string SenderEmail { get; set; } = string.Empty;
}
