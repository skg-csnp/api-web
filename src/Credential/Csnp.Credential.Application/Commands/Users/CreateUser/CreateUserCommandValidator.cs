using FluentValidation;

namespace Csnp.Credential.Application.Commands.Users.CreateUser;

/// <summary>
/// Validator for the <see cref="CreateUserCommand"/>.
/// </summary>
public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateUserCommandValidator"/> class.
    /// </summary>
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.UserName)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(6);
    }
}
