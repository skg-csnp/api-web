using Csnp.Credential.Application.Commands.Authorizes.SignUp;
using FluentValidation;

namespace Csnp.Credential.Application.Commands.Users.CreateUser;

public class SignUpValidator : AbstractValidator<SignUpCommand>
{
    public SignUpValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(6);
    }
}
