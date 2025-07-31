using Csnp.Credential.Domain.Entities;

namespace Csnp.Credential.Application.Abstractions.Persistence;

public interface IUserWriteRepository
{
    Task AddAsync(User user, CancellationToken cancellationToken);
    Task UpdateAsync(User user, CancellationToken cancellationToken);
    Task DeleteAsync(User user, CancellationToken cancellationToken);
}
