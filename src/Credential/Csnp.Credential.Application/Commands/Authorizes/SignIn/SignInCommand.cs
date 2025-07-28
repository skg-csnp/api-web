using MediatR;

namespace Csnp.Credential.Application.Commands.Authorizes.SignIn;

public class SignInCommand : IRequest<SignInResponse>
{
    public string Email { get; init; } = default!;
    public string Password { get; init; } = default!;
}
