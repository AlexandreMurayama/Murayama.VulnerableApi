using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Murayama.VulnerableApi.Controllers;

[ApiController]
[Route("api/vulnerable/diagnostics")]
[Authorize]
public class VulnerableDiagnosticsController : ControllerBase
{
    [HttpGet("error")]
    public IActionResult GenerateError()
    {
        try
        {
            throw new InvalidOperationException(
                "Simulated internal database failure.");
        }
        catch (Exception ex)
        {
            return StatusCode(
                StatusCodes.Status500InternalServerError,
                new
                {
                    error = ex.Message,
                    exceptionType = ex.GetType().FullName,
                    stackTrace = ex.StackTrace,
                    environment = Environment.GetEnvironmentVariable(
                        "ASPNETCORE_ENVIRONMENT")
                });
        }
    }
}