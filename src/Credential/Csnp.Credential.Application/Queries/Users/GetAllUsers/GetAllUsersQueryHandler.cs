using Csnp.Credential.Application.Abstractions.Persistence;
using Csnp.Credential.Application.Queries.Users.Dtos;
using MediatR;

namespace Csnp.Credential.Application.Queries.Users.GetAllUsers;

public class GetAllUsersHandler : IRequestHandler<GetAllUsersQuery, IEnumerable<UserDto>>
{
    private readonly IUserReadRepository _userRepo;

    public GetAllUsersHandler(IUserReadRepository userRepo)
    {
        _userRepo = userRepo;
    }

    public async Task<IEnumerable<UserDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await _userRepo.GetAllAsync(cancellationToken);
        return users.Select(u => new UserDto(u.Id, u.Email.Value, u.DisplayName));
    }
}
