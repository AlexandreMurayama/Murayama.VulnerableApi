using Microsoft.AspNetCore.Mvc;

namespace Murayama.VulnerableApi.Controllers;

[ApiController]
[Route("api/internal/metadata")]
[ApiExplorerSettings(IgnoreApi = true)]
public class InternalMetadataController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new
        {
            service = "Murayama Vulnerable API",
            environment = "internal",
            databaseHost = "postgres",
            internalApiKey = "INTERNAL-LAB-KEY-DO-NOT-USE"
        });
    }
}