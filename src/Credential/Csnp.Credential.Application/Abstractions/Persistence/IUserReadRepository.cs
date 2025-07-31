using Csnp.Credential.Domain.Entities;

namespace Csnp.Credential.Application.Abstractions.Persistence;

public interface IUserReadRepository
{
    Task<User?> GetByIdAsync(long id, CancellationToken cancellationToken);
    Task<IReadOnlyList<User>> GetAllAsync(CancellationToken cancellationToken);
    Task<User?> GetByUserNameAsync(string userName, CancellationToken cancellationToken);
    Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
    Task<bool> CheckPasswordAsync(User user, string password);
}
