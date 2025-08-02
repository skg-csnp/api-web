using Csnp.Credential.Domain.Events.Users;
using Csnp.SeedWork.Domain.ValueObjects;
using Csnp.SharedKernel.Domain;

namespace Csnp.Credential.Domain.Entities;

/// <summary>
/// Represents a user within the credential domain.
/// </summary>
public class User : Entity<long>, IAggregateRoot
{
    #region -- Methods --

    /// <summary>
    /// Required by Entity Framework.
    /// </summary>
    protected User() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="User"/> class.
    /// </summary>
    /// <param name="email">The email address of the user.</param>
    /// <param name="password">The password of the user.</param>
    /// <param name="displayName">The display name of the user.</param>
    private User(EmailAddress email, string password, string displayName)
    {
        Email = email;
        Password = password;
        DisplayName = displayName;
    }

    /// <summary>
    /// Triggers the sign-in domain event.
    /// </summary>
    public void SignIn()
    {
        AddDomainEvent(new UserSignedInDomainEvent(Id, DateTime.UtcNow));
    }

    /// <summary>
    /// Sets the ID for the user entity if not already set.
    /// </summary>
    /// <param name="id">The ID to assign.</param>
    /// <exception cref="InvalidOperationException">Thrown if the ID is already set.</exception>
    public void SetId(long id)
    {
        if (Id != default)
        {
            throw new InvalidOperationException("Id is already set");
        }

        Id = id;
    }

    /// <summary>
    /// Creates a new user instance from the signup flow, including signup-related domain events.
    /// </summary>
    /// <param name="email">The user's email address.</param>
    /// <param name="password">The user's password.</param>
    /// <param name="displayName">The user's display name.</param>
    /// <returns>A new <see cref="User"/> instance with <see cref="UserSignedUpDomainEvent"/>.</returns>
    public static User CreateBySignup(EmailAddress email, string password, string displayName)
    {
        User user = new User(email, password, displayName);
        user.AddDomainEvent(new UserCreatedDomainEvent(user.Id, email.Value));
        user.AddDomainEvent(new UserSignedUpDomainEvent(user));
        return user;
    }

    /// <summary>
    /// Creates a new user instance by an administrator, without raising signup-related events.
    /// </summary>
    /// <param name="email">The user's email address.</param>
    /// <param name="password">The user's password.</param>
    /// <param name="displayName">The user's display name.</param>
    /// <returns>A new <see cref="User"/> instance with <see cref="UserCreatedDomainEvent"/> only.</returns>
    public static User CreateByAdmin(EmailAddress email, string password, string displayName)
    {
        User user = new User(email, password, displayName);
        user.AddDomainEvent(new UserCreatedDomainEvent(user.Id, email.Value));
        return user;
    }

    /// <summary>
    /// Rehydrates a user from persisted data (for repository reconstitution).
    /// </summary>
    /// <param name="id">The user ID.</param>
    /// <param name="email">The user's email address.</param>
    /// <param name="password">The user's password.</param>
    /// <param name="displayName">The user's display name.</param>
    /// <returns>A <see cref="User"/> instance.</returns>
    public static User Rehydrate(long id, EmailAddress email, string password, string displayName)
    {
        User user = new User(email, password, displayName);
        user.SetId(id);
        return user;
    }

    #endregion

    #region -- Properties --

    /// <summary>
    /// Gets the email address of the user.
    /// </summary>
    public EmailAddress Email { get; private set; } = default!;

    /// <summary>
    /// Gets the password of the user.
    /// </summary>
    public string Password { get; private set; } = default!;

    /// <summary>
    /// Gets the display name of the user.
    /// </summary>
    public string DisplayName { get; private set; } = default!;

    #endregion
}
