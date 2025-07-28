namespace Csnp.Credential.Application.Commands.Authorizes.SignIn;

public class SignInResponse
{
    public string AccessToken { get; set; } = default!;
    public string RefreshToken { get; set; } = default!;
    public DateTime ExpiresAt { get; set; }
}
