using FluentValidation;

namespace Csnp.Notification.Application.Commands.EmailLogs.CreateEmailLog;

/// <summary>
/// Validator for <see cref="CreateEmailLogCommand"/>.
/// </summary>
public sealed class CreateEmailLogCommandValidator : AbstractValidator<CreateEmailLogCommand>
{
    #region -- Methods --

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateEmailLogCommandValidator"/> class.
    /// </summary>
    public CreateEmailLogCommandValidator()
    {
        RuleFor(x => x.To)
            .NotEmpty().WithMessage("Recipient email address is required.")
            .EmailAddress().WithMessage("Invalid email format.");

        RuleFor(x => x.Subject)
            .NotEmpty().WithMessage("Email subject is required.")
            .MaximumLength(255).WithMessage("Email subject must be less than 255 characters.");

        RuleFor(x => x.Body)
            .NotEmpty().WithMessage("Email body is required.");
    }

    #endregion
}
