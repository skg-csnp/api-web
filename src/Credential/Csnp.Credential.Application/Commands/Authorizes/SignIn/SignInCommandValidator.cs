using FluentValidation;

namespace Csnp.Credential.Application.Commands.Authorizes.SignIn;

/// <summary>
/// Validator for <see cref="SignInCommand"/>.
/// </summary>
public class SignInCommandValidator : AbstractValidator<SignInCommand>
{
    #region -- Methods --

    /// <summary>
    /// Initializes a new instance of the <see cref="SignInCommandValidator"/> class.
    /// </summary>
    public SignInCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(6);
    }

    #endregion
}
