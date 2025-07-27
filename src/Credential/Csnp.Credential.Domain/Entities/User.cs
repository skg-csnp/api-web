namespace Csnp.Credential.Domain.Entities;

public class User
{
    public long Id { get; set; }
    public string UserName { get; set; } = default!;
    public string DisplayName { get; set; } = default!;
}
