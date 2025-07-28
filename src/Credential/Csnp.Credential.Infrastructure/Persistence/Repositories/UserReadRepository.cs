using Csnp.Credential.Application.Abstractions.Persistence;
using Csnp.Credential.Domain.Entities;
using Csnp.Credential.Infrastructure.Mappers;
using Csnp.Credential.Infrastructure.Persistence.Shared;
using Microsoft.EntityFrameworkCore;

namespace Csnp.Credential.Infrastructure.Persistence.Repositories;

public class UserReadRepository : IUserReadRepository
{
    private readonly CredentialDbContext _context;

    public UserReadRepository(CredentialDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<User>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.Users
            .AsNoTracking()
            .Select(u => u.ToDomain())
            .ToListAsync(cancellationToken);
    }
}
