using Csnp.Credential.Application.Abstractions.Persistence;
using Csnp.Credential.Domain.Entities;
using Csnp.Credential.Infrastructure.Mappers;
using IdGen;
using Microsoft.AspNetCore.Identity;

namespace Csnp.Credential.Infrastructure.Persistence.Repositories;

/// <summary>
/// Implements write operations for persisting <see cref="User"/> entities using <see cref="UserManager{TUser}"/>.
/// </summary>
public sealed class UserWriteRepository : IUserWriteRepository
{
    #region -- Implements --

    /// <inheritdoc />
    public async Task AddAsync(User user, CancellationToken cancellationToken)
    {
        UserEntity entity = user.ToEntity();
        entity.Id = _idGen.CreateId();

        IdentityResult result = await _userManager.CreateAsync(entity, user.Password);
        if (!result.Succeeded)
        {
            string errors = string.Join("; ", result.Errors.Select(e => e.Description));
            throw new InvalidOperationException($"Create user failed: {errors}");
        }

        user.SetId(entity.Id);
    }

    #endregion

    #region -- Methods --

    /// <summary>
    /// Initializes a new instance of the <see cref="UserWriteRepository"/> class.
    /// </summary>
    /// <param name="userManager">The ASP.NET Core identity user manager.</param>
    /// <param name="idGen">The ID generator for creating unique identifiers.</param>
    public UserWriteRepository(UserManager<UserEntity> userManager, IdGenerator idGen)
    {
        _userManager = userManager;
        _idGen = idGen;
    }

    #endregion

    #region -- Fields --

    /// <summary>
    /// The identity framework user manager for managing <see cref="UserEntity"/> persistence.
    /// </summary>
    private readonly UserManager<UserEntity> _userManager;

    /// <summary>
    /// The ID generator for creating unique identifiers.
    /// </summary>
    private readonly IdGenerator _idGen;

    #endregion
}
