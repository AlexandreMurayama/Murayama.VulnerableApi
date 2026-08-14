using Microsoft.AspNetCore.Mvc;

namespace Murayama.VulnerableApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HealthController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new
        {
            status = "healthy",
            application = "Murayama Vulnerable API",
            version = "1.0.0"
        });
    }
}