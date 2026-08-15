namespace Murayama.VulnerableApi.DTOs.Users;

public class UserProfileResponse
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public required string Email { get; set; }
}