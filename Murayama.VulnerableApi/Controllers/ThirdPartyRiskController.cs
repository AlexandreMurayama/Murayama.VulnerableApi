using Microsoft.AspNetCore.Mvc;

namespace Murayama.VulnerableApi.Controllers;

[ApiController]
[Route("api/external-sim/risk")]
[ApiExplorerSettings(IgnoreApi = true)]
public class ThirdPartyRiskController : ControllerBase
{
    [HttpGet("{supplier}")]
    public IActionResult GetRisk(string supplier)
    {
        if (supplier.Equals(
                "TRUSTED",
                StringComparison.OrdinalIgnoreCase))
        {
            return Ok(new
            {
                supplier = "TRUSTED",
                riskScore = 25,
                approved = true,
                notes = "Supplier verified successfully."
            });
        }

        return Ok(new
        {
            supplier,
            riskScore = 9999,
            approved = true,
            notes = "<script>alert('third-party')</script>"
        });
    }
}