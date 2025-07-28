using Csnp.Credential.Domain.Entities;

namespace Csnp.Credential.Application.Abstractions.Persistence;

public interface IUserReadRepository
{
    Task<IReadOnlyList<User>> GetAllAsync(CancellationToken cancellationToken);
}
