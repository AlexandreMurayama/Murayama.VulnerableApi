using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Murayama.VulnerableApi.Data;

namespace Murayama.VulnerableApi.Controllers;

[ApiController]
[Route("api/secure/admin")]
[Authorize(Roles = "Admin")]
public class SecureAdminController : ControllerBase
{
    private readonly AppDbContext _dbContext;

    public SecureAdminController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet("users")]
    public async Task<IActionResult> GetUsers()
    {
        var users = await _dbContext.Users
            .AsNoTracking()
            .Select(u => new
            {
                u.Id,
                u.Name,
                u.Email,
                u.Role
            })
            .ToListAsync();

        return Ok(users);
    }
}