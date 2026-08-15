using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Murayama.VulnerableApi.Data;
using Murayama.VulnerableApi.DTOs.Users;

namespace Murayama.VulnerableApi.Controllers;

[ApiController]
[Route("api/secure/users")]
[Authorize]
public class SecureUsersController : ControllerBase
{
    private readonly AppDbContext _dbContext;

    public SecureUsersController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet("me")]
    public async Task<IActionResult> GetCurrentUser()
    {
        var userIdClaim =
            User.FindFirstValue(ClaimTypes.NameIdentifier)
            ?? User.FindFirstValue("sub");

        if (!int.TryParse(userIdClaim, out var userId))
            return Unauthorized();

        var user = await _dbContext.Users
            .Where(u => u.Id == userId)
            .Select(u => new UserProfileResponse
            {
                Id = u.Id,
                Name = u.Name,
                Email = u.Email
            })
            .FirstOrDefaultAsync();

        if (user is null)
            return NotFound();

        return Ok(user);
    }
    
    [HttpPut("me")]
    public async Task<IActionResult> UpdateCurrentUser(
        UpdateUserProfileRequest request)
    {
        var userIdClaim =
            User.FindFirstValue(ClaimTypes.NameIdentifier)
            ?? User.FindFirstValue("sub");

        if (!int.TryParse(userIdClaim, out var userId))
            return Unauthorized();

        var user = await _dbContext.Users
            .FirstOrDefaultAsync(u => u.Id == userId);

        if (user is null)
            return NotFound();

        user.Name = request.Name;
        user.Email = request.Email;

        await _dbContext.SaveChangesAsync();

        return Ok(new UserProfileResponse
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email
        });
    }
}