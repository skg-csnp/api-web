using Csnp.Credential.Application.Queries.Users.Dtos;
using MediatR;

namespace Csnp.Credential.Application.Queries.Users.GetAllUsers;

public record GetAllUsersQuery() : IRequest<IEnumerable<UserDto>>;

