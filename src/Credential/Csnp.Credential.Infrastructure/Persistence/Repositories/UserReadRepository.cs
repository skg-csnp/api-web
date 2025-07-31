using System.Globalization;
using Csnp.Credential.Application.Abstractions.Persistence;
using Csnp.Credential.Domain.Entities;
using Csnp.Credential.Infrastructure.Mappers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Csnp.Credential.Infrastructure.Persistence.Repositories;

public class UserReadRepository : IUserReadRepository
{
    private readonly CredentialDbContext _context;
    private readonly UserManager<UserEntity> _userManager;

    public UserReadRepository(CredentialDbContext context, UserManager<UserEntity> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task<User?> GetByIdAsync(long id, CancellationToken cancellationToken)
    {
        UserEntity? entity = await _userManager.FindByIdAsync(id.ToString(CultureInfo.InvariantCulture));
        return entity?.ToDomain();
    }

    public async Task<IReadOnlyList<User>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.Users
            .AsNoTracking()
            .Select(u => u.ToDomain())
            .ToListAsync(cancellationToken);
    }

    public async Task<User?> GetByUserNameAsync(string userName, CancellationToken cancellationToken)
    {
        UserEntity? entity = await _userManager.FindByNameAsync(userName);
        return entity?.ToDomain();
    }

    public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        UserEntity? entity = await _userManager.FindByEmailAsync(email);
        return entity?.ToDomain();
    }

    public async Task<bool> CheckPasswordAsync(User user, string password)
    {
        UserEntity? entity = await _userManager.FindByIdAsync(user.Id.ToString(CultureInfo.InvariantCulture));
        if (entity is null)
        {
            return false;
        }

        return await _userManager.CheckPasswordAsync(entity, password);
    }
}
