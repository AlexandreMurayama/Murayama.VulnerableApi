namespace Murayama.VulnerableApi.DTOs.Auth;

public class LoginResponse
{
    public required string AccessToken { get; set; }

    public string TokenType { get; set; } = "Bearer";

    public int ExpiresIn { get; set; }
}