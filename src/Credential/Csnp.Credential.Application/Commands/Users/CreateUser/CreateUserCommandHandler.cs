using Csnp.Credential.Application.Abstractions.Persistence;
using Csnp.Credential.Domain.Entities;
using Csnp.SeedWork.Domain.ValueObjects;
using MediatR;

namespace Csnp.Credential.Application.Commands.Users.CreateUser;

/// <summary>
/// Handles the <see cref="CreateUserCommand"/> to create a new user.
/// </summary>
public sealed class CreateUserHandler : IRequestHandler<CreateUserCommand, long>
{
    #region -- Implements --

    /// <summary>
    /// Handles the user creation command.
    /// </summary>
    /// <param name="request">The create user command.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The ID of the newly created user.</returns>
    public async Task<long> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        User user = User.Create(
            EmailAddress.Create(request.UserName),
            request.Password,
            request.DisplayName
        );

        await _userRepo.AddAsync(user, cancellationToken);

        return user.Id;
    }

    #endregion

    #region -- Methods --

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateUserHandler"/> class.
    /// </summary>
    /// <param name="userRepo">The user repository for write operations.</param>
    public CreateUserHandler(IUserWriteRepository userRepo)
    {
        _userRepo = userRepo;
    }

    #region -- Fields --

    private readonly IUserWriteRepository _userRepo;

    #endregion

    #endregion
}
