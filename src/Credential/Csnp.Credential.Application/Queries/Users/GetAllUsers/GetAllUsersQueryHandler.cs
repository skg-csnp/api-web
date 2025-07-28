using Csnp.Credential.Application.Queries.Users.Dtos;
using Csnp.Credential.Domain.Interfaces;
using MediatR;

namespace Csnp.Credential.Application.Queries.Users.GetAllUsers;

public class GetAllUsersHandler : IRequestHandler<GetAllUsersQuery, IEnumerable<UserDto>>
{
    private readonly IUserRepository _userRepo;

    public GetAllUsersHandler(IUserRepository userRepo)
    {
        _userRepo = userRepo;
    }

    public async Task<IEnumerable<UserDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await _userRepo.GetAllAsync();
        return users.Select(u => new UserDto(u.Id, u.UserName, u.DisplayName));
    }
}
