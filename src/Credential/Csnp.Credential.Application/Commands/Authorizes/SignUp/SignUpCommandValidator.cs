using FluentValidation;

namespace Csnp.Credential.Application.Commands.Authorizes.SignUp;

/// <summary>
/// Validator for <see cref="SignUpCommand"/> using FluentValidation.
/// </summary>
public sealed class SignUpCommandValidator : AbstractValidator<SignUpCommand>
{
    #region -- Methods --

    /// <summary>
    /// Initializes a new instance of the <see cref="SignUpCommandValidator"/> class.
    /// </summary>
    public SignUpCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email must not be empty.")
            .EmailAddress().WithMessage("Email format is invalid.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password must not be empty.")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters.");
    }

    #endregion
}
