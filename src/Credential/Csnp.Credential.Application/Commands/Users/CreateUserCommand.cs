using MediatR;

namespace Csnp.Credential.Application.Commands.Users;

public record CreateUserCommand(string UserName, string DisplayName) : IRequest<long>;
