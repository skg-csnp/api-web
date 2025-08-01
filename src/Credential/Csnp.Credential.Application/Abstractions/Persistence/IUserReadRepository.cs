using Csnp.Credential.Domain.Entities;

namespace Csnp.Credential.Application.Abstractions.Persistence;

/// <summary>
/// Provides read access methods for <see cref="User"/> entities.
/// </summary>
public interface IUserReadRepository
{
    /// <summary>
    /// Gets all users from the data source.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A list of users.</returns>
    Task<IReadOnlyList<User>> GetAllAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Gets a user by their user name.
    /// </summary>
    /// <param name="userName">The user name.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The user if found; otherwise, null.</returns>
    Task<User?> GetByUserNameAsync(string userName, CancellationToken cancellationToken);

    /// <summary>
    /// Gets a user by their email.
    /// </summary>
    /// <param name="email">The email address.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The user if found; otherwise, null.</returns>
    Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);

    /// <summary>
    /// Checks if the provided password is valid for the given user.
    /// </summary>
    /// <param name="user">The user entity.</param>
    /// <param name="password">The plain-text password.</param>
    /// <returns>True if the password is valid; otherwise, false.</returns>
    Task<bool> CheckPasswordAsync(User user, string password);
}
