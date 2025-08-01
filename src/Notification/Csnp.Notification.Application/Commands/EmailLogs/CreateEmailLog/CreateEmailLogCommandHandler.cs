using Csnp.Notification.Application.Abstractions.Persistence;
using Csnp.Notification.Domain.Entities;
using MediatR;

namespace Csnp.Notification.Application.Commands.EmailLogs.CreateEmailLog;

/// <summary>
/// Handler for <see cref="CreateEmailLogCommand"/>.
/// </summary>
internal sealed class CreateEmailLogCommandHandler : IRequestHandler<CreateEmailLogCommand, long>
{
    #region -- Implements --

    /// <inheritdoc />
    public async Task<long> Handle(CreateEmailLogCommand request, CancellationToken cancellationToken)
    {
        EmailLog emailLog = EmailLog.Create(
            to: request.To,
            subject: request.Subject,
            body: request.Body,
            isSuccess: request.IsSuccess,
            errorMessage: request.ErrorMessage
        );

        await _emailLogWriteRepository.InsertAsync(emailLog, cancellationToken);
        return emailLog.Id;
    }

    #endregion

    #region -- Methods --

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateEmailLogCommandHandler"/> class.
    /// </summary>
    /// <param name="emailLogWriteRepository">The repository to persist email logs.</param>
    public CreateEmailLogCommandHandler(IEmailLogWriteRepository emailLogWriteRepository)
    {
        _emailLogWriteRepository = emailLogWriteRepository;
    }

    #endregion

    #region -- Fields --

    /// <summary>
    /// The repository used to persist <see cref="EmailLog"/> entities.
    /// </summary>
    private readonly IEmailLogWriteRepository _emailLogWriteRepository;

    #endregion
}
