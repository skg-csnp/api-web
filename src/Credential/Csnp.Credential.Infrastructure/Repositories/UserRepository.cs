using Csnp.Credential.Domain.Entities;
using Csnp.Credential.Domain.Interfaces;
using Csnp.Credential.Infrastructure.Persistence.Shared;
using IdGen;
using Microsoft.EntityFrameworkCore;

namespace Csnp.Credential.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly CredentialDbContext _db;
    private readonly IdGenerator _idGen;

    public UserRepository(CredentialDbContext db, IdGenerator idGen)
    {
        _db = db;
        _idGen = idGen;
    }

    public async Task<User?> GetByIdAsync(long id)
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
            Id = _idGen.CreateId(),
            UserName = user.UserName,
            DisplayName = user.DisplayName
        };

        _db.Users.Add(entity);
        await _db.SaveChangesAsync();

        user.Id = entity.Id;
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
