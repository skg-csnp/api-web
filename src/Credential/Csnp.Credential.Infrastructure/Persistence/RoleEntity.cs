using Microsoft.AspNetCore.Identity;

namespace Csnp.Credential.Infrastructure.Persistence;

/// <summary>
/// Represents a role entity for the Credential module using long as the key type.
/// </summary>
public class RoleEntity : IdentityRole<long>
{
}
