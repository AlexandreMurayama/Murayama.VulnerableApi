using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Murayama.VulnerableApi.Data;
using Murayama.VulnerableApi.Models;

namespace Murayama.VulnerableApi.Controllers;

[ApiController]
[Route("api/vulnerable/users")]
[Authorize]
public class VulnerableUsersController : ControllerBase
{
    private readonly AppDbContext _dbContext;

    public VulnerableUsersController(AppDbContext dbContext)
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
            .FirstOrDefaultAsync(u => u.Id == userId);

        if (user is null)
            return NotFound();

        // Intentionally vulnerable:
        // returns the complete database entity.
        return Ok(user);
    }

    [HttpPut("me")]
    public async Task<IActionResult> UpdateCurrentUser(User input)
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

        user.Name = input.Name;
        user.Email = input.Email;

        // Intentionally vulnerable:
        // the client is allowed to modify a sensitive property.
        user.Role = input.Role;

        await _dbContext.SaveChangesAsync();

        return Ok(new
        {
            user.Id,
            user.Name,
            user.Email,
            user.Role
        });
    }
}