using Csnp.SeedWork.Domain;
using Csnp.SharedKernel.Domain.ValueObjects;

namespace Csnp.Credential.Domain.Entities;

public class User : Entity<long>, IAggregateRoot
{
    public EmailAddress Email { get; private set; }
    public string Password { get; private set; }
    public string DisplayName { get; private set; }

    protected User() { } // For EF

    private User(EmailAddress email, string password, string displayName)
    {
        Email = email;
        Password = password;
        DisplayName = displayName;
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
        return new User(email, password, displayName);
    }
}
