using Csnp.Credential.Application.Abstractions.Persistence;
using Csnp.Credential.Domain.Entities;
using Csnp.Credential.Infrastructure.Mappers;
using Csnp.Credential.Infrastructure.Persistence.Shared;
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

    public async Task<IReadOnlyList<User>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.Users
            .AsNoTracking()
            .Select(u => u.ToDomain())
            .ToListAsync(cancellationToken);
    }

    public async Task<User?> GetByUserNameAsync(string userName, CancellationToken cancellationToken)
    {
        var entity = await _userManager.FindByNameAsync(userName);
        return entity?.ToDomain();
    }
}
