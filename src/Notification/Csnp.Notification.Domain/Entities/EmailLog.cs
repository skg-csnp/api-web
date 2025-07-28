namespace Csnp.Notification.Domain.Entities;

public class EmailLog
{
    public long Id { get; set; }
    public string To { get; private set; } = default!;
    public string Subject { get; private set; } = default!;
    public string Body { get; private set; } = default!;

    public DateTime SentAt { get; private set; }
    public bool IsSuccess { get; private set; }
    public string? ErrorMessage { get; private set; }

    public string CorrelationId { get; private set; } = Guid.NewGuid().ToString();

    protected EmailLog() { } // For EF

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

    public void MarkAsFailed(string error)
    {
        IsSuccess = false;
        ErrorMessage = error;
    }
}
