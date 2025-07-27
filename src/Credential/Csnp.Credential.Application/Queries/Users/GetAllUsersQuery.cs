using Csnp.Credential.Application.Dtos;
using MediatR;

namespace Csnp.Credential.Application.Queries.Users;

public record GetAllUsersQuery() : IRequest<IEnumerable<UserDto>>;

