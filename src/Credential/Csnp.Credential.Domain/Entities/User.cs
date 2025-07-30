using Csnp.Credential.Domain.Events.Users;
using Csnp.SeedWork.Domain;
using Csnp.SharedKernel.Domain.ValueObjects;

namespace Csnp.Credential.Domain.Entities;

public class User : Entity<long>, IAggregateRoot
{
    public EmailAddress Email { get; private set; } = default!;
    public string Password { get; private set; } = default!;
    public string DisplayName { get; private set; } = default!;

    protected User() { } // For EF

    private User(EmailAddress email, string password, string displayName)
    {
        Email = email;
        Password = password;
        DisplayName = displayName;
    }

    public void SignIn()
    {
        AddDomainEvent(new UserSignedInDomainEvent(Id, DateTime.UtcNow));
    }

    public void SetId(long id)
    {
        if (Id != default)
        {
            throw new InvalidOperationException("Id is already set");
        }

        Id = id;
    }

    public static User Create(EmailAddress email, string password, string displayName)
    {
        // You can validate domain rules here (e.g. unique email if using domain service)
        var user = new User(email, password, displayName);
        user.AddDomainEvent(new UserCreatedDomainEvent(user.Id, email.Value));
        user.AddDomainEvent(new UserSignedUpDomainEvent(user));
        return user;
    }

    public static User Rehydrate(long id, EmailAddress email, string password, string displayName)
    {
        var user = new User(email, password, displayName);
        user.SetId(id);
        return user;
    }
}
