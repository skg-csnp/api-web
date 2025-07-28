namespace Csnp.Notification.Infrastructure.Persistence.Shared;

public class EmailLogEntity
{
    public long Id { get; set; }

    public string To { get; set; } = default!;
    public string Subject { get; set; } = default!;
    public string Body { get; set; } = default!;

    public DateTime SentAt { get; set; }
    public bool IsSuccess { get; set; }
    public string? ErrorMessage { get; set; }

    public string CorrelationId { get; set; } = Guid.NewGuid().ToString();
}
