using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Murayama.VulnerableApi.Data;

namespace Murayama.VulnerableApi.Controllers;

[ApiController]
[Route("api/v1/users")]
[ApiExplorerSettings(IgnoreApi = true)]
public class LegacyUsersController : ControllerBase
{
    private readonly AppDbContext _dbContext;

    public LegacyUsersController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
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

        return Ok(new
        {
            apiVersion = "v1",
            deprecated = true,
            users
        });
    }
}