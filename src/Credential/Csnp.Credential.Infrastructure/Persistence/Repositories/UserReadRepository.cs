using System.Globalization;
using Csnp.Credential.Application.Abstractions.Persistence;
using Csnp.Credential.Domain.Entities;
using Csnp.Credential.Infrastructure.Mappers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Csnp.Credential.Infrastructure.Persistence.Repositories;

/// <summary>
/// Provides read operations for retrieving <see cref="User"/> entities from the credential store.
/// </summary>
public sealed class UserReadRepository : IUserReadRepository
{
    #region -- Implements --

    /// <inheritdoc />
    public async Task<IReadOnlyList<User>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.Users
            .AsNoTracking()
            .Select(u => u.ToDomain())
            .ToListAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<User?> GetByUserNameAsync(string userName, CancellationToken cancellationToken)
    {
        UserEntity? entity = await _userManager.FindByNameAsync(userName);
        return entity?.ToDomain();
    }

    /// <inheritdoc />
    public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        UserEntity? entity = await _userManager.FindByEmailAsync(email);
        return entity?.ToDomain();
    }

    /// <inheritdoc />
    public async Task<bool> CheckPasswordAsync(User user, string password)
    {
        UserEntity? entity = await _userManager.FindByIdAsync(user.Id.ToString(CultureInfo.InvariantCulture));
        if (entity is null)
        {
            return false;
        }

        return await _userManager.CheckPasswordAsync(entity, password);
    }

    #endregion

    #region -- Methods --

    /// <summary>
    /// Initializes a new instance of the <see cref="UserReadRepository"/> class.
    /// </summary>
    /// <param name="context">The credential database context.</param>
    /// <param name="userManager">The identity user manager for querying user-related data.</param>
    public UserReadRepository(CredentialDbContext context, UserManager<UserEntity> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    #endregion

    #region -- Fields --

    /// <summary>
    /// The database context for the credential module.
    /// </summary>
    private readonly CredentialDbContext _context;

    /// <summary>
    /// The ASP.NET Core identity user manager.
    /// </summary>
    private readonly UserManager<UserEntity> _userManager;

    #endregion
}
