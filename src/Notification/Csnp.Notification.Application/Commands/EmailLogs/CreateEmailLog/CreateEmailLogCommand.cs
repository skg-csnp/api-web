using MediatR;

namespace Csnp.Notification.Application.Commands.EmailLogs.CreateEmailLog;

/// <summary>
/// Command to create a new email log.
/// </summary>
public sealed class CreateEmailLogCommand : IRequest<long>
{
    #region -- Properties --

    /// <summary>
    /// Gets or sets the recipient email address.
    /// </summary>
    public required string To { get; init; }

    /// <summary>
    /// Gets or sets the subject of the email.
    /// </summary>
    public required string Subject { get; init; }

    /// <summary>
    /// Gets or sets the body of the email.
    /// </summary>
    public required string Body { get; init; }

    /// <summary>
    /// Gets or sets a value indicating whether the email was sent successfully.
    /// </summary>
    public required bool IsSuccess { get; init; }

    /// <summary>
    /// Gets or sets the error message if the email sending failed.
    /// </summary>
    public string? ErrorMessage { get; init; }

    #endregion
}
