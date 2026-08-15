using Microsoft.AspNetCore.Identity;
using Murayama.VulnerableApi.Models;

namespace Murayama.VulnerableApi.Services;

public class PasswordService
{
    private readonly PasswordHasher<User> _passwordHasher = new();

    public string HashPassword(User user, string password)
    {
        return _passwordHasher.HashPassword(user, password);
    }

    public bool VerifyPassword(
        User user,
        string hashedPassword,
        string providedPassword)
    {
        var result = _passwordHasher.VerifyHashedPassword(
            user,
            hashedPassword,
            providedPassword);

        return result != PasswordVerificationResult.Failed;
    }
}