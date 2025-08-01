using Csnp.SharedKernel.Domain;

namespace Csnp.Notification.Infrastructure.Persistence;

/// <summary>
/// Represents the EF Core entity for persisting email logs.
/// </summary>
public class EmailLogEntity : Entity<long>
{
    #region -- Methods --

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

    #endregion

    #region -- Properties --

    /// <summary>
    /// Gets or sets the recipient email address.
    /// </summary>
    public string To { get; set; } = default!;

    /// <summary>
    /// Gets or sets the subject of the email.
    /// </summary>
    public string Subject { get; set; } = default!;

    /// <summary>
    /// Gets or sets the body content of the email.
    /// </summary>
    public string Body { get; set; } = default!;

    /// <summary>
    /// Gets or sets the timestamp when the email was sent.
    /// </summary>
    public DateTime SentAt { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the email was sent successfully.
    /// </summary>
    public bool IsSuccess { get; set; }

    /// <summary>
    /// Gets or sets the error message if the email failed to send.
    /// </summary>
    public string? ErrorMessage { get; set; }

    /// <summary>
    /// Gets or sets the correlation ID used to trace the request.
    /// </summary>
    public string CorrelationId { get; set; } = Guid.NewGuid().ToString();

    #endregion
}
