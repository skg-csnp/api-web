using Csnp.Notification.Domain.Entities;
using Csnp.Notification.Infrastructure.Persistence;

namespace Csnp.Notification.Infrastructure.Mappers;

/// <summary>
/// Provides extension methods for mapping between EmailLog domain model and EmailLogEntity persistence model.
/// </summary>
internal static class EmailLogEntityExtensions
{
    #region -- Methods --

    /// <summary>
    /// Converts a domain <see cref="EmailLog"/> object to a persistence <see cref="EmailLogEntity"/>.
    /// </summary>
    /// <param name="domain">The domain EmailLog instance.</param>
    /// <returns>A mapped EmailLogEntity object.</returns>
    public static EmailLogEntity ToEntity(this EmailLog domain)
    {
        return new EmailLogEntity
        {
            Id = domain.Id,
            To = domain.To,
            Subject = domain.Subject,
            Body = domain.Body,
            SentAt = domain.SentAt,
            IsSuccess = domain.IsSuccess,
            ErrorMessage = domain.ErrorMessage,
            CorrelationId = domain.CorrelationId
        };
    }

    /// <summary>
    /// Converts a persistence <see cref="EmailLogEntity"/> object to a domain <see cref="EmailLog"/>.
    /// </summary>
    /// <param name="entity">The EmailLogEntity instance.</param>
    /// <returns>A mapped EmailLog domain object.</returns>
    public static EmailLog ToDomain(this EmailLogEntity entity)
    {
        var emailLog = EmailLog.Create(
            to: entity.To,
            subject: entity.Subject,
            body: entity.Body,
            isSuccess: entity.IsSuccess,
            errorMessage: entity.ErrorMessage
        );

        typeof(EmailLog)
            .GetProperty(nameof(EmailLog.Id))!
            .SetValue(emailLog, entity.Id);

        return emailLog;
    }

    #endregion
}
