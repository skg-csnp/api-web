using Csnp.Credential.Domain.Entities;

namespace Csnp.Credential.Domain.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(long id);
    Task AddAsync(User user);
    Task<IEnumerable<User>> GetAllAsync();
}
