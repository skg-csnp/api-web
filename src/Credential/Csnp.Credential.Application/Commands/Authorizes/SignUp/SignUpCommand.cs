using MediatR;

namespace Csnp.Credential.Application.Commands.Authorizes.SignUp;

public record SignUpCommand(string Email, string Password, string DisplayName) : IRequest<long>;
