using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Murayama.VulnerableApi.DTOs.External;

namespace Murayama.VulnerableApi.Controllers;

[ApiController]
[Route("api/secure/suppliers")]
[Authorize]
public class SecureSupplierController : ControllerBase
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<SecureSupplierController> _logger;

    public SecureSupplierController(
        IHttpClientFactory httpClientFactory,
        ILogger<SecureSupplierController> logger)
    {
        _httpClientFactory = httpClientFactory;
        _logger = logger;
    }

    [HttpGet("{supplier}/risk")]
    public async Task<IActionResult> GetRisk(
        string supplier,
        CancellationToken cancellationToken)
    {
        var client = _httpClientFactory.CreateClient();

        client.Timeout = TimeSpan.FromSeconds(3);

        var url =
            $"http://localhost:5248/api/external-sim/risk/{Uri.EscapeDataString(supplier)}";

        SupplierRiskResponse? risk;

        try
        {
            using var response =
                await client.GetAsync(url, cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogWarning(
                    "Third-party risk API returned status code {StatusCode}.",
                    response.StatusCode);

                return StatusCode(
                    StatusCodes.Status502BadGateway,
                    new
                    {
                        message = "Invalid response from external service."
                    });
            }

            risk = await response.Content
                .ReadFromJsonAsync<SupplierRiskResponse>(
                    cancellationToken: cancellationToken);
        }
        catch (OperationCanceledException)
        {
            return StatusCode(
                StatusCodes.Status504GatewayTimeout,
                new
                {
                    message = "External service request timed out."
                });
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(
                ex,
                "Unable to communicate with third-party risk API.");

            return StatusCode(
                StatusCodes.Status502BadGateway,
                new
                {
                    message = "Unable to communicate with external service."
                });
        }

        if (risk is null)
        {
            return StatusCode(
                StatusCodes.Status502BadGateway,
                new
                {
                    message = "Invalid response from external service."
                });
        }

        if (!string.Equals(
                risk.Supplier,
                supplier,
                StringComparison.OrdinalIgnoreCase))
        {
            return StatusCode(
                StatusCodes.Status502BadGateway,
                new
                {
                    message = "External service returned inconsistent supplier data."
                });
        }

        if (risk.RiskScore < 0 || risk.RiskScore > 100)
        {
            return StatusCode(
                StatusCodes.Status502BadGateway,
                new
                {
                    message = "External service returned an invalid risk score."
                });
        }

        if (ContainsUnsafeContent(risk.Notes))
        {
            return StatusCode(
                StatusCodes.Status502BadGateway,
                new
                {
                    message = "External service returned unsafe content."
                });
        }

        return Ok(new
        {
            supplier = risk.Supplier,
            riskScore = risk.RiskScore,
            approved = risk.Approved,
            notes = risk.Notes
        });
    }

    private static bool ContainsUnsafeContent(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return false;

        return value.Contains("<script", StringComparison.OrdinalIgnoreCase) ||
               value.Contains("javascript:", StringComparison.OrdinalIgnoreCase);
    }
}