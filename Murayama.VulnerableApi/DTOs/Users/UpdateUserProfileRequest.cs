namespace Murayama.VulnerableApi.DTOs.Users;

public class UpdateUserProfileRequest
{
    public required string Name { get; set; }

    public required string Email { get; set; }
}