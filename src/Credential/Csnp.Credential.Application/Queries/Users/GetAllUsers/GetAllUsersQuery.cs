using Csnp.Credential.Application.Queries.Users.Dtos;
using MediatR;

namespace Csnp.Credential.Application.Queries.Users.GetAllUsers;

/// <summary>
/// Represents a query to retrieve all users.
/// </summary>
public sealed record GetAllUsersQuery() : IRequest<IEnumerable<UserDto>>;
