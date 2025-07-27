using Csnp.Credential.Domain.Entities;

namespace Csnp.Credential.Domain.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(Guid id);
    Task AddAsync(User user);
    Task<IEnumerable<User>> GetAllAsync();
}
