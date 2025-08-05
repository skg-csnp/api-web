using Csnp.SharedKernel.Domain;

namespace Csnp.Notification.Domain.Entities;

/// <summary>
/// Represents a log entry for an email that was attempted to be sent.
/// </summary>
public class EmailLog : EntityRoot<long>, IAggregateRoot
{
    #region -- Methods --

    /// <summary>
    /// Initializes a new instance of the <see cref="EmailLog"/> class.
    /// Required by Entity Framework.
    /// </summary>
    protected EmailLog()
    {
    }

    /// <summary>
    /// Marks the email log as failed with the specified error message.
    /// </summary>
    /// <param name="error">The error message describing the failure.</param>
    public void MarkAsFailed(string error)
    {
        IsSuccess = false;
        ErrorMessage = error;
    }

    /// <summary>
    /// Sets the ID for the user entity if not already set.
    /// </summary>
    /// <param name="id">The ID to assign.</param>
    /// <exception cref="InvalidOperationException">Thrown if the ID is already set.</exception>
    public void SetId(long id)
    {
        if (Id != default)
        {
            throw new InvalidOperationException("Id is already set");
        }

        Id = id;
    }

    /// <summary>
    /// Creates a new email log entry.
    /// </summary>
    /// <param name="to">The recipient email address.</param>
    /// <param name="subject">The subject of the email.</param>
    /// <param name="body">The body content of the email.</param>
    /// <param name="isSuccess">Indicates whether the email was sent successfully.</param>
    /// <param name="errorMessage">Optional error message if the email failed to send.</param>
    /// <returns>A new instance of <see cref="EmailLog"/>.</returns>
    public static EmailLog Create(string to, string subject, string body, bool isSuccess, string? errorMessage = null)
    {
        return new EmailLog
        {
            To = to,
            Subject = subject,
            Body = body,
            SentAt = DateTime.UtcNow,
            IsSuccess = isSuccess,
            ErrorMessage = errorMessage
        };
    }

    #endregion

    #region -- Properties --

    /// <summary>
    /// Gets the recipient email address.
    /// </summary>
    public string To { get; private set; } = default!;

    /// <summary>
    /// Gets the subject of the email.
    /// </summary>
    public string Subject { get; private set; } = default!;

    /// <summary>
    /// Gets the body content of the email.
    /// </summary>
    public string Body { get; private set; } = default!;

    /// <summary>
    /// Gets the timestamp when the email was sent.
    /// </summary>
    public DateTime SentAt { get; private set; }

    /// <summary>
    /// Gets a value indicating whether the email was sent successfully.
    /// </summary>
    public bool IsSuccess { get; private set; }

    /// <summary>
    /// Gets the error message if the email failed to send.
    /// </summary>
    public string? ErrorMessage { get; private set; }

    /// <summary>
    /// Gets the correlation ID used to trace the request.
    /// </summary>
    public string CorrelationId { get; private set; } = Guid.NewGuid().ToString();

    #endregion
}
