using MediatR;

namespace Csnp.Credential.Application.Commands.Users.CreateUser;

public record CreateUserCommand(string UserName, string Password, string DisplayName) : IRequest<long>;
