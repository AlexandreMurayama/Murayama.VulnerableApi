using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Murayama.VulnerableApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ProfileController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new
        {
            userId = User.FindFirstValue(ClaimTypes.NameIdentifier)
                     ?? User.FindFirstValue("sub"),
            name = User.FindFirstValue(ClaimTypes.Name),
            email = User.FindFirstValue(ClaimTypes.Email)
                    ?? User.FindFirstValue("email"),
            role = User.FindFirstValue(ClaimTypes.Role)
        });
    }
}