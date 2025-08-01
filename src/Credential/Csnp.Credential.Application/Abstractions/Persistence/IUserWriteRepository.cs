using Csnp.Credential.Domain.Entities;

namespace Csnp.Credential.Application.Abstractions.Persistence;

/// <summary>
/// Provides write access methods for <see cref="User"/> entities.
/// </summary>
public interface IUserWriteRepository
{
    /// <summary>
    /// Adds a new user to the data source.
    /// </summary>
    /// <param name="user">The user entity to add.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    Task AddAsync(User user, CancellationToken cancellationToken);
}
