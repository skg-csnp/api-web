namespace Csnp.Credential.Application.Queries.Users.Dtos;

/// <summary>
/// Represents a user data transfer object (DTO) used for query results.
/// </summary>
/// <param name="Id">The unique identifier of the user.</param>
/// <param name="UserName">The email or username of the user.</param>
/// <param name="DisplayName">The display name of the user.</param>
public record UserDto(long Id, string UserName, string DisplayName);
