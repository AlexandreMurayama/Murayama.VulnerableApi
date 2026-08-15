using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Murayama.VulnerableApi.DTOs.External;

namespace Murayama.VulnerableApi.Controllers;

[ApiController]
[Route("api/vulnerable/suppliers")]
[Authorize]
public class VulnerableSupplierController : ControllerBase
{
    private readonly IHttpClientFactory _httpClientFactory;

    public VulnerableSupplierController(
        IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    [HttpGet("{supplier}/risk")]
    public async Task<IActionResult> GetRisk(string supplier)
    {
        var client = _httpClientFactory.CreateClient();

        var url =
            $"http://localhost:5248/api/external-sim/risk/{Uri.EscapeDataString(supplier)}";

        var risk =
            await client.GetFromJsonAsync<SupplierRiskResponse>(url);

        return Ok(risk);
    }
}