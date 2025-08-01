using Csnp.Credential.Domain.Entities;
using Csnp.Credential.Infrastructure.Persistence;
using Csnp.SeedWork.Domain.ValueObjects;

namespace Csnp.Credential.Infrastructure.Mappers;

/// <summary>
/// Extension methods for mapping between <see cref="User"/> domain models and <see cref="UserEntity"/> entities.
/// </summary>
public static class UserEntityExtensions
{
    #region -- Methods --

    /// <summary>
    /// Maps the <see cref="User"/> domain object to a <see cref="UserEntity"/> entity.
    /// </summary>
    /// <param name="domain">The domain user.</param>
    /// <returns>The corresponding <see cref="UserEntity"/>.</returns>
    public static UserEntity ToEntity(this User domain)
    {
        return new UserEntity
        {
            Id = domain.Id,
            Email = domain.Email.Value,
            UserName = domain.Email.Value,
            DisplayName = domain.DisplayName
        };
    }

    /// <summary>
    /// Maps the <see cref="UserEntity"/> entity to a <see cref="User"/> domain object.
    /// </summary>
    /// <param name="entity">The entity to map from.</param>
    /// <returns>The corresponding <see cref="User"/>.</returns>
    public static User ToDomain(this UserEntity entity)
    {
        return User.Rehydrate(
            entity.Id,
            EmailAddress.Create(entity.Email!),
            entity.PasswordHash!,
            entity.DisplayName ?? string.Empty
        );
    }

    #endregion
}
