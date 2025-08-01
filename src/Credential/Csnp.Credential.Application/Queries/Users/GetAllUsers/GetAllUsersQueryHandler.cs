using Csnp.Credential.Application.Abstractions.Persistence;
using Csnp.Credential.Application.Queries.Users.Dtos;
using MediatR;

namespace Csnp.Credential.Application.Queries.Users.GetAllUsers;

/// <summary>
/// Handles the query to retrieve all users.
/// </summary>
public class GetAllUsersHandler : IRequestHandler<GetAllUsersQuery, IEnumerable<UserDto>>
{
    #region -- Fields --

    private readonly IUserReadRepository _userRepo;

    #endregion

    #region -- Methods --

    /// <summary>
    /// Initializes a new instance of the <see cref="GetAllUsersHandler"/> class.
    /// </summary>
    /// <param name="userRepo">The user read repository.</param>
    public GetAllUsersHandler(IUserReadRepository userRepo)
    {
        _userRepo = userRepo;
    }

    /// <inheritdoc />
    public async Task<IEnumerable<UserDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        IReadOnlyList<Domain.Entities.User> users = await _userRepo.GetAllAsync(cancellationToken);
        return users.Select(u => new UserDto(u.Id, u.Email.Value, u.DisplayName));
    }

    #endregion
}
