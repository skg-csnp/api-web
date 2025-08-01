using MediatR;

namespace Csnp.Credential.Application.Commands.Users.CreateUser;

/// <summary>
/// Represents a command to create a new user.
/// </summary>
/// <param name="UserName">The username of the user.</param>
/// <param name="Password">The password of the user.</param>
/// <param name="DisplayName">The display name of the user.</param>
public sealed record CreateUserCommand(
    string UserName,
    string Password,
    string DisplayName
) : IRequest<long>;
