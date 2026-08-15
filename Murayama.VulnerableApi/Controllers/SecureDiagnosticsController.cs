using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Murayama.VulnerableApi.Controllers;

[ApiController]
[Route("api/secure/diagnostics")]
[Authorize]
public class SecureDiagnosticsController : ControllerBase
{
    private readonly ILogger<SecureDiagnosticsController> _logger;

    public SecureDiagnosticsController(
        ILogger<SecureDiagnosticsController> logger)
    {
        _logger = logger;
    }

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
            _logger.LogError(
                ex,
                "An internal error occurred while processing the request.");

            return StatusCode(
                StatusCodes.Status500InternalServerError,
                new
                {
                    message = "An internal server error occurred."
                });
        }
    }
}