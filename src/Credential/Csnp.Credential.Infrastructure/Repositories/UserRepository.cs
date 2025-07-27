using Csnp.Credential.Domain.Entities;
using Csnp.Credential.Domain.Interfaces;
using Csnp.Credential.Infrastructure.Persistence.Shared;
using Microsoft.EntityFrameworkCore;

namespace Csnp.Credential.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly CredentialDbContext _db;

    public UserRepository(CredentialDbContext db)
    {
        _db = db;
    }

    public async Task<User?> GetByIdAsync(Guid id)
    {
        var entity = await _db.Users.FindAsync(id);
        if (entity == null)
        {
            return null;
        }

        return new User
        {
            Id = entity.Id,
            UserName = entity.UserName,
            DisplayName = entity.DisplayName
        };
    }

    public async Task AddAsync(User user)
    {
        var entity = new UserEntity
        {
            // Id = user.Id,
            UserName = user.UserName,
            DisplayName = user.DisplayName
        };

        _db.Users.Add(entity);
        await _db.SaveChangesAsync();
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _db.Users
            .Select(p => new User
            {
                Id = p.Id,
                UserName = p.UserName,
                DisplayName = p.DisplayName
            })
            .ToListAsync();
    }
}
